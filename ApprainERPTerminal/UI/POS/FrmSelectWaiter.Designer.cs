namespace ApprainERPTerminal.UI.POS
{
    partial class FrmSelectWaiter
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
            pnlWaiterPanel = new Panel();
            SuspendLayout();
            // 
            // pnlWaiterPanel
            // 
            pnlWaiterPanel.BackColor = SystemColors.Control;
            pnlWaiterPanel.Dock = DockStyle.Fill;
            pnlWaiterPanel.Location = new Point(0, 0);
            pnlWaiterPanel.Margin = new Padding(3, 4, 3, 4);
            pnlWaiterPanel.Name = "pnlWaiterPanel";
            pnlWaiterPanel.Size = new Size(909, 881);
            pnlWaiterPanel.TabIndex = 0;
            // 
            // FrmSelectWaiter
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(909, 881);
            Controls.Add(pnlWaiterPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmSelectWaiter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select Waiter";
            Load += FrmSelectWaiter_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlWaiterPanel;
    }
}