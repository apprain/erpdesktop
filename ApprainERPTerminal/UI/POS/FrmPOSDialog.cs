using ApprainERPTerminal.Modules;
using System.Data;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmPOSDialog : Form
    {
        public FrmPOS frmPOS;
        private string action;
        private DatabaseHelper db;
        private ERPCloud erpCloud;
        private Panel panelSarchInvoice;
        private ComboBox comboBox1;


        public FrmPOSDialog(FrmPOS masterForm, string masterAction)
        {
            this.db = new DatabaseHelper();
            this.erpCloud = new ERPCloud();
            this.InitializeComponent();
            this.frmPOS = masterForm;
            this.action = masterAction;
            this.Text = masterAction;
        }

        DataGridView gridInvoice;
        public FrmPOSDialog(DataGridView grid, string masterAction)
        {
            this.db = new DatabaseHelper();
            this.erpCloud = new ERPCloud();
            this.InitializeComponent();
            this.action = masterAction;
            this.Text = masterAction;
            this.gridInvoice = grid;
        }

        private void btnPOSDSrcInvoice_Click(object sender, EventArgs e)
        {
            srcInvoice();
        }

        private void srcInvoice()
        {
            if (txtPOSDSrcText.Text == "")
            {
                MessageBox.Show("Enter value.");
                txtPOSDSrcText.Focus();
            }
            else if (cmboPODDSrcOpts.Text == "")
            {
                MessageBox.Show("Selection a option.");
                cmboPODDSrcOpts.Focus();
            }
            else
            {
                string text = this.txtPOSDSrcText.Text;
                DataRow Invoice2Load;
                switch (this.cmboPODDSrcOpts.Text)
                {
                    case "ID":
                        Invoice2Load = this.db.Find(DatabaseHelper.INVOICE_TABLE, "id=" + text);
                        break;
                    case "ONLINE":
                        Invoice2Load = this.db.Find(DatabaseHelper.INVOICE_TABLE, "onlineid=" + text);
                        break;
                    case "KEY":
                        Invoice2Load = this.db.Find(DatabaseHelper.INVOICE_TABLE, "fkey='" + text + "'");
                        break;
                    default:
                        Invoice2Load = this.db.Find(DatabaseHelper.INVOICE_TABLE, "remark='" + text + "'");
                        break;
                }
                if (Invoice2Load == null)
                {
                    if (this.cmboPODDSrcOpts.Text.Equals("KEY"))
                    {
                        erpCloud.DownloadInvoice(text, "fkey", this.frmPOS, this);
                    }
                    else if (this.cmboPODDSrcOpts.Text.Equals("ONLINE"))
                    {
                        erpCloud.DownloadInvoice(text, "id", this.frmPOS, this);
                    }
                    else
                    {
                        MessageBox.Show("Data not found");
                    }
                }
                else
                {
                    this.frmPOS.loadInvoice(Invoice2Load);
                    this.Dispose();
                }
            }
        }

        private void txtPOSDSrcText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                srcInvoice();
            }
        }

        private void FrmPOSDialog_Load(object sender, EventArgs e)
        {
            panelInquiry.Visible = !action.Equals("PROFIT-LOSS");
            panelProfitLostGrid.Visible = action.Equals("PROFIT-LOSS");

            if (action.Equals("PROFIT-LOSS"))
            {
                loadProfitAndLossGrid();
            }
        }

        private void loadProfitAndLossGrid()
        {
            double total_profit = 0;
            for (int index = 0; index < this.gridInvoice.Rows.Count; ++index)
            {
                string gridBarcode = gridInvoice.Rows[index].Cells[0].Value.ToString();
                string gridName = gridInvoice.Rows[index].Cells[1].Value.ToString();
                string gridQty = gridInvoice.Rows[index].Cells[3].Value.ToString();
                string gridSaleprice = gridInvoice.Rows[index].Cells[5].Value.ToString();
                DataRow itemRow = this.db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + gridBarcode + "'");
                double purchaseprice = App.Common().itemAvailablePurchasePrice(itemRow["id"].ToString(), gridSaleprice);

                double total_salesprice = Convert.ToDouble(gridQty) * Convert.ToDouble(gridSaleprice);
                double total_purchaseprice = Convert.ToDouble(gridQty) * Convert.ToDouble(purchaseprice);
                double profit = total_salesprice - total_purchaseprice;
                total_profit += profit;
               
                dataGridProfitLoss.Rows.Add(
                    gridBarcode,
                    gridName,
                    gridQty,
                    gridSaleprice,
                    purchaseprice,
                    total_salesprice,
                    total_purchaseprice,                    
                    Utility.currencyFormat(profit,2)
                );
            }

            this.Text = "Total Profit : " + total_profit;

            dataGridProfitLoss.Rows.Add(
                "",
                "Total Profit",
                "",
                "",
                "",
                "",
                "",
                total_profit.ToString()
            );
        }

        
    }
}
