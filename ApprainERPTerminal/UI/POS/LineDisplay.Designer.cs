namespace ApprainERPTerminal.UI.POS
{
    partial class LineDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineDisplay));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pictureBox = new PictureBox();
            dataGridViewLD = new DataGridView();
            panel1 = new Panel();
            tblLayoutSummary = new TableLayoutPanel();
            lblLDTotal = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblLDTax = new Label();
            lblLDPayable = new Label();
            lblLDDiscount = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblManufacture = new Label();
            ls = new DataGridViewTextBoxColumn();
            itemName = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLD).BeginInit();
            panel1.SuspendLayout();
            tblLayoutSummary.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = (Image)resources.GetObject("pictureBox.Image");
            pictureBox.Location = new Point(3, 2);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(835, 872);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox1_Click;
            // 
            // dataGridViewLD
            // 
            dataGridViewLD.AllowUserToAddRows = false;
            dataGridViewLD.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewLD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewLD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLD.Columns.AddRange(new DataGridViewColumn[] { ls, itemName, Qty, Price, Total });
            dataGridViewLD.Dock = DockStyle.Fill;
            dataGridViewLD.Location = new Point(3, 2);
            dataGridViewLD.Margin = new Padding(3, 2, 3, 2);
            dataGridViewLD.Name = "dataGridViewLD";
            dataGridViewLD.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridViewLD.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewLD.RowHeadersVisible = false;
            dataGridViewLD.RowHeadersWidth = 51;
            dataGridViewLD.RowTemplate.Height = 40;
            dataGridViewLD.Size = new Size(835, 623);
            dataGridViewLD.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(tblLayoutSummary);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 629);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(835, 265);
            panel1.TabIndex = 3;
            // 
            // tblLayoutSummary
            // 
            tblLayoutSummary.ColumnCount = 2;
            tblLayoutSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutSummary.Controls.Add(lblLDTotal, 1, 0);
            tblLayoutSummary.Controls.Add(label1, 0, 0);
            tblLayoutSummary.Controls.Add(label3, 0, 1);
            tblLayoutSummary.Controls.Add(label4, 0, 2);
            tblLayoutSummary.Controls.Add(label5, 0, 3);
            tblLayoutSummary.Controls.Add(lblLDTax, 1, 1);
            tblLayoutSummary.Controls.Add(lblLDPayable, 1, 3);
            tblLayoutSummary.Controls.Add(lblLDDiscount, 1, 2);
            tblLayoutSummary.Dock = DockStyle.Fill;
            tblLayoutSummary.Location = new Point(0, 0);
            tblLayoutSummary.Name = "tblLayoutSummary";
            tblLayoutSummary.RowCount = 4;
            tblLayoutSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tblLayoutSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tblLayoutSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tblLayoutSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
            tblLayoutSummary.Size = new Size(835, 265);
            tblLayoutSummary.TabIndex = 9;
            // 
            // lblLDTotal
            // 
            lblLDTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLDTotal.BackColor = Color.Black;
            lblLDTotal.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLDTotal.ForeColor = Color.Orange;
            lblLDTotal.Location = new Point(420, 0);
            lblLDTotal.Name = "lblLDTotal";
            lblLDTotal.RightToLeft = RightToLeft.Yes;
            lblLDTotal.Size = new Size(412, 34);
            lblLDTotal.TabIndex = 1;
            lblLDTotal.Text = "0.00";
            lblLDTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Orange;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(411, 34);
            label1.TabIndex = 0;
            label1.Text = "Total";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Orange;
            label3.Location = new Point(3, 34);
            label3.Name = "label3";
            label3.Size = new Size(411, 28);
            label3.TabIndex = 2;
            label3.Text = "TAX(+)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Orange;
            label4.Location = new Point(3, 62);
            label4.Name = "label4";
            label4.Size = new Size(411, 26);
            label4.TabIndex = 3;
            label4.Text = "Discount(-)";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(64, 64, 64);
            label5.Dock = DockStyle.Top;
            label5.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.LimeGreen;
            label5.Location = new Point(3, 88);
            label5.Name = "label5";
            label5.Size = new Size(411, 38);
            label5.TabIndex = 8;
            label5.Text = "Total";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLDTax
            // 
            lblLDTax.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLDTax.BackColor = Color.Black;
            lblLDTax.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLDTax.ForeColor = Color.Orange;
            lblLDTax.Location = new Point(420, 34);
            lblLDTax.Name = "lblLDTax";
            lblLDTax.RightToLeft = RightToLeft.Yes;
            lblLDTax.Size = new Size(412, 28);
            lblLDTax.TabIndex = 5;
            lblLDTax.Text = "0.00";
            lblLDTax.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLDPayable
            // 
            lblLDPayable.BackColor = Color.FromArgb(64, 64, 64);
            lblLDPayable.Dock = DockStyle.Top;
            lblLDPayable.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLDPayable.ForeColor = Color.Lime;
            lblLDPayable.Location = new Point(420, 88);
            lblLDPayable.Name = "lblLDPayable";
            lblLDPayable.RightToLeft = RightToLeft.Yes;
            lblLDPayable.Size = new Size(412, 38);
            lblLDPayable.TabIndex = 7;
            lblLDPayable.Text = "0.00";
            lblLDPayable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLDDiscount
            // 
            lblLDDiscount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLDDiscount.BackColor = Color.Black;
            lblLDDiscount.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLDDiscount.ForeColor = Color.Orange;
            lblLDDiscount.Location = new Point(420, 62);
            lblLDDiscount.Name = "lblLDDiscount";
            lblLDDiscount.RightToLeft = RightToLeft.Yes;
            lblLDDiscount.Size = new Size(412, 26);
            lblLDDiscount.TabIndex = 6;
            lblLDDiscount.Text = "0.00";
            lblLDDiscount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1694, 902);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(dataGridViewLD, 0, 0);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.Size = new Size(841, 896);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(pictureBox, 0, 0);
            tableLayoutPanel3.Controls.Add(lblManufacture, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(850, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(841, 896);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // lblManufacture
            // 
            lblManufacture.AutoSize = true;
            lblManufacture.BackColor = Color.DodgerBlue;
            lblManufacture.Dock = DockStyle.Fill;
            lblManufacture.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblManufacture.ForeColor = SystemColors.HighlightText;
            lblManufacture.Location = new Point(3, 876);
            lblManufacture.Name = "lblManufacture";
            lblManufacture.Size = new Size(835, 20);
            lblManufacture.TabIndex = 1;
            lblManufacture.Text = "APPRAIN TECHNOLOGIES";
            lblManufacture.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ls
            // 
            ls.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            ls.DefaultCellStyle = dataGridViewCellStyle2;
            ls.FillWeight = 39.6708031F;
            ls.HeaderText = "#";
            ls.MinimumWidth = 6;
            ls.Name = "ls";
            ls.ReadOnly = true;
            ls.Width = 50;
            // 
            // itemName
            // 
            itemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.NullValue = null;
            itemName.DefaultCellStyle = dataGridViewCellStyle3;
            itemName.FillWeight = 439.086334F;
            itemName.HeaderText = "Description";
            itemName.MinimumWidth = 6;
            itemName.Name = "itemName";
            itemName.ReadOnly = true;
            itemName.Width = 300;
            // 
            // Qty
            // 
            Qty.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            Qty.DefaultCellStyle = dataGridViewCellStyle4;
            Qty.FillWeight = 12.2643471F;
            Qty.HeaderText = "Qty";
            Qty.MinimumWidth = 6;
            Qty.Name = "Qty";
            Qty.ReadOnly = true;
            Qty.Width = 68;
            // 
            // Price
            // 
            Price.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            Price.DefaultCellStyle = dataGridViewCellStyle5;
            Price.FillWeight = 7.13467932F;
            Price.HeaderText = "Price";
            Price.MinimumWidth = 6;
            Price.Name = "Price";
            Price.ReadOnly = true;
            // 
            // Total
            // 
            Total.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0.00";
            Total.DefaultCellStyle = dataGridViewCellStyle6;
            Total.FillWeight = 1.84387088F;
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.ReadOnly = true;
            Total.Resizable = DataGridViewTriState.True;
            Total.Width = 200;
            // 
            // LineDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1694, 902);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LineDisplay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Greetings and happy shopping!";
            Load += LineDisplay_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLD).EndInit();
            panel1.ResumeLayout(false);
            tblLayoutSummary.ResumeLayout(false);
            tblLayoutSummary.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private DataGridView dataGridViewLD;
        private Panel panel1;
        private Label label1;
        private Label lblLDTotal;
        private Label label3;
        private Label lblLDTax;
        private Label label4;
        private Label lblLDDiscount;
        private Label lblLDPayable;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tblLayoutSummary;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblManufacture;
        private DataGridViewTextBoxColumn ls;
        private DataGridViewTextBoxColumn itemName;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Total;
    }
}