using ApprainERPTerminal.UI.POS;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal.UI
{
    public partial class FrmWorkperiod : Form
    {
        private DatabaseHelper db;
        private bool isPeriodActive = false;

        public FrmWorkperiod()
        {
            InitializeComponent();
        }

        private void FrmWorkperiod_Load(object sender, EventArgs e)
        {
            db = new DatabaseHelper();

            InitializeWorkPeriodInfo();
            loadSessionLog();
            controlAddInputfields();
        }


        private void InitializeWorkPeriodInfo()
        {
            String workPeriodTime = App.Config().Setting("workperiodtime");
            isPeriodActive = false;
            lblWorkPeriodInfo.Text = dataforamte(workPeriodTime);

            if (!String.IsNullOrEmpty(workPeriodTime))
            {
                btnWorkPeriodStartStop.Text = "End Period";
                isPeriodActive = true;
            }
        }

        private void controlAddInputfields()
        {
            String workPeriodTime = App.Config().Setting("workperiodtime");

            txtCashCollected.Enabled = false;
            txtNote.Enabled = false;
            if (!String.IsNullOrEmpty(workPeriodTime))
            {
                txtCashCollected.Enabled = true;
                txtNote.Enabled = true;
            }
        }


        private string dataforamte(String dt)
        {
            if (dt.Equals(""))
            {
                return "";
            }

            return DateTime.ParseExact(dt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
        }

        private void btnWorkPeriodStartStop_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string currentime = now.ToString("yyyy-MM-dd HH:mm:ss");
            DataRow Record = db.Find(DatabaseHelper.WORKPERIOD_TABLE, "id > 0 ORDER BY id DESC");

            if (isPeriodActive)
            {
                String starttimeStr = App.Config().Setting("workperiodtime");
                DateTime starttime;
                int timespent = 0;

                if (!string.IsNullOrEmpty(starttimeStr) && DateTime.TryParse(starttimeStr, out starttime))
                {
                    TimeSpan timeDifference = now - starttime;
                    timespent = timeDifference.Minutes;
                }
                App.Config().Update("workperiodtime", "");

                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "id", Record["id"].ToString() },
                    { "endtime", currentime },
                    { "timespent", timespent.ToString() },
                    { "cashcollected", txtCashCollected.Text.ToString() },
                    { "note", txtNote.Text.ToString() }
                };
                db.Save(DatabaseHelper.WORKPERIOD_TABLE, values);

                btnWorkPeriodStartStop.Text = "Start Period";
                isPeriodActive = false;
            }
            else
            {

                lblWorkPeriodInfo.Text = currentime;
                App.Config().Update("workperiodtime", currentime);

                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "operator", App.Config().Setting("adminref") ?? "" },
                    { "starttime", currentime }
                };
                db.Save(DatabaseHelper.WORKPERIOD_TABLE, values);
                btnWorkPeriodStartStop.Text = "End Period";
                isPeriodActive = true;
            }

            loadSessionLog();
            controlAddInputfields();
        }

        private void loadSessionLog()
        {
            DataTable all = db.FindAll(DatabaseHelper.WORKPERIOD_TABLE, "id > 0 ORDER BY id DESC");
            DataRow Record;
            string name = "";

            if (all.Rows.Count <= 0) return;

            girdSessionLog.Rows.Clear();

            foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
            {
                DataGridViewButtonColumn View = new DataGridViewButtonColumn();
                name = "<" + row["operator"] + ">";
                Record = db.Find(DatabaseHelper.ADMIN_TABLE, "id=" + row["operator"].ToString());
                if (Record != null)
                {
                    name = Record["f_name"] + " " + Record["l_name"];
                }

                girdSessionLog.Rows.Add(
                    name,
                    dataforamte(row["starttime"].ToString()),
                    dataforamte(row["endtime"].ToString()),
                    ConvertMinutesToHoursAndMinutes(row["timespent"].ToString()),
                    row["cashcollected"],
                    row["note"]
                );
            }
        }

        public static string ConvertMinutesToHoursAndMinutes(string totalMinutesString)
        {
            if (int.TryParse(totalMinutesString, out int totalMinutes))
            {
                int hours = totalMinutes / 60;
                int minutes = totalMinutes % 60;

                string result = "";

                if (hours > 0)
                {
                    result += $"{hours}hr";
                }

                if (minutes > 0)
                {
                    if (result != "") result += ",";
                    result += $"{minutes}min";
                }

                return result == "" ? "0min" : result;
            }
            else
            {
                return "";
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCurrencyCalculator_Click(object sender, EventArgs e)
        {
            new FrmCashCounter().ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable("WorkperiodReport");

            dataTable.Columns.Add("NAME", typeof(string));
            dataTable.Columns.Add("IN", typeof(string));
            dataTable.Columns.Add("OUT", typeof(string));
            dataTable.Columns.Add("TIME SPENT", typeof(string));
            dataTable.Columns.Add("CASH", typeof(string));
            dataTable.Columns.Add("NOTE", typeof(string));



            DataTable all = db.FindAll(DatabaseHelper.WORKPERIOD_TABLE, "id > 0 ORDER BY id DESC");
            DataRow Record;
            string name = "";

            if (all.Rows.Count <= 0) return;


            foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
            {
                DataGridViewButtonColumn View = new DataGridViewButtonColumn();
                name = "<" + row["operator"] + ">";
                Record = db.Find(DatabaseHelper.ADMIN_TABLE, "id=" + row["operator"].ToString());
                if (Record != null)
                {
                    name = Record["f_name"] + " " + Record["l_name"];
                }

                dataTable.Rows.Add(
                    name,
                    dataforamte(row["starttime"].ToString()),
                    dataforamte(row["endtime"].ToString()),
                    ConvertMinutesToHoursAndMinutes(row["timespent"].ToString()),
                    row["cashcollected"],
                    row["note"]
                );
            }



            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.FileName = "WorkperiodReport.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(dataTable, saveFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("File save cancelled.");
                }
            }
        }

        static void ExportToExcel(DataTable dataTable, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                try
                {
                    var worksheet = workbook.Worksheets.Add(dataTable, "Sheet1");
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

    }
}
