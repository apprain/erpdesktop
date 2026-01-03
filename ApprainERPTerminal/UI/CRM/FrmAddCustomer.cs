using ApprainERPTerminal.Modules;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace ApprainERPTerminal.UI.CRM
{
    public partial class FrmAddCustomer : Form
    {
        private HttpClient client;
        private Config config;
        private string UrlAuth;
        private string UrlDataExchnage;
        private DatabaseHelper db;

        public FrmAddCustomer()
        {
            InitializeComponent();

            this.UrlAuth = "";
            this.UrlDataExchnage = "";
            this.config = new Config();
            this.client = new HttpClient();
            this.UrlAuth = this.config.AuthUrl();
            this.UrlDataExchnage = this.config.UrldataExchnage();
            this.db = new DatabaseHelper();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveCustomer();
        }


        private async Task saveCustomer()
        {
            Dictionary<string, string> Collection = new Dictionary<string, string>();
            Collection.Add("token", config.Setting("token"));
            Collection.Add("timestamp", config.Setting("timestamp"));
            Collection.Add("com", "Admission");
            Collection.Add("action", "savePerson");
            Collection.Add("branchcode", "101");
            Collection.Add("rollno", "");
            Collection.Add("department", "13");
            Collection.Add("fname", txtFName.Text.ToString());
            Collection.Add("lname", txtLastName.Text.ToString());
            Collection.Add("phoneno", txtPhoneno.Text.ToString());
            Collection.Add("email", txtEmailAddress.Text.ToString());
            Collection.Add("address", txtAddress.Text.ToString());
            Collection.Add("city", txtCity.Text.ToString());
            Collection.Add("address2", txtPostcode.Text.ToString());
            Collection.Add("socialid", txtSocialId.Text.ToString());
            Collection.Add("remark", txtRemark.Text.ToString());

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)Collection);

            var response = await client.PostAsync(config.UrldataExchnage(), content);
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);

            dynamic dynObj = JsonConvert.DeserializeObject(responseString);
            String status = dynObj["status"];
            String state = dynObj["state"];
            if (status.Equals("1"))
            {
                if (state.Equals("100"))
                {
                    MessageBox.Show("Customer added successfully");
                }
                else
                {
                    MessageBox.Show((string)dynObj["message"]);
                }
            }
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private async Task fetchCustomers()
        {

            string srckey = txtSrcKey.Text.ToString();
            if (srckey.Equals("")) return;

            Dictionary<string, string> Collection = new Dictionary<string, string>();
            Collection.Add("token", config.Setting("token"));
            Collection.Add("timestamp", config.Setting("timestamp"));
            Collection.Add("com", "Dataexpert");
            Collection.Add("action", "fetchFromModel");
            Collection.Add("method", "findAll");
            Collection.Add("sourcename", "Admentry");
            Collection.Add("condition", "rollno='" + srckey + "' OR phoneno LIKE '%" + srckey + "%' OR CONCAT(fname, ' ', lname) LIKE '%" + srckey + "%'");

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)Collection);

            var response = await client.PostAsync(config.UrldataExchnage(), content);
            var responseString = await response.Content.ReadAsStringAsync();
           // Debug.WriteLine(responseString);

            dynamic dynObj = JsonConvert.DeserializeObject(responseString);
            String status = dynObj["status"];
            if (status.Equals("1"))
            {
                try
                {
                    String data = Convert.ToString(dynObj["data"]["data"]);

                    if (!data.Equals(""))
                    {
                        JArray jarray = JArray.Parse(data);
                        if (jarray != null)
                        {
                            IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                            gridCustomerList.Rows.Clear();
                            while (enumerator.MoveNext())
                            {
                                try
                                {
                                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                    gridCustomerList.Rows.Add(values["rollno"].ToString(), values["fname"].ToString() + " " + values["lname"].ToString(), values["phoneno"].ToString());

                                }
                                catch (Exception ex) { }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fetchCustomers();
        }

        private void FrmAddCustomer_Load(object sender, EventArgs e)
        {
            DataTable deptlist = db.FindAll(DatabaseHelper.CATEGORY_TABLE, " type='department' ");
            foreach (DataRow row in deptlist.Rows)
            {
                cmboDepartment.Items.Add(row["title"]);
            }

        }

    }
}
