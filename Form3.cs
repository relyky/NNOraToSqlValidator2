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

    public partial class Form3 : Form
    {
        #region Do Actions

        /// <summary>
        /// A temporary class for exchange information
        /// </summary>
        private class DbTableInfo
        {
            public DbTableInfo(string _dbName, string _tableName, string _PrimaryKey)
            {
                DbName = _dbName;
                TableName = _tableName;
                PrimaryKey = _PrimaryKey;
            }

            public string DbName;
            public string TableName;
            public string PrimaryKey;
        }

        private string BuildQracleQueryCmd(string dbName, string tableName, string whereCondition, string orderBy, decimal rownum)
        {
            string cmd = string.Format("SELECT * FROM \"{0}\".\"{1}\"", dbName, tableName);

            if (whereCondition != string.Empty)
            {
                cmd += string.Format(" WHERE {0}", whereCondition);
            }

            if (orderBy != string.Empty)
            {
                // replace NVL -> ISNULL
                //orderBy = orderBy.Replace("NVL", "NVL");

                // {ASCII} -> COLLATE Latin1_General_BIN
                orderBy = orderBy.Replace("{ASCII}", "");

                cmd += string.Format(" ORDER BY {0}", orderBy);
            }

            if (rownum != 0)
            {
                // sample: SELECT * FROM (SELECT * FROM AAAM1TO1.BV_USER ORDER BY USER_ID ASC) WHERE ROWNUM <= 100

                cmd = string.Format("SELECT * FROM ({0}) WHERE ROWNUM <= {1}", cmd, rownum);
            }

            return cmd;
        }

        private string BuildSqlQueryCmd(string dbName, string tableName, string whereCondition, string orderBy, decimal rownum)
        {
            string cmd = (rownum == 0)
                       ? string.Format("SELECT * FROM [{0}].[dbo].[{1}]", dbName, tableName)
                       : string.Format("SELECT TOP {2} * FROM [{0}].[dbo].[{1}]", dbName, tableName, rownum);

            if (whereCondition != string.Empty)
            {
                cmd += string.Format(" WHERE {0}", whereCondition);
            }

            if (orderBy != string.Empty)
            {
                // replace NVL -> ISNULL
                orderBy = orderBy.Replace("NVL", "ISNULL");

                // {ASCII} -> COLLATE Latin1_General_BIN
                orderBy = orderBy.Replace("{ASCII}", "COLLATE Latin1_General_BIN");

                cmd += string.Format(" ORDER BY {0}", orderBy);
            }

            return cmd;
        }

        #endregion

        #region resource

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
                TimeSpan remainTime = new TimeSpan(0, 0, remainSteps * _interval_seconds);
                return string.Format("{0:00}:{1:00}:{2:00}", (int)remainTime.TotalHours, remainTime.Minutes, remainTime.Seconds);
            }
        }

        /// <summary>
        /// Use to exchange the execution status of [_CompareBgWorker]
        /// </summary> 
        private class CompareBgWorker_UserState
        {
            // arguments
            public string DbName;
            public string TableName;
            public string OraQryCmd;
            public string SqlQryCmd;

            // user state 
            public int TotalRowCnt;
            public int OraReadCnt;
            public int SqlReadCnt;
            public int NotEqualCnt;
            //public string Message;

            public int PercentProgress
            {
                get
                {
                    return (int)((double)OraReadCnt / (double)TotalRowCnt * 100.0);
                }
            }

            public bool ManualStop = false;
        }

        #endregion

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //_CompareBgWorker = new BackgroundWorker();
            //_CompareBgWorker.WorkerReportsProgress = true;
            //_CompareBgWorker.WorkerSupportsCancellation = true;
            //_CompareBgWorker.DoWork += new DoWorkEventHandler(_CompareBgWorker_DoWork);
            //_CompareBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_CompareBgWorker_RunWorkerCompleted);
            //_CompareBgWorker.ProgressChanged += new ProgressChangedEventHandler(_CompareBgWorker_ProgressChanged);
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
                    Database1DataSet.TabCompareRecordRow lastCmpRec = null; // last table compare record / is also compare paremetres

                    //# get the last compare record
                    this.tabCompareRecordTableAdapter.FillByPk(this.DataStore.TabCompareRecord, db_table.DbName, db_table.TableName);
                    Database1DataSet.TabCompareRecordDataTable dtTabCmpRec = this.DataStore.TabCompareRecord;
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
                        lastCmpRec = dtTabCmpRec.NewTabCompareRecordRow();

                        lastCmpRec.DbName = db_table.DbName;
                        lastCmpRec.TableName = db_table.TableName;
                        lastCmpRec.WhereCond = string.Empty;
                        lastCmpRec.OrderBy = string.Empty;
                        lastCmpRec.TopRow = 0;
                        lastCmpRec.OraQryCmd = string.Empty;
                        lastCmpRec.SqlQryCmd = string.Empty;
                        lastCmpRec.LastCmpResult = "uncheck";
                        lastCmpRec.LogDtm = DateTime.MinValue;

                        #region # organize [OrderBy] by primary-key

                        if (db_table.PrimaryKey != string.Empty)
                        {
                            // ORDER BY [key1] ASC, [key2] ASC
                            string orderBy = string.Empty;
                            foreach (string keyName in db_table.PrimaryKey.Split(','))
                            {
                                orderBy += (orderBy == string.Empty)
                                        ? string.Format("{0} ASC", keyName)
                                        : string.Format(",{0} ASC", keyName);
                            }

                            lastCmpRec.OrderBy = orderBy; // show in screen
                        }

                        #endregion

                        // put into DataStore
                        dtTabCmpRec.AddTabCompareRecordRow(lastCmpRec);
                    }

                    //# show compare parameters & reset result.
                    //reset result.
                    lsvCompareResult.Clear();

                    // show compare parameters
                    txtDbName.Text = lastCmpRec.DbName;
                    txtTableName.Text = lastCmpRec.TableName;
                    txtPrimaryKey.Text = db_table.PrimaryKey; // string.Empty; // "(no keys)";
                    txtWhereCond.Text = lastCmpRec.WhereCond;
                    txtOrderBy.Text = lastCmpRec.OrderBy;
                    numTopRow.Value = (decimal)lastCmpRec.TopRow;

                    txtOraQryCmd.Text = lastCmpRec.OraQryCmd;
                    txtSqlQryCmd.Text = lastCmpRec.SqlQryCmd;
                    txtCmpResult.Text = lastCmpRec.LastCmpResult;
                    txtLogDtm.Text = lastCmpRec.LogDtm == DateTime.MinValue ? string.Empty : lastCmpRec.LogDtm.ToString("yyyy/MM/dd HH:mm:ss");
                    //
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

                            #region # get primary keys

                            string primaryKeyList = string.Empty;

                            // Build command
                            string cmdText2 = string.Format("SELECT *  FROM \"{0}\".\"{1}\" WHERE ROWNUM = 1", dbName, tableName);
                            OracleCommand dbCmd2 = new OracleCommand(cmdText2, conn);

                            // Execute cmmand and get ResultSet
                            using (var reader2 = dbCmd2.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
                            {
                                DataTable dt = reader2.GetSchemaTable();

                                foreach (DataRow r in dt.Rows)
                                {
                                    if (r["IsKey"].Equals(true))
                                    {
                                        primaryKeyList += (primaryKeyList == string.Empty)
                                                        ? string.Format("{0}",  r["ColumnName"])
                                                        : string.Format(",{0}", r["ColumnName"]);
                                    }
                                }
                            }

                            #endregion

                            // show result
                            string tableNameDesc = tableName + (primaryKeyList == string.Empty ? "" : "*");
                            TreeNode trnTableName = new TreeNode(tableNameDesc);
                            trnTableName.Tag = new DbTableInfo(dbName, tableName, primaryKeyList);
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

        #region compare - backup

        //private BackgroundWorker _CompareBgWorker = null;

        ///// <summary>
        ///// Use to exchange the execution status of [_CompareBgWorker]
        ///// </summary> 
        //private class CompareBgWorker_UserState
        //{
        //    // arguments
        //    public string DbName;
        //    public string TableName;
        //    public string OraQryCmd;
        //    public string SqlQryCmd;

        //    // user state 
        //    public int TotalRowCnt;
        //    public int OraReadCnt;
        //    public int SqlReadCnt;
        //    public int NotEqualCnt;
        //    //public string Message;

        //    public int PercentProgress
        //    {
        //        get
        //        {
        //            return (int)((double)OraReadCnt / (double)TotalRowCnt * 100.0);
        //        }
        //    }

        //    public bool ManualStop = false;
        //}

        //private void btnCompare2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //# prefix
        //        if (txtDbName.Text.Trim() == string.Empty)
        //        {
        //            MessageBox.Show("need [DB.Table] to use to compare.");
        //            return;
        //        }

        //        if (txtOrcRowCnt.Text.Trim() == string.Empty || txtSqlRowCnt.Text.Trim() == string.Empty)
        //        {
        //            MessageBox.Show("the [Oracle Row Count], [Sql Row Count] should be taken before");
        //            return;
        //        }

        //        //# UI control
        //        this.Cursor = Cursors.WaitCursor;
        //        this.lsvCompareResult.Cursor = Cursors.WaitCursor;
        //        this.trvDbTables.Enabled = false;
        //        this.btnCompare2.Enabled = false;
        //        this.btnCancel.Enabled = true;
        //        this.btnShowSchema.Enabled = false;

        //        //# UI control - reste columns
        //        lsvCompareResult.Clear();
        //        lsvCompareResult.Columns.Add(colMsgTag);
        //        lsvCompareResult.Columns.Add(colKeyValue);
        //        lsvCompareResult.Columns.Add(colDescription);

        //        // Initialize [UserState]
        //        CompareBgWorker_UserState userState = new CompareBgWorker_UserState();
        //        // arguments
        //        userState.DbName = txtDbName.Text;
        //        userState.TableName = txtTableName.Text;
        //        userState.OraQryCmd = BuildQracleQueryCmd(txtDbName.Text, txtTableName.Text, txtWhereCond.Text, txtOrderBy.Text, numTopRow.Value);
        //        userState.SqlQryCmd = BuildSqlQueryCmd(txtDbName.Text, txtTableName.Text, txtWhereCond.Text, txtOrderBy.Text, numTopRow.Value);
        //        // user state
        //        userState.TotalRowCnt = numTopRow.Value == 0 ? int.Parse(txtOrcRowCnt.Text.Replace(",", "")) : (int)numTopRow.Value;
        //        userState.OraReadCnt = 0;
        //        userState.SqlReadCnt = 0;
        //        userState.NotEqualCnt = 0;
        //        userState.ManualStop = false;
        //        //userState.Message = string.Empty;

        //        // show arguments
        //        lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //              "Argument" // message tag
        //            , "Oracle Query Command" // key value
        //            , userState.OraQryCmd // description
        //            }));

        //        lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //              "Argument" // message tag
        //            , "Sql Query Command" // key value
        //            , userState.SqlQryCmd // description
        //            }));

        //        // -- to show progress
        //        this.StatusProgressBar1.Minimum = 0;
        //        this.StatusProgressBar1.Maximum = userState.TotalRowCnt;
        //        this.StatusProgressBar1.Value = 0; // userState.OraReadCnt; // to show progress - 0% - BEGIN

        //        // go 
        //        _CompareBgWorker.RunWorkerAsync(userState);
        //    }
        //    finally
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void _CompareBgWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Debug.WriteLine("ON : _CompareBgWorker_DoWork()...");

        //    // resource
        //    OracleConnection connOra = null; // new OracleConnection(connInfoOra.ConnString);
        //    SqlConnection connSql = null; // new SqlConnection(connInfoSql.ConnString);

        //    try
        //    {

        //        // parse & take argument.
        //        CompareBgWorker_UserState userState = (CompareBgWorker_UserState)e.Argument;

        //        Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", userState.DbName)[0];
        //        Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", userState.DbName)[0];

        //        // 0. default

        //        // 1.Get Connection
        //        //OracleConnection 
        //        connOra = new OracleConnection(connInfoOra.ConnString);
        //        //SqlConnection 
        //        connSql = new SqlConnection(connInfoSql.ConnString);

        //        // 2.Open
        //        connOra.Open();
        //        connSql.Open();

        //        // 3.Build command
        //        string cmdTextOra = userState.OraQryCmd; // string.Format("SELECT * FROM \"{0}\".\"{1}\"", userState.DbName, userState.TableName);
        //        OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);

        //        string cmdTextSql = userState.SqlQryCmd; // string.Format("SELECT * FROM [{0}].dbo.[{1}]", userState.DbName, userState.TableName);
        //        SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);

        //        // 4.Execute cmmand and get ResultSet
        //        var readerOra = dbCmdOra.ExecuteReader();
        //        var readerSql = dbCmdSql.ExecuteReader();
        //        try
        //        {
        //            // get schema
        //            DataTable dtSchema = readerOra.GetSchemaTable();

        //            userState.OraReadCnt = 0;
        //            userState.SqlReadCnt = 0;
        //            DateTime checkPoint = DateTime.Now;

        //            // compare field by field
        //            while (readerOra.Read())
        //            {
        //                userState.OraReadCnt++;

        //                if (readerSql.Read())
        //                {
        //                    userState.SqlReadCnt++;

        //                    for (int i = 0; i < dtSchema.Rows.Count; i++) // schema-column
        //                    {
        //                        Type oraType = readerOra.GetFieldType(i);
        //                        Type sqlType = readerSql.GetFieldType(i);
        //                        object oraValue; // = readerOra.GetValue(i);
        //                        object sqlValue; // = readerSql.GetValue(i);

        //                        if (oraType == typeof(decimal) && sqlType == typeof(double))
        //                        {
        //                            if (readerOra.IsDBNull(i))
        //                                oraValue = DBNull.Value;
        //                            else
        //                                oraValue = readerOra.GetDecimal(i);

        //                            if (readerSql.IsDBNull(i))
        //                                sqlValue = DBNull.Value;
        //                            else
        //                                sqlValue = new Decimal(readerSql.GetDouble(i));
        //                        }
        //                        else // default
        //                        {
        //                            oraValue = readerOra.GetValue(i);
        //                            sqlValue = readerSql.GetValue(i);
        //                        }

        //                        // compare
        //                        if (!(oraValue.Equals(sqlValue)))
        //                        {
        //                            userState.NotEqualCnt++;
        //                        }
        //                    }
        //                }

        //                // repore progrss in one second.
        //                if (DateTime.Now.Subtract(checkPoint).TotalSeconds >= 1.0)
        //                {
        //                    _CompareBgWorker.ReportProgress(userState.PercentProgress, userState);
        //                    checkPoint = DateTime.Now;
        //                }

        //                if (_CompareBgWorker.CancellationPending)
        //                {
        //                    // e.Cancel = true; 
        //                    // remark "e.Cancel", beecust it makes "e.Result" un-available. use "userState.ManualStop" to instead.
        //                    userState.ManualStop = true;
        //                    break;
        //                }
        //            }

        //            // success & set up result to return
        //            e.Result = userState;
        //        }
        //        finally
        //        {
        //            readerOra.Close();
        //            readerSql.Close();
        //        }

        //        // 5.close
        //        connOra.Close();
        //        connSql.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // release resource
        //        connOra.Close();
        //        connSql.Close();

        //        MessageBox.Show(ex.Message);

        //        e.Result = ex;
        //    }
        //}

        //private void _CompareBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Debug.WriteLine("ON : _CompareBgWorker_RunWorkerCompleted()...");

        //    try
        //    {
        //        //# make "userState.ManualSto" to instade of e.Cancelled.
        //        //if (e.Cancelled)
        //        //{
        //        //    return; // leave
        //        //}

        //        if (e.Result is Exception)
        //        {
        //            Exception ex = (Exception)e.Result;

        //            // show statistics & message
        //            lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //                    "Abort"
        //                  , ex.GetType().ToString()
        //                  , ex.Message
        //                }));

        //            return; // leave
        //        }

        //        // parse & take argument.
        //        CompareBgWorker_UserState userState = (CompareBgWorker_UserState)e.Result;

        //        // show statistics & message
        //        if(userState.ManualStop)
        //            lsvCompareResult.Items.Add("Compare cancelled");
        //        else
        //            lsvCompareResult.Items.Add("Compare finished");

        //        lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //              "Ora read count"
        //            , string.Format("{0:N0}", userState.OraReadCnt)
        //            }));

        //        lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //              "Sql read count"
        //            , string.Format("{0:N0}", userState.SqlReadCnt)
        //            }));

        //        lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //              "Not equal fields"
        //            , string.Format("{0:N0}", userState.NotEqualCnt)
        //            }));

        //        // to show progress - 0% - END
        //        this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;
        //        this.StatusProgressBar1.Invalidate();

        //        // to show result.
        //        txtOraQryCmd.Text = userState.OraQryCmd;
        //        txtSqlQryCmd.Text = userState.SqlQryCmd;
        //        txtCmpResult.Text = userState.ManualStop ? "cancel" 
        //                          : userState.NotEqualCnt == 0 ? "equal" 
        //                          : "may not equal";

        //        txtLogDtm.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        //        //# save the result to DB
        //        // update status
        //        Database1DataSet.TabCompareRecordRow lastCmpRec = this.DataStore.TabCompareRecord[0];

        //        //lastCmpRec.DbName = userState.DbName; // PK
        //        //lastCmpRec.TableName = userState.TableName; // PK
        //        lastCmpRec.WhereCond = txtWhereCond.Text.Trim();
        //        lastCmpRec.OrderBy = txtOrderBy.Text.Trim();
        //        lastCmpRec.TopRow = (int)numTopRow.Value;
        //        lastCmpRec.OraQryCmd = userState.OraQryCmd;
        //        lastCmpRec.SqlQryCmd = userState.SqlQryCmd;
        //        lastCmpRec.LastCmpResult = txtCmpResult.Text.Trim();
        //        lastCmpRec.LogDtm = DateTime.Now;
        //        // 
        //        int ret = this.tabCompareRecordTableAdapter.Update(lastCmpRec);
        //    }
        //    finally
        //    {
        //        // UI control - mouse
        //        this.lsvCompareResult.Cursor = Cursors.Default;
        //        this.trvDbTables.Enabled = true;
        //        this.btnCompare2.Enabled = true;
        //        this.btnCancel.Enabled = false;
        //        this.btnShowSchema.Enabled = true;
        //    }
        //}

        //private void _CompareBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    Debug.WriteLine("ON : _CompareBgWorker_RunWorkerCompleted()...");

        //    // parse & take argument.
        //    CompareBgWorker_UserState userState = (CompareBgWorker_UserState)e.UserState;

        //    // show "step" message
        //    ListViewItem addedItem = lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //          "Progress" // Message Tag
        //        , string.Format("{0:N0}%", userState.PercentProgress) // key value
        //        , string.Format("{0:N0} \\ {1:N0} \\ {2:N0} \\ {3:N0}", userState.TotalRowCnt, userState.OraReadCnt, userState.SqlReadCnt, userState.NotEqualCnt)  // description
        //        }));

        //    addedItem.EnsureVisible();

        //    // to show progress - 0% - END
        //    this.StatusProgressBar1.Value = userState.OraReadCnt;
        //    this.StatusProgressBar1.Invalidate();
        //}

        #endregion

        private void btnShowSchema_Click(object sender, EventArgs e)
        {
            try
            {
                //# UI control
                this.Cursor = Cursors.WaitCursor;

                //# prefix
                if (txtDbName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("need [DB.Table] to use to compare.");
                    return;
                }

                //# go

                // get arguments
                string dbName = txtDbName.Text.Trim();
                string tableName = txtTableName.Text.Trim();

                Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];
                Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", dbName)[0];

                // 0. default

                //# reste columns
                lsvCompareResult.Clear();
                lsvCompareResult.Columns.Add("Ora Column Name", 150);
                lsvCompareResult.Columns.Add("Ora Data Type", 180);
                lsvCompareResult.Columns.Add("Sql Column Name", 150);
                lsvCompareResult.Columns.Add("Sql Data Type", 180);

                // 1.Get Connection
                OracleConnection connOra = new OracleConnection(connInfoOra.ConnString);
                SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);

                // 2.Open
                connOra.Open();
                connSql.Open();

                // 3.Build command
                string cmdTextOra = string.Format("SELECT * FROM \"{0}\".\"{1}\"", dbName, tableName);
                OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);
                dbCmdOra.CommandTimeout = 0;

                string cmdTextSql = string.Format("SELECT * FROM [{0}].dbo.[{1}]", dbName, tableName);
                SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);
                dbCmdSql.CommandTimeout = 0;

                // 4.Execute cmmand and get ResultSet
                var readerOra = dbCmdOra.ExecuteReader(CommandBehavior.SchemaOnly);
                var readerSql = dbCmdSql.ExecuteReader(CommandBehavior.SchemaOnly);
                try
                {
                    // get schema
                    DataTable dtSchemaOra = readerOra.GetSchemaTable();
                    DataTable dtSchemaSql = readerSql.GetSchemaTable();

                    var qry = from a in dtSchemaSql.AsEnumerable()
                              join b in dtSchemaOra.AsEnumerable() on a.Field<string>("ColumnName") equals b.Field<string>("ColumnName") into ps
                              from b in ps.DefaultIfEmpty()
                              select new {
                                    OraColumnName = b == null ? "<NULL>" : b.Field<string>("ColumnName")
                                  , OraColumnType = b == null ? "<NULL>" : b.Field<Type>("DataType").ToString()
                                  , SqlColumnName = a.Field<string>("ColumnName")
                                  , SqlColumnType = a.Field<Type>("DataType").ToString()
                              };

                    foreach (var c in qry)
                    {
                        ListViewItem newItem = new ListViewItem(new string[] { c.OraColumnName, c.OraColumnType, c.SqlColumnName, c.SqlColumnType });
                        lsvCompareResult.Items.Add(newItem);
                    }
                }
                finally
                {
                    readerOra.Close();
                    readerSql.Close();
                }

                // 5.close
                connOra.Close();
                connSql.Close();
            }
            finally
            {
                //# UI control
                this.Cursor = Cursors.Default;
            }
        }

        private void lsvCompareResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != lsvCompareResult) return;

            // 'Ctrl+C' => copy 
            if(e.Control && e.KeyCode == Keys.C)
            {
                var builder = new StringBuilder();
                foreach (ListViewItem item in lsvCompareResult.SelectedItems)
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
            if (e.Control && e.KeyCode == Keys.C && btnCompare3.Enabled)
            {
                // trigger event 
                btnCompare3_Click(this, new EventArgs());
            }
        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    lsvCompareResult.Items.Add("Cancelling...");
        //    _CompareBgWorker.CancelAsync();
        //}

        private void btnCompare3_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("ON : btnCompare3_Click()...");

            // resource
            OracleConnection connOra = null; // new OracleConnection(connInfoOra.ConnString);
            SqlConnection connSql = null; // new SqlConnection(connInfoSql.ConnString);

            try
            {
                //# prefix
                if (txtDbName.Text.Trim() == string.Empty)
                {
                    if (sender == btnBatchCmp)
                        toolTip1.Show("need [DB.Table] to use to compare.", this);
                    else
                        MessageBox.Show("need [DB.Table] to use to compare.");

                    return;
                }

                if (txtOrcRowCnt.Text.Trim() == string.Empty || txtSqlRowCnt.Text.Trim() == string.Empty)
                {
                    if (sender == btnBatchCmp)
                        toolTip1.Show("the [Oracle Row Count], [Sql Row Count] should be taken before", this);
                    else
                        MessageBox.Show("the [Oracle Row Count], [Sql Row Count] should be taken before");

                    return;
                }

                //# UI control
                //this.Cursor = Cursors.WaitCursor;
                this.lsvCompareResult.Cursor = Cursors.WaitCursor;
                this.trvDbTables.Cursor = Cursors.WaitCursor;
                this.btnCompare3.Enabled = false;
                this.btnCancel3.Enabled = true;
                this.btnShowSchema.Enabled = false;

                //# UI control - reste columns
                lsvCompareResult.Clear();
                lsvCompareResult.Columns.Add(colMsgTag);
                lsvCompareResult.Columns.Add(colKeyValue);
                lsvCompareResult.Columns.Add(colDescription);

                // Initialize [UserState]
                CompareBgWorker_UserState userState = new CompareBgWorker_UserState();
                // arguments
                userState.DbName = txtDbName.Text;
                userState.TableName = txtTableName.Text;
                userState.OraQryCmd = BuildQracleQueryCmd(txtDbName.Text, txtTableName.Text, txtWhereCond.Text, txtOrderBy.Text, numTopRow.Value);
                userState.SqlQryCmd = BuildSqlQueryCmd(txtDbName.Text, txtTableName.Text, txtWhereCond.Text, txtOrderBy.Text, numTopRow.Value);
                // user state
                userState.TotalRowCnt = numTopRow.Value == 0 ? int.Parse(txtOrcRowCnt.Text.Replace(",", "")) : (int)numTopRow.Value;
                userState.OraReadCnt = 0;
                userState.SqlReadCnt = 0;
                userState.NotEqualCnt = 0;
                userState.ManualStop = false;
                //userState.Message = string.Empty;
                btnCancel3.Tag = null; // "CancellationPending";

                // resource : for EvaluteFinishTime
                double interval_time = 2.0;
                EvaluteFinishTime evaluteFinishTime = new EvaluteFinishTime(userState.TotalRowCnt, (int)interval_time);

                // show arguments
                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                      "Argument" // message tag
                    , "Oracle Query Command" // key value
                    , userState.OraQryCmd // description
                    }));

                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                      "Argument" // message tag
                    , "Sql Query Command" // key value
                    , userState.SqlQryCmd // description
                    }));

                // -- to show progress
                this.StatusProgressBar1.Minimum = 0;
                this.StatusProgressBar1.Maximum = userState.TotalRowCnt;
                this.StatusProgressBar1.Value = 0; // userState.OraReadCnt; // to show progress - 0% - BEGIN

                Application.DoEvents(); // show me some stuff

                //// parse & take argument.
                //CompareBgWorker_UserState userState = (CompareBgWorker_UserState)e.Argument;

                // 1.Get connection information.
                Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", userState.DbName)[0];
                Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", userState.DbName)[0];

                //OracleConnection 
                connOra = new OracleConnection(connInfoOra.ConnString);
                //SqlConnection 
                connSql = new SqlConnection(connInfoSql.ConnString);

                // 2.Open connection
                connOra.Open();
                connSql.Open();

                // 3.Build command
                string cmdTextOra = userState.OraQryCmd; // string.Format("SELECT * FROM \"{0}\".\"{1}\"", userState.DbName, userState.TableName);
                OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);
                dbCmdOra.CommandTimeout = (int)numTimeout.Value;

                string cmdTextSql = userState.SqlQryCmd; // string.Format("SELECT * FROM [{0}].dbo.[{1}]", userState.DbName, userState.TableName);
                SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);
                dbCmdSql.CommandTimeout = (int)numTimeout.Value;

                #region 4.Execute cmmand and get ResultSet

                var readerOra = dbCmdOra.ExecuteReader();
                var readerSql = dbCmdSql.ExecuteReader();
                try
                {
                    // resource
                    string first_not_equal_field = string.Empty;

                    // get schema
                    DataTable dtSchema = readerOra.GetSchemaTable();

                    userState.OraReadCnt = 0;
                    userState.SqlReadCnt = 0;
                    DateTime checkPoint = DateTime.Now;

                    // compare field by field
                    while (readerOra.Read())
                    {
                        userState.OraReadCnt++;

                        if (readerSql.Read())
                        {
                            userState.SqlReadCnt++;

                            for (int i = 0; i < dtSchema.Rows.Count; i++) // schema-column
                            {
                                Type oraType = readerOra.GetFieldType(i);
                                Type sqlType = readerSql.GetFieldType(i);
                                object oraValue; // = readerOra.GetValue(i);
                                object sqlValue; // = readerSql.GetValue(i);

                                if (oraType == typeof(decimal) && sqlType == typeof(double))
                                {
                                    if (readerOra.IsDBNull(i))
                                        oraValue = DBNull.Value;
                                    else
                                        oraValue = readerOra.GetDecimal(i);

                                    if (readerSql.IsDBNull(i))
                                        sqlValue = DBNull.Value;
                                    else
                                        sqlValue = new Decimal(readerSql.GetDouble(i));
                                }
                                else // default
                                {
                                    oraValue = readerOra.GetValue(i);
                                    sqlValue = readerSql.GetValue(i);
                                }

                                // compare
                                if (!(oraValue.Equals(sqlValue)))
                                {
                                    userState.NotEqualCnt++;

                                    //
                                    if (first_not_equal_field == string.Empty)
                                        first_not_equal_field = Convert.ToString(dtSchema.Rows[i]["ColumnName"]);
                                }
                            }

                            //# Not equal & stopping comparing
                            if (userState.NotEqualCnt > 0)
                            {
                                StringBuilder oraFiledValues = new StringBuilder("{"); // json format
                                StringBuilder sqlFiledValues = new StringBuilder("{");

                                // the first
                                object oraValue = readerOra.GetValue(0);
                                object sqlValue = readerSql.GetValue(0);
                                oraFiledValues.AppendFormat("{0}: {1}", dtSchema.Rows[0]["ColumnName"], Convert.ToString(oraValue));
                                sqlFiledValues.AppendFormat("{0}: {1}", dtSchema.Rows[0]["ColumnName"], Convert.ToString(sqlValue));

                                // as following
                                for (int i = 1; i < dtSchema.Rows.Count; i++) // schema-column
                                {
                                    oraValue = readerOra.GetValue(i);
                                    sqlValue = readerSql.GetValue(i);
                                    oraFiledValues.AppendFormat(",{0}: {1}", dtSchema.Rows[i]["ColumnName"], Convert.ToString(oraValue));
                                    sqlFiledValues.AppendFormat(",{0}: {1}", dtSchema.Rows[i]["ColumnName"], Convert.ToString(sqlValue));
                                }
                                
                                oraFiledValues.Append("}");
                                sqlFiledValues.Append("}");

                                #region show progress

                                // show "step" message
                                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                                  "Progress" // Message Tag
                                , string.Format("{0:N0}%", userState.PercentProgress) // key value
                                , string.Format("{0:N0} \\ {1:N0} \\ {2:N0} \\ {3:N0}", userState.TotalRowCnt, userState.OraReadCnt, userState.SqlReadCnt, userState.NotEqualCnt)  // description
                                }));

                                // show "Not equal"
                                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                                  "Not equal!!!" // Message Tag
                                , "first_not_equal_field" // key value
                                , first_not_equal_field // description
                                }));

                                // 
                                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                                  "Not equal first" // Message Tag
                                , "Ora value" // key value
                                , oraFiledValues.ToString() // description
                                }));

                                // 
                                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                                  "Not equal first" // Message Tag
                                , "Sql value" // key value
                                , sqlFiledValues.ToString() // description
                                })).EnsureVisible();

                                Application.DoEvents(); // show me some stuff

                                #endregion

                                //## not equal & leave
                                break;
                            }

                            //-------------------------
                        }

                        // repore progrss in one second.
                        //double interval_time = 1.0;
                        if (DateTime.Now.Subtract(checkPoint).TotalSeconds >= interval_time)
                        {
                            //_CompareBgWorker.ReportProgress(userState.PercentProgress, userState);

                            #region show progress

                            // evaluate the finish time
                            StatusLabel1.Text = evaluteFinishTime.Evalute(userState.OraReadCnt);

                            // show "step" message
                            ListViewItem addedItem = lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                                  "Progress" // Message Tag
                                , string.Format("{0:N0}%", userState.PercentProgress) // key value
                                , string.Format("{0:N0} \\ {1:N0} \\ {2:N0} \\ {3:N0}", userState.TotalRowCnt, userState.OraReadCnt, userState.SqlReadCnt, userState.NotEqualCnt)  // description
                                }));

                            addedItem.EnsureVisible(); // force to show

                            // to show progress
                            this.StatusProgressBar1.Value = userState.OraReadCnt;
                            this.StatusProgressBar1.Invalidate();

                            // don't let application fall into "not response"!
                            Application.DoEvents();

                            #endregion

                            // next check point
                            checkPoint = DateTime.Now;
                        }

                        //if (_CompareBgWorker.CancellationPending)
                        if(btnCancel3.Tag != null && (string)btnCancel3.Tag == "CancellationPending")
                        {
                            //## cancel & leave
                            // e.Cancel = true; 
                            // remark "e.Cancel", beecust it makes "e.Result" un-available. use "userState.ManualStop" to instead.
                            userState.ManualStop = true;
                            break;
                        }
                    }

                    //// success & set up result to return
                    //e.Result = userState;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    //e.Result = ex;

                    // show statistics & message
                    lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                        "Abort"
                      , ex.GetType().ToString()
                      , ex.Message
                    }));

                    return; // leave
                }
                finally
                {
                    readerOra.Close();
                    readerSql.Close();
                }

                #endregion

                //## 5.close
                connOra.Close();
                connSql.Close();

                //## 6 success & show result

                // show statistics & message
                if (userState.ManualStop)
                    lsvCompareResult.Items.Add("Compare cancelled");
                else
                    lsvCompareResult.Items.Add("Compare finished");

                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                      "Ora read count"
                    , string.Format("{0:N0}", userState.OraReadCnt)
                    }));

                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                      "Sql read count"
                    , string.Format("{0:N0}", userState.SqlReadCnt)
                    }));

                lsvCompareResult.Items.Add(new ListViewItem(new string[] {
                      "Not equal fields"
                    , string.Format("{0:N0}", userState.NotEqualCnt)
                    })).EnsureVisible(); // show me

                // to show progress - 0% - END
                this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;
                StatusLabel1.Text = "00:00:00";
                //this.StatusProgressBar1.Invalidate();

                // to show result.
                txtOraQryCmd.Text = userState.OraQryCmd;
                txtSqlQryCmd.Text = userState.SqlQryCmd;
                txtCmpResult.Text = userState.ManualStop ? "cancel"
                                  : userState.NotEqualCnt == 0 ? "equal"
                                  : "may not equal";

                txtLogDtm.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //# save the result to DB
                // update status
                Database1DataSet.TabCompareRecordRow lastCmpRec = this.DataStore.TabCompareRecord[0];

                //lastCmpRec.DbName = userState.DbName; // PK
                //lastCmpRec.TableName = userState.TableName; // PK
                lastCmpRec.WhereCond = txtWhereCond.Text.Trim();
                lastCmpRec.OrderBy = txtOrderBy.Text.Trim();
                lastCmpRec.TopRow = (int)numTopRow.Value;
                lastCmpRec.OraQryCmd = userState.OraQryCmd;
                lastCmpRec.SqlQryCmd = userState.SqlQryCmd;
                lastCmpRec.LastCmpResult = txtCmpResult.Text.Trim();
                lastCmpRec.LogDtm = DateTime.Now;
                // 
                int ret = this.tabCompareRecordTableAdapter.Update(lastCmpRec);

            }
            catch (Exception ex)
            {
                // release resource
                connOra.Close();
                connSql.Close();

                MessageBox.Show(ex.Message);

                //e.Result = ex;
            }
            finally
            {
                // UI control - mouse
                //this.Cursor = Cursors.Default;
                this.lsvCompareResult.Cursor = Cursors.Default;
                this.trvDbTables.Cursor = Cursors.Default;
                this.btnCompare3.Enabled = true;
                this.btnCancel3.Enabled = false;
                this.btnShowSchema.Enabled = true;
            }
        }

        private void btnCancel3_Click(object sender, EventArgs e)
        {
            lsvCompareResult.Items.Add("Cancelling...");
            //_CompareBgWorker.CancelAsync();
            btnCancel3.Tag = "CancellationPending";
        }

        private void trvDbTables_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!this.btnCompare3.Enabled) // is comparing
            {
                e.Cancel = true;

                // show message
                toolTip1.Show("In comparing. no changing selection.", this.trvDbTables);
                Debug.WriteLine("In comparing. no changing selection.");
            }
        }

        private void btnBatchCmp_Click(object sender, EventArgs e)
        {
            TreeNode curTabNode = this.trvDbTables.SelectedNode;

            while(curTabNode != null && curTabNode.Tag != null) // is Table node
            {
                // focus to make it VISIBLE.
                this.trvDbTables.Select();
                this.trvDbTables.Update();

                // parse arguments
                DbTableInfo db_table = (DbTableInfo)curTabNode.Tag;

                // by pass or not
                bool byPass = false;
                if (db_table.DbName == "BENF" && 
                    ( db_table.TableName == "SELLBENEMGRM_BD" ||
                      db_table.TableName == "SELLBENEMGRD_BD" ||
                      db_table.TableName == "SELLBENEMGRM_FA" ||
                      db_table.TableName == "SELLBENEMGRD_FA" ))
                {
                    byPass = true;
                }

                if (!byPass)
                {
                    // trigger - Compare3
                    if (btnCompare3.Enabled)
                    {
                        // trigger event 
                        btnCompare3_Click(btnBatchCmp, new EventArgs());
                    }
                    else
                    {
                        break;
                    }
                }

                // next
                curTabNode = curTabNode.NextNode;
                if (curTabNode != null)
                {
                    // select next node.
                    this.trvDbTables.SelectedNode = curTabNode;
                }
            }
        }

        #region backup

        //private void btnCompare_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.WaitCursor;

        //        //# prefix
        //        //if (txtPrimaryKey.Text.Trim() == string.Empty)
        //        //{
        //        //    MessageBox.Show("need [Primary Key] to use to compare.");
        //        //    return;
        //        //}

        //        //# go

        //        // get arguments
        //        string dbName = txtDbName.Text.Trim();
        //        string tableName = txtTableName.Text.Trim();

        //        Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];
        //        Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", dbName)[0];

        //        // 0. default

        //        //# reste columns
        //        lsvCompareResult.Clear();
        //        lsvCompareResult.Columns.Add("Key Value", 150);
        //        lsvCompareResult.Columns.Add("Difference Tag", 100);
        //        lsvCompareResult.Columns.Add("Description", 200);

        //        // 1.Get Connection
        //        OracleConnection connOra = new OracleConnection(connInfoOra.ConnString);
        //        SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);

        //        // 2.Open
        //        connOra.Open();
        //        connSql.Open();

        //        // 3.Build command
        //        string cmdTextOra = string.Format("SELECT * FROM \"{0}\".\"{1}\"", dbName, tableName);
        //        OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);

        //        string cmdTextSql = string.Format("SELECT * FROM [{0}].dbo.[{1}]", dbName, tableName);
        //        SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);

        //        // 4.Execute cmmand and get ResultSet
        //        var readerOra = dbCmdOra.ExecuteReader();
        //        var readerSql = dbCmdSql.ExecuteReader();
        //        try
        //        {
        //            // get schema
        //            DataTable dtSchema = readerOra.GetSchemaTable();

        //            int oraReadCnt = 0;
        //            int sqlReadCnt = 0;

        //            // -- to show progress
        //            this.StatusProgressBar1.Minimum = 0;
        //            this.StatusProgressBar1.Maximum = int.Parse(txtOrcRowCnt.Text.Replace(",",""));
        //            this.StatusProgressBar1.Value = 0; // to show progress - 0% - BEGIN

        //            // compare field by field
        //            while (readerOra.Read())
        //            {
        //                oraReadCnt++;

        //                if (readerSql.Read())
        //                {
        //                    sqlReadCnt++;

        //                    for (int i = 0; i < dtSchema.Rows.Count; i++) // schema-column
        //                    {
        //                        Type oraType = readerOra.GetFieldType(i);
        //                        Type sqlType = readerSql.GetFieldType(i);
        //                        object oraValue = readerOra.GetValue(i);
        //                        object sqlValue = readerSql.GetValue(i);

        //                        // compare
        //                        if (!(oraValue.Equals(sqlValue)))
        //                        {
        //                            //ListViewItem newItem = new ListViewItem();
        //                            //lsvCompareResult.Items.Add(itm);

        //                            lsvCompareResult.Items.Add("Oh NO!!!");

        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    lsvCompareResult.Items.Add("Oh NO!!!");
        //                }

        //                // -- to show progress - increase one step.
        //                this.StatusProgressBar1.Value++;
        //            }

        //            // to show progress - 0% - END
        //            this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;

        //            // show statistics
        //            lsvCompareResult.Items.Add("Compare finished.");

        //            lsvCompareResult.Items.Add(new ListViewItem( new string[] {
        //                  "Ora Read Count"
        //                , string.Format("{0:N0}", oraReadCnt)
        //                }));

        //            lsvCompareResult.Items.Add(new ListViewItem(new string[] {
        //                  "Sql Read Count"
        //                , string.Format("{0:N0}", sqlReadCnt)
        //                }));
        //        }
        //        finally
        //        {
        //            readerOra.Close();
        //            readerSql.Close();
        //        }

        //        // 5.close
        //        connOra.Close();
        //        connSql.Close();
        //    }
        //    finally
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        #endregion
    }
}