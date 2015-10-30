using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace NNOraToSqlValidator2
{
    public partial class Form4 : Form
    {
        #region Do Actions

        private string CalculatePercent(int value, int totalValue)
        {
            return string.Format("{0}%", value * 100 / totalValue);
        }

        #endregion

        #region Resource

        /// <summary>
        /// A temporary class for exchange information
        /// </summary>
        private struct DbTableInfo
        {
            //public DbTableInfo(string _dbName, string _tableName, string _PrimaryKey)
            public DbTableInfo(string _dbName, string _tableName)
            {
                DbName = _dbName;
                TableName = _tableName;
                //PrimaryKey = _PrimaryKey;
            }

            public string DbName;
            public string TableName;
            //public string PrimaryKey;
        }

        private class EvaluteFinishTime
        {
            private int[] _stepCntHistory = new int[] { 0, 0, 0, 0, 0 };
            private int _lastCnt = 0;
            private int _totalCnt;
            private int _interval_seconds;

            public EvaluteFinishTime(int totalCnt, int intervalSeconds)
            {
                _totalCnt = totalCnt;
                _interval_seconds = intervalSeconds;
            }

            public string Evalute(int value)
            {
                // shift the history step count.
                for (int i = _stepCntHistory.Length - 1; i >= 1; i--)
                    _stepCntHistory[i] = _stepCntHistory[i - 1];
                _stepCntHistory[0] = value - _lastCnt; // current step count;

                // evalute finish time
                int avgStepCnt = (int)_stepCntHistory.Average();
                int remainSteps = (avgStepCnt == 0) 
                                ? 0
                                : (_totalCnt - value) / avgStepCnt;

                // next
                _lastCnt = value;

                // return
                TimeSpan remainTime = new TimeSpan(0,0,remainSteps * _interval_seconds);
                return string.Format("{0:00}:{1:00}:{2:00}", (int)remainTime.TotalHours, remainTime.Minutes, remainTime.Seconds);
            }
        }

        #endregion

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnHardCompare_Click(object sender, EventArgs e)
        {
            try
            {
                //# prefix
                if (txtDbName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("need [DB.Table] to use to compare.");
                    return;
                }

                if (txtOrcRowCnt.Text.Trim() == string.Empty || txtSqlRowCnt.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("the [Oracle Row Count], [Sql Row Count] should be taken before");
                    return;
                }

                //# UI Control
                this.Cursor = Cursors.WaitCursor;
                this.btnHardCompare.Enabled = false;
                this.lsvMessage.Items.Clear();

                //# take parameters
                string dbName = txtDbName.Text.Trim();
                string tableName = txtTableName.Text.Trim();

                //# 
                // 1.Get connection information.
                Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];
                Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", dbName)[0];

                OracleConnection connOra = new OracleConnection(connInfoOra.ConnString);
                SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);

                // 2.Open connection
                connOra.Open();
                connSql.Open();

                // 3.Build command - take schema
                // select from
                string cmdTextOra = string.Format("SELECT * FROM \"{0}\".\"{1}\"", dbName, tableName);
                // where
                if (txtWhereCond.Text != string.Empty)
                    cmdTextOra += " WHERE " + txtWhereCond.Text;

                OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);
                dbCmdOra.CommandTimeout = 0;

                // 4.Execute cmmand - take schema
                DataTable dtSchema;
                using(OracleDataReader readerOra = dbCmdOra.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    dtSchema = readerOra.GetSchemaTable();
                }

                // 5.Compare Fields
                int oraReadCnt = 0;
                int sqlCheckCnt = 0;
                int notEqualCnt = 0;
                //
                int totRowCnt = int.MaxValue; // = int.Parse(txtOrcRowCnt.Text);
                int.TryParse(txtOrcRowCnt.Text.Replace(",", ""), out totRowCnt);

                // resource : for EvaluteFinishTime
                double interval_time = 2.0;
                EvaluteFinishTime evaluteFinishTime = new EvaluteFinishTime(totRowCnt, (int)interval_time);
                StringBuilder cmdTextSql = new StringBuilder();

                // show arguments
                lsvMessage.Items.Add(new ListViewItem(new string[] {
                      "Argument" // message tag
                    , "Oracle Query Command" // key value
                    , cmdTextOra // description
                    }));

                // -- to show progress
                this.StatusProgressBar1.Minimum = 0;
                this.StatusProgressBar1.Maximum = totRowCnt;
                this.StatusProgressBar1.Value = 0;

                Application.DoEvents(); // show me 

                DateTime checkPoint = DateTime.Now;
                using (OracleDataReader readerOra = dbCmdOra.ExecuteReader(CommandBehavior.Default))
                {
                    while (readerOra.Read())
                    {
                        oraReadCnt++;

                        //# build command to compare.
                        //StringBuilder 
                        cmdTextSql = new StringBuilder(string.Format(@"SELECT COUNT(*) FROM [dbo].[{0}]", tableName));
                        for (int i = 0; i < dtSchema.Rows.Count; i++)
                        {
                            Type oraType = readerOra.GetFieldType(i);
                            object oraValue = readerOra.GetValue(i);

                            // arguments
                            string concat = (i == 0) ? "WHERE" : "AND";

                            //if (dtSchema.Rows[i]["ColumnName"].Equals("RATING"))
                            //    Debugger.Break();

                            //if (dtSchema.Rows[i]["ColumnName"].Equals("REPORT_NAV"))
                            //    Debugger.Break();

                            // concat field comparing...
                            if (readerOra.IsDBNull(i))
                                cmdTextSql.AppendFormat(" {0} [{1}] IS NULL", concat, dtSchema.Rows[i]["ColumnName"]);
                            else if (oraValue is string)
                                cmdTextSql.AppendFormat(" {0} [{1}]=N'{2}'", concat, dtSchema.Rows[i]["ColumnName"], oraValue);
                            else if (oraValue is DateTime)
                                cmdTextSql.AppendFormat(" {0} [{1}]='{2:yyyy-MM-dd HH:mm:ss}'", concat, dtSchema.Rows[i]["ColumnName"], oraValue);
                            //cmdTextSql.AppendFormat(" {0} CONVERT(VARCHAR,[{1}],121)='{2:yyyy-MM-dd HH:mm:ss}'", concat, columns[i], values[i]);
                            else if (oraType == typeof(Decimal) && dtSchema.Rows[i]["NumericPrecision"].Equals((Int16)126) && dtSchema.Rows[i]["NumericScale"].Equals((Int16)129)) // float
                            {
                                // ex: AND ABS([REPORT_NAV]-14.29) < 0.00001
                                cmdTextSql.AppendFormat(" {0} ABS([{1}]-({2}))<0.00001", concat, dtSchema.Rows[i]["ColumnName"], oraValue);
                            }
                            else
                                cmdTextSql.AppendFormat(" {0} [{1}]={2}", concat, dtSchema.Rows[i]["ColumnName"], oraValue);
                        }

                        ////# TRACE
                        //listView1.Items.Add(new ListViewItem(new string[]{
                        //     "TRACE"
                        //    ,"CmdText"
                        //    , cmdText.ToString()}));

                        //# do compare
                        //string cmdTextOra = string.Format("SELECT * FROM \"{0}\".\"{1}\"", dbName, tableName);
                        SqlCommand dbCmdSql = new SqlCommand(cmdTextSql.ToString(), connSql);
                        dbCmdSql.CommandTimeout = 0;

                        int IsExists = (int)dbCmdSql.ExecuteScalar();
                        if (IsExists > 0)
                        {
                            sqlCheckCnt++;
                        }
                        else
                        {
                            notEqualCnt++;

                            // show "not equal" message
                            lsvMessage.Items.Add(new ListViewItem(new string[] {
                                  "Message" // Message Tag
                                , "Not equal" //string.Format("{0:N0}%", ) // key value
                                , cmdTextSql.ToString()  // description
                                })).EnsureVisible(); // force to show

                            break; // leave
                        }

                        //# repore progrss each 2 second.
                        //double interval_time = 2.0;
                        if (DateTime.Now.Subtract(checkPoint).TotalSeconds >= interval_time)
                        {
                            #region ReportProgress

                            // show what doing
                            txtSqlLastChkCmd.Text = cmdTextSql.ToString();

                            // evaluate the finish time
                            StatusLabel1.Text = evaluteFinishTime.Evalute(oraReadCnt);

                            // show "step" message
                            ListViewItem addedItem = lsvMessage.Items.Add(new ListViewItem(new string[] {
                                  "Progress" // Message Tag
                                , CalculatePercent(oraReadCnt, totRowCnt) // key value
                                , string.Format("{0:N0} \\ {1:N0} \\ {2:N0} \\ {3:N0}", totRowCnt, oraReadCnt, sqlCheckCnt , notEqualCnt)  // description
                                }));

                            addedItem.EnsureVisible(); // force to show

                            // show progress
                            this.StatusProgressBar1.Value = oraReadCnt; 

                            // don't let application fall into "not response"!
                            Application.DoEvents();

                            #endregion

                            // next check point
                            checkPoint = DateTime.Now;
                        }

                    } // end of: while (readerOra.Read())
                }

                //## 6 success & show result

                // show what doing
                txtSqlLastChkCmd.Text = cmdTextSql.ToString();

                // show statistics & message
                lsvMessage.Items.Add("Compare Finished");

                lsvMessage.Items.Add(new ListViewItem(new string[] {
                      "Ora Read Count"
                    , string.Format("{0:N0}", oraReadCnt)
                    }));

                lsvMessage.Items.Add(new ListViewItem(new string[] {
                      "Sql Check Count"
                    , string.Format("{0:N0}", sqlCheckCnt)
                    }));

                lsvMessage.Items.Add(new ListViewItem(new string[] {
                      "Not Equal Rows"
                    , string.Format("{0:N0}", notEqualCnt)
                    })).EnsureVisible(); // show me

                // to show progress - 0% - END
                this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;
                StatusLabel1.Text = "00:00:00";

                // to show result.
                txtOraQryCmd.Text = cmdTextOra;
                //txtSqlQryCmd.Text = userState.SqlQryCmd;
                txtCmpResult.Text = (notEqualCnt == 0) ? "equal" : "may not equal";
                txtLogDtm.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //# save the result to DB
                // update status
                Database1DataSet.TabHardCmpRecordRow lastCmpRec = this.DataStore.TabHardCmpRecord[0];

                //lastCmpRec.DbName = userState.DbName; // PK
                //lastCmpRec.TableName = userState.TableName; // PK
                lastCmpRec.WhereCond = txtWhereCond.Text.Trim();
                lastCmpRec.OraQryCmd = cmdTextOra;
                lastCmpRec.SqlLastChkCmd = cmdTextSql.ToString();
                lastCmpRec.LastCmpResult = txtCmpResult.Text.Trim();
                lastCmpRec.LogDtm = DateTime.Now;
                // 
                int ret = this.tabHardCmpRecordTableAdapter.Update(lastCmpRec);

                // 7.close
                connOra.Close();
                connSql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //# UI Control
                this.Cursor = Cursors.Default;
                this.btnHardCompare.Enabled = true;
            }
        }

        private void trvDbTables_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!btnHardCompare.Enabled) // is comparing
            {
                e.Cancel = true;

                // show message
                toolTip1.Show("In comparing. no changing selection.", this.trvDbTables);
                Debug.WriteLine("In comparing. no changing selection.");
            }
        }

        private void trvDbTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Debug.WriteLine("ON : treeView1_AfterSelect()...");

            try
            {
                //# UI control
                this.Cursor = Cursors.WaitCursor;

                // not selected target & leave;
                if (e.Node.Tag != null) //# is Table node ===========================================
                {
                    // parse arguments
                    DbTableInfo db_table = (DbTableInfo)e.Node.Tag;

                    //# get the last compare record
                    Database1DataSet.TabHardCmpRecordRow lastCmpRec = null; // last table compare record / is also compare paremetres
                    this.tabHardCmpRecordTableAdapter.FillByPk(this.DataStore.TabHardCmpRecord, db_table.DbName, db_table.TableName);
                    Database1DataSet.TabHardCmpRecordDataTable dtTabCmpRec = this.DataStore.TabHardCmpRecord;
                    if (dtTabCmpRec.Count > 0)
                    {
                        //#--- had compared and take the last result. --------------
                        // Database1DataSet.TabCompareRecordRow
                        lastCmpRec = dtTabCmpRec[0]; // the last table compare record.
                    }
                    else
                    {
                        //#--- never compared, to creaet new --------------
                        // Database1DataSet.TabCompareRecordRow
                        lastCmpRec = dtTabCmpRec.NewTabHardCmpRecordRow();

                        lastCmpRec.DbName = db_table.DbName;
                        lastCmpRec.TableName = db_table.TableName;
                        lastCmpRec.WhereCond = string.Empty;
                        lastCmpRec.OraQryCmd = string.Empty;
                        lastCmpRec.SqlLastChkCmd = string.Empty;
                        lastCmpRec.LastCmpResult = "uncheck";
                        lastCmpRec.LogDtm = DateTime.MinValue;

                        // put into DataStore
                        dtTabCmpRec.AddTabHardCmpRecordRow(lastCmpRec);
                    }

                    //# show compare parameters & reset result.
                    //reset result.
                    lsvMessage.Items.Clear();

                    // show compare parameters
                    txtDbName.Text = db_table.DbName; // lastCmpRec.DbName;
                    txtTableName.Text = db_table.TableName; // lastCmpRec.TableName;
                    txtWhereCond.Text = lastCmpRec.WhereCond;

                    txtOraQryCmd.Text = lastCmpRec.OraQryCmd;
                    txtSqlLastChkCmd.Text = lastCmpRec.SqlLastChkCmd;
                    txtCmpResult.Text = lastCmpRec.LastCmpResult;
                    txtLogDtm.Text = lastCmpRec.LogDtm == DateTime.MinValue ? string.Empty : lastCmpRec.LogDtm.ToString("yyyy/MM/dd HH:mm:ss");

                    txtOrcRowCnt.Text = string.Empty;
                    txtSqlRowCnt.Text = string.Empty;

                    #region # get table rows count

                    // get arguments
                    Database1DataSet.TabRowCntDataTable dtTabRowCnt = this.tabRowCntTableAdapter.GetDataByPk(db_table.DbName, db_table.TableName);
                    if (dtTabRowCnt.Count > 0) // is exists
                    {
                        Database1DataSet.TabRowCntRow tabRowCnt = dtTabRowCnt[0];

                        txtOrcRowCnt.Text = tabRowCnt.OraRowCnt.ToString("N0");
                        txtSqlRowCnt.Text = tabRowCnt.SqlRowCnt.ToString("N0"); ;
                    }

                    #endregion
                }
                else //# is DB node ===========================================
                {
                    // when is initialized then leave;
                    if (e.Node.Nodes.Count > 0)
                        return;

                    #region # Init: qeury table name and bind to controller trvDbTables.

                    // get arguments
                    TreeNode trnDbName = e.Node; // DB node
                    string dbName = trnDbName.Text;
                    Database1DataSet.ConnInfoRow connInfo = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];

                    //// 1.Get Connection
                    OracleConnection conn = new OracleConnection(connInfo.ConnString);

                    //// 2.Open
                    conn.Open();

                    // 3.Build command
                    string cmdText = string.Format("SELECT OWNER, TABLE_NAME FROM all_tables WHERE OWNER = '{0}' ORDER BY TABLE_NAME", dbName);
                    OracleCommand dbCmd = new OracleCommand(cmdText, conn);

                    // 4.Execute cmmand and get ResultSet
                    using (var reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read()) // read one row
                        {
                            // get values
                            //string dbName = (string)reader.GetValue(0); // OWNER
                            string tableName = (string)reader.GetValue(1); // TABLE_NAME

                            // show result
                            TreeNode trnTableName = new TreeNode(tableName);
                            trnTableName.Tag = new DbTableInfo(dbName, tableName);
                            trnDbName.Nodes.Add(trnTableName);
                        }
                    }

                    // 5.close
                    conn.Close();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //# UI control
                this.Cursor = Cursors.Default;
            }
        }

        private void lsvMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != lsvMessage) return;

            // 'Ctrl+C' => copy 
            if (e.Control && e.KeyCode == Keys.C)
            {
                var builder = new StringBuilder();
                foreach (ListViewItem item in lsvMessage.SelectedItems)
                {
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        builder.Append(subItem.Text + "\t");
                    }

                    builder.AppendLine();
                }

                Clipboard.SetText(builder.ToString());
            }
        }

        private void trvDbTables_KeyUp(object sender, KeyEventArgs e)
        {
            //# 'Ctrl+C' => click [btnCompare2] 
            if (e.Control && e.KeyCode == Keys.C && btnHardCompare.Enabled)
            {
                // trigger event 
                btnHardCompare_Click(this, new EventArgs());
            }
        }
    }
}
