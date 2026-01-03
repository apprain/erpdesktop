using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Printer;
using ApprainERPTerminal.UI.Inventory;
using ApprainERPTerminal.UI.Report;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmTable : Form
    {
        private DatabaseHelper db;

        public FrmTable()
        {
            InitializeComponent();
        }

        String changeTableId = "";
        public FrmTable(String id)
        {
            changeTableId = id;
            InitializeComponent();
        }

        private void FrmTable_Load(object sender, EventArgs e)
        {
            db = new DatabaseHelper();

            if (!changeTableId.Equals(""))
            {
                loadButtons("Open");
            }
            else
            {
                loadButtons("All");
            }

        }

        private void loadButtons(String status)
        {


            pnlTableList.Controls.Clear();
            DataTable ItemDataTable;
            if (status.Equals("All"))
            {
                ItemDataTable = db.FindAll(DatabaseHelper.RESTAURANT_TABLE_TABLE); ;
            }
            else
            {
                ItemDataTable = db.FindAll(DatabaseHelper.RESTAURANT_TABLE_TABLE, "status='" + status + "'"); ;
            }
            int entryCount = ItemDataTable.Rows.Count;

            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.ColumnCount = 8;
            panel.RowCount = entryCount / panel.ColumnCount;
            if ((entryCount % panel.ColumnCount) > 0)
            {
                panel.RowCount++;
            }

            IEnumerator enumerator = ItemDataTable.Rows.GetEnumerator();
            for (int j = 0; j < panel.RowCount; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    enumerator.MoveNext();

                    if (enumerator.Current != null)
                    {
                        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
                        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
                        panel.Controls.Add(addPanel((DataRow)enumerator.Current), i, j);
                    }
                }
            }

            pnlTableList.Controls.Add(panel);
        }

        List<string> tableIds = new List<string>();
        private Button addPanel(DataRow Item)
        {
            String tableid = Item["id"].ToString();
            String lastinvoice = Item["lastinvoice"].ToString();

            double received = 0;
            if (!string.IsNullOrEmpty(lastinvoice))
            {
                JObject lastInvoice = JObject.Parse(lastinvoice);
                JObject head = JObject.Parse(lastInvoice.GetValue("head").ToString());
                if (head.ContainsKey("received"))
                {
                    received = Convert.ToDouble(head.GetValue("received").ToString());
                }
            }

            String color = "#3785FA";
            if (Item["status"].ToString().Equals("Dining")) color = "#339900";
            if (Item["status"].ToString().Equals("Merged")) color = "#000000";
            if (Item["status"].ToString().Equals("Closed")) color = "#FF3333";
            if (Item["status"].ToString().Equals("Billed")) color = "#F23737";

            // Top Title
            Button btnTable = new Button();
            btnTable.Font = new Font("Segoe UI Black", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnTable.Text = Item["name"].ToString().ToUpper();
            if (received > 0)
            {
                btnTable.Text += "\n(" + received + App.Config().Setting("currency") + ")";
                color = "#FF0000";
            }


            btnTable.BackColor = ColorTranslator.FromHtml(color);
            btnTable.FlatStyle = FlatStyle.Flat;
            btnTable.FlatAppearance.BorderColor = ColorTranslator.FromHtml(color);
            btnTable.Dock = DockStyle.Top; ;
            btnTable.ForeColor = Color.White;
            btnTable.Height = 100;
            btnTable.TextAlign = ContentAlignment.MiddleCenter;
            btnTable.Click += (sender, EventArgs) =>
            {
                var button = sender as Button;
                if (PrintKot)
                {
                    new PrintKOT(tableid).Print();
                    PrintKot = false;
                    loadButtons("All");
                }
                else if (mergeFlag)
                {
                    if (button != null)
                    {
                        if (tableIds.Contains(tableid))
                        {
                            tableIds.Remove(Item["id"].ToString());
                            button.BackColor = ColorTranslator.FromHtml(color);
                        }
                        else
                        {

                            button.BackColor = Color.BlueViolet;
                            tableIds.Add(Item["id"].ToString());
                        }
                    }
                }
                else if (!changeTableId.Equals(""))
                {

                    DataRow OldRow = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + changeTableId);
                    DataRow NewRow = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableid);

                    Dictionary<string, string> values = new Dictionary<string, string>
                    {
                        { "id", tableid },
                        { "lastinvoice", OldRow["lastinvoice"].ToString()},
                        { "qtdata", OldRow["qtdata"].ToString()},
                        { "status", OldRow["status"].ToString()}
                    };
                    db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);

                    values = new Dictionary<string, string>
                    {
                        { "id", changeTableId },
                        { "lastinvoice", ""},
                        { "qtdata", ""},
                        { "status", "Open"}
                    };
                    db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);

                    this.Dispose();
                    new FrmPOS(Item["id"].ToString()).ShowDialog();
                }
                else
                {
                    if (Item["status"].ToString().Equals("Merged"))
                    {
                        DialogResult result = MessageBox.Show(
                                "Are you sure you want to unmerge this table?",
                                "Confirm Unmerge",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            button.BackColor = ColorTranslator.FromHtml("#2d84e1");

                            Dictionary<string, string> values = new Dictionary<string, string>
                            {
                                { "id", tableid },
                                { "status", "Open"}
                            };
                            db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);

                            Console.WriteLine("Table unmerged successfully.");
                        }

                        return;
                    }
                    else
                    {
                        if (Item["status"].ToString().Equals("Open"))
                        {
                            App.Common().setWaiter(Item["id"].ToString(), "");
                        }

                        this.Dispose();
                        string[] common = Utility.InvoiceCommonParameters();
                        if (Item["status"].ToString().Equals("Open") && common.Contains("waiterlist"))
                        {
                            new FrmSelectWaiter(Item["id"].ToString()).ShowDialog();
                        }
                        else
                        {
                            new FrmPOS(Item["id"].ToString()).ShowDialog();
                        }
                    }
                }

            };

            return btnTable;
        }

        private void btnAllTables_Click(object sender, EventArgs e)
        {
            loadButtons("All");
        }

        private void btnTableBusy_Click(object sender, EventArgs e)
        {
            loadButtons("Dining");
        }

        private void btnTableBilled_Click(object sender, EventArgs e)
        {
            loadButtons("Billed");
        }

        bool mergeFlag = false;
        private void btnMerged_Click(object sender, EventArgs e)
        {
            if (mergeFlag == false)
            {
                btnMerged.Text = "Apply";
                btnMerged.BackColor = Color.Red;
                mergeFlag = true;
            }
            else
            {
                foreach (string tableId in tableIds)
                {
                    Dictionary<string, string> values = new Dictionary<string, string>
                    {
                        { "id", tableId },
                        { "status", "Merged"}
                    };
                    db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);
                }


                mergeFlag = false;
                btnMerged.BackColor = SystemColors.Control;
                loadButtons("All");
                btnMerged.Text = "Merge";
            }
        }

        bool PrintKot = false;
        private void btnPrintKOT_Click(object sender, EventArgs e)
        {
            if (!App.Utility().checkPinVerified("PrintKOT")) return;

            if (PrintKot)
            {
                loadButtons("All");
            }
            else
            {
                loadButtons("Dining");

            }
            PrintKot = !PrintKot;

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmSales().ShowDialog();
        }

        private void manageProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmManageItems().ShowDialog();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmPreference().ShowDialog();
        }
    }
}
