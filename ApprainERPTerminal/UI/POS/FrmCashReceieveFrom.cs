using ApprainERPTerminal.Modules.Inventory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmCashFrom : Form
    {
        TextBox txtAmount;
        public FrmPOS frmPOS;
        private double payable;
        public FrmCashFrom(FrmPOS masterForm, double payable)
        {
            InitializeComponent();

            this.frmPOS = masterForm;
            this.payable = payable;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

        }

        private void FrmCashFrom_Load(object sender, EventArgs e)
        {
            loadNumberPad();
            loadOtherAmount();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Escape key is pressed
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Close the form
            }
        }

        private void loadOtherAmount()
        {
            float fontsize = 12f;
            var panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;

            panel.ColumnCount = 3;
            panel.RowCount = 6;

            for (int i = 0; i < 6; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            }

            string[,] NumberPad = new string[6, 3] {
                    { "5000", "3000", "1000" } ,
                    { "500", "300", "100" } ,
                    { "50", "25", "4" } ,
                    { "45", "20", "3" } ,
                    { "40", "15", "2" } ,
                    { "++", "10", "Clear" }
            };

            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Button btnNPad = new Button();
                   
                    btnNPad.Text = NumberPad[j,i];                       
                    btnNPad.Dock = DockStyle.Fill;
                    btnNPad.FlatStyle= FlatStyle.Flat;
                    btnNPad.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#92BEE5");
                    btnNPad.ForeColor = ColorTranslator.FromHtml("#2A3F6E");
                    btnNPad.BackColor = ColorTranslator.FromHtml("#92BEE5");
                    btnNPad.Click += (sender, EventArgs) =>
                    {
                        if (btnNPad.Text.Equals("Clear"))
                        {
                            setValue("");
                        }
                        else if (btnNPad.Text.Equals("++"))
                        {
                            double val = getValue();
                            val++;
                            setValue(val.ToString());

                        }
                        else
                        {
                            setValue(btnNPad.Text);
                        }

                    };
                    btnNPad.Font = new Font("Segoe UI Black", fontsize, FontStyle.Bold, GraphicsUnit.Point);
                    panel.Controls.Add(btnNPad, i, j);
                }
            }
            tblLayoutCashReceiveBase.Controls.Add(panel, 0, 0);
        }


        
        private void loadNumberPad()
        {
            float fontsize = 25f;
            var panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.ColumnCount = 3;
            panel.RowCount = 1;

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            Label lblAmount = new Label();
            lblAmount.Text = "Total";
            lblAmount.AutoSize = false;
            lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblAmount.Dock = DockStyle.Fill;
            lblAmount.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            panel.Controls.Add(lblAmount, 0, 0);

            Button btnAmount = new Button();
            btnAmount.Text = payable.ToString();
            btnAmount.Dock = DockStyle.Fill;
            btnAmount.FlatStyle = FlatStyle.Popup;
            btnAmount.BackColor = Color.Cyan;
            btnAmount.Font = new Font("Segoe UI Black", fontsize, FontStyle.Bold, GraphicsUnit.Point);
            panel.Controls.Add(btnAmount, 1, 0);
            tblLayoutCashReceiveRight.Controls.Add(panel, 0, 0);


            /////
            // Second row
            panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.ColumnCount = 3;
            panel.RowCount = 1;

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            txtAmount = new TextBox();
            txtAmount.Text = "";
            txtAmount.AutoSize = false;
            txtAmount.TextAlign = HorizontalAlignment.Right;
            txtAmount.Dock = DockStyle.Fill;
            txtAmount.Font = new Font("Segoe UI Black", 30F, FontStyle.Bold, GraphicsUnit.Point);
            txtAmount.KeyPress += (sender, EventArgs) => {
                if (EventArgs.KeyChar == (char)Keys.Enter)
                {
                     compeltePayment();
                }
            };
            panel.Controls.Add(txtAmount, 0, 0);

            Button btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Click += (sender, EventArgs) => { backSpace(); };
            btnBack.Dock = DockStyle.Fill;
            btnBack.Font = new Font("Segoe UI Black", 15F, FontStyle.Bold, GraphicsUnit.Point);
            panel.Controls.Add(btnBack, 1, 0);
            tblLayoutCashReceiveRight.Controls.Add(panel, 0, 1);


            /////////// Numbers 
            panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;

            panel.ColumnCount = 3;
            panel.RowCount = 4;


            string[,] NumberPad = new string[4,3] { 
                    { "7", "8", "9" } ,
                    { "4", "5", "6" } ,
                    { "1", "2", "3" } ,
                    { ".", "0", "Enter" } 
            };

            for (int i = 0; i < 4; i++)
            {
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            }

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Button btnNPad = new Button();
                    btnNPad.Text = NumberPad[j,i];
                    btnNPad.Click += (sender, EventArgs) => {
                        if (btnNPad.Text.Equals("Enter"))
                        {
                            compeltePayment();
                        }
                        else
                        {
                            addNumber(btnNPad.Text);
                        }                         
                    };

                   
                    btnNPad.Dock = DockStyle.Fill;
                    btnNPad.Font = new Font("Segoe UI Black", fontsize, FontStyle.Bold, GraphicsUnit.Point);
                    panel.Controls.Add(btnNPad,i,j);
                }
            }
            tblLayoutCashReceiveRight.Controls.Add(panel, 0, 2);

            //////////// Bottom
            Button btnFullAmount = new Button();
            btnFullAmount.Text = "Enter " + App.Config().Setting("currency") + " " + payable.ToString();
            btnFullAmount.Click += (sender, EventArgs) => {
                setValue(payable.ToString());
            };
            btnFullAmount.Dock = DockStyle.Fill;
            btnFullAmount.Font = new Font("Segoe UI Black", 15F, FontStyle.Bold, GraphicsUnit.Point);
            tblLayoutCashReceiveRight.Controls.Add(btnFullAmount, 0, 3);

            this.ActiveControl = txtAmount;

        }

        private void compeltePayment()
        {
            if (getValue() > 0)
            {
                frmPOS.CashReceive(getValue());
                frmPOS.CompletePayment();
            }
            this.Dispose();
        }
        private double getValue()
        {
            if (txtAmount.Text.Equals(""))
            {
                return 0;
            }
            else
            {

                return Convert.ToDouble(txtAmount.Text);
            }
        }


        private void setValue(String value)
        {
            txtAmount.Text = value;
        }

        private void addNumber(String value)
        {
            txtAmount.Text = txtAmount.Text + value;
        }
        private void backSpace()
        {
            if(txtAmount.Text.Length == 0) return;

            if (txtAmount.Text.Length == 1)
            {
                txtAmount.Text = "";
            }
            else
            {
                txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
            }
        }
    }
}
