using ApprainERPTerminal.Modules.Inventory;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;


namespace ApprainERPTerminal.Modules.Printer
{
    internal class PrintKOT
    {
        private PrintDocument PrintDocument;
        private Graphics graphics;
        double ScreenWidth = 250;
        public int C1;
        public int C2;
        public int C3;
        public int C4;
        public int TW;

        private string printerName;
        public int CA;
        public int CB;
        private DatabaseHelper db;
        Font font;
        Font fontBold;
        String tableId;
        JArray lastInvoice;
        DataRow TableRecord;

        public PrintKOT(string Id)
        {
            CA = 6;
            CB = 12;
            setFont();
            printerName = App.Config().Setting("pos_printer");
            this.tableId = Id;
            db = new DatabaseHelper();
            
        }

        public string Print()
        {
            if (!tableId.Equals(""))
            {
                TableRecord = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableId);
            }
            string InvoiceStr = TableRecord["lastinvoice"]?.ToString();

            return Print(InvoiceStr,true);

        }
        public String Print(String InvoiceJStr)
        {
            return Print(InvoiceJStr, false);
        }

        public String Print(String InvoiceJStr, Boolean isFull)
        {
            if (!tableId.Equals(""))
            {
                TableRecord = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableId);
            }

            if (string.IsNullOrEmpty(InvoiceJStr)) return "";

            JObject InvoiceObj = JObject.Parse(InvoiceJStr);

            lastInvoice = JArray.Parse(InvoiceObj.GetValue("body").ToString());

            AddPrinterInformation();

            String KOTJsonStr = "";
            string no_of_kot_copy = App.Config().Setting("no_of_kot_copy", "1");

            for(int i = 0; i < Convert.ToInt32(no_of_kot_copy); i++)
            {
                KOTJsonStr = printText(isFull);
            }


            //////////
            if (!tableId.Equals(""))
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values = new Dictionary<string, string>();
                values.Add("id", tableId);
                values.Add("qtdata", KOTJsonStr);
                db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);
            }
            /////////////// 
            
            return KOTJsonStr;
        }

        private void AddPrinterInformation()
        {
            foreach (var item in lastInvoice)
            {
                string barcode = (string)item["barcode"];
                DataRow product = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");

                if (product == null) continue;

                string category = product["category"]?.ToString();
                if (string.IsNullOrEmpty(category)) continue;

                DataRow categoryRow = db.Find(DatabaseHelper.CATEGORY_TABLE, "id='" + category + "'");
                
                item["printer"] = "";
                if (categoryRow != null)
                {
                    item["printer"] = GetPrinterByCategoryId(categoryRow["id"].ToString());
                }
            }
        }

        public static string GetPrinterByCategoryId(string categoryId)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryId))
                {
                    return "";
                }
               
                String path = App.DATA_PATH + App.DS + "QTPrinterMap.json";

                if (!File.Exists(path)) return "";

                string jsonData = File.ReadAllText(path);               
                if (string.IsNullOrEmpty(jsonData)) return "";

                JArray jsonArray = JArray.Parse(jsonData);
                var item = jsonArray.FirstOrDefault(j => (string)j["CategoryId"] == categoryId);


                return item != null ? (string)item["Printer"] : "";
            }
            catch (Exception e)
            {
                return "";
            }
        }



        String printBody;
        private String printText(Boolean isFull)
        {
            var groupedByPrinter = lastInvoice
                .GroupBy(item => (string)item["printer"])
                .Select(printerGroup => new
                {
                    Printer = printerGroup.Key,
                    Barcodes = printerGroup
                        .GroupBy(item => (string)item["barcode"])
                        .Select(barcodeGroup => new
                        {
                            Barcode = barcodeGroup.Key,
                            TotalValue = barcodeGroup.Sum(item =>
                            {
                                if (double.TryParse(item["value"]?.ToString(), out double value))
                                    return value;
                                return 0; // Default to 0 if conversion fails
                            })//,
                           // SalesPrice = barcodeGroup.First()?["salesprice"]?.ToString() ?? "0"                                                                 
                        })
                });


            bool isPrint = false ;
            string firstLine, secondLine, thirdLine, productName;
            Utility utility = new Utility();

            JArray filteredArray = new JArray();
            foreach (var row in groupedByPrinter)
            {               
                if (!row.Printer.Equals(""))
                {
                    printBody = $"{"NAME",-20} {"QTY",-4}\n";
                    printBody += Line();
                    foreach (var item in row.Barcodes)
                    {
                        double value = GetValueByBarcode(item.Barcode);

                        if (isFull)
                        {
                            DataRow Product = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + item.Barcode.ToString() + "'");
                            double qty = item.TotalValue;
                            productName = Product["name"].ToString();

                            firstLine = Utility.GetLine(productName, 0, 20);
                            secondLine = Utility.GetLine(productName, firstLine.Length, 20);
                            thirdLine = productName.Substring(firstLine.Length + secondLine.Length).Trim();

                            printBody += $"{firstLine.Trim(),-20} | {qty,-4}\n";
                            if (!string.IsNullOrWhiteSpace(secondLine)) printBody += $"{secondLine.Trim(),-20} |\n";
                            if (!string.IsNullOrWhiteSpace(thirdLine)) printBody += $"{thirdLine.Trim(),-20} |\n";

                            printBody += Line();
                            isPrint = true;

                        }
                        else
                        {
                            DataRow Product = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + item.Barcode.ToString() + "'");
                            if (item.TotalValue > value)
                            {

                                double qty = item.TotalValue - value;
                                productName = Product["name"].ToString();

                                firstLine = Utility.GetLine(productName, 0, 20);
                                secondLine = Utility.GetLine(productName, firstLine.Length, 20);
                                thirdLine = productName.Substring(firstLine.Length + secondLine.Length).Trim();

                                printBody += $"{firstLine.Trim(),-20} | {qty,-4}\n";
                                if (!string.IsNullOrWhiteSpace(secondLine)) printBody += $"{secondLine.Trim(),-20} |\n";
                                if (!string.IsNullOrWhiteSpace(thirdLine)) printBody += $"{thirdLine.Trim(),-20} |\n";

                                printBody += Line();
                                isPrint = true;
                            }
                            else if (item.TotalValue < value)
                            {
                                double qty_reduced = value - item.TotalValue;
                                JObject jLogData = new JObject();
                                jLogData.Add("barcode", Product["barcode"].ToString());
                                jLogData.Add("value", qty_reduced.ToString());        
                                App.Log().Save("Executive", "KOT Item (" + Product["name"].ToString() + ") reduced by " + qty_reduced, "KOT_ITEM_CLEARED", tableId, "[" + jLogData.ToString() + "]");
                            }
                        }
  
                        filteredArray.Add(new JObject
                            {
                                { "barcode", item.Barcode.ToString()},
                                { "value", item.TotalValue }
                            }
                        );
                       
                    }
                    

                    if (isPrint)
                    {
                        try
                        {
                            PrintDocument = new PrintDocument();
                            PrintDocument.PrinterSettings.PrinterName = row.Printer;
                            PrintDocument.PrintPage += new PrintPageEventHandler(FormatPage);
                            PrintDocument.Print();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }

            return filteredArray.ToString();
        }

        

        public double GetValueByBarcode(string barcode)
        {
            if (TableRecord == null) return 0;

            string jsonData = TableRecord["qtdata"].ToString();
            if (jsonData.Equals("")) return 0;

            JArray deductionArray = JArray.Parse(jsonData);

            var item = deductionArray.FirstOrDefault(d => (string)d["barcode"] == barcode);
            return item != null ? (double)item["value"] : 0; 
        }

        private void setFont()
        {

            string fontSize = App.Config().Setting("pos_printer_font_size");
            string fontFaceName = App.Config().Setting("pos_printer_font");

            if (fontFaceName.Equals(""))
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

        private string  serverName(string serverId)
        {
            if (string.IsNullOrEmpty(serverId))
            {
                return "";
            }


            DataRow itemRow = db.Find(DatabaseHelper.ADMIN_TABLE, "id='" + serverId + "'");

            if (itemRow == null) return "";

            return itemRow["f_name"].ToString() + itemRow["l_name"].ToString();
        }

        private void FormatPage(object sender, PrintPageEventArgs e)
        {
            graphics = e.Graphics;
            font = new Font("courier new", 10f, FontStyle.Bold);
            Font font2 = new Font("courier new", 20f, FontStyle.Bold);
            Font fontBig = new Font("courier new", 18f, FontStyle.Bold);

            String tableName = "Take Away";
            String Server = "Counter";
            if (TableRecord != null)
            {
                tableName = TableRecord["name"].ToString();
                Server = serverName(TableRecord["servername"].ToString());
            }
            float yPosition = 0.0f;

            graphics.DrawString("KOT", font2, Brushes.Black, 100, yPosition);
            yPosition += font2.GetHeight(graphics);

            graphics.DrawString(
                "    " + DateTime.Now.ToString("dd-MM-yy hh:mm:ss tt") + Br(),
                font, new SolidBrush(Color.Black),
                0.0f, yPosition
             );
            yPosition += font.GetHeight(graphics);

            graphics.DrawString(
                Line(),
                font, new SolidBrush(Color.Black),
                0.0f, yPosition
            );
            yPosition += font.GetHeight(graphics);

            graphics.DrawString($"{tableName}", fontBig, Brushes.Black, 0.0f, yPosition);
            yPosition += fontBig.GetHeight(graphics);

            if (!string.IsNullOrEmpty(Server))
            {
                graphics.DrawString($"{Server}", fontBig, Brushes.Black, 0.0f, yPosition);
                yPosition += fontBig.GetHeight(graphics) * 2;
            }

            graphics.DrawString(
                printBody,
                font, new SolidBrush(Color.Black),
                0.0f, yPosition
             );
        }
       
        private string Line()
        {
            page_line++;
          //  return "-------------------------------------" + Br();
            return "----------------------------" + Br();
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
