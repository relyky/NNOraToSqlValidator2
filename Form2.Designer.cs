namespace NNOraToSqlValidator2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Reset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip_SelectDbName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip_ListTable = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_CountTabRows2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Clear = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.dbNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oraRowCntDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqlRowCntDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastCheckStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountRows = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabRowCntBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datastore = new NNOraToSqlValidator2.Database1DataSet();
            this.tabRowCntTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter();
            this.connInfoTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabRowCntBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datastore)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Refresh,
            this.toolStrip_Save,
            this.toolStrip_Reset,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStrip_SelectDbName,
            this.toolStrip_ListTable,
            this.toolStrip_CountTabRows2,
            this.toolStrip_Clear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(748, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStrip_Refresh
            // 
            this.toolStrip_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_Refresh.Image")));
            this.toolStrip_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Refresh.Name = "toolStrip_Refresh";
            this.toolStrip_Refresh.Size = new System.Drawing.Size(70, 22);
            this.toolStrip_Refresh.Text = "Refresh";
            this.toolStrip_Refresh.ToolTipText = "Refresh";
            this.toolStrip_Refresh.Click += new System.EventHandler(this.toolStrip_Refresh_Click);
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
            // toolStrip_Reset
            // 
            this.toolStrip_Reset.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_Reset.Image")));
            this.toolStrip_Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Reset.Name = "toolStrip_Reset";
            this.toolStrip_Reset.Size = new System.Drawing.Size(59, 22);
            this.toolStrip_Reset.Text = "Reset";
            this.toolStrip_Reset.ToolTipText = "Delete all data in dababase, used to re-do.";
            this.toolStrip_Reset.Click += new System.EventHandler(this.toolStrip_Reset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 22);
            this.toolStripLabel1.Text = "Select DB";
            // 
            // toolStrip_SelectDbName
            // 
            this.toolStrip_SelectDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStrip_SelectDbName.Items.AddRange(new object[] {
            "AAAMSEC",
            "AAAMSEC_FUND",
            "AAAMPOSTMAN",
            "AAAM1TO1",
            "BENF",
            "EVBENF",
            "IVRBENF"});
            this.toolStrip_SelectDbName.Name = "toolStrip_SelectDbName";
            this.toolStrip_SelectDbName.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStrip_ListTable
            // 
            this.toolStrip_ListTable.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_ListTable.Image")));
            this.toolStrip_ListTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_ListTable.Name = "toolStrip_ListTable";
            this.toolStrip_ListTable.Size = new System.Drawing.Size(81, 22);
            this.toolStrip_ListTable.Text = "List Table";
            this.toolStrip_ListTable.Click += new System.EventHandler(this.toolStrip_ListTable_Click);
            // 
            // toolStrip_CountTabRows2
            // 
            this.toolStrip_CountTabRows2.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_CountTabRows2.Image")));
            this.toolStrip_CountTabRows2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_CountTabRows2.Name = "toolStrip_CountTabRows2";
            this.toolStrip_CountTabRows2.Size = new System.Drawing.Size(176, 22);
            this.toolStrip_CountTabRows2.Text = "Count Rows and Compare";
            this.toolStrip_CountTabRows2.Click += new System.EventHandler(this.toolStrip_CountTabRows2_Click);
            // 
            // toolStrip_Clear
            // 
            this.toolStrip_Clear.Enabled = false;
            this.toolStrip_Clear.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_Clear.Image")));
            this.toolStrip_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Clear.Name = "toolStrip_Clear";
            this.toolStrip_Clear.Size = new System.Drawing.Size(57, 22);
            this.toolStrip_Clear.Text = "Clear";
            this.toolStrip_Clear.Visible = false;
            this.toolStrip_Clear.Click += new System.EventHandler(this.toolStrip_Clear_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 358);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(631, 17);
            this.StatusLabel1.Spring = true;
            this.StatusLabel1.Text = "StatusLabel1";
            // 
            // StatusProgressBar1
            // 
            this.StatusProgressBar1.Name = "StatusProgressBar1";
            this.StatusProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dbNameDataGridViewTextBoxColumn,
            this.tableNameDataGridViewTextBoxColumn,
            this.oraRowCntDataGridViewTextBoxColumn,
            this.sqlRowCntDataGridViewTextBoxColumn,
            this.lastCheckStatusDataGridViewTextBoxColumn,
            this.CountRows});
            this.dgvData.DataSource = this.tabRowCntBindingSource;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 25);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(748, 333);
            this.dgvData.TabIndex = 2;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // dbNameDataGridViewTextBoxColumn
            // 
            this.dbNameDataGridViewTextBoxColumn.DataPropertyName = "DbName";
            this.dbNameDataGridViewTextBoxColumn.HeaderText = "DbName";
            this.dbNameDataGridViewTextBoxColumn.Name = "dbNameDataGridViewTextBoxColumn";
            this.dbNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tableNameDataGridViewTextBoxColumn
            // 
            this.tableNameDataGridViewTextBoxColumn.DataPropertyName = "TableName";
            this.tableNameDataGridViewTextBoxColumn.HeaderText = "TableName";
            this.tableNameDataGridViewTextBoxColumn.Name = "tableNameDataGridViewTextBoxColumn";
            this.tableNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oraRowCntDataGridViewTextBoxColumn
            // 
            this.oraRowCntDataGridViewTextBoxColumn.DataPropertyName = "OraRowCnt";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.oraRowCntDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.oraRowCntDataGridViewTextBoxColumn.HeaderText = "OraRowCnt";
            this.oraRowCntDataGridViewTextBoxColumn.Name = "oraRowCntDataGridViewTextBoxColumn";
            this.oraRowCntDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sqlRowCntDataGridViewTextBoxColumn
            // 
            this.sqlRowCntDataGridViewTextBoxColumn.DataPropertyName = "SqlRowCnt";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.sqlRowCntDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.sqlRowCntDataGridViewTextBoxColumn.HeaderText = "SqlRowCnt";
            this.sqlRowCntDataGridViewTextBoxColumn.Name = "sqlRowCntDataGridViewTextBoxColumn";
            this.sqlRowCntDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastCheckStatusDataGridViewTextBoxColumn
            // 
            this.lastCheckStatusDataGridViewTextBoxColumn.DataPropertyName = "LastCheckStatus";
            this.lastCheckStatusDataGridViewTextBoxColumn.HeaderText = "LastCheckStatus";
            this.lastCheckStatusDataGridViewTextBoxColumn.Name = "lastCheckStatusDataGridViewTextBoxColumn";
            this.lastCheckStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CountRows
            // 
            this.CountRows.HeaderText = "Count Rows";
            this.CountRows.Name = "CountRows";
            this.CountRows.ReadOnly = true;
            this.CountRows.Text = "Count Rows";
            this.CountRows.ToolTipText = "Count one table rows and compare";
            this.CountRows.UseColumnTextForButtonValue = true;
            // 
            // tabRowCntBindingSource
            // 
            this.tabRowCntBindingSource.DataMember = "TabRowCnt";
            this.tabRowCntBindingSource.DataSource = this.datastore;
            // 
            // datastore
            // 
            this.datastore.DataSetName = "Database1DataSet";
            this.datastore.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabRowCntTableAdapter
            // 
            this.tabRowCntTableAdapter.ClearBeforeFill = true;
            // 
            // connInfoTableAdapter
            // 
            this.connInfoTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 380);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form2";
            this.Text = "Form2 - Count All Tables Rows";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabRowCntBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datastore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStrip_Refresh;
        private System.Windows.Forms.ToolStripButton toolStrip_Save;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStrip_SelectDbName;
        private System.Windows.Forms.ToolStripButton toolStrip_Clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dgvData;
        private Database1DataSet datastore;
        private System.Windows.Forms.BindingSource tabRowCntBindingSource;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter tabRowCntTableAdapter;
        private System.Windows.Forms.ToolStripButton toolStrip_ListTable;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter connInfoTableAdapter;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dbNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oraRowCntDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlRowCntDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastCheckStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn CountRows;
        private System.Windows.Forms.ToolStripButton toolStrip_CountTabRows2;
        private System.Windows.Forms.ToolStripButton toolStrip_Reset;
    }
}