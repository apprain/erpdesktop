namespace ApprainERPTerminal.UI
{
    partial class FrmMDI
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            pOSToolStripMenuItem = new ToolStripMenuItem();
            tablesToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            workPeriodToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            paymentMethodsToolStripMenuItem = new ToolStripMenuItem();
            manageProductsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            qTPrinterToolStripMenuItem = new ToolStripMenuItem();
            manageOrderToolStripMenuItem = new ToolStripMenuItem();
            cashCounterCalculatorToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            accountToolStripMenuItem = new ToolStripMenuItem();
            voucherToolStripMenuItem = new ToolStripMenuItem();
            syncVoucherToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, accountToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pOSToolStripMenuItem, tablesToolStripMenuItem, logoutToolStripMenuItem, workPeriodToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // pOSToolStripMenuItem
            // 
            pOSToolStripMenuItem.Name = "pOSToolStripMenuItem";
            pOSToolStripMenuItem.Size = new Size(172, 26);
            pOSToolStripMenuItem.Text = "POS";
            pOSToolStripMenuItem.Click += pOSToolStripMenuItem_Click;
            // 
            // tablesToolStripMenuItem
            // 
            tablesToolStripMenuItem.Name = "tablesToolStripMenuItem";
            tablesToolStripMenuItem.Size = new Size(172, 26);
            tablesToolStripMenuItem.Text = "Tables";
            tablesToolStripMenuItem.Click += tablesToolStripMenuItem_Click;
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(172, 26);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // workPeriodToolStripMenuItem
            // 
            workPeriodToolStripMenuItem.Name = "workPeriodToolStripMenuItem";
            workPeriodToolStripMenuItem.Size = new Size(172, 26);
            workPeriodToolStripMenuItem.Text = "Work Period";
            workPeriodToolStripMenuItem.Click += workPeriodToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(172, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { preferencesToolStripMenuItem, paymentMethodsToolStripMenuItem, manageProductsToolStripMenuItem, toolStripMenuItem1, qTPrinterToolStripMenuItem, manageOrderToolStripMenuItem, cashCounterCalculatorToolStripMenuItem, reportsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(58, 24);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(210, 26);
            preferencesToolStripMenuItem.Text = "Preferences";
            preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
            // 
            // paymentMethodsToolStripMenuItem
            // 
            paymentMethodsToolStripMenuItem.Name = "paymentMethodsToolStripMenuItem";
            paymentMethodsToolStripMenuItem.Size = new Size(210, 26);
            paymentMethodsToolStripMenuItem.Text = "Payment Methods";
            paymentMethodsToolStripMenuItem.Click += paymentMethodsToolStripMenuItem_Click;
            // 
            // manageProductsToolStripMenuItem
            // 
            manageProductsToolStripMenuItem.Name = "manageProductsToolStripMenuItem";
            manageProductsToolStripMenuItem.Size = new Size(210, 26);
            manageProductsToolStripMenuItem.Text = "Manage Products";
            manageProductsToolStripMenuItem.Click += manageProductsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(210, 26);
            toolStripMenuItem1.Text = "Manage Orders";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // qTPrinterToolStripMenuItem
            // 
            qTPrinterToolStripMenuItem.Name = "qTPrinterToolStripMenuItem";
            qTPrinterToolStripMenuItem.Size = new Size(210, 26);
            qTPrinterToolStripMenuItem.Text = "KOT Printer Setup";
            qTPrinterToolStripMenuItem.Click += qTPrinterToolStripMenuItem_Click;
            // 
            // manageOrderToolStripMenuItem
            // 
            manageOrderToolStripMenuItem.Name = "manageOrderToolStripMenuItem";
            manageOrderToolStripMenuItem.Size = new Size(210, 26);
            manageOrderToolStripMenuItem.Text = "Manage Order";
            // 
            // cashCounterCalculatorToolStripMenuItem
            // 
            cashCounterCalculatorToolStripMenuItem.Name = "cashCounterCalculatorToolStripMenuItem";
            cashCounterCalculatorToolStripMenuItem.Size = new Size(210, 26);
            cashCounterCalculatorToolStripMenuItem.Text = "Cash Calculator";
            cashCounterCalculatorToolStripMenuItem.Click += cashCounterCalculatorToolStripMenuItem_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(210, 26);
            reportsToolStripMenuItem.Text = "Reports";
            reportsToolStripMenuItem.Click += reportsToolStripMenuItem_Click;
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { voucherToolStripMenuItem, syncVoucherToolStripMenuItem });
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(77, 24);
            accountToolStripMenuItem.Text = "Account";
            // 
            // voucherToolStripMenuItem
            // 
            voucherToolStripMenuItem.Name = "voucherToolStripMenuItem";
            voucherToolStripMenuItem.Size = new Size(224, 26);
            voucherToolStripMenuItem.Text = "Create Voucher ";
            voucherToolStripMenuItem.Click += voucherToolStripMenuItem_Click;
            // 
            // syncVoucherToolStripMenuItem
            // 
            syncVoucherToolStripMenuItem.Name = "syncVoucherToolStripMenuItem";
            syncVoucherToolStripMenuItem.Size = new Size(224, 26);
            syncVoucherToolStripMenuItem.Text = "Manage Vouchers";
            syncVoucherToolStripMenuItem.Click += syncVoucherToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(64, 24);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // FrmMDI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmMDI";
            Text = "Apprain ERP";
            WindowState = FormWindowState.Maximized;
            Load += FrmMDI_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem pOSToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem productsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private ToolStripMenuItem paymentMethodsToolStripMenuItem;
        private ToolStripMenuItem qTPrinterToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem manageProductsToolStripMenuItem;
        private ToolStripMenuItem tablesToolStripMenuItem;
        private ToolStripMenuItem workPeriodToolStripMenuItem;
        private ToolStripMenuItem cashCounterCalculatorToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem manageOrderToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem voucherToolStripMenuItem;
        private ToolStripMenuItem syncVoucherToolStripMenuItem;
    }
}