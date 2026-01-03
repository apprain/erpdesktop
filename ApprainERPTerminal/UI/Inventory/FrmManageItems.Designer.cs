namespace ApprainERPTerminal.UI.Inventory
{
    partial class FrmManageItems
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridProductList = new DataGridView();
            barcode = new DataGridViewTextBoxColumn();
            name = new DataGridViewTextBoxColumn();
            Enter = new DataGridViewButtonColumn();
            pnlProductList = new Panel();
            btnCreateNewItem2 = new Button();
            txtProductSearchBarcode = new TextBox();
            pnlItemSingle = new Panel();
            btnRefreshItem = new Button();
            btnQuickSunc = new Button();
            lblTotalStock = new Label();
            btnNewItem = new Button();
            btnBack = new Button();
            btnSave = new Button();
            groupBox2 = new GroupBox();
            dataGridViewItemStock = new DataGridView();
            groupBox1 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtItmRemark = new TextBox();
            txtItmTax = new TextBox();
            txtItmFeatured = new TextBox();
            cmbItmSize = new ComboBox();
            cmbItmColor = new ComboBox();
            cmbItmCategory = new ComboBox();
            txtItmName = new TextBox();
            txtItmOldBarcode = new TextBox();
            txtItmBarcode = new TextBox();
            qty = new DataGridViewTextBoxColumn();
            Remain = new DataGridViewTextBoxColumn();
            entry = new DataGridViewTextBoxColumn();
            sprice = new DataGridViewTextBoxColumn();
            entrydate = new DataGridViewTextBoxColumn();
            color = new DataGridViewTextBoxColumn();
            size = new DataGridViewTextBoxColumn();
            Submit = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridProductList).BeginInit();
            pnlProductList.SuspendLayout();
            pnlItemSingle.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewItemStock).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridProductList
            // 
            dataGridProductList.AllowUserToAddRows = false;
            dataGridProductList.AllowUserToDeleteRows = false;
            dataGridProductList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridProductList.Columns.AddRange(new DataGridViewColumn[] { barcode, name, Enter });
            dataGridProductList.Location = new Point(26, 68);
            dataGridProductList.Name = "dataGridProductList";
            dataGridProductList.ReadOnly = true;
            dataGridProductList.RowTemplate.Height = 25;
            dataGridProductList.Size = new Size(1014, 482);
            dataGridProductList.TabIndex = 0;
            dataGridProductList.CellContentClick += dataGridProductList_CellContentClick;
            // 
            // barcode
            // 
            barcode.HeaderText = "Barcode";
            barcode.Name = "barcode";
            barcode.ReadOnly = true;
            barcode.Width = 120;
            // 
            // name
            // 
            name.HeaderText = "Name";
            name.Name = "name";
            name.ReadOnly = true;
            name.Width = 700;
            // 
            // Enter
            // 
            Enter.HeaderText = "Action";
            Enter.Name = "Enter";
            Enter.ReadOnly = true;
            Enter.Text = "Action";
            // 
            // pnlProductList
            // 
            pnlProductList.Controls.Add(btnCreateNewItem2);
            pnlProductList.Controls.Add(txtProductSearchBarcode);
            pnlProductList.Controls.Add(dataGridProductList);
            pnlProductList.Dock = DockStyle.Fill;
            pnlProductList.Location = new Point(0, 0);
            pnlProductList.Name = "pnlProductList";
            pnlProductList.Size = new Size(1072, 578);
            pnlProductList.TabIndex = 1;
            // 
            // btnCreateNewItem2
            // 
            btnCreateNewItem2.Location = new Point(888, 18);
            btnCreateNewItem2.Name = "btnCreateNewItem2";
            btnCreateNewItem2.Size = new Size(152, 32);
            btnCreateNewItem2.TabIndex = 2;
            btnCreateNewItem2.Text = "Create New Item";
            btnCreateNewItem2.UseVisualStyleBackColor = true;
            btnCreateNewItem2.Click += btnCreateNewItem2_Click;
            // 
            // txtProductSearchBarcode
            // 
            txtProductSearchBarcode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtProductSearchBarcode.Location = new Point(26, 21);
            txtProductSearchBarcode.Name = "txtProductSearchBarcode";
            txtProductSearchBarcode.PlaceholderText = "Type barcode, old barcode or product name";
            txtProductSearchBarcode.Size = new Size(828, 29);
            txtProductSearchBarcode.TabIndex = 1;
            txtProductSearchBarcode.TextChanged += txtProductSearchBarcode_TextChanged;
            // 
            // pnlItemSingle
            // 
            pnlItemSingle.Controls.Add(btnRefreshItem);
            pnlItemSingle.Controls.Add(btnQuickSunc);
            pnlItemSingle.Controls.Add(lblTotalStock);
            pnlItemSingle.Controls.Add(btnNewItem);
            pnlItemSingle.Controls.Add(btnBack);
            pnlItemSingle.Controls.Add(btnSave);
            pnlItemSingle.Controls.Add(groupBox2);
            pnlItemSingle.Controls.Add(groupBox1);
            pnlItemSingle.Dock = DockStyle.Fill;
            pnlItemSingle.Location = new Point(0, 0);
            pnlItemSingle.Name = "pnlItemSingle";
            pnlItemSingle.Size = new Size(1072, 578);
            pnlItemSingle.TabIndex = 2;
            // 
            // btnRefreshItem
            // 
            btnRefreshItem.Location = new Point(255, 36);
            btnRefreshItem.Name = "btnRefreshItem";
            btnRefreshItem.Size = new Size(75, 33);
            btnRefreshItem.TabIndex = 8;
            btnRefreshItem.Text = "Reload";
            btnRefreshItem.UseVisualStyleBackColor = true;
            btnRefreshItem.Click += btnRefreshItem_Click;
            // 
            // btnQuickSunc
            // 
            btnQuickSunc.Location = new Point(965, 320);
            btnQuickSunc.Name = "btnQuickSunc";
            btnQuickSunc.Size = new Size(75, 35);
            btnQuickSunc.TabIndex = 7;
            btnQuickSunc.Text = "Quick Sync";
            btnQuickSunc.UseVisualStyleBackColor = true;
            btnQuickSunc.Click += btnQuickSunc_Click;
            // 
            // lblTotalStock
            // 
            lblTotalStock.AutoSize = true;
            lblTotalStock.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalStock.Location = new Point(26, 324);
            lblTotalStock.Name = "lblTotalStock";
            lblTotalStock.Size = new Size(116, 21);
            lblTotalStock.TabIndex = 6;
            lblTotalStock.Text = "Total  Stock: 0";
            // 
            // btnNewItem
            // 
            btnNewItem.Location = new Point(26, 36);
            btnNewItem.Name = "btnNewItem";
            btnNewItem.Size = new Size(142, 33);
            btnNewItem.TabIndex = 5;
            btnNewItem.Text = "Create New Item";
            btnNewItem.UseVisualStyleBackColor = true;
            btnNewItem.Click += btnNewItem_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(174, 36);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 33);
            btnBack.TabIndex = 4;
            btnBack.Text = "Item List";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(965, 36);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 33);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save Item";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridViewItemStock);
            groupBox2.Location = new Point(26, 355);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1014, 195);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Stock";
            // 
            // dataGridViewItemStock
            // 
            dataGridViewItemStock.AllowUserToOrderColumns = true;
            dataGridViewItemStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewItemStock.Columns.AddRange(new DataGridViewColumn[] { qty, Remain, entry, sprice, entrydate, color, size, Submit });
            dataGridViewItemStock.Location = new Point(21, 31);
            dataGridViewItemStock.Name = "dataGridViewItemStock";
            dataGridViewItemStock.RowTemplate.Height = 25;
            dataGridViewItemStock.Size = new Size(973, 132);
            dataGridViewItemStock.TabIndex = 0;
            dataGridViewItemStock.CellContentClick += dataGridViewItemStock_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtItmRemark);
            groupBox1.Controls.Add(txtItmTax);
            groupBox1.Controls.Add(txtItmFeatured);
            groupBox1.Controls.Add(cmbItmSize);
            groupBox1.Controls.Add(cmbItmColor);
            groupBox1.Controls.Add(cmbItmCategory);
            groupBox1.Controls.Add(txtItmName);
            groupBox1.Controls.Add(txtItmOldBarcode);
            groupBox1.Controls.Add(txtItmBarcode);
            groupBox1.Location = new Point(26, 88);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1014, 226);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Item Details";
            // 
            // label8
            // 
            label8.Location = new Point(600, 168);
            label8.Name = "label8";
            label8.Size = new Size(100, 20);
            label8.TabIndex = 16;
            label8.Text = "Remark";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Location = new Point(600, 123);
            label7.Name = "label7";
            label7.Size = new Size(100, 20);
            label7.TabIndex = 15;
            label7.Text = "Featured";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Location = new Point(600, 77);
            label6.Name = "label6";
            label6.Size = new Size(100, 20);
            label6.TabIndex = 14;
            label6.Text = "Attribute";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Location = new Point(600, 39);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 13;
            label5.Text = "Category";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(42, 171);
            label4.Name = "label4";
            label4.Size = new Size(100, 20);
            label4.TabIndex = 12;
            label4.Text = "Tax(%)";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(42, 123);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 11;
            label3.Text = "Name";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(42, 80);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 10;
            label2.Text = "Factory Barcode";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Location = new Point(42, 39);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
            label1.TabIndex = 9;
            label1.Text = "Barcode(Search)";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtItmRemark
            // 
            txtItmRemark.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmRemark.Location = new Point(717, 168);
            txtItmRemark.Name = "txtItmRemark";
            txtItmRemark.Size = new Size(268, 29);
            txtItmRemark.TabIndex = 8;
            // 
            // txtItmTax
            // 
            txtItmTax.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmTax.Location = new Point(167, 168);
            txtItmTax.Name = "txtItmTax";
            txtItmTax.Size = new Size(269, 29);
            txtItmTax.TabIndex = 7;
            // 
            // txtItmFeatured
            // 
            txtItmFeatured.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmFeatured.Location = new Point(717, 120);
            txtItmFeatured.Name = "txtItmFeatured";
            txtItmFeatured.Size = new Size(268, 29);
            txtItmFeatured.TabIndex = 6;
            // 
            // cmbItmSize
            // 
            cmbItmSize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbItmSize.FormattingEnabled = true;
            cmbItmSize.Location = new Point(862, 77);
            cmbItmSize.Name = "cmbItmSize";
            cmbItmSize.Size = new Size(123, 29);
            cmbItmSize.TabIndex = 5;
            // 
            // cmbItmColor
            // 
            cmbItmColor.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbItmColor.FormattingEnabled = true;
            cmbItmColor.Location = new Point(717, 77);
            cmbItmColor.Name = "cmbItmColor";
            cmbItmColor.Size = new Size(127, 29);
            cmbItmColor.TabIndex = 4;
            // 
            // cmbItmCategory
            // 
            cmbItmCategory.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cmbItmCategory.FormattingEnabled = true;
            cmbItmCategory.Location = new Point(717, 34);
            cmbItmCategory.Name = "cmbItmCategory";
            cmbItmCategory.Size = new Size(268, 29);
            cmbItmCategory.TabIndex = 3;
            // 
            // txtItmName
            // 
            txtItmName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmName.Location = new Point(167, 120);
            txtItmName.Name = "txtItmName";
            txtItmName.Size = new Size(269, 29);
            txtItmName.TabIndex = 2;
            // 
            // txtItmOldBarcode
            // 
            txtItmOldBarcode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmOldBarcode.Location = new Point(168, 77);
            txtItmOldBarcode.Name = "txtItmOldBarcode";
            txtItmOldBarcode.Size = new Size(268, 29);
            txtItmOldBarcode.TabIndex = 1;
            // 
            // txtItmBarcode
            // 
            txtItmBarcode.BackColor = Color.FromArgb(224, 224, 224);
            txtItmBarcode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtItmBarcode.Location = new Point(168, 36);
            txtItmBarcode.Name = "txtItmBarcode";
            txtItmBarcode.Size = new Size(268, 29);
            txtItmBarcode.TabIndex = 0;
            txtItmBarcode.TextChanged += txtItmBarcode_TextChanged;
            // 
            // qty
            // 
            qty.HeaderText = "Qty";
            qty.Name = "qty";
            // 
            // Remain
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.SelectionBackColor = Color.Silver;
            Remain.DefaultCellStyle = dataGridViewCellStyle1;
            Remain.HeaderText = "Remain";
            Remain.Name = "Remain";
            Remain.ReadOnly = true;
            Remain.ToolTipText = "Remain";
            // 
            // entry
            // 
            dataGridViewCellStyle2.BackColor = Color.White;
            entry.DefaultCellStyle = dataGridViewCellStyle2;
            entry.HeaderText = "Entry";
            entry.Name = "entry";
            // 
            // sprice
            // 
            sprice.HeaderText = "S.Price";
            sprice.Name = "sprice";
            // 
            // entrydate
            // 
            dataGridViewCellStyle3.BackColor = Color.FromArgb(224, 224, 224);
            entrydate.DefaultCellStyle = dataGridViewCellStyle3;
            entrydate.HeaderText = "Entry Date";
            entrydate.Name = "entrydate";
            // 
            // color
            // 
            color.HeaderText = "Color";
            color.Name = "color";
            // 
            // size
            // 
            size.HeaderText = "Size";
            size.Name = "size";
            // 
            // Submit
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(192, 0, 0);
            dataGridViewCellStyle4.SelectionBackColor = Color.Red;
            Submit.DefaultCellStyle = dataGridViewCellStyle4;
            Submit.HeaderText = "Submit";
            Submit.Name = "Submit";
            Submit.Text = "Submit";
            Submit.UseColumnTextForButtonValue = true;
            // 
            // FrmManageItems
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 578);
            Controls.Add(pnlItemSingle);
            Controls.Add(pnlProductList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmManageItems";
            Text = "ManageItems";
            Load += FrmManageItems_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridProductList).EndInit();
            pnlProductList.ResumeLayout(false);
            pnlProductList.PerformLayout();
            pnlItemSingle.ResumeLayout(false);
            pnlItemSingle.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewItemStock).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridProductList;
        private Panel pnlProductList;
        private TextBox txtProductSearchBarcode;
        private Panel pnlItemSingle;
        private TextBox textBox1;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private GroupBox groupBox2;
        private DataGridView dataGridViewItemStock;
        private Label label1;
        private TextBox textBox6;
        private TextBox textBox5;
        private TextBox textBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label7;
        private TextBox txtItmTax;
        private ComboBox cmbItmSize;
        private ComboBox cmbItmColor;
        private ComboBox cmbItmCategory;
        private TextBox txtItmName;
        private TextBox txtItmOldBarcode;
        private TextBox txtItmBarcode;
        private TextBox txtItmRemark;
        private TextBox txtItmFeatured;
        private DataGridViewTextBoxColumn barcode;
        private DataGridViewTextBoxColumn name;
        private DataGridViewButtonColumn Enter;
        private Button btnBack;
        private Button btnSave;
        private Button btnNewItem;
        private Button btnCreateNewItem2;
        private Label lblTotalStock;
        private Button btnQuickSunc;
        private Button btnRefreshItem;
        private DataGridViewTextBoxColumn qty;
        private DataGridViewTextBoxColumn Remain;
        private DataGridViewTextBoxColumn entry;
        private DataGridViewTextBoxColumn sprice;
        private DataGridViewTextBoxColumn entrydate;
        private DataGridViewTextBoxColumn color;
        private DataGridViewTextBoxColumn size;
        private DataGridViewButtonColumn Submit;
    }
}