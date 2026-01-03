namespace ApprainERPTerminal.UI
{
    partial class FrmPreference
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
            cmbPOSPrintersList = new ComboBox();
            btnTestPrint = new Button();
            btnItemFull = new Button();
            btnStock = new Button();
            picBoxLogo = new PictureBox();
            txtLogowidth = new TextBox();
            btnSaveLogoSetting = new Button();
            grpInvoiceLogo = new GroupBox();
            label1 = new Label();
            grpSyncSetting = new GroupBox();
            btnCategory = new Button();
            cmbFontSize = new ComboBox();
            cmbFont = new ComboBox();
            label3 = new Label();
            btnCompany = new Button();
            btnUser = new Button();
            btnPaymentMethods = new Button();
            lblPrinter = new Label();
            groupBox1 = new GroupBox();
            comboLineDisplay = new ComboBox();
            groupBox2 = new GroupBox();
            chkEventLog = new CheckBox();
            btnTable = new Button();
            ((System.ComponentModel.ISupportInitialize)picBoxLogo).BeginInit();
            grpInvoiceLogo.SuspendLayout();
            grpSyncSetting.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // cmbPOSPrintersList
            // 
            cmbPOSPrintersList.FormattingEnabled = true;
            cmbPOSPrintersList.Location = new Point(60, 34);
            cmbPOSPrintersList.Name = "cmbPOSPrintersList";
            cmbPOSPrintersList.Size = new Size(145, 23);
            cmbPOSPrintersList.TabIndex = 0;
            cmbPOSPrintersList.SelectedIndexChanged += cmbPOSPrintersList_SelectedIndexChanged;
            // 
            // btnTestPrint
            // 
            btnTestPrint.Location = new Point(383, 67);
            btnTestPrint.Name = "btnTestPrint";
            btnTestPrint.Size = new Size(75, 26);
            btnTestPrint.TabIndex = 1;
            btnTestPrint.Text = "Test Print";
            btnTestPrint.UseVisualStyleBackColor = true;
            btnTestPrint.Click += btnTestPrint_Click;
            // 
            // btnItemFull
            // 
            btnItemFull.Location = new Point(219, 32);
            btnItemFull.Name = "btnItemFull";
            btnItemFull.Size = new Size(75, 26);
            btnItemFull.TabIndex = 2;
            btnItemFull.Text = "Items(Full)";
            btnItemFull.UseVisualStyleBackColor = true;
            btnItemFull.Click += btnItemFull_Click;
            // 
            // btnStock
            // 
            btnStock.Location = new Point(299, 32);
            btnStock.Name = "btnStock";
            btnStock.Size = new Size(75, 26);
            btnStock.TabIndex = 3;
            btnStock.Text = "Stock";
            btnStock.UseVisualStyleBackColor = true;
            btnStock.Click += btnStock_Click;
            // 
            // picBoxLogo
            // 
            picBoxLogo.BackColor = SystemColors.ControlLight;
            picBoxLogo.Location = new Point(25, 32);
            picBoxLogo.Name = "picBoxLogo";
            picBoxLogo.Size = new Size(100, 100);
            picBoxLogo.TabIndex = 0;
            picBoxLogo.TabStop = false;
            picBoxLogo.Click += picBoxLogo_Click;
            // 
            // txtLogowidth
            // 
            txtLogowidth.Location = new Point(328, 51);
            txtLogowidth.Name = "txtLogowidth";
            txtLogowidth.Size = new Size(65, 23);
            txtLogowidth.TabIndex = 4;
            txtLogowidth.Text = "100";
            // 
            // btnSaveLogoSetting
            // 
            btnSaveLogoSetting.Location = new Point(242, 82);
            btnSaveLogoSetting.Name = "btnSaveLogoSetting";
            btnSaveLogoSetting.Size = new Size(154, 27);
            btnSaveLogoSetting.TabIndex = 6;
            btnSaveLogoSetting.Text = "Save";
            btnSaveLogoSetting.UseVisualStyleBackColor = true;
            btnSaveLogoSetting.Click += btnSaveLogoSetting_Click;
            // 
            // grpInvoiceLogo
            // 
            grpInvoiceLogo.Controls.Add(label1);
            grpInvoiceLogo.Controls.Add(picBoxLogo);
            grpInvoiceLogo.Controls.Add(txtLogowidth);
            grpInvoiceLogo.Controls.Add(btnSaveLogoSetting);
            grpInvoiceLogo.Location = new Point(22, 172);
            grpInvoiceLogo.Name = "grpInvoiceLogo";
            grpInvoiceLogo.Size = new Size(464, 152);
            grpInvoiceLogo.TabIndex = 7;
            grpInvoiceLogo.TabStop = false;
            grpInvoiceLogo.Text = "Invoice Logo Setting";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(246, 57);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 7;
            label1.Text = "Logo Width";
            // 
            // grpSyncSetting
            // 
            grpSyncSetting.Controls.Add(btnTable);
            grpSyncSetting.Controls.Add(btnCategory);
            grpSyncSetting.Controls.Add(cmbFontSize);
            grpSyncSetting.Controls.Add(cmbFont);
            grpSyncSetting.Controls.Add(label3);
            grpSyncSetting.Controls.Add(btnCompany);
            grpSyncSetting.Controls.Add(btnUser);
            grpSyncSetting.Controls.Add(btnPaymentMethods);
            grpSyncSetting.Controls.Add(lblPrinter);
            grpSyncSetting.Controls.Add(btnItemFull);
            grpSyncSetting.Controls.Add(btnStock);
            grpSyncSetting.Controls.Add(cmbPOSPrintersList);
            grpSyncSetting.Controls.Add(btnTestPrint);
            grpSyncSetting.Location = new Point(22, 12);
            grpSyncSetting.Name = "grpSyncSetting";
            grpSyncSetting.Size = new Size(464, 146);
            grpSyncSetting.TabIndex = 8;
            grpSyncSetting.TabStop = false;
            grpSyncSetting.Text = "Data Sync";
            // 
            // btnCategory
            // 
            btnCategory.Location = new Point(219, 99);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(75, 26);
            btnCategory.TabIndex = 11;
            btnCategory.Text = "Categoreis";
            btnCategory.UseVisualStyleBackColor = true;
            btnCategory.Click += btnCategory_Click;
            // 
            // cmbFontSize
            // 
            cmbFontSize.FormattingEnabled = true;
            cmbFontSize.Items.AddRange(new object[] { "6", "7", "8", "9", "10" });
            cmbFontSize.Location = new Point(164, 64);
            cmbFontSize.Margin = new Padding(3, 2, 3, 2);
            cmbFontSize.Name = "cmbFontSize";
            cmbFontSize.Size = new Size(41, 23);
            cmbFontSize.TabIndex = 10;
            cmbFontSize.SelectedIndexChanged += cmbPrintForm_SelectedIndexChanged;
            // 
            // cmbFont
            // 
            cmbFont.FormattingEnabled = true;
            cmbFont.Items.AddRange(new object[] { "Courier New", "Cascadia Code", "Consolas" });
            cmbFont.Location = new Point(60, 64);
            cmbFont.Name = "cmbFont";
            cmbFont.Size = new Size(99, 23);
            cmbFont.TabIndex = 9;
            cmbFont.SelectedIndexChanged += cmbFont_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 67);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Font";
            // 
            // btnCompany
            // 
            btnCompany.Location = new Point(302, 67);
            btnCompany.Margin = new Padding(3, 2, 3, 2);
            btnCompany.Name = "btnCompany";
            btnCompany.Size = new Size(73, 26);
            btnCompany.TabIndex = 7;
            btnCompany.Text = "Company";
            btnCompany.UseVisualStyleBackColor = true;
            btnCompany.Click += btnCompany_Click;
            // 
            // btnUser
            // 
            btnUser.Location = new Point(383, 32);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(75, 26);
            btnUser.TabIndex = 6;
            btnUser.Text = "Users";
            btnUser.UseVisualStyleBackColor = true;
            btnUser.Click += btnUser_Click;
            // 
            // btnPaymentMethods
            // 
            btnPaymentMethods.Location = new Point(219, 67);
            btnPaymentMethods.Name = "btnPaymentMethods";
            btnPaymentMethods.Size = new Size(75, 26);
            btnPaymentMethods.TabIndex = 5;
            btnPaymentMethods.Text = "Payment Methods";
            btnPaymentMethods.UseVisualStyleBackColor = true;
            btnPaymentMethods.Click += btnPaymentMethods_Click;
            // 
            // lblPrinter
            // 
            lblPrinter.AutoSize = true;
            lblPrinter.Location = new Point(9, 38);
            lblPrinter.Name = "lblPrinter";
            lblPrinter.Size = new Size(42, 15);
            lblPrinter.TabIndex = 4;
            lblPrinter.Text = "Printer";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboLineDisplay);
            groupBox1.Location = new Point(10, 350);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(248, 94);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Line Display";
            // 
            // comboLineDisplay
            // 
            comboLineDisplay.FormattingEnabled = true;
            comboLineDisplay.Location = new Point(21, 35);
            comboLineDisplay.Margin = new Padding(3, 2, 3, 2);
            comboLineDisplay.Name = "comboLineDisplay";
            comboLineDisplay.Size = new Size(208, 23);
            comboLineDisplay.TabIndex = 0;
            comboLineDisplay.SelectedIndexChanged += comboLineDisplay_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkEventLog);
            groupBox2.Location = new Point(268, 350);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(218, 94);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Preferences";
            // 
            // chkEventLog
            // 
            chkEventLog.AutoSize = true;
            chkEventLog.Location = new Point(5, 21);
            chkEventLog.Name = "chkEventLog";
            chkEventLog.Size = new Size(113, 19);
            chkEventLog.TabIndex = 1;
            chkEventLog.Text = "Enable event log";
            chkEventLog.UseVisualStyleBackColor = true;
            chkEventLog.CheckedChanged += chkEventLog_CheckedChanged;
            // 
            // btnTable
            // 
            btnTable.Location = new Point(300, 101);
            btnTable.Name = "btnTable";
            btnTable.Size = new Size(75, 23);
            btnTable.TabIndex = 12;
            btnTable.Text = "Table";
            btnTable.UseVisualStyleBackColor = true;
            btnTable.Click += btnTable_Click;
            // 
            // FrmPreference
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 464);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(grpSyncSetting);
            Controls.Add(grpInvoiceLogo);
            Name = "FrmPreference";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Preference";
            Load += FrmPreference_Load;
            ((System.ComponentModel.ISupportInitialize)picBoxLogo).EndInit();
            grpInvoiceLogo.ResumeLayout(false);
            grpInvoiceLogo.PerformLayout();
            grpSyncSetting.ResumeLayout(false);
            grpSyncSetting.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbPOSPrintersList;
        private Button btnTestPrint;
        private Button btnItemFull;
        private Button btnStock;
        private PictureBox picBoxLogo;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private GroupBox grpInvoiceLogo;
        private GroupBox grpSyncSetting;
        private Label lblPrinter;
        private Label label2;
        private Label label1;
        private TextBox txtLogowidth;
        private TextBox txtLogoTopOffset;
        private Button btnSaveLogoSetting;
        private TextBox txtLogoLeftMargin;
        private Button btnPaymentMethods;
        private Button btnUser;
        private Button btnCompany;
        private GroupBox groupBox1;
        private ComboBox comboLineDisplay;
        private GroupBox groupBox2;
        private CheckBox chkEventLog;
        private ComboBox cmbFont;
        private Label label3;
        private ComboBox cmbFontSize;
        private Button btnCategory;
        private Button btnTable;
    }
}