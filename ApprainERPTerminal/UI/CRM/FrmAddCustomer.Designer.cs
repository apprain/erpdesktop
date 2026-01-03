namespace ApprainERPTerminal.UI.CRM
{
    partial class FrmAddCustomer
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
            txtCustomerId = new TextBox();
            groupBox1 = new GroupBox();
            cmboDepartment = new ComboBox();
            txtRemark = new TextBox();
            txtSocialId = new TextBox();
            label3 = new Label();
            btnClosed = new Button();
            btnSave = new Button();
            txtPostcode = new TextBox();
            txtCity = new TextBox();
            checkBox1 = new CheckBox();
            txtEmailAddress = new TextBox();
            label5 = new Label();
            txtPhoneno = new TextBox();
            label4 = new Label();
            txtAddress = new TextBox();
            txtLastName = new TextBox();
            txtFName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtSrcKey = new TextBox();
            btnSearch = new Button();
            groupBox2 = new GroupBox();
            gridCustomerList = new DataGridView();
            CMID = new DataGridViewTextBoxColumn();
            CMName = new DataGridViewTextBoxColumn();
            CMPhoneno = new DataGridViewTextBoxColumn();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustomerList).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // txtCustomerId
            // 
            txtCustomerId.BackColor = SystemColors.ControlLight;
            txtCustomerId.Enabled = false;
            txtCustomerId.Location = new Point(110, 33);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.Size = new Size(159, 23);
            txtCustomerId.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmboDepartment);
            groupBox1.Controls.Add(txtRemark);
            groupBox1.Controls.Add(txtSocialId);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnClosed);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtPostcode);
            groupBox1.Controls.Add(txtCity);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(txtEmailAddress);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtPhoneno);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtAddress);
            groupBox1.Controls.Add(txtLastName);
            groupBox1.Controls.Add(txtFName);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtCustomerId);
            groupBox1.Location = new Point(666, 28);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(591, 392);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Create New Entry";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // cmboDepartment
            // 
            cmboDepartment.Enabled = false;
            cmboDepartment.FormattingEnabled = true;
            cmboDepartment.Location = new Point(350, 33);
            cmboDepartment.Name = "cmboDepartment";
            cmboDepartment.Size = new Size(210, 23);
            cmboDepartment.TabIndex = 20;
            // 
            // txtRemark
            // 
            txtRemark.Location = new Point(344, 220);
            txtRemark.Name = "txtRemark";
            txtRemark.PlaceholderText = "Remark";
            txtRemark.Size = new Size(216, 23);
            txtRemark.TabIndex = 10;
            // 
            // txtSocialId
            // 
            txtSocialId.Location = new Point(110, 217);
            txtSocialId.Name = "txtSocialId";
            txtSocialId.PlaceholderText = "Social Id";
            txtSocialId.Size = new Size(215, 23);
            txtSocialId.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 220);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 17;
            label3.Text = "Identification";
            // 
            // btnClosed
            // 
            btnClosed.Location = new Point(312, 284);
            btnClosed.Name = "btnClosed";
            btnClosed.Size = new Size(93, 33);
            btnClosed.TabIndex = 12;
            btnClosed.Text = "Close";
            btnClosed.UseVisualStyleBackColor = true;
            btnClosed.Click += btnClosed_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(189, 284);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 33);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtPostcode
            // 
            txtPostcode.Location = new Point(344, 134);
            txtPostcode.Name = "txtPostcode";
            txtPostcode.PlaceholderText = "Post Code";
            txtPostcode.Size = new Size(221, 23);
            txtPostcode.TabIndex = 6;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(344, 101);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "City";
            txtCity.Size = new Size(221, 23);
            txtCity.TabIndex = 5;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(276, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(52, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Auto";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtEmailAddress
            // 
            txtEmailAddress.Location = new Point(344, 177);
            txtEmailAddress.Name = "txtEmailAddress";
            txtEmailAddress.PlaceholderText = "Email Address";
            txtEmailAddress.Size = new Size(221, 23);
            txtEmailAddress.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 180);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 9;
            label5.Text = "Contact";
            // 
            // txtPhoneno
            // 
            txtPhoneno.Location = new Point(110, 176);
            txtPhoneno.Name = "txtPhoneno";
            txtPhoneno.PlaceholderText = "Phone No";
            txtPhoneno.Size = new Size(214, 23);
            txtPhoneno.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 104);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 7;
            label4.Text = "Address";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(110, 97);
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "Address Line 1";
            txtAddress.Size = new Size(213, 64);
            txtAddress.TabIndex = 4;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(344, 65);
            txtLastName.Name = "txtLastName";
            txtLastName.PlaceholderText = "Last Name";
            txtLastName.Size = new Size(221, 23);
            txtLastName.TabIndex = 3;
            // 
            // txtFName
            // 
            txtFName.Location = new Point(109, 65);
            txtFName.Name = "txtFName";
            txtFName.PlaceholderText = "First Name";
            txtFName.Size = new Size(214, 23);
            txtFName.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 68);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 2;
            label2.Text = "First Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 36);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 1;
            label1.Text = "Customer Id";
            // 
            // txtSrcKey
            // 
            txtSrcKey.Location = new Point(19, 28);
            txtSrcKey.Name = "txtSrcKey";
            txtSrcKey.PlaceholderText = "Search (Client Id/Phone No/Name)";
            txtSrcKey.Size = new Size(290, 23);
            txtSrcKey.TabIndex = 1;
            txtSrcKey.TextChanged += txtSearchCustomer_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(315, 28);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(79, 23);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtSrcKey);
            groupBox2.Controls.Add(btnSearch);
            groupBox2.Location = new Point(24, 28);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(621, 68);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Search Customer";
            // 
            // gridCustomerList
            // 
            gridCustomerList.AllowUserToAddRows = false;
            gridCustomerList.AllowUserToDeleteRows = false;
            gridCustomerList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCustomerList.Columns.AddRange(new DataGridViewColumn[] { CMID, CMName, CMPhoneno });
            gridCustomerList.Location = new Point(18, 24);
            gridCustomerList.Name = "gridCustomerList";
            gridCustomerList.ReadOnly = true;
            gridCustomerList.RowHeadersVisible = false;
            gridCustomerList.RowTemplate.Height = 25;
            gridCustomerList.Size = new Size(584, 260);
            gridCustomerList.TabIndex = 0;
            // 
            // CMID
            // 
            CMID.HeaderText = "ID";
            CMID.Name = "CMID";
            CMID.ReadOnly = true;
            // 
            // CMName
            // 
            CMName.HeaderText = "Name";
            CMName.Name = "CMName";
            CMName.ReadOnly = true;
            CMName.Width = 300;
            // 
            // CMPhoneno
            // 
            CMPhoneno.HeaderText = "Phone No";
            CMPhoneno.Name = "CMPhoneno";
            CMPhoneno.ReadOnly = true;
            CMPhoneno.Width = 180;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(gridCustomerList);
            groupBox3.Location = new Point(24, 117);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(621, 303);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Search Result";
            // 
            // FrmAddCustomer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 457);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmAddCustomer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Customers";
            Load += FrmAddCustomer_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustomerList).EndInit();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtCustomerId;
        private GroupBox groupBox1;
        private TextBox txtLastName;
        private TextBox txtFName;
        private Label label2;
        private Label label1;
        private TextBox txtEmailAddress;
        private Label label5;
        private TextBox txtPhoneno;
        private Label label4;
        private TextBox txtAddress;
        private CheckBox checkBox1;
        private Button btnClosed;
        private Button btnSave;
        private TextBox txtPostcode;
        private TextBox txtCity;
        private TextBox txtSrcKey;
        private Button btnSearch;
        private GroupBox groupBox2;
        private DataGridView gridCustomerList;
        private GroupBox groupBox3;
        private TextBox txtSocialId;
        private Label label3;
        private TextBox txtRemark;
        private ComboBox cmboDepartment;
        private DataGridViewTextBoxColumn CMID;
        private DataGridViewTextBoxColumn CMName;
        private DataGridViewTextBoxColumn CMPhoneno;
    }
}