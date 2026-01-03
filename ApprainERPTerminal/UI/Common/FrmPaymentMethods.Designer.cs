namespace ApprainERPTerminal.UI.Common
{
    partial class FrmPaymentMethods
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
            gridPaymentmethods = new DataGridView();
            btnSave = new Button();
            name = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            port = new DataGridViewComboBoxColumn();
            protocol = new DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)gridPaymentmethods).BeginInit();
            SuspendLayout();
            // 
            // gridPaymentmethods
            // 
            gridPaymentmethods.AllowUserToAddRows = false;
            gridPaymentmethods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPaymentmethods.Columns.AddRange(new DataGridViewColumn[] { name, type, port, protocol });
            gridPaymentmethods.Location = new Point(12, 12);
            gridPaymentmethods.Name = "gridPaymentmethods";
            gridPaymentmethods.RowHeadersWidth = 62;
            gridPaymentmethods.RowTemplate.Height = 30;
            gridPaymentmethods.Size = new Size(790, 239);
            gridPaymentmethods.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(727, 266);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // name
            // 
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            name.HeaderText = "Name";
            name.MinimumWidth = 8;
            name.Name = "name";
            // 
            // type
            // 
            type.HeaderText = "Type";
            type.MinimumWidth = 8;
            type.Name = "type";
            type.Width = 200;
            // 
            // port
            // 
            port.HeaderText = "Port";
            port.MinimumWidth = 8;
            port.Name = "port";
            port.Resizable = DataGridViewTriState.True;
            port.SortMode = DataGridViewColumnSortMode.Automatic;
            port.Width = 200;
            // 
            // protocol
            // 
            protocol.HeaderText = "Protocol";
            protocol.Items.AddRange(new object[] { "", "NEXGO" });
            protocol.Name = "protocol";
            protocol.Resizable = DataGridViewTriState.True;
            protocol.SortMode = DataGridViewColumnSortMode.Automatic;
            protocol.Width = 150;
            // 
            // FrmPaymentMethods
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 301);
            Controls.Add(btnSave);
            Controls.Add(gridPaymentmethods);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "FrmPaymentMethods";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payment Methods";
            Load += FrmPaymentMethods_Load;
            ((System.ComponentModel.ISupportInitialize)gridPaymentmethods).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridPaymentmethods;
        private Button btnSave;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn type;
        private DataGridViewComboBoxColumn port;
        private DataGridViewComboBoxColumn protocol;
    }
}