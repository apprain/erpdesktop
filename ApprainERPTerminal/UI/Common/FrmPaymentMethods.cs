using ApprainERPTerminal.Modules.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApprainERPTerminal.UI.Common
{
    public partial class FrmPaymentMethods : Form
    {
        private DatabaseHelper db;

        public FrmPaymentMethods()
        {
            InitializeComponent();
            this.db = new DatabaseHelper();
        }

        private void FrmPaymentMethods_Load(object sender, EventArgs e)
        {
            gridPaymentmethods.Rows.Clear();
            DataTable allPaymentMethods = db.FindAll(DatabaseHelper.INVPAYMENTMETHOD_TABLE);
            if (allPaymentMethods != null)
            {
                int i = 0;
                foreach (DataRow row in allPaymentMethods.Rows)
                {
                    DataGridViewComboBoxCell CB = new DataGridViewComboBoxCell();

                    string[] ports = SerialPort.GetPortNames();
                    CB.Items.Add("");
                    if (ports.Length > 0)
                    {
                        foreach (string comport in ports)
                        {
                            CB.Items.Add(comport.ToString());
                        }
                    }


                    String port = "";
                    String protocol = "";
                    if (!row["txndata"].Equals(""))
                    {
                        Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(row["txndata"].ToString());

                        if (dictionary.ContainsKey("port"))
                        {
                            port = dictionary.GetValueOrDefault("port");
                        }

                        if (dictionary.ContainsKey("port"))
                        {
                            protocol = dictionary.GetValueOrDefault("protocol");
                        }
                        Debug.WriteLine(port);
                    }

                    gridPaymentmethods.Rows.Add(row["name"], row["spaymenttype"], "", "");
                    gridPaymentmethods.Rows[i].Cells[0].ToolTipText = row["id"].ToString();
                    gridPaymentmethods.Rows[i].Cells[2] = CB;
                    gridPaymentmethods.Rows[i].Cells[2].Value = port;
                    gridPaymentmethods.Rows[i].Cells[3].Value = protocol;

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridPaymentmethods.Rows.Count == 0) {
                return;
            }

            for(int i = 0; i < gridPaymentmethods.Rows.Count; i++)
            {
                string id = gridPaymentmethods.Rows[i].Cells[0].ToolTipText.ToString();
                string port = "";
                string protocol = "";

                if (gridPaymentmethods.Rows[i].Cells[2].Value != null)
                {
                    port = gridPaymentmethods.Rows[i].Cells[2].Value.ToString();
                }

                if (gridPaymentmethods.Rows[i].Cells[3].Value != null)
                {
                    protocol = gridPaymentmethods.Rows[i].Cells[3].Value.ToString();
                }

                string str = "{\"port\":\"" + port + "\", \"protocol\":\"" + protocol + "\" }";

                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("id", id);
                values.Add("txndata", str);
                db.Save(DatabaseHelper.INVPAYMENTMETHOD_TABLE, values);

                MessageBox.Show("Saved Successfull");
            }
        }
    }
}
