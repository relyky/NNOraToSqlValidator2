namespace NNOraToSqlValidator2
{
    partial class Form4
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader3;
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AAAMSEC");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("AAAMSEC_FUND");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("AAAMPOSTMAN");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("AAAM1TO1");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("BENF");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("EVBENF");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("IVRBENF");
            this.lsvMessage = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.btnHardCompare = new System.Windows.Forms.Button();
            this.txtSqlLastChkCmd = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvDbTables = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOraQryCmd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogDtm = new System.Windows.Forms.TextBox();
            this.txtCmpResult = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtWhereCond = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSqlRowCnt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOrcRowCnt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.connInfoTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter();
            this.tabRowCntTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter();
            this.tabHardCmpRecordTableAdapter = new NNOraToSqlValidator2.Database1DataSetTableAdapters.TabHardCmpRecordTableAdapter();
            this.DataStore = new NNOraToSqlValidator2.Database1DataSet();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataStore)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tag";
            columnHeader1.Width = 136;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Key Message";
            columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Description";
            columnHeader3.Width = 520;
            // 
            // lsvMessage
            // 
            this.lsvMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3});
            this.lsvMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvMessage.FullRowSelect = true;
            this.lsvMessage.Location = new System.Drawing.Point(0, 309);
            this.lsvMessage.Name = "lsvMessage";
            this.lsvMessage.Size = new System.Drawing.Size(657, 240);
            this.lsvMessage.TabIndex = 3;
            this.lsvMessage.UseCompatibleStateImageBehavior = false;
            this.lsvMessage.View = System.Windows.Forms.View.Details;
            this.lsvMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsvMessage_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DB.Table";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(70, 21);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.ReadOnly = true;
            this.txtDbName.Size = new System.Drawing.Size(122, 22);
            this.txtDbName.TabIndex = 0;
            this.txtDbName.Text = "AAAMSEC";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(198, 21);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.ReadOnly = true;
            this.txtTableName.Size = new System.Drawing.Size(280, 22);
            this.txtTableName.TabIndex = 1;
            this.txtTableName.Text = "CM_PARAMETER";
            // 
            // btnHardCompare
            // 
            this.btnHardCompare.Location = new System.Drawing.Point(70, 123);
            this.btnHardCompare.Name = "btnHardCompare";
            this.btnHardCompare.Size = new System.Drawing.Size(120, 23);
            this.btnHardCompare.TabIndex = 2;
            this.btnHardCompare.Text = "Hard Compare";
            this.btnHardCompare.UseVisualStyleBackColor = true;
            this.btnHardCompare.Click += new System.EventHandler(this.btnHardCompare_Click);
            // 
            // txtSqlLastChkCmd
            // 
            this.txtSqlLastChkCmd.BackColor = System.Drawing.SystemColors.Info;
            this.txtSqlLastChkCmd.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSqlLastChkCmd.Location = new System.Drawing.Point(0, 236);
            this.txtSqlLastChkCmd.Multiline = true;
            this.txtSqlLastChkCmd.Name = "txtSqlLastChkCmd";
            this.txtSqlLastChkCmd.ReadOnly = true;
            this.txtSqlLastChkCmd.Size = new System.Drawing.Size(657, 73);
            this.txtSqlLastChkCmd.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvDbTables);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lsvMessage);
            this.splitContainer1.Panel2.Controls.Add(this.txtSqlLastChkCmd);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(818, 549);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 5;
            // 
            // trvDbTables
            // 
            this.trvDbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDbTables.Location = new System.Drawing.Point(0, 13);
            this.trvDbTables.Name = "trvDbTables";
            treeNode1.Name = "Node0";
            treeNode1.Text = "AAAMSEC";
            treeNode2.Name = "Node1";
            treeNode2.Text = "AAAMSEC_FUND";
            treeNode3.Name = "Node2";
            treeNode3.Text = "AAAMPOSTMAN";
            treeNode4.Name = "Node3";
            treeNode4.Text = "AAAM1TO1";
            treeNode5.Name = "Node4";
            treeNode5.Text = "BENF";
            treeNode6.Name = "Node5";
            treeNode6.Text = "EVBENF";
            treeNode7.Name = "Node6";
            treeNode7.Text = "IVRBENF";
            this.trvDbTables.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            this.trvDbTables.Size = new System.Drawing.Size(157, 536);
            this.trvDbTables.TabIndex = 1;
            this.trvDbTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDbTables_AfterSelect);
            this.trvDbTables.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trvDbTables_KeyUp);
            this.trvDbTables.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvDbTables_BeforeSelect);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "DB-Table";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOraQryCmd);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtLogDtm);
            this.groupBox2.Controls.Add(this.txtCmpResult);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(657, 79);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Last Compare Status";
            // 
            // txtOraQryCmd
            // 
            this.txtOraQryCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOraQryCmd.Location = new System.Drawing.Point(85, 21);
            this.txtOraQryCmd.Name = "txtOraQryCmd";
            this.txtOraQryCmd.ReadOnly = true;
            this.txtOraQryCmd.Size = new System.Drawing.Size(560, 22);
            this.txtOraQryCmd.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "Ora Qry Cmd";
            // 
            // txtLogDtm
            // 
            this.txtLogDtm.Location = new System.Drawing.Point(259, 49);
            this.txtLogDtm.Name = "txtLogDtm";
            this.txtLogDtm.ReadOnly = true;
            this.txtLogDtm.Size = new System.Drawing.Size(122, 22);
            this.txtLogDtm.TabIndex = 22;
            // 
            // txtCmpResult
            // 
            this.txtCmpResult.Location = new System.Drawing.Point(85, 49);
            this.txtCmpResult.Name = "txtCmpResult";
            this.txtCmpResult.ReadOnly = true;
            this.txtCmpResult.Size = new System.Drawing.Size(115, 22);
            this.txtCmpResult.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "Log Dtm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "Cmp Result";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtWhereCond);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSqlRowCnt);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtOrcRowCnt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnHardCompare);
            this.groupBox1.Controls.Add(this.txtDbName);
            this.groupBox1.Controls.Add(this.txtTableName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(657, 157);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table Filed Compare Setting";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(68, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "* Oracle PL/SQL syntax.";
            // 
            // txtWhereCond
            // 
            this.txtWhereCond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWhereCond.Location = new System.Drawing.Point(70, 49);
            this.txtWhereCond.Name = "txtWhereCond";
            this.txtWhereCond.Size = new System.Drawing.Size(577, 22);
            this.txtWhereCond.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "Where";
            // 
            // txtSqlRowCnt
            // 
            this.txtSqlRowCnt.Location = new System.Drawing.Point(322, 95);
            this.txtSqlRowCnt.Name = "txtSqlRowCnt";
            this.txtSqlRowCnt.ReadOnly = true;
            this.txtSqlRowCnt.Size = new System.Drawing.Size(122, 22);
            this.txtSqlRowCnt.TabIndex = 19;
            this.txtSqlRowCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(239, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "Sql Row Count";
            // 
            // txtOrcRowCnt
            // 
            this.txtOrcRowCnt.Location = new System.Drawing.Point(111, 95);
            this.txtOrcRowCnt.Name = "txtOrcRowCnt";
            this.txtOrcRowCnt.ReadOnly = true;
            this.txtOrcRowCnt.Size = new System.Drawing.Size(122, 22);
            this.txtOrcRowCnt.TabIndex = 18;
            this.txtOrcRowCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "Oracle Row Count";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 549);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(818, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(701, 17);
            this.StatusLabel1.Spring = true;
            this.StatusLabel1.Text = "StatusLabel1";
            // 
            // StatusProgressBar1
            // 
            this.StatusProgressBar1.Name = "StatusProgressBar1";
            this.StatusProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // connInfoTableAdapter
            // 
            this.connInfoTableAdapter.ClearBeforeFill = true;
            // 
            // tabRowCntTableAdapter
            // 
            this.tabRowCntTableAdapter.ClearBeforeFill = true;
            // 
            // tabHardCmpRecordTableAdapter
            // 
            this.tabHardCmpRecordTableAdapter.ClearBeforeFill = true;
            // 
            // DataStore
            // 
            this.DataStore.DataSetName = "Database1DataSet";
            this.DataStore.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataStore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button btnHardCompare;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.ConnInfoTableAdapter connInfoTableAdapter;
        private System.Windows.Forms.TextBox txtSqlLastChkCmd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView trvDbTables;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtSqlRowCnt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOrcRowCnt;
        private System.Windows.Forms.Label label6;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.TabRowCntTableAdapter tabRowCntTableAdapter;
        private System.Windows.Forms.TextBox txtWhereCond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLogDtm;
        private System.Windows.Forms.TextBox txtCmpResult;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOraQryCmd;
        private System.Windows.Forms.Label label7;
        private NNOraToSqlValidator2.Database1DataSetTableAdapters.TabHardCmpRecordTableAdapter tabHardCmpRecordTableAdapter;
        private Database1DataSet DataStore;
    }
}