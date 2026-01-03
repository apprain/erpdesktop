namespace ApprainERPTerminal.UI.POS
{
    partial class FrmCashFrom
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
            tblLayoutCashReceiveBase = new TableLayoutPanel();
            tblLayoutCashReceiveRight = new TableLayoutPanel();
            tblLayoutCashReceiveBase.SuspendLayout();
            SuspendLayout();
            // 
            // tblLayoutCashReceiveBase
            // 
            tblLayoutCashReceiveBase.ColumnCount = 2;
            tblLayoutCashReceiveBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutCashReceiveBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayoutCashReceiveBase.Controls.Add(tblLayoutCashReceiveRight, 1, 0);
            tblLayoutCashReceiveBase.Dock = DockStyle.Fill;
            tblLayoutCashReceiveBase.Location = new Point(0, 0);
            tblLayoutCashReceiveBase.Name = "tblLayoutCashReceiveBase";
            tblLayoutCashReceiveBase.RowCount = 1;
            tblLayoutCashReceiveBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblLayoutCashReceiveBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblLayoutCashReceiveBase.Size = new Size(1082, 720);
            tblLayoutCashReceiveBase.TabIndex = 0;
            // 
            // tblLayoutCashReceiveRight
            // 
            tblLayoutCashReceiveRight.ColumnCount = 1;
            tblLayoutCashReceiveRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblLayoutCashReceiveRight.Dock = DockStyle.Fill;
            tblLayoutCashReceiveRight.Location = new Point(544, 3);
            tblLayoutCashReceiveRight.Name = "tblLayoutCashReceiveRight";
            tblLayoutCashReceiveRight.RowCount = 4;
            tblLayoutCashReceiveRight.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tblLayoutCashReceiveRight.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tblLayoutCashReceiveRight.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tblLayoutCashReceiveRight.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tblLayoutCashReceiveRight.Size = new Size(535, 714);
            tblLayoutCashReceiveRight.TabIndex = 0;
            // 
            // FrmCashFrom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 720);
            Controls.Add(tblLayoutCashReceiveBase);
            Name = "FrmCashFrom";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Receive Cash";
            Load += FrmCashFrom_Load;
            tblLayoutCashReceiveBase.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tblLayoutCashReceiveBase;
        private TableLayoutPanel tblLayoutCashReceiveRight;
    }
}