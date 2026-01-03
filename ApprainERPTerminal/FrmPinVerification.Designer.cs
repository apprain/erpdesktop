namespace ApprainERPTerminal
{
    partial class FrmPinVerification
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
            txtPin = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtPin
            // 
            txtPin.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            txtPin.Location = new Point(167, 80);
            txtPin.Name = "txtPin";
            txtPin.PasswordChar = '#';
            txtPin.Size = new Size(169, 25);
            txtPin.TabIndex = 0;
            txtPin.TextAlign = HorizontalAlignment.Center;
            txtPin.UseSystemPasswordChar = true;
            txtPin.KeyDown += txtPin_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 86);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 1;
            label1.Text = "Enter Pin ";
            // 
            // FrmPinVerification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 245);
            Controls.Add(label1);
            Controls.Add(txtPin);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmPinVerification";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PIN Verification";
            Load += FrmPinVerification_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPin;
        private Label label1;
    }
}