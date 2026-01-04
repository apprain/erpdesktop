using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Printing;

namespace ApprainERPTerminal.Modules.Voucher.Print
{
    internal class VoucherPrintService
    {
        private VoucherModel voucher;
        private int currentY;

        public VoucherPrintService(VoucherModel v)
        {
            voucher = v;
        }

        public void Print(bool toPdf = false)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += PrintPage;

            if (toPdf)
            {
                doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            }

            doc.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            currentY = 20;

            DrawHeader(g);
            DrawClientBox(g);
            DrawTransactionTable(g);
            DrawFooter(g);
        }

        private void DrawHeader(Graphics g)
        {
            Font title = new Font("Segoe UI", 14, FontStyle.Bold);
            Font normal = new Font("Segoe UI", 9);

            g.DrawString("HUNGRY DUCK", title, Brushes.Black, 20, currentY);
            currentY += 25;

            g.DrawString("9390 Sheppard Ave E.", normal, Brushes.Black, 20, currentY);
            currentY += 15;

            g.DrawString("Printed On: " + DateTime.Now.ToString("dd MMM yyyy hh:mm tt"),
                normal, Brushes.Black, 420, 20);
        }

        private void DrawClientBox(Graphics g)
        {
            Rectangle rect = new Rectangle(20, currentY + 10, 550, 70);
            g.FillRectangle(new SolidBrush(Color.FromArgb(220, 230, 255)), rect);

            Font bold = new Font("Segoe UI", 10, FontStyle.Bold);
            Font normal = new Font("Segoe UI", 9);

            g.DrawString("No Name", bold, Brushes.Black, 30, currentY + 20);
            g.DrawString("Balance Due", normal, Brushes.Black, 450, currentY + 20);

            g.DrawString(
                "Tk " + voucher.Total.ToString("N2"),
                bold,
                Brushes.Black,
                450,
                currentY + 40
            );

            currentY += 90;
        }

        private void DrawTransactionTable(Graphics g)
        {
            Font header = new Font("Segoe UI", 9, FontStyle.Bold);
            Font rowFont = new Font("Segoe UI", 9);

            int startX = 20;
            int descX = 40;
            int amtX = 450;

            g.DrawString("Description", header, Brushes.Black, descX, currentY);
            g.DrawString("Amount", header, Brushes.Black, amtX, currentY);
            currentY += 15;

            g.DrawLine(Pens.Black, startX, currentY, 580, currentY);
            currentY += 5;

            //  Deserialize rows from JSON
            var rows = JsonConvert.DeserializeObject<List<VoucherRowModel>>(voucher.TRows)
                       ?? new List<VoucherRowModel>();

            foreach (var r in voucher.Rows)
            {
                g.DrawString(r.Description, rowFont, Brushes.Black, descX, currentY);
                g.DrawString(r.Amount.ToString("N2"), rowFont, Brushes.Black, amtX, currentY);
                currentY += 15;
            }

            currentY += 10;
            g.DrawLine(Pens.Black, startX, currentY, 580, currentY);

            g.DrawString(
                "Total: Tk " + voucher.Total.ToString("N2"),
                header,
                Brushes.Black,
                amtX,
                currentY + 10
            );

            currentY += 40;
        }


        private void DrawFooter(Graphics g)
        {
            Font f = new Font("Segoe UI", 9);

            g.DrawString("Prepared by", f, Brushes.Black, 50, currentY);
            g.DrawString("Checked by", f, Brushes.Black, 200, currentY);
            g.DrawString("Approved by", f, Brushes.Black, 350, currentY);
            g.DrawString("Received by", f, Brushes.Black, 500, currentY);
        }




    }
}