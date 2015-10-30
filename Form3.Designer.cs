namespace NNOraToSqlValidator2
{
    partial class Form3
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
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("AAAMSEC");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("AAAMSEC_FUND");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("AAAMPOSTMAN");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("AAAM1TO1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("BENF");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("EVBENF");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("IVRBENF");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trvDbTables = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsvCompareResult = new System.Windows.Forms.ListView();
            this.colMsgTag = new System.Windows.Forms.ColumnHeader();
            this.colKeyValue = new System.Windows.Forms.ColumnHeader();
            this.colDescription = new System.Windows.Forms.ColumnHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.btnBatchCmp = new System.Windows.Forms.Button();
            this.btnCancel3 = new System.Windows.Forms.Button();
            this.btnCompare3 = new System.Windows.Forms.Button();
            this.txtSqlRowCnt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnShowSchema = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLogDtm = new System.Windows.Forms.TextBox();
            this.txtCmpResult = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSqlQryCmd = new System.Windows.Forms.TextBox();
            this.txtOraQryCmd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOrcRowCnt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrderBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrimaryKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWhereCond = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numTopRow = new System.Windows.Forms.NumericUpDown();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.connInfoTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter();
            this.tabCompareRecordTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.TabCompareRecordTableAdapter();
            this.DataStore = new NNOraToSqlValidator2.Database1DataSet();
            this.tabRowCntTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataStore)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(851, 427);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(181, 427);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trvDbTables);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(173, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DB-Table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trvDbTables
            // 
            this.trvDbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDbTables.Location = new System.Drawing.Point(3, 3);
            this.trvDbTables.Name = "trvDbTables";
            treeNode8.Name = "Node0";
            treeNode8.Text = "AAAMSEC";
            treeNode9.Name = "Node1";
            treeNode9.Text = "AAAMSEC_FUND";
            treeNode10.Name = "Node2";
            treeNode10.Text = "AAAMPOSTMAN";
            treeNode11.Name = "Node3";
            treeNode11.Text = "AAAM1TO1";
            treeNode12.Name = "Node4";
            treeNode12.Text = "BENF";
            treeNode13.Name = "Node5";
            treeNode13.Text = "EVBENF";
            treeNode14.Name = "Node6";
            treeNode14.Text = "IVRBENF";
            this.trvDbTables.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            this.trvDbTables.Size = new System.Drawing.Size(167, 395);
            this.trvDbTables.TabIndex = 0;
            this.trvDbTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDbTables_AfterSelect);
            this.trvDbTables.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trvDbTables_KeyUp);
            this.trvDbTables.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvDbTables_BeforeSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lsvCompareResult);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 325);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 102);
            this.panel1.TabIndex = 1;
            // 
            // lsvCompareResult
            // 
            this.lsvCompareResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMsgTag,
            this.colKeyValue,
            this.colDescription});
            this.lsvCompareResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvCompareResult.FullRowSelect = true;
            this.lsvCompareResult.Location = new System.Drawing.Point(0, 0);
            this.lsvCompareResult.Name = "lsvCompareResult";
            this.lsvCompareResult.Size = new System.Drawing.Size(666, 80);
            this.lsvCompareResult.TabIndex = 0;
            this.lsvCompareResult.UseCompatibleStateImageBehavior = false;
            this.lsvCompareResult.View = System.Windows.Forms.View.Details;
            this.lsvCompareResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsvCompareResult_KeyUp);
            // 
            // colMsgTag
            // 
            this.colMsgTag.Text = "Message Tag";
            this.colMsgTag.Width = 94;
            // 
            // colKeyValue
            // 
            this.colKeyValue.Text = "Key Value";
            this.colKeyValue.Width = 134;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 500;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 80);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(666, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(518, 17);
            this.StatusLabel1.Spring = true;
            this.StatusLabel1.Text = "StatusLabel1";
            // 
            // StatusProgressBar1
            // 
            this.StatusProgressBar1.Name = "StatusProgressBar1";
            this.StatusProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTimeout);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btnBatchCmp);
            this.groupBox1.Controls.Add(this.btnCancel3);
            this.groupBox1.Controls.Add(this.btnCompare3);
            this.groupBox1.Controls.Add(this.txtSqlRowCnt);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnShowSchema);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtOrcRowCnt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtOrderBy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPrimaryKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtWhereCond);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numTopRow);
            this.groupBox1.Controls.Add(this.txtTableName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table Filed Compare Setting";
            // 
            // numTimeout
            // 
            this.numTimeout.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numTimeout.Location = new System.Drawing.Point(521, 179);
            this.numTimeout.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(120, 22);
            this.numTimeout.TabIndex = 11;
            this.numTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTimeout.ThousandsSeparator = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(442, 184);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "Timeout (sec.)";
            // 
            // btnBatchCmp
            // 
            this.btnBatchCmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatchCmp.BackColor = System.Drawing.Color.Orange;
            this.btnBatchCmp.Location = new System.Drawing.Point(533, 11);
            this.btnBatchCmp.Name = "btnBatchCmp";
            this.btnBatchCmp.Size = new System.Drawing.Size(121, 23);
            this.btnBatchCmp.TabIndex = 20;
            this.btnBatchCmp.TabStop = false;
            this.btnBatchCmp.Text = "Batch Compare";
            this.toolTip1.SetToolTip(this.btnBatchCmp, "Compare table one by one. To select \"table\" and will run to end of \"DB\".");
            this.btnBatchCmp.UseVisualStyleBackColor = false;
            this.btnBatchCmp.Click += new System.EventHandler(this.btnBatchCmp_Click);
            // 
            // btnCancel3
            // 
            this.btnCancel3.Enabled = false;
            this.btnCancel3.Location = new System.Drawing.Point(278, 179);
            this.btnCancel3.Name = "btnCancel3";
            this.btnCancel3.Size = new System.Drawing.Size(92, 23);
            this.btnCancel3.TabIndex = 10;
            this.btnCancel3.Text = "Cancel";
            this.btnCancel3.UseVisualStyleBackColor = true;
            this.btnCancel3.Click += new System.EventHandler(this.btnCancel3_Click);
            // 
            // btnCompare3
            // 
            this.btnCompare3.Location = new System.Drawing.Point(180, 179);
            this.btnCompare3.Name = "btnCompare3";
            this.btnCompare3.Size = new System.Drawing.Size(92, 23);
            this.btnCompare3.TabIndex = 9;
            this.btnCompare3.Text = "Compare";
            this.btnCompare3.UseVisualStyleBackColor = true;
            this.btnCompare3.Click += new System.EventHandler(this.btnCompare3_Click);
            // 
            // txtSqlRowCnt
            // 
            this.txtSqlRowCnt.Location = new System.Drawing.Point(520, 150);
            this.txtSqlRowCnt.Name = "txtSqlRowCnt";
            this.txtSqlRowCnt.ReadOnly = true;
            this.txtSqlRowCnt.Size = new System.Drawing.Size(122, 22);
            this.txtSqlRowCnt.TabIndex = 7;
            this.txtSqlRowCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(437, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "Sql Row Count";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(80, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(311, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "* NVL -> ISNULL ; {ASCII} -> COLLATE Latin1_General_BIN";
            // 
            // btnShowSchema
            // 
            this.btnShowSchema.Location = new System.Drawing.Point(82, 179);
            this.btnShowSchema.Name = "btnShowSchema";
            this.btnShowSchema.Size = new System.Drawing.Size(92, 23);
            this.btnShowSchema.TabIndex = 8;
            this.btnShowSchema.Text = "Show Schema";
            this.btnShowSchema.UseVisualStyleBackColor = true;
            this.btnShowSchema.Click += new System.EventHandler(this.btnShowSchema_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLogDtm);
            this.groupBox2.Controls.Add(this.txtCmpResult);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSqlQryCmd);
            this.groupBox2.Controls.Add(this.txtOraQryCmd);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 107);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Last Compare Status";
            // 
            // txtLogDtm
            // 
            this.txtLogDtm.Location = new System.Drawing.Point(283, 74);
            this.txtLogDtm.Name = "txtLogDtm";
            this.txtLogDtm.ReadOnly = true;
            this.txtLogDtm.Size = new System.Drawing.Size(122, 22);
            this.txtLogDtm.TabIndex = 3;
            // 
            // txtCmpResult
            // 
            this.txtCmpResult.Location = new System.Drawing.Point(79, 74);
            this.txtCmpResult.Name = "txtCmpResult";
            this.txtCmpResult.ReadOnly = true;
            this.txtCmpResult.Size = new System.Drawing.Size(100, 22);
            this.txtCmpResult.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(230, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "Log Dtm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "Cmp Result";
            // 
            // txtSqlQryCmd
            // 
            this.txtSqlQryCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlQryCmd.Location = new System.Drawing.Point(79, 46);
            this.txtSqlQryCmd.Name = "txtSqlQryCmd";
            this.txtSqlQryCmd.ReadOnly = true;
            this.txtSqlQryCmd.Size = new System.Drawing.Size(572, 22);
            this.txtSqlQryCmd.TabIndex = 1;
            // 
            // txtOraQryCmd
            // 
            this.txtOraQryCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOraQryCmd.Location = new System.Drawing.Point(79, 18);
            this.txtOraQryCmd.Name = "txtOraQryCmd";
            this.txtOraQryCmd.ReadOnly = true;
            this.txtOraQryCmd.Size = new System.Drawing.Size(572, 22);
            this.txtOraQryCmd.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sql Qry Cmd";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Ora Qry Cmd";
            // 
            // txtOrcRowCnt
            // 
            this.txtOrcRowCnt.Location = new System.Drawing.Point(299, 150);
            this.txtOrcRowCnt.Name = "txtOrcRowCnt";
            this.txtOrcRowCnt.ReadOnly = true;
            this.txtOrcRowCnt.Size = new System.Drawing.Size(122, 22);
            this.txtOrcRowCnt.TabIndex = 6;
            this.txtOrcRowCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(201, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Oracle Row Count";
            // 
            // txtOrderBy
            // 
            this.txtOrderBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderBy.Location = new System.Drawing.Point(82, 104);
            this.txtOrderBy.Name = "txtOrderBy";
            this.txtOrderBy.Size = new System.Drawing.Size(572, 22);
            this.txtOrderBy.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtOrderBy, "＊support: NVL");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Order By";
            // 
            // txtPrimaryKey
            // 
            this.txtPrimaryKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimaryKey.Location = new System.Drawing.Point(82, 48);
            this.txtPrimaryKey.Name = "txtPrimaryKey";
            this.txtPrimaryKey.ReadOnly = true;
            this.txtPrimaryKey.Size = new System.Drawing.Size(572, 22);
            this.txtPrimaryKey.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Primary Key";
            // 
            // txtWhereCond
            // 
            this.txtWhereCond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWhereCond.Location = new System.Drawing.Point(82, 76);
            this.txtWhereCond.Name = "txtWhereCond";
            this.txtWhereCond.Size = new System.Drawing.Size(572, 22);
            this.txtWhereCond.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Where";
            // 
            // numTopRow
            // 
            this.numTopRow.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTopRow.Location = new System.Drawing.Point(82, 151);
            this.numTopRow.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numTopRow.Name = "numTopRow";
            this.numTopRow.Size = new System.Drawing.Size(100, 22);
            this.numTopRow.TabIndex = 5;
            this.numTopRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTopRow.ThousandsSeparator = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(188, 19);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.ReadOnly = true;
            this.txtTableName.Size = new System.Drawing.Size(211, 22);
            this.txtTableName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Top Rows";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(82, 19);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.ReadOnly = true;
            this.txtDbName.Size = new System.Drawing.Size(100, 22);
            this.txtDbName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "DB.Table";
            // 
            // connInfoTableAdapter
            // 
            this.connInfoTableAdapter.ClearBeforeFill = true;
            // 
            // tabCompareRecordTableAdapter
            // 
            this.tabCompareRecordTableAdapter.ClearBeforeFill = true;
            // 
            // DataStore
            // 
            this.DataStore.DataSetName = "Database1DataSet";
            this.DataStore.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabRowCntTableAdapter
            // 
            this.tabRowCntTableAdapter.ClearBeforeFill = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 427);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form3";
            this.Text = "Form3 -  Compare Fields of One Table.";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataStore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView trvDbTables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrimaryKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWhereCond;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numTopRow;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lsvCompareResult;
        private System.Windows.Forms.ColumnHeader colKeyValue;
        private System.Windows.Forms.ColumnHeader colMsgTag;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter connInfoTableAdapter;
        private System.Windows.Forms.TextBox txtOrderBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOrcRowCnt;
        private System.Windows.Forms.Button btnShowSchema;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSqlQryCmd;
        private System.Windows.Forms.TextBox txtOraQryCmd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCmpResult;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLogDtm;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.TabCompareRecordTableAdapter tabCompareRecordTableAdapter;
        private Database1DataSet DataStore;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSqlRowCnt;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter tabRowCntTableAdapter;
        private System.Windows.Forms.Button btnCompare3;
        private System.Windows.Forms.Button btnCancel3;
        private System.Windows.Forms.Button btnBatchCmp;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label label13;
    }
}