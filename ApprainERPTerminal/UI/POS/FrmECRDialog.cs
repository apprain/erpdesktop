using ApprainERPTerminal.Modules.ECR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmECRDialog : Form
    {
        DataRow PaymentMethod;
        double amount2Charge;
        string textReTry = "Start Payment";

        public FrmECRDialog(DataRow itemRow, double amount)
        {
            InitializeComponent();

            PaymentMethod = itemRow;
            amount2Charge = amount;

        }
        private void FrmECRDialog_Load(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Black;
            startPayment();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (btnComplete.Text.Equals(textReTry))
            {
                startPayment();
            }
            else
            {
                complete();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                complete();
            }

            return true;
        }

        private void complete()
        {
            this.Dispose();
        }



        public void updateStatus(JObject obj)
        {
            String status = obj.GetValue("status").ToString();
            lblStatus.ForeColor = Color.Red;
            if (status.Equals("SUCCESSFUL"))
            {
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                btnComplete.Text = textReTry;
            }

            btnComplete.Enabled = true;
            lblStatus.Text = status;
            lblMessage.Text = obj.GetValue("message").ToString();
        }

        private void startPayment()
        {
            // Method Data
            string spaymenttype = PaymentMethod["spaymenttype"].ToString();
            string txndata = PaymentMethod["txndata"].ToString();

            //Update Information for customer view
            lblPaymentMethodName.Text = PaymentMethod["name"].ToString();
            lblAmount.Text = App.Config().Setting("currency") + " " + amount2Charge.ToString();
            lblStatus.Text = "Transaction Processing";
            btnComplete.Enabled = false;
            btnComplete.Text = "Complete";

            // Transaction Data
            dynamic txndataObj = JsonConvert.DeserializeObject(txndata);
            String protocol = txndataObj["protocol"];


            switch (protocol)
            {
                case "NEXGO":
                    NEXGO nEXGO = new NEXGO();
                    nEXGO.sendCMD(this, amount2Charge, txndataObj);
                    break;
            }
        }
    }
}
