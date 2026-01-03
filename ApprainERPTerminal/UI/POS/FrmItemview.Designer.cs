namespace ApprainERPTerminal.UI.POS
{
    partial class FrmItemview
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
            pnlItemSearchBar = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            pnlItemSearchBar.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlItemSearchBar
            // 
            pnlItemSearchBar.BackColor = SystemColors.ActiveBorder;
            pnlItemSearchBar.Controls.Add(textBox1);
            pnlItemSearchBar.Location = new Point(12, 12);
            pnlItemSearchBar.Name = "pnlItemSearchBar";
            pnlItemSearchBar.Size = new Size(918, 55);
            pnlItemSearchBar.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(16, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(198, 29);
            textBox1.TabIndex = 1;
            textBox1.Text = "Barcode";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(204, 33);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(248, 29);
            textBox2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(31, 33);
            label1.Name = "label1";
            label1.Size = new Size(56, 21);
            label1.TabIndex = 2;
            label1.Text = "Name";
            label1.Click += label1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 88);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(918, 385);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Item Information";
            // 
            // FrmItemview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 696);
            Controls.Add(groupBox1);
            Controls.Add(pnlItemSearchBar);
            Name = "FrmItemview";
            Text = "Item View";
            Load += FrmItemview_Load;
            pnlItemSearchBar.ResumeLayout(false);
            pnlItemSearchBar.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlItemSearchBar;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private GroupBox groupBox1;
    }
}