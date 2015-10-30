namespace NNOraToSqlValidator2
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_TestConn = new System.Windows.Forms.ToolStripButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.dbTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.providerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.connStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastCheckStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Test = new System.Windows.Forms.DataGridViewButtonColumn();
            this.connInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datastore = new NNOraToSqlValidator2.Database1DataSet();
            this.connInfoTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datastore)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip1_Refresh,
            this.toolStrip_Save,
            this.toolStrip_TestConn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(654, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStrip1_Refresh
            // 
            this.toolStrip1_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip1_Refresh.Image")));
            this.toolStrip1_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip1_Refresh.Name = "toolStrip1_Refresh";
            this.toolStrip1_Refresh.Size = new System.Drawing.Size(70, 22);
            this.toolStrip1_Refresh.Text = "Refresh";
            this.toolStrip1_Refresh.Click += new System.EventHandler(this.toolStrip1_Refresh_Click);
            // 
            // toolStrip_Save
            // 
            this.toolStrip_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_Save.Image")));
            this.toolStrip_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Save.Name = "toolStrip_Save";
            this.toolStrip_Save.Size = new System.Drawing.Size(55, 22);
            this.toolStrip_Save.Text = "Save";
            this.toolStrip_Save.Click += new System.EventHandler(this.toolStrip_Save_Click);
            // 
            // toolStrip_TestConn
            // 
            this.toolStrip_TestConn.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_TestConn.Image")));
            this.toolStrip_TestConn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_TestConn.Name = "toolStrip_TestConn";
            this.toolStrip_TestConn.Size = new System.Drawing.Size(119, 22);
            this.toolStrip_TestConn.Text = "Test Connection";
            this.toolStrip_TestConn.Click += new System.EventHandler(this.toolStrip_TestConn_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dbTypeDataGridViewTextBoxColumn,
            this.dbNameDataGridViewTextBoxColumn,
            this.providerNameDataGridViewTextBoxColumn,
            this.connStringDataGridViewTextBoxColumn,
            this.lastCheckStatusDataGridViewTextBoxColumn,
            this.Test});
            this.dgvData.DataSource = this.connInfoBindingSource;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 25);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(654, 396);
            this.dgvData.TabIndex = 1;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // dbTypeDataGridViewTextBoxColumn
            // 
            this.dbTypeDataGridViewTextBoxColumn.DataPropertyName = "DbType";
            this.dbTypeDataGridViewTextBoxColumn.HeaderText = "DbType";
            this.dbTypeDataGridViewTextBoxColumn.Name = "dbTypeDataGridViewTextBoxColumn";
            // 
            // dbNameDataGridViewTextBoxColumn
            // 
            this.dbNameDataGridViewTextBoxColumn.DataPropertyName = "DbName";
            this.dbNameDataGridViewTextBoxColumn.HeaderText = "DbName";
            this.dbNameDataGridViewTextBoxColumn.Name = "dbNameDataGridViewTextBoxColumn";
            // 
            // providerNameDataGridViewTextBoxColumn
            // 
            this.providerNameDataGridViewTextBoxColumn.DataPropertyName = "ProviderName";
            this.providerNameDataGridViewTextBoxColumn.HeaderText = "ProviderName";
            this.providerNameDataGridViewTextBoxColumn.Items.AddRange(new object[] {
            "unknow",
            "System.Data.OracleClient",
            "System.Data.SqlClient"});
            this.providerNameDataGridViewTextBoxColumn.Name = "providerNameDataGridViewTextBoxColumn";
            this.providerNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.providerNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // connStringDataGridViewTextBoxColumn
            // 
            this.connStringDataGridViewTextBoxColumn.DataPropertyName = "ConnString";
            this.connStringDataGridViewTextBoxColumn.HeaderText = "ConnString";
            this.connStringDataGridViewTextBoxColumn.Name = "connStringDataGridViewTextBoxColumn";
            // 
            // lastCheckStatusDataGridViewTextBoxColumn
            // 
            this.lastCheckStatusDataGridViewTextBoxColumn.DataPropertyName = "LastCheckStatus";
            this.lastCheckStatusDataGridViewTextBoxColumn.HeaderText = "LastCheckStatus";
            this.lastCheckStatusDataGridViewTextBoxColumn.Name = "lastCheckStatusDataGridViewTextBoxColumn";
            // 
            // Test
            // 
            this.Test.HeaderText = "Test";
            this.Test.Name = "Test";
            this.Test.Text = "Test";
            this.Test.UseColumnTextForButtonValue = true;
            // 
            // connInfoBindingSource
            // 
            this.connInfoBindingSource.DataMember = "ConnInfo";
            this.connInfoBindingSource.DataSource = this.datastore;
            // 
            // datastore
            // 
            this.datastore.DataSetName = "Database1DataSet";
            this.datastore.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // connInfoTableAdapter
            // 
            this.connInfoTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 421);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1 - Test Connection";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datastore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStrip1_Refresh;
        private System.Windows.Forms.ToolStripButton toolStrip_Save;
        private System.Windows.Forms.DataGridView dgvData;
        private Database1DataSet datastore;
        private System.Windows.Forms.BindingSource connInfoBindingSource;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter connInfoTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn providerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastCheckStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Test;
        private System.Windows.Forms.ToolStripButton toolStrip_TestConn;
    }
}

