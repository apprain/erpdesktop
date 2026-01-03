namespace ApprainERPTerminal.UI.POS
{
    partial class FrmTable
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
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnTableBusy = new Button();
            btnAllTables = new Button();
            btnTableBilled = new Button();
            btnMerged = new Button();
            btnPrintKOT = new Button();
            pnlTableList = new Panel();
            menuStrip1 = new MenuStrip();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            manageProductsToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlTableList);
            splitContainer1.Size = new Size(1295, 675);
            splitContainer1.SplitterDistance = 181;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnTableBusy, 0, 1);
            tableLayoutPanel1.Controls.Add(btnAllTables, 0, 0);
            tableLayoutPanel1.Controls.Add(btnTableBilled, 0, 2);
            tableLayoutPanel1.Controls.Add(btnMerged, 0, 3);
            tableLayoutPanel1.Controls.Add(btnPrintKOT, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 420F));
            tableLayoutPanel1.Size = new Size(181, 675);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnTableBusy
            // 
            btnTableBusy.Dock = DockStyle.Fill;
            btnTableBusy.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnTableBusy.Location = new Point(3, 54);
            btnTableBusy.Name = "btnTableBusy";
            btnTableBusy.Size = new Size(175, 45);
            btnTableBusy.TabIndex = 1;
            btnTableBusy.Text = "Dining";
            btnTableBusy.UseVisualStyleBackColor = true;
            btnTableBusy.Click += btnTableBusy_Click;
            // 
            // btnAllTables
            // 
            btnAllTables.Dock = DockStyle.Fill;
            btnAllTables.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnAllTables.Location = new Point(3, 3);
            btnAllTables.Name = "btnAllTables";
            btnAllTables.Size = new Size(175, 45);
            btnAllTables.TabIndex = 0;
            btnAllTables.Text = "All Tables";
            btnAllTables.UseVisualStyleBackColor = true;
            btnAllTables.Click += btnAllTables_Click;
            // 
            // btnTableBilled
            // 
            btnTableBilled.Dock = DockStyle.Fill;
            btnTableBilled.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnTableBilled.Location = new Point(3, 105);
            btnTableBilled.Name = "btnTableBilled";
            btnTableBilled.Size = new Size(175, 45);
            btnTableBilled.TabIndex = 2;
            btnTableBilled.Text = "Billed";
            btnTableBilled.UseVisualStyleBackColor = true;
            btnTableBilled.Click += btnTableBilled_Click;
            // 
            // btnMerged
            // 
            btnMerged.Dock = DockStyle.Fill;
            btnMerged.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnMerged.Location = new Point(3, 156);
            btnMerged.Name = "btnMerged";
            btnMerged.Size = new Size(175, 45);
            btnMerged.TabIndex = 3;
            btnMerged.Text = "Merge";
            btnMerged.UseVisualStyleBackColor = true;
            btnMerged.Click += btnMerged_Click;
            // 
            // btnPrintKOT
            // 
            btnPrintKOT.Dock = DockStyle.Fill;
            btnPrintKOT.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrintKOT.Location = new Point(3, 207);
            btnPrintKOT.Name = "btnPrintKOT";
            btnPrintKOT.Size = new Size(175, 45);
            btnPrintKOT.TabIndex = 4;
            btnPrintKOT.Text = "Print KOT";
            btnPrintKOT.UseVisualStyleBackColor = true;
            btnPrintKOT.Click += btnPrintKOT_Click;
            // 
            // pnlTableList
            // 
            pnlTableList.BackColor = Color.White;
            pnlTableList.BorderStyle = BorderStyle.Fixed3D;
            pnlTableList.Dock = DockStyle.Fill;
            pnlTableList.Location = new Point(0, 0);
            pnlTableList.Name = "pnlTableList";
            pnlTableList.Size = new Size(1110, 675);
            pnlTableList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1295, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { preferencesToolStripMenuItem, manageProductsToolStripMenuItem, reportsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // manageProductsToolStripMenuItem
            // 
            manageProductsToolStripMenuItem.Name = "manageProductsToolStripMenuItem";
            manageProductsToolStripMenuItem.Size = new Size(167, 22);
            manageProductsToolStripMenuItem.Text = "Manage Products";
            manageProductsToolStripMenuItem.Click += manageProductsToolStripMenuItem_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(167, 22);
            reportsToolStripMenuItem.Text = "Reports";
            reportsToolStripMenuItem.Click += reportsToolStripMenuItem_Click;
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(167, 22);
            preferencesToolStripMenuItem.Text = "Preferences";
            preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
            // 
            // FrmTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1295, 699);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmTable";
            Text = "FrmTable";
            WindowState = FormWindowState.Maximized;
            Load += FrmTable_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnTableBusy;
        private Button btnAllTables;
        private Button btnTableBilled;
        private Panel pnlTableList;
        private Button btnMerged;
        private Button btnPrintKOT;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem manageProductsToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem;
    }
}