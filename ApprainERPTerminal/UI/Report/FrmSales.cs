
using System.Text.Json;

using System.Data;

using System.Text;

using System.Drawing.Printing;
using ApprainERPTerminal.Modules;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace ApprainERPTerminal.UI.Report
{
    public partial class FrmSales : Form
    {
        private DatabaseHelper db;
        int TW = 37;
        public FrmSales()
        {
            db = new DatabaseHelper();
            InitializeComponent();
        }


        // Display Report
        private void DisplayReport()
        {
            // Use StringBuilder to build the report
            string Name = "";
            var reportBuilder = new StringBuilder();
            DateTime selectedDate = fromDate.Value;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd hh:mm:ss tt");

            // Summary Section

            //reportBuilder.AppendLine(Utility.Center(App.Config().Setting("site_title"), TW));
            reportBuilder.AppendLine(Utility.Center(formattedDate, TW));
            reportBuilder.AppendLine("\n");
            reportBuilder.AppendLine(Utility.Center("=== SUMMARY ===", TW));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Total Sales", Math.Round(Total, 2)));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Discount", Math.Round(discount, 2)));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Total Received", Math.Round(totaleceived, 2)));
            reportBuilder.AppendLine(Utility.Center("\n", TW));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Return Amount", Math.Round(returnamount, 2))); 
            reportBuilder.AppendLine(Utility.Center("\n", TW));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Cash Received", Math.Round(cashreceived, 2)));
            reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Bank Received", Math.Round(Otherpaymentreceived, 2)));
            

            // Other Payments Section
            if (otherpayment != null && otherpayment.Any())
            {
                var groupedData = otherpayment
                    .GroupBy(item => item.FirstColumn)
                    .Select(group => new
                    {
                        FirstColumn = group.Key,
                        SecondColumnSum = group.Sum(item => item.SecondColumn)
                    });
                //  reportBuilder.AppendLine(" ");
                reportBuilder.AppendLine(Utility.Center("Bank Received Details", TW));
                foreach (var item in groupedData)
                {
                    Name = Convert.ToString(item.FirstColumn);
                    DataRow row = db.Find(DatabaseHelper.INVPAYMENTMETHOD_TABLE, "id=" + Name);
                    if (row != null)
                    {
                        Name = row["name"].ToString();
                    }
                    reportBuilder.AppendLine(string.Format("{0,-20} {1,10:N2}", Name, Math.Round(item.SecondColumnSum)));
                }

            }
            else
            {
                // No data available
                reportBuilder.AppendLine(" ");
                reportBuilder.AppendLine(Utility.Center("=== OTHER PAYMENT ===", TW));
                reportBuilder.AppendLine(Utility.Center("No data available.", TW));
            }



            DataTable all = db.FindAll(DatabaseHelper.RESTAURANT_TABLE_TABLE, "lastinvoice <> ''");
            if (all.Rows.Count > 0)
            {
                reportBuilder.AppendLine(Utility.Center("\n", TW));
                reportBuilder.AppendLine(Utility.Center("=== ADVANCE SUMMARY ===", TW));

                double total_sum = 0;
                foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
                {
                    JObject InvoiceJObj = JObject.Parse(row["lastinvoice"].ToString());
                    JObject InvioceHead = JObject.Parse(InvoiceJObj.GetValue("head").ToString());
                    double sum = 0;
                    if (InvioceHead.ContainsKey("paymentreceivedlist"))
                    {
                        JObject otherPRListJObj = JObject.Parse(InvioceHead.GetValue("paymentreceivedlist").ToString());
                        foreach (var prop in otherPRListJObj.Properties())
                        {
                            string valStr = prop.Value.ToString();

                            if (!string.IsNullOrWhiteSpace(valStr)) // skip empty values
                            {
                                if (double.TryParse(valStr, out double num))
                                {
                                    sum += num;
                                }
                            }
                        }
                    }

                    reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", row["name"], Math.Round(sum, 2)));
                    total_sum += sum;

                }
                reportBuilder.AppendLine(string.Format("{0,-22}: {1,10:F2}", "Total", Math.Round(total_sum, 2)));
            }


                    // Set the report text
                    reportResult.Text = reportBuilder.ToString();
        }



        double totaleceived;
        double Otherpaymentreceived;
        double Total;
        List<(double FirstColumn, double SecondColumn)> otherpayment; //= new List<(double FirstColumn, double SecondColumn)> { };
        double cashreceived;
        double discount;
        double returnamount;
        private PrintDocument printDocument1;

        private void Display() 
        {
            Otherpaymentreceived = 0;
            totaleceived = 0;
            Total = 0;
            discount = 0;
            returnamount = 0;
            cashreceived = 0;
            otherpayment = new List<(double FirstColumn, double SecondColumn)> { };

            reportResult.Text = "";
            DateTime selectedDate = fromDate.Value;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            string queryCondition = $"DATE(entrydate) = '{formattedDate}' ORDER BY id DESC";
            string queryCondition2 = $"DATE(entrydate) = '{formattedDate}' and status='Complete' ORDER BY id DESC";
            DataTable all = db.FindAll(DatabaseHelper.INVOICE_TABLE, queryCondition2);

            dataGridInvoice.Rows.Clear();
            if (all.Rows.Count > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
                {
                    DataGridViewButtonColumn View = new DataGridViewButtonColumn();

                    double cashRecied = Convert.ToDouble(row["received"]) - Convert.ToDouble(row["otherpaymentreceived"]) - Convert.ToDouble(row["returned"]);

                    string name = "";
                    DataRow Admin = db.Find(DatabaseHelper.ADMIN_TABLE, $"id={row["operator"]}");
                    if (Admin != null)
                    {
                        name = Admin["f_name"] + " " + Admin["l_name"];
                    }

                    string onlineid = row["onlineid"].ToString();
                    if (onlineid.ToString().Equals(""))
                    {
                        onlineid = "Waiting...";
                    }

                    dataGridInvoice.Rows.Add(
                        row["id"],
                        row["fkey"],
                        onlineid,
                        row["terminal"],
                        Utility.Round(Convert.ToDouble(row["total"]), 2),
                        Utility.Round(Convert.ToDouble(row["discount"]), 2),
                        Utility.Round(cashRecied, 2),
                        Utility.Round(Convert.ToDouble(row["otherpaymentreceived"].ToString()), 2),
                        row["entrydate"],
                        name,
                        row["status"]
                    );


                    discount += Convert.ToDouble(row["discount"]);

                    Total += Convert.ToDouble(row["total"]);
                    cashreceived += Convert.ToDouble(cashRecied);
                    Otherpaymentreceived += Convert.ToDouble(row["otherpaymentreceived"]);
                    totaleceived += Convert.ToDouble(row["received"]) - Convert.ToDouble(row["returned"]); ;

                    if (!row["otherpaymentreceived"].Equals(""))
                    {
                        try
                        {
                            string jsonString = row["otherpaymentreceivedlist"].ToString();
                            var parsedData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                            foreach (var kvp in parsedData)
                            {
                                otherpayment.Add((Convert.ToDouble(kvp.Key), Convert.ToDouble(kvp.Value)));
                            }
                        }
                        catch { }
                    }
                }

               
            }

            DataTable Returnlist = db.selectQuery("SELECT sum((unitprice - ((discount * unitprice)/100)) * returned) discount FROM app_invinvoicehistory where CAST(returned AS REAL) > 0 and " + queryCondition);
            if (Returnlist.Rows.Count > 0)
            {
                try
                {
                    returnamount += Convert.ToDouble(Returnlist.Rows[0]["discount"].ToString());
                }
                catch
                {

                }
            }




            DisplayReport();
        }

        private void FrmSales_Load_1(object sender, EventArgs e)
        {
            Utility utility = new Utility();

            //utility.checkPinVerified("Report")

            //utility.ShowPINDialogBox("Report");
            if (!utility.checkPinVerified("Report"))
            {
                Close();
                // MessageBox.Show("Sorry! Verification is unsuccessful.");
                return;
            }

            Display();
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private PrintDocument PrintDocument;
        private void btnPrintSummary_Click(object sender, EventArgs e)
        {
            String printerName = App.Config().Setting("pos_printer");

            if (string.IsNullOrEmpty(printerName))
            {
                MessageBox.Show("Printer not defined");
                return;
            }
            try
            {
                PrintDocument = new PrintDocument();
                PrintDocument.PrinterSettings.PrinterName = App.Config().Setting("pos_printer");
                PrintDocument.PrintPage += new PrintPageEventHandler(FormatPage);
                PrintDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        double ScreenWidth = 250;
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
        private void FormatPage(object sender, PrintPageEventArgs e)
        {
            string poslogo = App.Config().Setting("poslogo");
            String path = App.Config().getbasePath("\\" + poslogo);
            string storelocation = App.Config().Setting("storelocation");
            string storephoneno = App.Config().Setting("storephoneno");
            string storeemailaddress = App.Config().Setting("storeemailaddress");
            int print_start_from = 10;
            DateTime selectedDate = fromDate.Value;
            string formattedDate = selectedDate.ToString("dd-MM-yyyy");


            if (File.Exists(path))
            {
                int poslogo_width;
                Image img = Image.FromFile(path);
                try
                {
                    poslogo_width = Convert.ToInt32(App.Config().Setting("poslogo_width", "100"));
                }
                catch
                {
                    poslogo_width = 100;
                }
                Double heightX = (Convert.ToDouble(poslogo_width) / img.Width) * img.Height;

                int height = Convert.ToInt32((float)heightX);
                Image img2print = resizeImage(img, new Size(poslogo_width, height));

                double leftMargin = 0;
                if (ScreenWidth > poslogo_width)
                {
                    leftMargin = (ScreenWidth - poslogo_width) / 2;
                }

                e.Graphics.DrawImage(img2print, new Point(Convert.ToInt32(leftMargin), 0));

                print_start_from += height;
            }
            string textToPrint = Utility.Center(App.Config().Setting("site_title"), TW) + "\n";

            if (!storelocation.Equals(""))
            {
                textToPrint += Utility.Center(storelocation, TW) + "\n";
            }

            if (!storephoneno.Equals(""))
            {
                textToPrint += Utility.Center(storephoneno, TW) + "\n";
            }

            if (!storeemailaddress.Equals(""))
            {
                textToPrint += Utility.Center(storeemailaddress, TW) + "\n";
            }
           // textToPrint += Utility.Center(formattedDate, TW) + "\n";
           // textToPrint += "\n";

            textToPrint += reportResult.Text;
            Font printFont = new Font("courier new", 8f, FontStyle.Bold); //new Font("Arial", 12);
            e.Graphics.DrawString(textToPrint, printFont, Brushes.Black, 0.0f, print_start_from);
        }

        private void btnClearSales_Click(object sender, EventArgs e)
        {
            Utility utility = new Utility();
            if (!utility.checkPinVerified("SystemTask"))
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                    "Are you sure you want to clear all sales records?\n\nThis action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (result == DialogResult.Yes)
            {
                db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "1=1");
                db.Delete(DatabaseHelper.INVOICE_TABLE, "1=1");
            }


            Display();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            Utility utility = new Utility();
            if (!utility.checkPinVerified("SystemTask"))
            {
                return;
            }
            this.Hide();
            new FrmAuditLog().ShowDialog();
        }
    }
}
