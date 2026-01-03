using ApprainERPTerminal.Modules.Inventory;
using ApprainERPTerminal.Modules;
using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmSelectWaiter : Form
    {
        private DatabaseHelper db;
        private string TableId;

        public FrmSelectWaiter(string table)
        {
            TableId = table;
            db = new DatabaseHelper();

            InitializeComponent();
        }

        private void FrmSelectWaiter_Load(object sender, EventArgs e)
        {
            loadPaymentMethodGrid();
        }

        private void loadPaymentMethodGrid()
        {
            pnlWaiterPanel.Controls.Clear();
            DataTable UserRecords = db.FindAll(DatabaseHelper.ADMIN_TABLE);
            int i = 0;
            if (UserRecords != null)
            {
                int size = UserRecords.Rows.Count;
                var panel = new TableLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                panel.ColumnCount = 1;

                for (i = 0; i < size; i++)
                {
                    var percent = 100f;
                    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
                    panel.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
                }


                Button btn = new Button();
                foreach (DataRow row in UserRecords.Rows)
                {
                    bool exists = false;
                    try
                    {
                        string json = row["aclobject"].ToString();
                        JObject data = JObject.Parse(json);
                        var inventoryUserCommonParam = data["inventoryaclopts"]["inventory_user_common_parameter"];

                        exists = inventoryUserCommonParam != null && inventoryUserCommonParam.ToObject<List<string>>().Contains("waiterlist");
                    }
                    catch (Exception ex) { }


                    if (exists)
                    {
                        btn = new Button();
                        btn.Dock = DockStyle.Fill;
                        btn.Text = row["f_name"].ToString() + " " + row["l_name"].ToString();
                        btn.Name = row["id"].ToString();
                        btn.Click += new System.EventHandler(btn_Click);
                        btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        panel.Controls.Add(btn);
                    }
                }
                pnlWaiterPanel.Controls.Add(panel);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string UserId = (sender as Button).Name;

            App.Common().setWaiter(TableId, UserId);

            this.Dispose();
            new FrmPOS(TableId).ShowDialog();
        }
    }
}
