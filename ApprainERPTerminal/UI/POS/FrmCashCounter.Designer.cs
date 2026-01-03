namespace ApprainERPTerminal.UI.POS
{
    partial class FrmCashCounter
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            labelTotalAmount = new TextBox();
            groupDenomination = new GroupBox();
            dataGridViewCashCounter = new DataGridView();
            Currency = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Amount = new DataGridViewTextBoxColumn();
            btnClear = new Button();
            groupBox1 = new GroupBox();
            groupDenomination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCashCounter).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // labelTotalAmount
            // 
            labelTotalAmount.BackColor = SystemColors.Control;
            labelTotalAmount.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTotalAmount.ForeColor = Color.Maroon;
            labelTotalAmount.Location = new Point(457, 32);
            labelTotalAmount.Name = "labelTotalAmount";
            labelTotalAmount.Size = new Size(201, 39);
            labelTotalAmount.TabIndex = 1;
            labelTotalAmount.Text = "0.00";
            labelTotalAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // groupDenomination
            // 
            groupDenomination.Controls.Add(dataGridViewCashCounter);
            groupDenomination.Location = new Point(49, 133);
            groupDenomination.Name = "groupDenomination";
            groupDenomination.Size = new Size(674, 497);
            groupDenomination.TabIndex = 2;
            groupDenomination.TabStop = false;
            groupDenomination.Text = "Denomination Box";
            // 
            // dataGridViewCashCounter
            // 
            dataGridViewCashCounter.AllowUserToAddRows = false;
            dataGridViewCashCounter.AllowUserToDeleteRows = false;
            dataGridViewCashCounter.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCashCounter.Columns.AddRange(new DataGridViewColumn[] { Currency, Quantity, Amount });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridViewCashCounter.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCashCounter.Location = new Point(15, 22);
            dataGridViewCashCounter.Name = "dataGridViewCashCounter";
            dataGridViewCashCounter.RowTemplate.Height = 25;
            dataGridViewCashCounter.Size = new Size(643, 456);
            dataGridViewCashCounter.TabIndex = 0;
            // 
            // Currency
            // 
            Currency.HeaderText = "Currency";
            Currency.Name = "Currency";
            Currency.Width = 200;
            // 
            // Quantity
            // 
            Quantity.HeaderText = "Quantity";
            Quantity.Name = "Quantity";
            Quantity.Width = 200;
            // 
            // Amount
            // 
            Amount.HeaderText = "Amount";
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.Width = 200;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnClear.Location = new Point(15, 33);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(122, 38);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnClear);
            groupBox1.Controls.Add(labelTotalAmount);
            groupBox1.Location = new Point(49, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(674, 89);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options";
            // 
            // FrmCashCounter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 677);
            Controls.Add(groupBox1);
            Controls.Add(groupDenomination);
            Name = "FrmCashCounter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Denomination Board";
            Load += FrmCashCounter_Load;
            groupDenomination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCashCounter).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox labelTotalAmount;
        private GroupBox groupDenomination;
        private DataGridView dataGridViewCashCounter;
        private Button btnClear;
        private GroupBox groupBox1;
        private DataGridViewTextBoxColumn Currency;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Amount;
    }
}