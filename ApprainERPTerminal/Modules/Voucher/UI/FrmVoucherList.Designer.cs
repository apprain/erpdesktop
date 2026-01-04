namespace ApprainERPTerminal.Modules.Voucher.UI
{
    partial class FrmVoucherList
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
            cboStatusFilter = new ComboBox();
            label2 = new Label();
            dgvVouchers = new DataGridView();
            btnSyncSelected = new Button();
            btnSyncAll = new Button();
            btnClose = new Button();
            btnPrint = new Button();
            btnPdf = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVouchers).BeginInit();
            SuspendLayout();
            // 
            // cboStatusFilter
            // 
            cboStatusFilter.FormattingEnabled = true;
            cboStatusFilter.Items.AddRange(new object[] { "All", "PENDING", " SYNCED,", "FAILED" });
            cboStatusFilter.Location = new Point(124, 18);
            cboStatusFilter.Name = "cboStatusFilter";
            cboStatusFilter.Size = new Size(180, 28);
            cboStatusFilter.TabIndex = 1;
            cboStatusFilter.SelectedIndexChanged += cboStatusFilter_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 21);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 2;
            label2.Text = "Status Filter:";
            // 
            // dgvVouchers
            // 
            dgvVouchers.ColumnHeadersHeight = 29;
            dgvVouchers.Location = new Point(12, 58);
            dgvVouchers.Name = "dgvVouchers";
            dgvVouchers.RowHeadersWidth = 51;
            dgvVouchers.Size = new Size(1199, 611);
            dgvVouchers.TabIndex = 7;
            dgvVouchers.CellContentClick += dgvVouchers_CellContentClick;
            dgvVouchers.RowPrePaint += dgvVouchers_RowPrePaint_1;
            // 
            // btnSyncSelected
            // 
            btnSyncSelected.Location = new Point(393, 700);
            btnSyncSelected.Name = "btnSyncSelected";
            btnSyncSelected.Size = new Size(130, 39);
            btnSyncSelected.TabIndex = 4;
            btnSyncSelected.Text = "Sync Selected";
            btnSyncSelected.UseVisualStyleBackColor = true;
            btnSyncSelected.Click += btnSyncSelected_Click;
            // 
            // btnSyncAll
            // 
            btnSyncAll.Location = new Point(535, 700);
            btnSyncAll.Name = "btnSyncAll";
            btnSyncAll.Size = new Size(124, 39);
            btnSyncAll.TabIndex = 5;
            btnSyncAll.Text = "Sync All";
            btnSyncAll.UseVisualStyleBackColor = true;
            btnSyncAll.Click += btnSyncAll_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(669, 698);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(116, 41);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(889, 700);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(94, 39);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnPdf
            // 
            btnPdf.Location = new Point(995, 703);
            btnPdf.Name = "btnPdf";
            btnPdf.Size = new Size(133, 36);
            btnPdf.TabIndex = 9;
            btnPdf.Text = "Print PDF";
            btnPdf.UseVisualStyleBackColor = true;
            btnPdf.Click += btnPdf_Click;
            // 
            // FrmVoucherList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 760);
            Controls.Add(btnPdf);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(btnSyncAll);
            Controls.Add(btnSyncSelected);
            Controls.Add(dgvVouchers);
            Controls.Add(label2);
            Controls.Add(cboStatusFilter);
            Name = "FrmVoucherList";
            Text = "FrmVoucherList";
            Load += FrmVoucherList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVouchers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cboStatusFilter;
        private Label label2;
        private DataGridView dgvVouchers;
        private Button btnSyncSelected;
        private Button btnSyncAll;
        private Button btnClose;
        private Button btnPrint;
        private Button btnPdf;
    }
}