namespace ApprainERPTerminal.UI.Report
{
    partial class FrmAuditLog
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
            dataGridAuditLog = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Action = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            Operator = new DataGridViewTextBoxColumn();
            fromDate = new DateTimePicker();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            pnlSummary = new Panel();
            textSummaryBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridAuditLog).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            pnlSummary.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridAuditLog
            // 
            dataGridAuditLog.AllowUserToAddRows = false;
            dataGridAuditLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAuditLog.Columns.AddRange(new DataGridViewColumn[] { Id, Action, Type, Message, Date, Operator });
            dataGridAuditLog.Dock = DockStyle.Fill;
            dataGridAuditLog.Location = new Point(3, 53);
            dataGridAuditLog.Name = "dataGridAuditLog";
            dataGridAuditLog.RowTemplate.Height = 25;
            dataGridAuditLog.Size = new Size(1149, 234);
            dataGridAuditLog.TabIndex = 0;
            dataGridAuditLog.CellEnter += dataGridAuditLog_CellEnter;
            // 
            // Id
            // 
            Id.HeaderText = "ID";
            Id.Name = "Id";
            Id.Width = 50;
            // 
            // Action
            // 
            Action.HeaderText = "Action";
            Action.Name = "Action";
            Action.Width = 150;
            // 
            // Type
            // 
            Type.HeaderText = "Type";
            Type.Name = "Type";
            Type.Width = 150;
            // 
            // Message
            // 
            Message.HeaderText = "Message";
            Message.Name = "Message";
            Message.Width = 350;
            // 
            // Date
            // 
            Date.HeaderText = "Date";
            Date.Name = "Date";
            Date.Width = 150;
            // 
            // Operator
            // 
            Operator.HeaderText = "Operator";
            Operator.Name = "Operator";
            // 
            // fromDate
            // 
            fromDate.CustomFormat = "yyyy-MM-dd";
            fromDate.Location = new Point(17, 9);
            fromDate.Name = "fromDate";
            fromDate.Size = new Size(161, 23);
            fromDate.TabIndex = 1;
            fromDate.ValueChanged += fromDate_ValueChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridAuditLog, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlSummary, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Size = new Size(1155, 450);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(fromDate);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1149, 44);
            panel1.TabIndex = 3;
            // 
            // pnlSummary
            // 
            pnlSummary.Controls.Add(textSummaryBox);
            pnlSummary.Dock = DockStyle.Fill;
            pnlSummary.Location = new Point(3, 293);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(1149, 154);
            pnlSummary.TabIndex = 4;
            // 
            // textSummaryBox
            // 
            textSummaryBox.Dock = DockStyle.Fill;
            textSummaryBox.Location = new Point(0, 0);
            textSummaryBox.Multiline = true;
            textSummaryBox.Name = "textSummaryBox";
            textSummaryBox.Size = new Size(1149, 154);
            textSummaryBox.TabIndex = 0;
            // 
            // FrmAuditLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "FrmAuditLog";
            Text = "Audit Log";
            Load += FrmAuditLog_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridAuditLog).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridAuditLog;
        private DateTimePicker fromDate;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel pnlSummary;
        private TextBox textSummaryBox;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Action;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn Message;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Operator;
    }
}