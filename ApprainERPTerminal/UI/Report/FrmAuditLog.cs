using ApprainERPTerminal.Modules;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal.UI.Report
{
    public partial class FrmAuditLog : Form
    {
        DatabaseHelper db;

        public FrmAuditLog()
        {
            db = new DatabaseHelper();
            InitializeComponent();
        }

        private void FrmAuditLog_Load(object sender, EventArgs e)
        {
            Display();
        }


        private void Display()
        {
            DateTime selectedDate = fromDate.Value;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");

            string queryCondition = $"DATE(entrydate) = '{formattedDate}' ORDER BY id DESC";
            DataTable all = db.FindAll(DatabaseHelper.LOG_TABLE, queryCondition);

            dataGridAuditLog.Rows.Clear();
            if (all.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
                {
                    string name = "";
                    DataRow Admin = db.Find(DatabaseHelper.ADMIN_TABLE, $"id={row["operator"]}");
                    if (Admin != null)
                    {
                        name = Admin["f_name"] + " " + Admin["l_name"];
                    }



                    dataGridAuditLog.Rows.Add(
                                row["id"],
                                row["action"],
                                row["fkeytype"],
                                row["message"],
                                row["entrydate"],
                                name
                         );
                }
            }
        }


        private void dataGridAuditLog_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dataGridAuditLog.Rows[e.RowIndex].Cells[0].Value.ToString();
                DataRow record = db.Find(DatabaseHelper.LOG_TABLE, "id=" + id);
                textSummaryBox.Text = record["data"].ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            Display();
        }
    }
}
