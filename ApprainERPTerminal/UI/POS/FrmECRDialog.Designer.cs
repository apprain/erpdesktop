namespace ApprainERPTerminal.UI.POS
{
    partial class FrmECRDialog
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
            btnComplete = new Button();
            lblPaymentMethodName = new Label();
            lblAmount = new Label();
            lblStatus = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblMessage = new Label();
            SuspendLayout();
            // 
            // btnComplete
            // 
            btnComplete.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnComplete.Location = new Point(498, 273);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(136, 35);
            btnComplete.TabIndex = 0;
            btnComplete.Text = "Start Payment";
            btnComplete.UseVisualStyleBackColor = true;
            btnComplete.Click += btnComplete_Click;
            // 
            // lblPaymentMethodName
            // 
            lblPaymentMethodName.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPaymentMethodName.ForeColor = Color.Black;
            lblPaymentMethodName.Location = new Point(498, 129);
            lblPaymentMethodName.Name = "lblPaymentMethodName";
            lblPaymentMethodName.Size = new Size(258, 21);
            lblPaymentMethodName.TabIndex = 1;
            lblPaymentMethodName.Text = "City Bank POS";
            lblPaymentMethodName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            lblAmount.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblAmount.ForeColor = Color.SandyBrown;
            lblAmount.Location = new Point(498, 178);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(258, 21);
            lblAmount.TabIndex = 2;
            lblAmount.Text = "0.00";
            lblAmount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(498, 220);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(258, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "None";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(418, 220);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "Status";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(354, 184);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 5;
            label2.Text = "Amount Charging";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(358, 134);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 6;
            label3.Text = "Payment Method";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(498, 255);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 15);
            lblMessage.TabIndex = 7;
            // 
            // FrmECRDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1031, 718);
            Controls.Add(lblMessage);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(lblAmount);
            Controls.Add(lblPaymentMethodName);
            Controls.Add(btnComplete);
            Name = "FrmECRDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ECR Connection";
            Load += FrmECRDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnComplete;
        private Label lblPaymentMethodName;
        private Label lblAmount;
        private Label lblStatus;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblMessage;
    }
}