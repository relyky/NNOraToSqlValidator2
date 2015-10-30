using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Diagnostics;

namespace NNOraToSqlValidator2
{
    public partial class Form1 : Form
    {
        #region Do Actions

        private string DoTestConnectionString(string providerName, string connString)
        {
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    return DoTestConnectionString_SqlClient(connString);
                case "System.Data.OracleClient":
                    return DoTestConnectionString_OracleClient(connString);
            }

            return "Unexpected provider";
        }

        private string DoTestConnectionString_OracleClient(string connString)
        {
            OracleConnection conn = new OracleConnection(connString);

            try
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                    return "失敗";

                conn.Close();
                return "成功";
            }
            catch
            {
                return "失敗";
            }
        }

        private string DoTestConnectionString_SqlClient(string connString)
        {
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                if(conn.State != ConnectionState.Open)
                    return "失敗";

                conn.Close();
                return "成功";
            }
            catch
            {
                return "失敗";
            }
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'database1DataSet.ConnInfo' 資料表。您可以視需要進行移動或移除。
            this.connInfoTableAdapter.Fill(this.datastore.ConnInfo);
        }

        private void toolStrip1_Refresh_Click(object sender, EventArgs e)
        {
            // Refresh from DB
            this.connInfoTableAdapter.Fill(this.datastore.ConnInfo);
        }

        private void toolStrip_Save_Click(object sender, EventArgs e)
        {
            // Save to DB
            this.connInfoTableAdapter.Update(this.datastore.ConnInfo);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //# UI control
                this.Cursor = Cursors.WaitCursor;

                //# prefix
                if (e.ColumnIndex != 5 || e.RowIndex < 0) // click [Test] button.
                    return; 
                
                //# go

                // get connection infe.
                var row = (Database1DataSet.ConnInfoRow)((DataRowView)dgvData.Rows[e.RowIndex].DataBoundItem).Row;

                // check connection
                string result = DoTestConnectionString(row.ProviderName, row.ConnString);

                // update status
                row.LastCheckStatus = result;

                // refresh screen
                dgvData.InvalidateRow(e.RowIndex);
            }
            finally
            {
                //# UI control
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStrip_TestConn_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow vr in dgvData.Rows)
            {
                // to trigger to click [Test] button.
                dgvData_CellContentClick(this, new DataGridViewCellEventArgs(5, vr.Index));
            }
        }
    }
}
