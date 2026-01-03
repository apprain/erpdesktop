using ApprainERPTerminal.Modules;
using ApprainERPTerminal.UI.Common;
using ApprainERPTerminal.UI.Driver;
using ApprainERPTerminal.UI.Inventory;
using ApprainERPTerminal.UI.POS;
using ApprainERPTerminal.UI.Report;
using System.Data;
using System.Reflection;

namespace ApprainERPTerminal.UI
{
    public partial class FrmMDI : Form
    {
        LineDisplay lineDisplay;
        DatabaseHelper db;
        public FrmMDI()
        {
            InitializeComponent();
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            db = new DatabaseHelper();
            Text = App.Config().Setting("site_title") + "~" + App.Config().Setting("username") + "~" + App.Config().Setting("terminalcode");

            manageProductsToolStripMenuItem.Enabled = App.Config().NavAccess("PRODUCT_LIST");
        }

        private void pOSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (isWorkPeriodNotStarted()) return;

            displayScreen();

            new FrmPOS(lineDisplay).ShowDialog();

        }

        private bool isWorkPeriodNotStarted()
        {
            string[] common = Utility.InvoiceCommonParameters();
            if (!common.Contains("enableworkperiod"))
            {
                return false;
            }

            String workPeriodTime = App.Config().Setting("workperiodtime");

            if (String.IsNullOrEmpty(workPeriodTime))
            {
                MessageBox.Show("Start your work period first to process billing!\n\nHints: Menu > Work Period");
                return true;
            }
            else
            {
                DataRow Record = db.Find(DatabaseHelper.WORKPERIOD_TABLE, "id > 0 ORDER BY id DESC");
                if (Record != null)
                {
                    if (!Record["operator"].ToString().Equals(App.Config().Setting("adminref")))
                    {
                        MessageBox.Show("Close previous work session and restat!\n\nHints: Menu > Work Period");
                        return true;
                    }
                }

                return false;
            }
        }

        private void displayScreen()
        {
            if (lineDisplay != null && lineDisplay.IsDisposed != true)
            {
                lineDisplay.Show();
                return;
            }

            string line_display_index = App.Config().Setting("line_display_index");
            if (line_display_index.Equals(""))
            {
                return;
            }

            if (Screen.AllScreens.Length <= 1)
            {
                return;

            }

            Screen monitor = Screen.AllScreens[Convert.ToInt32(line_display_index)];
            if (monitor == null)
            {
                return;
            }


            lineDisplay = new LineDisplay(this);
            lineDisplay.StartPosition = FormStartPosition.Manual;
            lineDisplay.Location = new Point(monitor.Bounds.Left, monitor.Bounds.Top);
            lineDisplay.Show();

        }

        private void clearDisplayScreen()
        {
            if (lineDisplay != null)
            {
                return;
            }

            lineDisplay.clearScreen();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(App.Config().Setting("site_title") + "\n\nToken:\n" + App.Config().Token() + "\n\nStore Name:\n" + App.Config().Setting("storename") + "\n\nUser Name:\n" + App.Config().Setting("username") + "\n\nApprain ERP Client " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\nCopyright © Apprain Technologies\nwww.apprain.com, info@apprain.com", "About");

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmPreference().ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            App.TOKEN = "";
            this.Hide();
            new FrmLogin().ShowDialog();
        }

        private void paymentMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmPaymentMethods().ShowDialog();
        }

        private void qTPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmQTPrinter().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void manageProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmManageItems().ShowDialog();
        }

        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isWorkPeriodNotStarted()) return;

            new FrmTable().ShowDialog();
        }

        private void workPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmWorkperiod().ShowDialog();
        }

        private void cashCounterCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmCashCounter().ShowDialog();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmSales().ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FrmManageOrders().ShowDialog();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmSales().ShowDialog();
        }
    }

}
