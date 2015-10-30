using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNOraToSqlValidator2
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // if exist then show it.
            Form frm = this.MdiChildren.FirstOrDefault(c => c is Form1);
            if (frm != null)
            {
                if (frm.WindowState == FormWindowState.Minimized)
                    frm.WindowState = FormWindowState.Maximized;

                frm.Show();
                frm.Focus();
                return;
            }

            // new form
            frm = new Form1();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // if exist then show it.
            Form frm = this.MdiChildren.FirstOrDefault(c => c is Form2);
            if (frm != null)
            {
                if (frm.WindowState == FormWindowState.Minimized)
                    frm.WindowState = FormWindowState.Maximized;

                frm.Show();
                frm.Focus();
                return;
            }

            // new form
            frm = new Form2();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // if exist then show it.
            Form frm = this.MdiChildren.FirstOrDefault(c => c is Form3);
            if (frm != null)
            {
                if (frm.WindowState == FormWindowState.Minimized)
                    frm.WindowState = FormWindowState.Maximized;

                frm.Show();
                frm.Focus();
                return;
            }

            // new form
            frm = new Form3();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(); 
        }

        private void hardCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if exist then show it.
            Form frm = this.MdiChildren.FirstOrDefault(c => c is Form4);
            if (frm != null)
            {
                if (frm.WindowState == FormWindowState.Minimized)
                    frm.WindowState = FormWindowState.Maximized;

                frm.Show();
                frm.Focus();
                return;
            }

            // new form
            frm = new Form4();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(); 
        }
    }
}
