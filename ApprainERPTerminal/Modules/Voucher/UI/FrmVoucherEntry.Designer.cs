namespace ApprainERPTerminal.Modules.Voucher.UI
{
    partial class FrmVoucherEntry
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
            lblAction = new Label();
            label1 = new Label();
            label2 = new Label();
            cboCustomer = new ComboBox();
            lblBalance = new Label();
            cboCompanyAcc = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            dtVoucherDate = new DateTimePicker();
            label5 = new Label();
            txtSubject = new TextBox();
            label6 = new Label();
            txtNote = new TextBox();
            label7 = new Label();
            dgvRows = new DataGridView();
            lblTotal = new Label();
            btnSave = new Button();
            btnSaveSync = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRows).BeginInit();
            SuspendLayout();
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblAction.ForeColor = Color.Blue;
            lblAction.Location = new Point(170, 18);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(125, 28);
            lblAction.TabIndex = 0;
            lblAction.Text = "RECEIVABLE";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 25);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 1;
            label1.Text = "Voucher Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 86);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 2;
            label2.Text = "Customer";
            // 
            // cboCustomer
            // 
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Items.AddRange(new object[] { "Reazaul Karim" });
            cboCustomer.Location = new Point(170, 75);
            cboCustomer.Margin = new Padding(3, 4, 3, 4);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(207, 28);
            cboCustomer.TabIndex = 3;
            cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(405, 79);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(36, 20);
            lblBalance.TabIndex = 5;
            lblBalance.Text = "0.00";
            // 
            // cboCompanyAcc
            // 
            cboCompanyAcc.FormattingEnabled = true;
            cboCompanyAcc.Items.AddRange(new object[] { "Cash A/C" });
            cboCompanyAcc.Location = new Point(170, 118);
            cboCompanyAcc.Margin = new Padding(3, 4, 3, 4);
            cboCompanyAcc.Name = "cboCompanyAcc";
            cboCompanyAcc.Size = new Size(207, 28);
            cboCompanyAcc.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 121);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 7;
            label3.Text = "Bill Account";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 194);
            label4.Name = "label4";
            label4.Size = new Size(98, 20);
            label4.TabIndex = 8;
            label4.Text = "Voucher Date";
            // 
            // dtVoucherDate
            // 
            dtVoucherDate.Location = new Point(170, 186);
            dtVoucherDate.Margin = new Padding(3, 4, 3, 4);
            dtVoucherDate.Name = "dtVoucherDate";
            dtVoucherDate.Size = new Size(207, 27);
            dtVoucherDate.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(494, 75);
            label5.Name = "label5";
            label5.Size = new Size(58, 20);
            label5.TabIndex = 10;
            label5.Text = "Subject";
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(553, 73);
            txtSubject.Margin = new Padding(3, 4, 3, 4);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(207, 27);
            txtSubject.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(498, 126);
            label6.Name = "label6";
            label6.Size = new Size(42, 20);
            label6.TabIndex = 12;
            label6.Text = "Note";
            // 
            // txtNote
            // 
            txtNote.Location = new Point(553, 118);
            txtNote.Margin = new Padding(3, 4, 3, 4);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(207, 27);
            txtNote.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(35, 274);
            label7.Name = "label7";
            label7.Size = new Size(134, 20);
            label7.TabIndex = 14;
            label7.Text = "Transaction Details";
            // 
            // dgvRows
            // 
            dgvRows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRows.Location = new Point(35, 307);
            dgvRows.Margin = new Padding(3, 4, 3, 4);
            dgvRows.Name = "dgvRows";
            dgvRows.RowHeadersWidth = 51;
            dgvRows.RowTemplate.Height = 25;
            dgvRows.Size = new Size(1057, 321);
            dgvRows.TabIndex = 15;
            dgvRows.CellContentClick += dgvRows_CellContentClick;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(35, 641);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(99, 20);
            lblTotal.TabIndex = 16;
            lblTotal.Text = "Total : Tk 0.00";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(323, 710);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(127, 39);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnSaveSync
            // 
            btnSaveSync.Location = new Point(465, 710);
            btnSaveSync.Margin = new Padding(3, 4, 3, 4);
            btnSaveSync.Name = "btnSaveSync";
            btnSaveSync.Size = new Size(127, 39);
            btnSaveSync.TabIndex = 18;
            btnSaveSync.Text = "Save and Sync";
            btnSaveSync.UseVisualStyleBackColor = true;
            btnSaveSync.Click += btnSaveSync_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(598, 710);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 39);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmVoucherEntry
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1121, 783);
            Controls.Add(btnCancel);
            Controls.Add(btnSaveSync);
            Controls.Add(btnSave);
            Controls.Add(lblTotal);
            Controls.Add(dgvRows);
            Controls.Add(label7);
            Controls.Add(txtNote);
            Controls.Add(label6);
            Controls.Add(txtSubject);
            Controls.Add(label5);
            Controls.Add(dtVoucherDate);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cboCompanyAcc);
            Controls.Add(lblBalance);
            Controls.Add(cboCustomer);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblAction);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmVoucherEntry";
            Text = "FrmVoucherEntry";
            Load += FrmVoucherEntry_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRows).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAction;
        private Label label1;
        private Label label2;
        private ComboBox cboCustomer;
        private Label lblBalance;
        private ComboBox cboCompanyAcc;
        private Label label3;
        private Label label4;
        private DateTimePicker dtVoucherDate;
        private Label label5;
        private TextBox txtSubject;
        private Label label6;
        private TextBox txtNote;
        private Label label7;
        private DataGridView dgvRows;
        private Label lblTotal;
        private Button btnSave;
        private Button btnSaveSync;
        private Button btnCancel;
    }
}