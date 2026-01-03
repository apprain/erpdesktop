using System;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text.Json;
using QRCoder;
using static System.Net.Mime.MediaTypeNames;

namespace ApprainERPTerminal.Modules.Printer
{
    public class PrintTicket
    {
        static Bitmap qrImage;
        static DataRow Item;
        static string dateText;

        public PrintTicket()
        {
        }


        public void Print(string invoiceId)
        {
            dateText = DateTime.Now.ToString("yyyy-MM-dd");
            DatabaseHelper db = new DatabaseHelper();
            DataTable InvoiceHistory = db.FindAll(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + invoiceId);
            foreach (DataRow row in InvoiceHistory.Rows)
            {
                int qty = Convert.ToInt32(row["value"].ToString());

                Item = db.Find(DatabaseHelper.INVITEMS_TABLE, "id=" + row["itemid"]);

                for (int i = 0; i < qty; i++)
                {
                    var qrdata = new Dictionary<string, object>
                    {
                        ["tuid"] = "BG" + Guid.NewGuid().ToString("N")[..16],
                        ["IID"] = invoiceId,
                        ["HID"] = row["id"],
                        ["visitdate"] = dateText  
                    };
                    string plaintext = JsonSerializer.Serialize(qrdata);
                    byte[] encrypted = AesEncryption.Encrypt(plaintext, "Aa123456"); ;
                    doPrint(Convert.ToBase64String(encrypted));                   
                }
            }
        }
        public void doPrint(string data)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    qrImage = qrCode.GetGraphic(10); // Smaller module size for smaller paper
                }
            }

            PrintDocument printDoc = new PrintDocument();

            // Set custom paper size for 72mm POS printer (72mm ≈ 283 pixels at 100 DPI)
            PaperSize paperSize = new PaperSize("POS_72mm", 283, 600); // Width x Height (can be adjusted)
            printDoc.DefaultPageSettings.PaperSize = paperSize;
            printDoc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

            printDoc.PrintPage += PrintDoc_PrintPage;

            printDoc.Print();
        }

        private static void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (qrImage != null)
            {
                Graphics g = e.Graphics;

                // Load item name safely (you may want to pass this in the constructor instead)
                string title = "Welcome to Baldha Garden"; // Replace or pass actual value
                Font titleFont = new Font("Arial", 8, FontStyle.Bold);
                int margin = 10;

                // Draw Title
                SizeF titleSize = g.MeasureString(title, titleFont);
                int textX = (e.PageBounds.Width - (int)titleSize.Width) / 2;
                int textY = margin;
                g.DrawString(title, titleFont, Brushes.Black, textX, textY);

                titleSize = g.MeasureString(title, titleFont);
                textX = (e.PageBounds.Width - (int)titleSize.Width) / 2;
                textY +=(int)titleSize.Height + 5;
                g.DrawString(Item["name"].ToString(), titleFont, Brushes.Black, textX, textY);
                Debug.WriteLine(Item["name"].ToString());

                // Draw Date
                string dateText = "Date : " + DateTime.Now.ToString("yyyy-MM-dd");
                SizeF dateSize = g.MeasureString(dateText, titleFont);
                int dateX = (e.PageBounds.Width - (int)dateSize.Width) / 2;
                int dateY = textY + (int)titleSize.Height + 5;
                g.DrawString(dateText, titleFont, Brushes.Black, textX, dateY);

                // Resize QR image
                int qrSize = 250; // Adjust for 72mm
                Bitmap resizedQr = new Bitmap(qrImage, new Size(qrSize, qrSize));

                // Draw QR code
               // int qrX = (e.PageBounds.Width - qrSize) / 2;
                int qrX = (e.PageBounds.Width - qrSize) ;
                int qrY = dateY + (int)dateSize.Height + 10;
                g.DrawImage(resizedQr, qrX, qrY);

                e.HasMorePages = false;
            }

        }
    }
}
