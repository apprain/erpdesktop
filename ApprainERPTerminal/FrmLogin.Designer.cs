namespace ApprainERPTerminal
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblSitetitle = new Label();
            lblWebsite = new Label();
            lblVersion = new Label();
            lblConnection = new Label();
            lblCompany = new Label();
            panelLogin = new Panel();
            chkLocallogin = new CheckBox();
            btnLogin = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            lblUsernane = new Label();
            txtUsername = new TextBox();
            panelConnection = new Panel();
            btnBack = new Button();
            btnSave = new Button();
            txtToken = new TextBox();
            lblToken = new Label();
            picLogo = new PictureBox();
            panelLogin.SuspendLayout();
            panelConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // lblSitetitle
            // 
            lblSitetitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblSitetitle.ForeColor = Color.OrangeRed;
            lblSitetitle.Location = new Point(3, 86);
            lblSitetitle.Margin = new Padding(2, 0, 2, 0);
            lblSitetitle.Name = "lblSitetitle";
            lblSitetitle.Size = new Size(301, 32);
            lblSitetitle.TabIndex = 0;
            lblSitetitle.Text = "Apprain ERP";
            lblSitetitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWebsite
            // 
            lblWebsite.AutoSize = true;
            lblWebsite.Location = new Point(108, 339);
            lblWebsite.Margin = new Padding(2, 0, 2, 0);
            lblWebsite.Name = "lblWebsite";
            lblWebsite.Size = new Size(104, 15);
            lblWebsite.TabIndex = 4;
            lblWebsite.Text = "www.apprain.com";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(11, 372);
            lblVersion.Margin = new Padding(2, 0, 2, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(75, 15);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "Version: 3.1.1";
            // 
            // lblConnection
            // 
            lblConnection.AutoSize = true;
            lblConnection.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblConnection.ForeColor = Color.Black;
            lblConnection.Location = new Point(252, 372);
            lblConnection.Margin = new Padding(2, 0, 2, 0);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new Size(40, 15);
            lblConnection.TabIndex = 6;
            lblConnection.Text = "Setup";
            lblConnection.Click += lblConnection_Click;
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Location = new Point(97, 321);
            lblCompany.Margin = new Padding(2, 0, 2, 0);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(121, 15);
            lblCompany.TabIndex = 7;
            lblCompany.Text = "Apprain Technologies";
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(chkLocallogin);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(lblPassword);
            panelLogin.Controls.Add(lblUsernane);
            panelLogin.Controls.Add(txtUsername);
            panelLogin.Location = new Point(7, 126);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(274, 176);
            panelLogin.TabIndex = 8;
            // 
            // chkLocallogin
            // 
            chkLocallogin.AutoSize = true;
            chkLocallogin.Location = new Point(82, 138);
            chkLocallogin.Name = "chkLocallogin";
            chkLocallogin.Size = new Size(91, 19);
            chkLocallogin.TabIndex = 5;
            chkLocallogin.Text = "Cloud Login";
            chkLocallogin.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.Location = new Point(82, 91);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(181, 32);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.Location = new Point(82, 55);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '#';
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.Size = new Size(185, 26);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblPassword.Location = new Point(12, 58);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password";
            // 
            // lblUsernane
            // 
            lblUsernane.AutoSize = true;
            lblUsernane.Location = new Point(10, 25);
            lblUsernane.Name = "lblUsernane";
            lblUsernane.Size = new Size(60, 15);
            lblUsernane.TabIndex = 1;
            lblUsernane.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            txtUsername.Location = new Point(82, 22);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Enter login name";
            txtUsername.Size = new Size(185, 26);
            txtUsername.TabIndex = 0;
            // 
            // panelConnection
            // 
            panelConnection.Controls.Add(btnBack);
            panelConnection.Controls.Add(btnSave);
            panelConnection.Controls.Add(txtToken);
            panelConnection.Controls.Add(lblToken);
            panelConnection.Location = new Point(3, 129);
            panelConnection.Name = "panelConnection";
            panelConnection.Size = new Size(280, 189);
            panelConnection.TabIndex = 6;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(180, 80);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(87, 23);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(70, 80);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(91, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // txtToken
            // 
            txtToken.Location = new Point(70, 43);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(197, 23);
            txtToken.TabIndex = 1;
            // 
            // lblToken
            // 
            lblToken.AutoSize = true;
            lblToken.Location = new Point(16, 46);
            lblToken.Name = "lblToken";
            lblToken.Size = new Size(38, 15);
            lblToken.TabIndex = 0;
            lblToken.Text = "Token";
            // 
            // picLogo
            // 
            picLogo.Location = new Point(110, 5);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(80, 80);
            picLogo.TabIndex = 9;
            picLogo.TabStop = false;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(307, 405);
            Controls.Add(picLogo);
            Controls.Add(panelLogin);
            Controls.Add(lblCompany);
            Controls.Add(lblConnection);
            Controls.Add(lblVersion);
            Controls.Add(lblWebsite);
            Controls.Add(panelConnection);
            Controls.Add(lblSitetitle);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Apprain ERP";
            Load += FrmLogin_Load;
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelConnection.ResumeLayout(false);
            panelConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSitetitle;
        private Label lblWebsite;
        private Label lblVersion;
        private Label lblConnection;
        private Label lblCompany;
        private Panel panelLogin;
        private Label lblPassword;
        private Label lblUsernane;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Panel panelConnection;
        private Button btnBack;
        private Button btnSave;
        private TextBox txtToken;
        private Label lblToken;
        private Button btnLogin;
        private CheckBox chkLocallogin;
        private Button button1;
        private PictureBox picLogo;
    }
}