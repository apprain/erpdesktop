namespace ApprainERPTerminal.UI.POS
{
    partial class FrmPOSDialog
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
            cmboPODDSrcOpts = new ComboBox();
            txtPOSDSrcText = new TextBox();
            btnPOSDSrcInvoice = new Button();
            panelInquiry = new Panel();
            label2 = new Label();
            label1 = new Label();
            panelProfitLostGrid = new Panel();
            dataGridProfitLoss = new DataGridView();
            code = new DataGridViewTextBoxColumn();
            name = new DataGridViewTextBoxColumn();
            qty = new DataGridViewTextBoxColumn();
            unitsalesprice = new DataGridViewTextBoxColumn();
            unitpurchaseprice = new DataGridViewTextBoxColumn();
            totalsalesprice = new DataGridViewTextBoxColumn();
            totalpurchaseprice = new DataGridViewTextBoxColumn();
            profit = new DataGridViewTextBoxColumn();
            panelInquiry.SuspendLayout();
            panelProfitLostGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridProfitLoss).BeginInit();
            SuspendLayout();
            // 
            // cmboPODDSrcOpts
            // 
            cmboPODDSrcOpts.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmboPODDSrcOpts.FormattingEnabled = true;
            cmboPODDSrcOpts.Items.AddRange(new object[] { "ID", "ONLINE", "KEY", "OTHER" });
            cmboPODDSrcOpts.Location = new Point(321, 110);
            cmboPODDSrcOpts.Name = "cmboPODDSrcOpts";
            cmboPODDSrcOpts.Size = new Size(195, 23);
            cmboPODDSrcOpts.TabIndex = 0;
            cmboPODDSrcOpts.Text = "KEY";
            // 
            // txtPOSDSrcText
            // 
            txtPOSDSrcText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtPOSDSrcText.Location = new Point(321, 69);
            txtPOSDSrcText.Name = "txtPOSDSrcText";
            txtPOSDSrcText.PlaceholderText = "Enter Value";
            txtPOSDSrcText.Size = new Size(195, 23);
            txtPOSDSrcText.TabIndex = 1;
            txtPOSDSrcText.KeyDown += txtPOSDSrcText_KeyDown;
            // 
            // btnPOSDSrcInvoice
            // 
            btnPOSDSrcInvoice.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnPOSDSrcInvoice.Location = new Point(321, 144);
            btnPOSDSrcInvoice.Name = "btnPOSDSrcInvoice";
            btnPOSDSrcInvoice.Size = new Size(195, 27);
            btnPOSDSrcInvoice.TabIndex = 2;
            btnPOSDSrcInvoice.Text = "Search";
            btnPOSDSrcInvoice.UseVisualStyleBackColor = true;
            btnPOSDSrcInvoice.Click += btnPOSDSrcInvoice_Click;
            // 
            // panelInquiry
            // 
            panelInquiry.Controls.Add(label2);
            panelInquiry.Controls.Add(label1);
            panelInquiry.Controls.Add(txtPOSDSrcText);
            panelInquiry.Controls.Add(btnPOSDSrcInvoice);
            panelInquiry.Controls.Add(cmboPODDSrcOpts);
            panelInquiry.Dock = DockStyle.Fill;
            panelInquiry.Location = new Point(0, 0);
            panelInquiry.Name = "panelInquiry";
            panelInquiry.Size = new Size(784, 443);
            panelInquiry.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(240, 112);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 4;
            label2.Text = "ID Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(227, 72);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 3;
            label1.Text = "Invoice Id";
            // 
            // panelProfitLostGrid
            // 
            panelProfitLostGrid.Controls.Add(dataGridProfitLoss);
            panelProfitLostGrid.Dock = DockStyle.Fill;
            panelProfitLostGrid.Location = new Point(0, 0);
            panelProfitLostGrid.Name = "panelProfitLostGrid";
            panelProfitLostGrid.Size = new Size(784, 443);
            panelProfitLostGrid.TabIndex = 4;
            // 
            // dataGridProfitLoss
            // 
            dataGridProfitLoss.AllowUserToAddRows = false;
            dataGridProfitLoss.AllowUserToDeleteRows = false;
            dataGridProfitLoss.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridProfitLoss.Columns.AddRange(new DataGridViewColumn[] { code, name, qty, unitsalesprice, unitpurchaseprice, totalsalesprice, totalpurchaseprice, profit });
            dataGridProfitLoss.Dock = DockStyle.Fill;
            dataGridProfitLoss.Location = new Point(0, 0);
            dataGridProfitLoss.Name = "dataGridProfitLoss";
            dataGridProfitLoss.ReadOnly = true;
            dataGridProfitLoss.RowTemplate.Height = 25;
            dataGridProfitLoss.Size = new Size(784, 443);
            dataGridProfitLoss.TabIndex = 0;
            // 
            // code
            // 
            code.HeaderText = "Code";
            code.Name = "code";
            code.ReadOnly = true;
            code.Width = 50;
            // 
            // name
            // 
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            name.HeaderText = "Name";
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // qty
            // 
            qty.HeaderText = "Qty";
            qty.Name = "qty";
            qty.ReadOnly = true;
            qty.Width = 50;
            // 
            // unitsalesprice
            // 
            unitsalesprice.HeaderText = "S. Price";
            unitsalesprice.Name = "unitsalesprice";
            unitsalesprice.ReadOnly = true;
            unitsalesprice.Width = 80;
            // 
            // unitpurchaseprice
            // 
            unitpurchaseprice.HeaderText = "P Price";
            unitpurchaseprice.Name = "unitpurchaseprice";
            unitpurchaseprice.ReadOnly = true;
            unitpurchaseprice.Width = 80;
            // 
            // totalsalesprice
            // 
            totalsalesprice.HeaderText = "T. S. Price";
            totalsalesprice.Name = "totalsalesprice";
            totalsalesprice.ReadOnly = true;
            // 
            // totalpurchaseprice
            // 
            totalpurchaseprice.HeaderText = "T. P. Price";
            totalpurchaseprice.Name = "totalpurchaseprice";
            totalpurchaseprice.ReadOnly = true;
            // 
            // profit
            // 
            profit.HeaderText = "Prifit";
            profit.Name = "profit";
            profit.ReadOnly = true;
            // 
            // FrmPOSDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 443);
            Controls.Add(panelProfitLostGrid);
            Controls.Add(panelInquiry);
            Name = "FrmPOSDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Search Invoice";
            Load += FrmPOSDialog_Load;
            panelInquiry.ResumeLayout(false);
            panelInquiry.PerformLayout();
            panelProfitLostGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridProfitLoss).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmboPODDSrcOpts;
        private TextBox txtPOSDSrcText;
        private Button btnPOSDSrcInvoice;
        private Panel panelInquiry;
        private Panel panelProfitLostGrid;
        private DataGridView dataGridProfitLoss;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn code;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn qty;
        private DataGridViewTextBoxColumn unitsalesprice;
        private DataGridViewTextBoxColumn unitpurchaseprice;
        private DataGridViewTextBoxColumn totalsalesprice;
        private DataGridViewTextBoxColumn totalpurchaseprice;
        private DataGridViewTextBoxColumn profit;
    }
}