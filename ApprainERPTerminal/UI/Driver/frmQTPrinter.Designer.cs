namespace ApprainERPTerminal.UI.Driver
{
    partial class frmQTPrinter
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
            dataGridQTPrinterSetup = new DataGridView();
            btnSave = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            KOTPrinterToolbar = new Panel();
            txtNoOfKOTCopy = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridQTPrinterSetup).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            KOTPrinterToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridQTPrinterSetup
            // 
            dataGridQTPrinterSetup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridQTPrinterSetup.Dock = DockStyle.Fill;
            dataGridQTPrinterSetup.Location = new Point(3, 52);
            dataGridQTPrinterSetup.Margin = new Padding(3, 2, 3, 2);
            dataGridQTPrinterSetup.Name = "dataGridQTPrinterSetup";
            dataGridQTPrinterSetup.RowHeadersVisible = false;
            dataGridQTPrinterSetup.RowHeadersWidth = 51;
            dataGridQTPrinterSetup.RowTemplate.Height = 29;
            dataGridQTPrinterSetup.Size = new Size(999, 404);
            dataGridQTPrinterSetup.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 37);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(KOTPrinterToolbar, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridQTPrinterSetup, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1005, 458);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // KOTPrinterToolbar
            // 
            KOTPrinterToolbar.Controls.Add(txtNoOfKOTCopy);
            KOTPrinterToolbar.Controls.Add(label1);
            KOTPrinterToolbar.Controls.Add(btnSave);
            KOTPrinterToolbar.Dock = DockStyle.Fill;
            KOTPrinterToolbar.Location = new Point(3, 3);
            KOTPrinterToolbar.Name = "KOTPrinterToolbar";
            KOTPrinterToolbar.Size = new Size(999, 44);
            KOTPrinterToolbar.TabIndex = 3;
            // 
            // txtNoOfKOTCopy
            // 
            txtNoOfKOTCopy.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtNoOfKOTCopy.Location = new Point(315, 11);
            txtNoOfKOTCopy.Name = "txtNoOfKOTCopy";
            txtNoOfKOTCopy.Size = new Size(80, 27);
            txtNoOfKOTCopy.TabIndex = 5;
            txtNoOfKOTCopy.Text = "1";
            txtNoOfKOTCopy.TextAlign = HorizontalAlignment.Center;
            txtNoOfKOTCopy.TextChanged += txtNoOfKOTCopy_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(183, 15);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 4;
            label1.Text = "Number of KOT Copy";
            // 
            // frmQTPrinter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 458);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmQTPrinter";
            Text = "KOT Printer Setup";
            Load += frmQTPrinter_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridQTPrinterSetup).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            KOTPrinterToolbar.ResumeLayout(false);
            KOTPrinterToolbar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridQTPrinterSetup;
        private Button btnSave;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel KOTPrinterToolbar;
        private TextBox txtNoOfKOTCopy;
        private Label label1;
    }
}