using ApprainERPTerminal.Modules.Printer;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;

namespace ApprainERPTerminal.UI
{
    public partial class FrmPreference : Form
    {

        private HttpClient client;
        private ERPCloud eRPCloud;
        private Config config;
        private string UrlAuth;
        private string UrlDataExchnage;
        private int logo_width = 100;
        private int logo_height = 100;


        public FrmPreference()
        {
            this.UrlAuth = "";
            this.UrlDataExchnage = "";
            this.InitializeComponent();
            this.eRPCloud = new ERPCloud();
            this.config = new Config();
            this.client = new HttpClient();
            this.UrlAuth = this.config.AuthUrl();
            this.UrlDataExchnage = this.config.UrldataExchnage();
        }

        private void FrmPreference_Load(object sender, EventArgs e)
        {
            String logo = App.Config().Setting("poslogo");
            String path = App.Config().getbasePath("\\" + logo);

            txtLogowidth.Text = App.Config().Setting("poslogo_width", "100");
            cmbFont.Text = App.Config().Setting("pos_printer_font", "Courier New");
            cmbFontSize.Text = App.Config().Setting("pos_printer_font_size", "8");


            if (App.Config().Setting("write_event_log").Equals("Enabled"))
            {
                chkEventLog.Checked = true;
            }

            if (File.Exists(path))
            {
                picBoxLogo.Image = new Bitmap(Image.FromFile(@path), new Size(logo_width, logo_height));
            }

            findPrinter();
            findLineDisplay();
        }

        public void findPrinter()
        {
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Printer not found!");
            }
            else
            {
                cmbPOSPrintersList.Items.Add((object)"");
                foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
                {
                    cmbPOSPrintersList.Items.Add((object)installedPrinter.ToString());
                }
                cmbPOSPrintersList.Text = App.Config().Setting("pos_printer");
            }
        }

        public void findLineDisplay()
        {
            if (Screen.AllScreens.Length <= 0)
            {
                return;
            }
            else
            {
                int i = 0;
                string txtValue, line_display_index = App.Config().Setting("line_display_index");
                comboLineDisplay.Items.Add((object)"");

                foreach (Screen monitor in Screen.AllScreens)
                {
                    txtValue = "Screen " + (i + 1) + "-" + monitor.WorkingArea.Size.Width + "x" + monitor.WorkingArea.Size.Height;

                    if (monitor.Primary == true)
                    {
                        txtValue += " (Primary)";
                    }

                    comboLineDisplay.Items.Add(txtValue);
                    if (line_display_index.Equals(i.ToString()))
                    {
                        comboLineDisplay.Text = txtValue;
                    }

                    i++;
                }

            }
        }

        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            if (cmbPOSPrintersList.Text.Equals(""))
            {
                MessageBox.Show("No printer selected!");
                return;
            }

            new PrintPOSInvoice().testPrint();
        }

        private void cmbPOSPrintersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.Config().Update("pos_printer", this.cmbPOSPrintersList.Text);
        }

        private void btnItemFull_Click(object sender, EventArgs e)
        {
            eRPCloud.syncItemAsync(btnItemFull);
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            btnStock.Text = "Wait..";
            eRPCloud.syncStockByAttribute();
        }

        private void picBoxLogo_Click(object sender, EventArgs e)
        {

            OpenFileDialog op1 = new OpenFileDialog();
            op1.DefaultExt = ".jpg";
            op1.Multiselect = false;
            op1.ShowDialog();

            op1.Filter = "Image Files|*.jpg";

            String path;
            String name;

            long timestamp = DateTime.Now.ToFileTime();

            string[] FName;
            foreach (string s in op1.FileNames)
            {
                String oldName = App.Config().Setting("poslogo");
                String oldpath = App.Config().getbasePath("\\" + oldName);

                FName = s.Split('\\');
                name = timestamp + FName[FName.Length - 1];
                path = App.Config().getbasePath("\\" + name);

                File.Copy(s, path);

                picBoxLogo.Image = new Bitmap(Image.FromFile(@path), new Size(logo_width, logo_height));
                App.Config().Update("poslogo", name);
                if (File.Exists(oldpath))
                {
                    //#    File.Delete(oldpath);
                }
                MessageBox.Show("Updated successfully.");
            }

        }

        private void btnSaveLogoSetting_Click(object sender, EventArgs e)
        {
            App.Config().Update("poslogo_width", txtLogowidth.Text);
            MessageBox.Show("Updated successfully.");
        }

        private void btnPaymentMethods_Click(object sender, EventArgs e)
        {
            eRPCloud.syncPaymentMethods();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            eRPCloud.syncUsers();
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            eRPCloud.syncProductCompanies();
        }

        private void comboLineDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = "";
            if (comboLineDisplay.SelectedIndex > 0)
            {
                value = (comboLineDisplay.SelectedIndex - 1).ToString();
            }

            App.Config().Update("line_display_index", value);
        }

        private void chkEventLog_CheckedChanged(object sender, EventArgs e)
        {
            App.Config().Update("write_event_log", "");
            if (chkEventLog.Checked)
            {
                App.Config().Update("write_event_log", "Enabled");
            }
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.Config().Update("pos_printer_font", cmbFont.Text);
        }

        private void cmbPrintForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.Config().Update("pos_printer_font_size", cmbFontSize.Text);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            eRPCloud.syncCategory();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            eRPCloud.syncRestaurant();
        }
    }
}
