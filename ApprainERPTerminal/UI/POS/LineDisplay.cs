using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Inventory;



namespace ApprainERPTerminal.UI.POS
{
    public partial class LineDisplay : Form
    {
        Invoice invoice;

        public LineDisplay(Form frm)
        {
            InitializeComponent();
        }

        private void LineDisplay_Load(object sender, EventArgs e)
        {
            string path = @"C:\data\display.jpg";
            if (File.Exists(path))
            {
                pictureBox.ImageLocation = path;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public void displaySales(DataGridView gridInvoice, double total, double tax, double discount, double payable)
        {
            dataGridViewLD.Rows.Clear();
            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    dataGridViewLD.Rows.Add(
                        (index + 1),
                        gridInvoice.Rows[index].Cells[1].Value,
                        gridInvoice.Rows[index].Cells[3].Value,
                        gridInvoice.Rows[index].Cells[5].Value,
                        gridInvoice.Rows[index].Cells[10].Value
                       );
                }
            }
            lblLDTotal.Text = Utility.currencyFormat(total, 2).ToString();
            lblLDTax.Text = Utility.currencyFormat(tax, 2).ToString();
            lblLDDiscount.Text = Utility.currencyFormat(discount, 2).ToString();
            lblLDPayable.Text = Utility.currencyFormat(payable, 2).ToString();
        }

        public void clearScreen()
        {
            dataGridViewLD.Rows.Clear();
            lblLDTotal.Text = "0.00";
            lblLDTax.Text = "0.00";
            lblLDDiscount.Text = "0.00";
            lblLDPayable.Text = "0.00";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
