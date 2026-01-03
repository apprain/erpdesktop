namespace ApprainERPTerminal.UI.Report
{
    partial class FrmSales
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
            reportResult = new TextBox();
            fromDate = new DateTimePicker();
            dataGridInvoice = new DataGridView();
            C1 = new DataGridViewTextBoxColumn();
            C2 = new DataGridViewTextBoxColumn();
            C3 = new DataGridViewTextBoxColumn();
            C4 = new DataGridViewTextBoxColumn();
            C5 = new DataGridViewTextBoxColumn();
            Discount = new DataGridViewTextBoxColumn();
            C6 = new DataGridViewTextBoxColumn();
            C7 = new DataGridViewTextBoxColumn();
            C8 = new DataGridViewTextBoxColumn();
            Operator = new DataGridViewTextBoxColumn();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            btnAuditLog = new Button();
            btnReload = new Button();
            btnClearSales = new Button();
            btnPrintSummary = new Button();
            id = new DataGridViewTextBoxColumn();
            fkey = new DataGridViewTextBoxColumn();
            onlineid = new DataGridViewTextBoxColumn();
            terminal = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoice).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // reportResult
            // 
            reportResult.Dock = DockStyle.Fill;
            reportResult.Font = new Font("Courier", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            reportResult.Location = new Point(3, 360);
            reportResult.Multiline = true;
            reportResult.Name = "reportResult";
            reportResult.Size = new Size(1592, 301);
            reportResult.TabIndex = 0;
            // 
            // fromDate
            // 
            fromDate.CustomFormat = "yyyy-MM-dd";
            fromDate.Format = DateTimePickerFormat.Custom;
            fromDate.Location = new Point(82, 11);
            fromDate.Name = "fromDate";
            fromDate.Size = new Size(107, 23);
            fromDate.TabIndex = 1;
            fromDate.ValueChanged += fromDate_ValueChanged;
            // 
            // dataGridInvoice
            // 
            dataGridInvoice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInvoice.Columns.AddRange(new DataGridViewColumn[] { C1, C2, C3, C4, C5, Discount, C6, C7, C8, Operator, Status });
            dataGridInvoice.Dock = DockStyle.Fill;
            dataGridInvoice.Location = new Point(3, 53);
            dataGridInvoice.Name = "dataGridInvoice";
            dataGridInvoice.RowTemplate.Height = 25;
            dataGridInvoice.Size = new Size(1592, 301);
            dataGridInvoice.TabIndex = 2;
            // 
            // C1
            // 
            C1.HeaderText = "ID";
            C1.Name = "C1";
            C1.ReadOnly = true;
            // 
            // C2
            // 
            C2.HeaderText = "F Key";
            C2.Name = "C2";
            C2.ReadOnly = true;
            C2.Width = 200;
            // 
            // C3
            // 
            C3.HeaderText = "Invoice Id";
            C3.Name = "C3";
            C3.ReadOnly = true;
            // 
            // C4
            // 
            C4.HeaderText = "Terminal";
            C4.Name = "C4";
            C4.ReadOnly = true;
            // 
            // C5
            // 
            C5.HeaderText = "Total";
            C5.Name = "C5";
            C5.ReadOnly = true;
            // 
            // Discount
            // 
            Discount.HeaderText = "Discount";
            Discount.Name = "Discount";
            // 
            // C6
            // 
            C6.HeaderText = "Cash";
            C6.Name = "C6";
            C6.ReadOnly = true;
            // 
            // C7
            // 
            C7.HeaderText = "Bank";
            C7.Name = "C7";
            C7.ReadOnly = true;
            // 
            // C8
            // 
            C8.HeaderText = "Date";
            C8.Name = "C8";
            C8.ReadOnly = true;
            C8.Width = 150;
            // 
            // Operator
            // 
            Operator.HeaderText = "Operator";
            Operator.Name = "Operator";
            Operator.ReadOnly = true;
            Operator.Width = 200;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 17);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 3;
            label1.Text = "Date";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(reportResult, 0, 2);
            tableLayoutPanel1.Controls.Add(dataGridInvoice, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1598, 664);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAuditLog);
            panel1.Controls.Add(btnReload);
            panel1.Controls.Add(btnClearSales);
            panel1.Controls.Add(btnPrintSummary);
            panel1.Controls.Add(fromDate);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1592, 44);
            panel1.TabIndex = 3;
            // 
            // btnAuditLog
            // 
            btnAuditLog.Location = new Point(541, 8);
            btnAuditLog.Name = "btnAuditLog";
            btnAuditLog.Size = new Size(94, 29);
            btnAuditLog.TabIndex = 7;
            btnAuditLog.Text = "Audit Log";
            btnAuditLog.UseVisualStyleBackColor = true;
            btnAuditLog.Click += btnAuditLog_Click;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(325, 8);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(75, 32);
            btnReload.TabIndex = 6;
            btnReload.Text = "Reload";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // btnClearSales
            // 
            btnClearSales.Location = new Point(407, 8);
            btnClearSales.Name = "btnClearSales";
            btnClearSales.Size = new Size(113, 32);
            btnClearSales.TabIndex = 5;
            btnClearSales.Text = "Clear Sales";
            btnClearSales.UseVisualStyleBackColor = true;
            btnClearSales.Click += btnClearSales_Click;
            // 
            // btnPrintSummary
            // 
            btnPrintSummary.Location = new Point(209, 8);
            btnPrintSummary.Name = "btnPrintSummary";
            btnPrintSummary.Size = new Size(111, 32);
            btnPrintSummary.TabIndex = 4;
            btnPrintSummary.Text = "Print Summary";
            btnPrintSummary.UseVisualStyleBackColor = true;
            btnPrintSummary.Click += btnPrintSummary_Click;
            // 
            // id
            // 
            id.HeaderText = "ID";
            id.Name = "id";
            // 
            // fkey
            // 
            fkey.HeaderText = "Fkey";
            fkey.Name = "fkey";
            fkey.Width = 200;
            // 
            // onlineid
            // 
            onlineid.HeaderText = "Invoice Id";
            onlineid.Name = "onlineid";
            // 
            // terminal
            // 
            terminal.HeaderText = "Terminal";
            terminal.Name = "terminal";
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            // 
            // FrmSales
            // 
            ClientSize = new Size(1598, 664);
            Controls.Add(tableLayoutPanel1);
            Name = "FrmSales";
            Load += FrmSales_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridInvoice).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox reportResult;
        private DateTimePicker fromDate;
        private DataGridView dataGridInvoice;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn fkey;
        private DataGridViewTextBoxColumn onlineid;
        private DataGridViewTextBoxColumn terminal;
        private Button btnPrintSummary;
        private Button btnClearSales;
        private Button btnReload;
        private Button btnAuditLog;
        private DataGridViewTextBoxColumn C1;
        private DataGridViewTextBoxColumn C2;
        private DataGridViewTextBoxColumn C3;
        private DataGridViewTextBoxColumn C4;
        private DataGridViewTextBoxColumn C5;
        private DataGridViewTextBoxColumn Discount;
        private DataGridViewTextBoxColumn C6;
        private DataGridViewTextBoxColumn C7;
        private DataGridViewTextBoxColumn C8;
        private DataGridViewTextBoxColumn Operator;
        private DataGridViewTextBoxColumn Status;
    }
}