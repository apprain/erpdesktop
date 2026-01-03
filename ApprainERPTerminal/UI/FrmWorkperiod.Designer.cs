namespace ApprainERPTerminal.UI
{
    partial class FrmWorkperiod
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
            groupBox1 = new GroupBox();
            lblWorkPeriodInfo = new Label();
            btnWorkPeriodStartStop = new Button();
            groupBox2 = new GroupBox();
            girdSessionLog = new DataGridView();
            user = new DataGridViewTextBoxColumn();
            starttime = new DataGridViewTextBoxColumn();
            endtime = new DataGridViewTextBoxColumn();
            timespent = new DataGridViewTextBoxColumn();
            cashcollected = new DataGridViewTextBoxColumn();
            note = new DataGridViewTextBoxColumn();
            btnExport = new Button();
            btnCurrencyCalculator = new Button();
            txtCashCollected = new TextBox();
            txtNote = new TextBox();
            groupBox3 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)girdSessionLog).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblWorkPeriodInfo);
            groupBox1.Controls.Add(btnWorkPeriodStartStop);
            groupBox1.Location = new Point(12, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(569, 110);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Work Period";
            // 
            // lblWorkPeriodInfo
            // 
            lblWorkPeriodInfo.AutoSize = true;
            lblWorkPeriodInfo.Location = new Point(116, 50);
            lblWorkPeriodInfo.Name = "lblWorkPeriodInfo";
            lblWorkPeriodInfo.Size = new Size(138, 15);
            lblWorkPeriodInfo.TabIndex = 1;
            lblWorkPeriodInfo.Text = "Work Period Information";
            // 
            // btnWorkPeriodStartStop
            // 
            btnWorkPeriodStartStop.Location = new Point(23, 42);
            btnWorkPeriodStartStop.Name = "btnWorkPeriodStartStop";
            btnWorkPeriodStartStop.Size = new Size(75, 33);
            btnWorkPeriodStartStop.TabIndex = 0;
            btnWorkPeriodStartStop.Text = "Start";
            btnWorkPeriodStartStop.UseVisualStyleBackColor = true;
            btnWorkPeriodStartStop.Click += btnWorkPeriodStartStop_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(girdSessionLog);
            groupBox2.Location = new Point(12, 182);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1019, 355);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Report";
            // 
            // girdSessionLog
            // 
            girdSessionLog.AllowUserToAddRows = false;
            girdSessionLog.AllowUserToDeleteRows = false;
            girdSessionLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            girdSessionLog.Columns.AddRange(new DataGridViewColumn[] { user, starttime, endtime, timespent, cashcollected, note });
            girdSessionLog.Dock = DockStyle.Fill;
            girdSessionLog.Location = new Point(3, 19);
            girdSessionLog.Name = "girdSessionLog";
            girdSessionLog.ReadOnly = true;
            girdSessionLog.RowTemplate.Height = 25;
            girdSessionLog.Size = new Size(1013, 333);
            girdSessionLog.TabIndex = 0;
            // 
            // user
            // 
            user.HeaderText = "Operator";
            user.Name = "user";
            user.ReadOnly = true;
            user.Width = 300;
            // 
            // starttime
            // 
            starttime.HeaderText = "Start Time";
            starttime.Name = "starttime";
            starttime.ReadOnly = true;
            starttime.Width = 150;
            // 
            // endtime
            // 
            endtime.HeaderText = "End Time";
            endtime.Name = "endtime";
            endtime.ReadOnly = true;
            endtime.Width = 150;
            // 
            // timespent
            // 
            timespent.HeaderText = "Time Spent";
            timespent.Name = "timespent";
            timespent.ReadOnly = true;
            // 
            // cashcollected
            // 
            cashcollected.HeaderText = "Cash Collected";
            cashcollected.Name = "cashcollected";
            cashcollected.ReadOnly = true;
            cashcollected.Width = 150;
            // 
            // note
            // 
            note.HeaderText = "Note";
            note.Name = "note";
            note.ReadOnly = true;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(924, 149);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(104, 31);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export Report";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnCurrencyCalculator
            // 
            btnCurrencyCalculator.Location = new Point(832, 148);
            btnCurrencyCalculator.Name = "btnCurrencyCalculator";
            btnCurrencyCalculator.Size = new Size(75, 31);
            btnCurrencyCalculator.TabIndex = 3;
            btnCurrencyCalculator.Text = "Calculator";
            btnCurrencyCalculator.UseVisualStyleBackColor = true;
            btnCurrencyCalculator.Click += btnCurrencyCalculator_Click;
            // 
            // txtCashCollected
            // 
            txtCashCollected.Location = new Point(180, 33);
            txtCashCollected.Name = "txtCashCollected";
            txtCashCollected.Size = new Size(213, 23);
            txtCashCollected.TabIndex = 2;
            // 
            // txtNote
            // 
            txtNote.Location = new Point(180, 68);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(213, 23);
            txtNote.TabIndex = 3;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(txtNote);
            groupBox3.Controls.Add(txtCashCollected);
            groupBox3.Location = new Point(599, 24);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(429, 110);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Closing Informaton";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(127, 68);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "Note";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 41);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 4;
            label1.Text = "Cash Collected";
            // 
            // FrmWorkperiod
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1043, 548);
            Controls.Add(groupBox3);
            Controls.Add(btnCurrencyCalculator);
            Controls.Add(btnExport);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmWorkperiod";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Work Period Management";
            Load += FrmWorkperiod_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)girdSessionLog).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnWorkPeriodStartStop;
        private GroupBox groupBox2;
        private Button btnExport;
        private Label lblWorkPeriodInfo;
        private Button btnCurrencyCalculator;
        private DataGridView girdSessionLog;
        private DataGridViewTextBoxColumn user;
        private DataGridViewTextBoxColumn starttime;
        private DataGridViewTextBoxColumn endtime;
        private DataGridViewTextBoxColumn timespent;
        private DataGridViewTextBoxColumn cashcollected;
        private DataGridViewTextBoxColumn note;
        private TextBox txtCashCollected;
        private TextBox txtNote;
        private GroupBox groupBox3;
        private Label label2;
        private Label label1;
    }
}