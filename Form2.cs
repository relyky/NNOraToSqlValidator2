using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Collections;
using System.Data.SqlServerCe;

namespace NNOraToSqlValidator2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            // use to show status
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            // show stauts
            //var qryNottMatch = this.datastore.TabRowCnt.Where(c => !c.IsOraRowCntNull() && !c.IsSqlRowCntNull() && c.OraRowCnt != c.SqlRowCnt);
            //var qryNottMatch = this.datastore.TabRowCnt.Where(c => c.IsOraRowCntNull() || c.IsSqlRowCntNull() || c.OraRowCnt != c.SqlRowCnt);
            var qryNottMatch = this.datastore.TabRowCnt.Where(c => c.RowState != DataRowState.Deleted && c.LastCheckStatus != "match");
 
            this.StatusLabel1.Text = string.Format("Table Count : {0}, Not Match : {1}", 
                this.datastore.TabRowCnt.Count,
                qryNottMatch.Count());
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'database1DataSet.TabRowCnt' 資料表。您可以視需要進行移動或移除。
            this.tabRowCntTableAdapter.Fill(this.datastore.TabRowCnt);
        }

        private void toolStrip_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.tabRowCntTableAdapter.Fill(this.datastore.TabRowCnt);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStrip_Save_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.tabRowCntTableAdapter.Update(this.datastore.TabRowCnt);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStrip_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.tabRowCntTableAdapter.Connection.Open();

                // delete all
                foreach (var dr in this.datastore.TabRowCnt)
                    this.tabRowCntTableAdapter.Delete(dr.DbName, dr.TableName);

                // Refresh
                this.tabRowCntTableAdapter.Fill(this.datastore.TabRowCnt);

                this.tabRowCntTableAdapter.Connection.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStrip_Clear_Click(object sender, EventArgs e)
        {
            this.datastore.TabRowCnt.Clear();
        }

        private void toolStrip_ListTable_Click(object sender, EventArgs e)
        {
            try
            {
                //# UI control
                this.Cursor = Cursors.WaitCursor;

                //# prefix
                if (toolStrip_SelectDbName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please [Select DB] first.");
                    return;
                }

                //# go

                // get arguments
                string dbName = toolStrip_SelectDbName.Text.Trim();
                Database1DataSet.ConnInfoRow connInfo = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];

                // clear current state
                //this.datastore.TabRowCnt.Clear();

                // 1.Get Connection
                OracleConnection conn = new OracleConnection(connInfo.ConnString);

                // 2.Open
                conn.Open();

                // 3.Build command
                string cmdText = string.Format("SELECT OWNER, TABLE_NAME FROM all_tables WHERE OWNER = '{0}'", dbName);
                OracleCommand dbCmd = new OracleCommand(cmdText, conn);

                // 4.Execute cmmand and get ResultSet
                using (var reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read()) // read one row
                    {
                        // get values
                        var nr = this.datastore.TabRowCnt.NewTabRowCntRow();

                        nr.DbName = (string)reader.GetValue(0); // OWNER
                        nr.TableName = (string)reader.GetValue(1); // TABLE_NAME
                        nr.LastCheckStatus = "uncheck";

                        // insert where not exists;
                        if (this.datastore.TabRowCnt.FindByDbNameTableName(nr.DbName, nr.TableName) == null)
                            this.datastore.TabRowCnt.AddTabRowCntRow(nr);
                    }
                }

                // 5.close
                conn.Close();

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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //# UI control
                this.Cursor = Cursors.WaitCursor;

                //# prefix
                if (e.ColumnIndex != 5 || e.RowIndex < 0) // click [Count Rwos] button.
                    return;

                //# go

                // get arguments

                // current data-row
                var cdr = (Database1DataSet.TabRowCntRow)((DataRowView)dgvData.Rows[e.RowIndex].DataBoundItem).Row;

                Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", cdr.DbName)[0];
                Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", cdr.DbName)[0];

                // 0. default
                cdr.LastCheckStatus = "uncheck";
                // force to refresh screen
                dgvData.InvalidateRow(e.RowIndex);
                dgvData.Update();

                // 1.Get Connection
                SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);
                OracleConnection connOra = new OracleConnection(connInfoOra.ConnString);

                //====== Sql part - Count table rows ======
                try
                {
                    // clear current state
                    cdr.SetSqlRowCntNull();

                    // 2.Open
                    connSql.Open();

                    // 3.Build command
                    //string cmdTextSql = string.Format("SELECT COUNT(*) FROM {0}.dbo.{1}", cdr.DbName, cdr.TableName);
                    string cmdTextSql = string.Format("SELECT COUNT(*) FROM [dbo].[{0}]", cdr.TableName);
                    SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);
                    dbCmdSql.CommandTimeout = 0;

                    // 4.Execute cmmand and get ResultSet
                    using (var reader = dbCmdSql.ExecuteReader())
                    {
                        if (reader.Read()) // read one row
                            cdr.SqlRowCnt = (int)reader.GetValue(0);
                    }
                }
                catch (Exception ex)
                {
                    cdr.SetSqlRowCntNull();
                    cdr.LastCheckStatus = ex.Message;
                }
                finally
                {
                    // 5.close
                    connSql.Close();
                }

                //====== Ora part - Count table rows ======
                try
                {
                    // clear current state
                    cdr.SetOraRowCntNull();

                    // 2.Open
                    connOra.Open();

                    // 3.Build command
                    string cmdTextOra = string.Format("SELECT COUNT(*) FROM \"{0}\".\"{1}\"", cdr.DbName, cdr.TableName);
                    OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);

                    // 4.Execute cmmand and get ResultSet
                    using (var reader = dbCmdOra.ExecuteReader())
                    {
                        if (reader.Read()) // read one row
                            cdr.OraRowCnt = (int)(decimal)reader.GetValue(0);
                    }
                }
                catch (Exception ex)
                {
                    cdr.SetOraRowCntNull();
                    cdr.LastCheckStatus = ex.Message;
                }
                finally
                {

                    // 5.close
                    connOra.Close();
                }

                // 5.Compare Row-Count
                if (!cdr.IsOraRowCntNull() && !cdr.IsSqlRowCntNull())
                {
                    int diff = cdr.SqlRowCnt - cdr.OraRowCnt;
                    cdr.LastCheckStatus = (diff == 0)
                                        ? "match"
                                        : string.Format("not match: {0:N0}", diff);

                    //cdr.LastCheckStatus = (cdr.OraRowCnt == cdr.SqlRowCnt)
                    //                    ? "match"
                    //                    : "not match";
                }

                // force to refresh screen
                dgvData.InvalidateRow(e.RowIndex);
                dgvData.Update();
            }
            finally
            {
                //# UI control
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStrip_CountTabRows2_Click(object sender, EventArgs e)
        {
            // paremeters
            string selectDbName = toolStrip_SelectDbName.Text.Trim();

            // query proceed target with DbName.
            List<DataGridViewRow> selectDbRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow vr in dgvData.Rows)
            {
                string dbName = (string)vr.Cells[0].Value;
                if (dbName == selectDbName)
                    selectDbRows.Add(vr);
            }

            // -- to show progress
            this.StatusProgressBar1.Minimum = 0;
            this.StatusProgressBar1.Maximum = selectDbRows.Count;
            this.StatusProgressBar1.Value = 0; // to show progress - 0% - BEGIN

            foreach (DataGridViewRow vr in selectDbRows)
            {
                int dispRowCnt = dgvData.DisplayedRowCount(false);
                dgvData.FirstDisplayedScrollingRowIndex = (vr.Index < dispRowCnt ? 0 : vr.Index - dispRowCnt);
                dgvData.DisplayedRowCount(false);

                // to trigger to click [Count Rows] button.
                dgvData_CellContentClick(this, new DataGridViewCellEventArgs(5, vr.Index));

                // -- to show progress - increase one step.
                this.StatusProgressBar1.Value++;

                // let windows form not be stun.
                Application.DoEvents();
            }

            // to show progress - 0% - END
            this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;
        }

        #region backup

        //private void toolStrip_CountTabRows_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.WaitCursor;

        //        //# prefix
        //        if (toolStrip_SelectDbName.Text.Trim() == string.Empty)
        //        {
        //            MessageBox.Show("Please [Select DB] first.");
        //            return;
        //        }

        //        //# go

        //        // get arguments
        //        string dbName = toolStrip_SelectDbName.Text.Trim();
        //        Database1DataSet.ConnInfoRow connInfoSql = this.connInfoTableAdapter.GetDataByPk("Sql", dbName)[0];
        //        Database1DataSet.ConnInfoRow connInfoOra = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];

        //        // 1.Get Connection
        //        //SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);
        //        SqlConnection connSql = new SqlConnection(connInfoSql.ConnString);
        //        OracleConnection connOra = new OracleConnection(connInfoOra.ConnString);

        //        // 2.Open
        //        connSql.Open();
        //        connOra.Open();

        //        // One by one
        //        // -- to show progress
        //        this.StatusProgressBar1.Minimum = 0;
        //        this.StatusProgressBar1.Maximum = this.datastore.TabRowCnt.Where(c => c.DbName == dbName).Count();
        //        this.StatusProgressBar1.Value = 0;
        //        foreach (var row in this.datastore.TabRowCnt.Where(c => c.DbName == dbName))
        //        {
        //            // default
        //            row.LastCheckStatus = "uncheck";

        //            //====== Sql part - Count table rows ======
        //            try
        //            {
        //                // clear current state
        //                row.SetSqlRowCntNull();

        //                // 3.Build command
        //                //string cmdTextSql = string.Format("SELECT COUNT(*) FROM {0}.dbo.{1}", row.DbName, row.TableName);
        //                string cmdTextSql = string.Format("SELECT COUNT(*) FROM dbo.[{0}]", row.TableName);
        //                SqlCommand dbCmdSql = new SqlCommand(cmdTextSql, connSql);

        //                // 4.Execute cmmand and get ResultSet
        //                using (var reader = dbCmdSql.ExecuteReader())
        //                {
        //                    if (reader.Read()) // read one row
        //                        row.SqlRowCnt = (int)reader.GetValue(0);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                row.SetSqlRowCntNull();
        //                row.LastCheckStatus = ex.Message;
        //            }

        //            //====== Ora part - Count table rows ======
        //            try
        //            {
        //                // clear current state
        //                row.SetOraRowCntNull();

        //                // 3.Build command
        //                string cmdTextOra = string.Format("SELECT COUNT(*) FROM {0}.{1}", row.DbName, row.TableName);
        //                OracleCommand dbCmdOra = new OracleCommand(cmdTextOra, connOra);

        //                // 4.Execute cmmand and get ResultSet
        //                using (var reader = dbCmdOra.ExecuteReader())
        //                {
        //                    if (reader.Read()) // read one row
        //                        row.OraRowCnt = (int)(decimal)reader.GetValue(0);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                row.SetOraRowCntNull();
        //                row.LastCheckStatus = ex.Message;
        //            }

        //            // 5.Compare Row-Count
        //            if (!row.IsOraRowCntNull() && !row.IsSqlRowCntNull())
        //            {
        //                row.LastCheckStatus = (row.OraRowCnt == row.SqlRowCnt)
        //                                    ? "match"
        //                                    : "not match";
        //            }

        //            // increset progress.
        //            this.StatusProgressBar1.Value++;
        //        }

        //        // 5.close
        //        connSql.Close();
        //        connOra.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.Default;
        //        this.StatusProgressBar1.Value = this.StatusProgressBar1.Minimum;
        //    }
        //}

        //private void toolStrip_CountOraPart_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.WaitCursor;

        //        //# prefix
        //        if (toolStrip_SelectDbName.Text.Trim() == string.Empty)
        //        {
        //            MessageBox.Show("Please [Select DB] first.");
        //            return;
        //        }

        //        //# go

        //        // get arguments
        //        string dbName = toolStrip_SelectDbName.Text.Trim();
        //        Database1DataSet.ConnInfoRow connInfo = this.connInfoTableAdapter.GetDataByPk("Ora", dbName)[0];

        //        // clear current state
        //        //this.datastore.TabRowCnt.Clear();


        //        // 1.Get Connection
        //        OracleConnection conn = new OracleConnection(connInfo.ConnString);

        //        // 2.Open
        //        conn.Open();

        //        // One by one
        //        foreach (var row in this.datastore.TabRowCnt.Where(c => c.DbName == dbName))
        //        {
        //            // clear current state
        //            row.SetOraRowCntNull();

        //            // 3.Build command
        //            string cmdText = string.Format("SELECT COUNT(*) FROM {0}.{1}", row.DbName, row.TableName);
        //            OracleCommand dbCmd = new OracleCommand(cmdText, conn);

        //            // 4.Execute cmmand and get ResultSet
        //            using (var reader = dbCmd.ExecuteReader())
        //            {
        //                if (reader.Read()) // read one row
        //                {
        //                    // get values
        //                    int rowCount = (int)(decimal)reader.GetValue(0);

        //                    // update to datastore
        //                    row.OraRowCnt = rowCount;
        //                }
        //            }
        //        }

        //        // 5.close
        //        conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void toolStrip_CountSqlPart_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.WaitCursor;

        //        //# prefix
        //        if (toolStrip_SelectDbName.Text.Trim() == string.Empty)
        //        {
        //            MessageBox.Show("Please [Select DB] first.");
        //            return;
        //        }

        //        //# go

        //        // get arguments
        //        string dbName = toolStrip_SelectDbName.Text.Trim();
        //        Database1DataSet.ConnInfoRow connInfo = this.connInfoTableAdapter.GetDataByPk("Sql", dbName)[0];

        //        // clear current state
        //        //this.datastore.TabRowCnt.Clear();

        //        // 1.Get Connection
        //        SqlConnection conn = new SqlConnection(connInfo.ConnString);

        //        // 2.Open
        //        conn.Open();

        //        // One by one
        //        foreach (var row in this.datastore.TabRowCnt.Where(c => c.DbName == dbName))
        //        {
        //            // clear current state
        //            row.SetSqlRowCntNull();

        //            // 3.Build command
        //            //string cmdText = string.Format("SELECT COUNT(*) FROM {1}.dbo.{0}", row.TableName, row.DbName);
        //            string cmdText = string.Format("SELECT COUNT(*) FROM dbo.{0}", row.TableName);
        //            SqlCommand dbCmd = new SqlCommand(cmdText, conn);

        //            // 4.Execute cmmand and get ResultSet
        //            using (var reader = dbCmd.ExecuteReader())
        //            {
        //                if (reader.Read()) // read one row
        //                {
        //                    // get values
        //                    int rowCount = (int)reader.GetValue(0);

        //                    // update to datastore
        //                    row.SqlRowCnt = rowCount;

        //                    // validate
        //                    if (!row.IsOraRowCntNull() && !row.IsSqlRowCntNull())
        //                    {
        //                        row.LastCheckStatus = (row.OraRowCnt == row.SqlRowCnt)
        //                                            ? "match"
        //                                            : "not match";
        //                    }
        //                    else
        //                    {
        //                        row.LastCheckStatus = "uncheck";
        //                    }
        //                }
        //            }
        //        }

        //        // 5.close
        //        conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        //# UI control
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        #endregion of backup
    }
}
