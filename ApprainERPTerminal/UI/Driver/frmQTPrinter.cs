using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Printer;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing.Printing;

namespace ApprainERPTerminal.UI.Driver
{
    public partial class frmQTPrinter : Form
    {
        private ERPCloud eRPCloud;
        private ComboBox cmbPOSPrintersList;
        private DatabaseHelper db;
        private String configlefile = "QTPrinterMap.json";

        public frmQTPrinter()
        {
            InitializeComponent();
            this.eRPCloud = new ERPCloud();
        }

        private void frmQTPrinter_Load(object sender, EventArgs e)
        {
            db = new DatabaseHelper();
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            cmb.Name = "Printer";
            cmb.HeaderText = "Printer";
            cmb.Width = 400;

            cmb.Items.Add("");
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                cmb.Items.Add((object)installedPrinter.ToString());
            }
            dataGridQTPrinterSetup.Columns.Add(cmb);

            DataTable CategoryList = db.FindAll(DatabaseHelper.CATEGORY_TABLE, "type='invitemcat'");
            DataGridViewComboBoxColumn cat = new DataGridViewComboBoxColumn();
            cat.Name = "Category";
            cat.HeaderText = "Category";
            cat.Width = 400;
            cat.Items.Add("");
            foreach (DataRow row in CategoryList.Rows)
            {
                cat.Items.Add(row["id"].ToString() + ":" + row["title"].ToString());
            }
            dataGridQTPrinterSetup.Columns.Add(cat);

            txtNoOfKOTCopy.Text = App.Config().Setting("no_of_kot_copy","1");

            loadData();
        }

        private void loadData()
        {
            try
            {
                if (File.Exists(path()))
                {
                    dataGridQTPrinterSetup.Rows.Clear();
                    string stream = File.ReadAllText(path());
                    if (stream != null && stream.Length > 0)
                    {
                        JArray jArray = JArray.Parse(stream);

                        for (int i = 0; i < jArray.Count; i++)
                        {
                            JObject PrinterConfig = (JObject)jArray[i];

                            //PrinterConfig.GetValue("Category").ToString()

                            String Id = PrinterConfig.GetValue("CategoryId").ToString();
                            DataRow DataRow = db.Find(DatabaseHelper.CATEGORY_TABLE, "id=" + Id);
                            if (DataRow == null)
                            {
                                dataGridQTPrinterSetup.Rows.Add(PrinterConfig.GetValue("Printer").ToString(), "");
                            }
                            else
                            {
                                dataGridQTPrinterSetup.Rows.Add(PrinterConfig.GetValue("Printer").ToString(), PrinterConfig.GetValue("Category").ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                JArray list = new JArray();
                JObject PrinterConfig = new JObject();
                string category = ""; string printer = ""; string categoryid = "";
                if (dataGridQTPrinterSetup.Rows.Count > 0)
                {
                    for (int index = 0; index < dataGridQTPrinterSetup.Rows.Count - 1; index++)
                    {
                        category = "";
                        categoryid = "";
                        printer = "";
                        if (dataGridQTPrinterSetup.Rows[index].Cells[0].Value != null)
                        {
                            printer = dataGridQTPrinterSetup.Rows[index].Cells[0].Value.ToString();
                        }

                        if (dataGridQTPrinterSetup.Rows[index].Cells[1].Value != null && !dataGridQTPrinterSetup.Rows[index].Cells[1].Value.Equals(""))
                        {
                            category = dataGridQTPrinterSetup.Rows[index].Cells[1].Value.ToString();
                            int charLocation = category.IndexOf(":", StringComparison.Ordinal);
                            categoryid = category.Substring(0, charLocation);
                        }
                        if (!string.IsNullOrEmpty(printer) && !string.IsNullOrEmpty(categoryid))
                        {
                            list.Add(JObject.FromObject(new { Printer = printer, Category = category, CategoryId = categoryid }));
                        }
                    }
                }
                File.WriteAllText(path(), list.ToString());
                loadData();
                MessageBox.Show("Data saved successfully.\n\n**Restart the application again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string path()
        {
            return App.DATA_PATH + App.DS + configlefile;
        }

        private void txtNoOfKOTCopy_TextChanged(object sender, EventArgs e)
        {
            App.Config().Update("no_of_kot_copy", txtNoOfKOTCopy.Text);
        }
    }
}
