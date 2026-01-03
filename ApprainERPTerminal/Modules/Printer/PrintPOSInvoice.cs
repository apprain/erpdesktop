using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using Zen.Barcode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApprainERPTerminal.Modules.Printer
{
    public class PrintPOSInvoice
    {
        private PrintDocument PrintDocument;
        private Graphics graphics;
        double ScreenWidth = 250;
        public int C1;
        public int C2;
        public int C3;
        public int C4;
        public int TW;
        private long InvoiceId;
        private string TableId;
        private string printerName;
        private string poslogo;
        private string CustomerName = "";
        private double LoyaltyBalance = 0;
        public int CA;
        public int CB;
        private DatabaseHelper db;
        private DataRow Invoice;
        private DataTable InvoiceHistory;
        int print_point;
        int print_start_from = 10;

        String PaymentStatus = "Paid";

        Font font ;
        Font fontBold ;
       
        public PrintPOSInvoice()
        {           
            CA = 6;
            CB = 12;
            setFont();

            printerName = App.Config().Setting("pos_printer");
            poslogo = App.Config().Setting("poslogo");
        }

        public PrintPOSInvoice(long Id)
        {            
            CA = 6;
            CB = 12;
            db = new DatabaseHelper();
            setFont();

            PaymentStatus = "Paid";
            InvoiceId = Id;
            printerName = App.Config().Setting("pos_printer");
            AdjustOrientation();
            Invoice = db.Find(DatabaseHelper.INVOICE_TABLE, "id=" + Id.ToString());
            InvoiceHistory = db.FindAll(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + Id.ToString());
        }

        public PrintPOSInvoice setPaymentStatus(String status)
        {
            PaymentStatus = status;
            return this;
        }
        public PrintPOSInvoice setTableId(string tableId)
        {
            TableId = tableId;

            return this;
        }

        public String getPaymentStatus()
        {
            return PaymentStatus;
        }

        private void setFont()
        {
            
            string fontSize = App.Config().Setting("pos_printer_font_size");
            string fontFaceName = App.Config().Setting("pos_printer_font");

            if(fontFaceName.Equals(""))
            {
                fontFaceName = "Courier New";
            }

            if (fontSize.Equals(""))
            {
                fontSize = "8";
            }

            font = new Font(fontFaceName, float.Parse(fontSize), FontStyle.Regular);
            fontBold = new Font(fontFaceName, float.Parse(fontSize), FontStyle.Bold);

        }

        public PrintPOSInvoice setCustomerName(string value)
        {
            CustomerName = value;

            return this;
        }

        public PrintPOSInvoice setLoyaltyBalance(double balance)
        {
            LoyaltyBalance = balance;

            return this;
        }

     

        private void AdjustOrientation()
        {
            C1 = 17;
            C2 = 5;
            C3 = 3;
            C4 = 8;
            TW = 37;
        }

        public void Print()
        {
            if (printerName.Equals(""))
            {
                return;
            }

            string auto_print = App.Config().Setting("inventorysettings_invoice_auto_print");
            if(auto_print.Equals("No"))
            {
                return;
            }


            if (auto_print.Equals("Yes"))
            {
                DialogResult result = MessageBox.Show("Print invoice receipt?", "Confirm box", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                PrintDocument = new PrintDocument();
                PrintDocument.PrinterSettings.PrinterName = this.printerName;

                PrintDocument.PrintPage += new PrintPageEventHandler(FormatPage);
                PrintDocument.Print();

                string[] common = Utility.InvoiceCommonParameters();
                if (common.Contains("printticket"))
                {
                    new PrintTicket().Print(InvoiceId.ToString());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void testPrint()
        {
            if (this.printerName.Equals(""))
            {
                return;
            }
            PrintDocument = new PrintDocument();
            PrintDocument.PrinterSettings.PrinterName = this.printerName;
            PrintDocument.PrintPage += new PrintPageEventHandler(FormatTestPage);
            PrintDocument.Print();
        }

        private void FormatTestPage(object sender, PrintPageEventArgs e)
        {
            graphics = e.Graphics;
            font = new Font("courier new", 8f,FontStyle.Bold);

            graphics.DrawString(
                Line() +
                Utility.Center("Test Print Successful", this.TW) + Br() +
                Line() + Utility.Center("Powered By appRain Technologies", this.TW) + this.Br() + Utility.Center("www.apprain.com", this.TW) + this.Br() + this.Line(),
                font, new SolidBrush(Color.Black),
                0.0f, 0.0f
             );
        }

        private void FormatPage(object sender, PrintPageEventArgs e)
        {
          
            this.poslogo = App.Config().Setting("poslogo");
            String path = App.Config().getbasePath("\\" + poslogo);
            String fkey = "none";
            if (Invoice != null && Invoice["fkey"] != null)
            {
                fkey = Invoice["fkey"].ToString();
            }
            
            string invoiceString;

            graphics = e.Graphics;

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

                graphics.DrawImage(img2print, new Point(Convert.ToInt32(leftMargin), 0));

                print_start_from += height;
            }

            findInvoiceString();

            Br(2);
            BarcodeDraw bdraw = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
            Image barcodeImage = bdraw.Draw(fkey, 50);

            print_point = line2Pixel(page_line) + print_start_from;
            graphics.DrawImage(barcodeImage, new Point(30, print_point));

            //# Print Invoice Barcode and Global Key
            print_point += 55;
            invoiceString = Utility.Center("Global Key: " + fkey, TW);
            graphics.DrawString(invoiceString, font, (Brush)new SolidBrush(Color.Black), 0.0f, print_point);
        }

        public int line2Pixel(int value)
        {
            return value * font.Height;
        }

        public int line2Pixel(int value,Font tFont)
        {
            return value * tFont.Height;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        public void printline(string str)
        {
            Br(); print_point = line2Pixel(page_line, font) + print_start_from;
            graphics.DrawString(str, font, (Brush)new SolidBrush(Color.Black), 0.0f, print_point);
        }

        public void printline(string str,Font font)
        {
            Br();
            print_point = line2Pixel(page_line, font) + print_start_from;
            graphics.DrawString(str, font, (Brush)new SolidBrush(Color.Black), 0.0f, print_point);
        }

        public string findInvoiceString()
        {
            string str = "";
            string username = App.Config().Setting("username");
            string storelocation = App.Config().Setting("storelocation");
            string storephoneno = App.Config().Setting("storephoneno");
            string storeemailaddress = App.Config().Setting("storeemailaddress");
            string onlineid = Invoice["onlineid"].ToString();
            string fkey = Invoice["fkey"].ToString();
            string client = Invoice["client"].ToString();
            string Remark = Invoice["Remark"].ToString();
            string InvoiceDate = Convert.ToDateTime(Invoice["entrydate"]).ToString("yyyy-MM-dd");

            //////// TOP Header Print Start
            // Print title of the Store 
            printline(Utility.Center(App.Config().Setting("site_title"), TW), fontBold);
            Br();

            // Store Location & Address
            if (!storelocation.Equals(""))
            {
                printline(Utility.Center(storelocation, TW));
            }

            if (!storephoneno.Equals(""))
            {
                printline(Utility.Center(storephoneno, this.TW));
            }

            if (!storeemailaddress.Equals(""))
            {
                printline(Utility.Center(storeemailaddress, this.TW));
            }

            // Print Other Informaiton 
            printline(Utility.Center("Print Date:" + Utility.getDate("yyyy-MM-dd h:m:s a"), this.TW));
            Br(2);

            if (onlineid != null && !onlineid.Equals(""))
            {
                printline(Utility.Center("Invoice Id: " + onlineid, TW));
            }
            
            printline(Utility.Center("Device ID: " + Invoice["id"].ToString(), TW));
            printline(Utility.Center("Key: " + fkey, TW));

            printline(Utility.Center("[ " + getPaymentStatus() + " ]", TW));
            //////// TOP Header Print Done 


            // Print Table Header
            printline(Line());
            printline(string.Format("{0,-" + CA.ToString() + "}{1,-" + CB.ToString() + "}{2,-" + CA.ToString() + "}{3," + CB.ToString() + "}"," By:", username.PadRight(12).Substring(0, 12), " Date:", InvoiceDate.PadLeft(12).Substring(0, 12)));
            if (!string.IsNullOrEmpty(TableId))
            {
                string tablename = App.Common().tableId2Name(TableId);
                string servedby = App.Common().tableId2ServerName(TableId);
                printline(string.Format("{0,-" + CA.ToString() + "}{1,-" + CB.ToString() + "}{2,-" + CA.ToString() + "}{3," + CB.ToString() + "}", " Sv:", servedby.PadRight(12).Substring(0,12), " Tabl:", tablename.PadLeft(12).Substring(0,12)));
            }
            printline(Line());

            if (!client.Equals("0"))
            {
                printline(string.Format("{0,-" + CA.ToString() + "}{1,-" + CB.ToString() + "}{2,-" + CA.ToString() + "}{3," + CB.ToString() + "}"," Name:", CustomerName, " Bal:", Utility.Round(LoyaltyBalance, 2)));
                printline(Line());
            }

            printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + C2.ToString() + "}|{2," + C3.ToString() + "}|{3," + C4.ToString() + "}|","NAME ", "PRICE", "QTY", "TOTAL"),fontBold);
            printline(Line());


            double total = Convert.ToDouble(Invoice["total"]);
            double discount = Convert.ToDouble(Invoice["discount"]);
            double num3 = Convert.ToDouble(Invoice["returned"]);
            double num4 = Convert.ToDouble(Invoice["received"]);

            double cashreceived = Convert.ToDouble(Invoice["cashreceived"]);
            double otherpaymentreceived = Convert.ToDouble(Invoice["otherpaymentreceived"]);
            string otherpaymentreceivedlist = Convert.ToString(Invoice["otherpaymentreceivedlist"]);

            double totalvat = 0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double adjustment = 0.0;

            if (!Invoice["vat"].ToString().Equals(""))
            {
                totalvat = Convert.ToDouble(Invoice["vat"]);
            }

            if (!Invoice["unsettled"].ToString().Equals(""))
            {
                num5 = Convert.ToDouble(Invoice["unsettled"]);
            }

            if (!Invoice["returned"].ToString().Equals(""))
            {
                num6 = Convert.ToDouble(Invoice["returned"]);
            }

            if (!Invoice["creditreceived"].ToString().Equals(""))
            {
                num7 = Convert.ToDouble(Invoice["creditreceived"]);
            }

            if (!Invoice["adjustment"].ToString().Equals(""))
            {
                adjustment = Convert.ToDouble(Invoice["adjustment"]);
            }

            string firstLine, secondLine, thirdLine, name;
            double total_return = 0.0;
            foreach (DataRow row in InvoiceHistory.Rows)
            {
                double subtotal = Convert.ToDouble(row["subtotal"]);
                double value = Convert.ToDouble(row["value"]);
                string itemid = row["itemid"].ToString();
                string vat = row["vat"].ToString();
                double unitprice = Convert.ToDouble(row["unitprice"]);
                double returned = Convert.ToDouble(row["returned"]);
                DataRow Item = db.Find(DatabaseHelper.INVITEMS_TABLE, "id=" + itemid);

                if (Item == null) continue;

                name = Item["name"].ToString().Trim();

                string vatmark = "|";

                if (!Item["vat"].Equals("") && Convert.ToDouble(Item["vat"]) > 0){
                    vatmark  = "*";
                }

                firstLine = Utility.GetLine(name, 0, 17);
                secondLine = Utility.GetLine(name, firstLine.Length, 17);
                thirdLine = name.Substring(firstLine.Length + secondLine.Length).Trim();

                printline(string.Format("|{0,-" + C1.ToString() + "}" + vatmark + "{1," + C2.ToString() + "}|{2," + this.C3.ToString() + "}|{3," + this.C4.ToString() + "}|", new object[4]
                {
                    firstLine.Trim(),
                    unitprice,
                    value,
                    Utility.currencyFormat(subtotal, 2)
                }));

                if (!secondLine.Equals("")) {
                    printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + this.C2.ToString() + "}|{2," + this.C3.ToString() + "}|{3," + this.C4.ToString() + "}|", new object[4]
                    {
                        secondLine.Trim(),
                        "",
                        "",
                        ""
                    }));
                }

                if (!thirdLine.Equals(""))
                {
                    printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + this.C2.ToString() + "}|{2," + this.C3.ToString() + "}|{3," + this.C4.ToString() + "}|", new object[4]
                    {
                        thirdLine.Trim(),
                        "",
                        "",
                        ""
                    }));
                }

                if (returned > 0.0)
                {
                    total_return += unitprice * Convert.ToDouble(returned);

                    printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "}|{2," + this.C3.ToString() + "}|{3," + this.C4.ToString() + "}|", 
                        ("(" + "Returned)"),
                        "",
                        "-"+ Utility.Round(returned,0).ToString(),
                        ""
                         )
                    );
                }
            }
            printline(Line());

            printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", "Total","","",Utility.currencyFormat(total, 2)));

            if (totalvat > 0.0)
            {
                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                  "VAT",
                  "",
                  "",
                   Utility.currencyFormat(totalvat, 2)
                }));
            }

            printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
            {
                "Discount(-)",
                "",
                "",
                Utility.currencyFormat(discount, 2)

            }));

            double net_payable = total;

            if (Convert.ToDouble(discount) > 0.0)
            {
                net_payable = Convert.ToDouble(total) - Convert.ToDouble(discount);
            }

            printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
            {
                "NET PAYABLE",
                "",
                "",
                Utility.currencyFormat(net_payable, 2)

            }), fontBold);

            if (adjustment > 0.0)
            {
                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                  "Adjustment(-)",
                  "",
                  "",
                  Utility.currencyFormat(adjustment, 2)
                }));
            }

            if (otherpaymentreceived > 0.0)
            {
                printline(Line());
                if (cashreceived > 0.0)
                {
                    printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + C2.ToString() + "} {2," + C3.ToString() + "} {3," + C4.ToString() + "}|","Cash", "", "", Utility.currencyFormat(cashreceived.ToString(), 2)), fontBold);
                }

                JObject otherpaymentreceivedlistObj = JObject.Parse(otherpaymentreceivedlist);
                foreach (var row in otherpaymentreceivedlistObj)
                {
                    DataRow MethodRow = db.Find(DatabaseHelper.INVPAYMENTMETHOD_TABLE, "id=" + row.Key);

                    if (MethodRow != null)
                    {
                        printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + C2.ToString() + "} {2," + C3.ToString() + "} {3," + C4.ToString() + "}|",MethodRow["name"], "", "", Utility.currencyFormat(row.Value.ToString(), 2)), fontBold);
                    }
                }
                printline(Line());
            }

            double totalReceived = num4 + num5 + num6;
            string lbl_total_rcv = "Total Received";
            totalReceived = cashreceived + otherpaymentreceived ;
            if (getPaymentStatus().Equals("Unpaid"))
            {               
                lbl_total_rcv = "Advance Received";
            }
            printline(string.Format("|{0,-" + C1.ToString() + "}|{1," + C2.ToString() + "} {2," + C3.ToString() + "} {3," + C4.ToString() + "}|", lbl_total_rcv, "", "", Utility.currencyFormat((totalReceived).ToString(), 2)),fontBold);

            if (num7 > 0.0)
            {
                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                    "Credit Amount",
                    "",
                    "",
                    num7
                }));
            }

            if (num3 > 0.0)
                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                    "Returned",
                    "",
                    "",
                    Utility.currencyFormat(num3.ToString(),2)
                }), fontBold);


            if (total_return > 0.0)
            {
                printline(Line());
                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                    "Rtn Amount(-)",
                    "",
                    "",
                    total_return
                }), fontBold);

                printline(string.Format("|{0,-" + this.C1.ToString() + "}|{1," + this.C2.ToString() + "} {2," + this.C3.ToString() + "} {3," + this.C4.ToString() + "}|", new object[4]
                {
                    "Total After Rtn",
                    "",
                    "",
                    (total - total_return)
                }));
            }

            printline(Line());

            if (!Remark.Equals(""))
            {
                string Comments = App.Config().Setting("inventorysettings_remark_label", "Comments");
                printline(Utility.Center(Comments + ":" + Remark, TW));
                printline(Line());
            }

            string footer_txt = App.Config().Setting("inventorysettings_inv_footer_txt");
            if (!footer_txt.Equals(""))
            {
                string[] footer_txtArr = footer_txt.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (string row in footer_txtArr)
                {
                    printline(Utility.Center(row, TW));
                }
            }

            return "";
        }

        private string Line()
        {
            return "-------------------------------------";
            Br();
        }

        int page_line = 0;
        private string Br()
        {
            page_line++;
            return "\n";
        }

        private string Br(int i)
        {
            if (i <= 0)
            {
                return "";
            }
            string str = "";
            for (int index = 0; index < i; ++index)
            {
                page_line++;
                str += "\n";
            }
            return str;
        }


    }
}
