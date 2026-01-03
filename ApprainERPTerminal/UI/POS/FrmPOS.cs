using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Inventory;
using ApprainERPTerminal.Modules.Printer;
using ApprainERPTerminal.UI.CRM;
using ApprainERPTerminal.UI.Inventory;
using ApprainERPTerminal.UI.Report;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmPOS : Form
    {
        private Timer timer;
        private string IndexMode = "QSearch";
        private Config config;
        private DatabaseHelper db;
        private Invoice Invoice;
        private ERPCloud erpCloud;
        JObject otherPaymentList;
        private HttpClient client;
        private ItemIndex itemIndex;
        Utility utility;
        BackgroundProcess backgroundProcess;
        LineDisplay lineDisplay;
        String myFocus = "";
        String tableId = "";
        DataRow TableReccord;

        public FrmPOS(LineDisplay lDisplay)
        {
            init();
            this.lineDisplay = lDisplay;
        }

        public FrmPOS(String tid)
        {
            init();
            this.tableId = tid;
            this.TableReccord = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableId);
        }

        private void init()
        {
            erpCloud = new ERPCloud();
            client = new HttpClient();
            InitializeComponent();
            config = new Config();
            db = new DatabaseHelper();
            Invoice = new Invoice();
            utility = new Utility();

        }

        private void FrmPOS_Closing(Object sender, FormClosingEventArgs e)
        {
            killBackgroundProcess();
        }

        private void killBackgroundProcess()
        {
            if (backgroundProcess != null)
            {
                App.IS_Thread_Running = false;
                backgroundProcess.End();
            }
        }

        private void FrmPOS_Load(object sender, EventArgs e)
        {

            if (!App.isLoggedin())
            {
                int num = (int)MessageBox.Show("Invalid login");
                Application.Exit();
            }
            lblInfo.Text = config.Setting("username") + " - " + config.Setting("terminalcode");
            setLogo();

            this.Invoice.setInvoiceCount(txtSettleButton);
            itemIndex = new ItemIndex(this, pnlProductListBody, pnlProductListHead);

            ClearInvoice();

            if (!tableId.Equals(""))
            {
                loadInvoiceLocallySaved(TableReccord["lastinvoice"].ToString());
            }

            SetuCommonComponent();
            recalculateGrid();
            checkSavedInvoce();
            setProductGirdDisplay();

            string[] common = Utility.InvoiceCommonParameters();
            if (!common.Contains("invoicequickmodedefault"))
            {
                setPaymentPanView();
            }

            gridInvoice.Columns[5].ReadOnly = false;

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); ;
            Debug.WriteLine(timestamp + ": Thread running.. outer");

            if (App.startBackgroundThread())
            {

                backgroundProcess = new BackgroundProcess();
                backgroundProcess.Start(txtSettleButton, timestamp);
            }
            else
            {
                Debug.WriteLine("Found Running Already");
            }


            InitializeWatch();
        }

        private void InitializeWatch()
        {
            lblInfo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblInfo.AutoSize = true;
            lblInfo.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblInfo.ForeColor = Color.Orange;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, e) =>
            {
                lblInfo.Text = DateTime.Now.ToString("hh:mm:ss tt");
            };
            timer.Start();
        }

        private void SetuCommonComponent()
        {
            if (tableId.Equals(""))
            {
                btnSend.Visible = false;
                btnSendbill.Visible = false;
                btnChangeTable.Visible = false;
            }

            btnReceieve.Enabled = App.Config().NavAccess("PRODUCT_LIST");
            btnProfit.Enabled = App.Config().NavAccess("INVOICE_PORFIT");
        }

        private void setLogo()
        {

            String logo = App.Config().Setting("poslogo");
            String path = App.Config().getbasePath("\\" + logo);


            if (File.Exists(path))
            {
                picBoxComLogo.Image = new Bitmap(Image.FromFile(@path));
            }

        }

        private void FromPos_SizeChanged(object sender, EventArgs e)
        {
            setProductGirdDisplay();
        }

        public void setProductGirdDisplay()
        {
            string[] common = Utility.InvoiceCommonParameters();
            if (common.Contains("invoicequickmodedefault"))
            {
                IndexMode = "QSearch";
                tblProducts.Show();
            }
            else
            {
                IndexMode = "Search";
                tblProducts.Hide();
            }

            int width = Convert.ToInt16(tblLayoutSrcAndListPanel.Width * 0.40);
            gridInvoice.Columns[1].Width = (int)(width * .5);
            int x = (int)(width * (.5 / 5));
            gridInvoice.Columns[3].Width = x;
            gridInvoice.Columns[5].Width = x;
            gridInvoice.Columns[9].Width = x;
            gridInvoice.Columns[7].Width = x;
            gridInvoice.Columns[8].Width = x;

            width = Convert.ToInt16(tblLayoutSrcAndListPanel.Width * 0.60);
            x = (int)(width * (.5 / 5));
            gridItemSearch.Columns[1].Width = (int)(width * .5);
            gridItemSearch.Columns[0].Width = x;
            gridItemSearch.Columns[2].Width = x;
            gridItemSearch.Columns[3].Width = x;
            gridItemSearch.Columns[4].Width = x;
            gridItemSearch.Columns[5].Width = x;

        }

        Button btnCashPayment;
        private void loadPaymentMethodGrid()
        {
            DataTable allPaymentMethods;

            gridPaymentmethods.Rows.Clear();

            allPaymentMethods = db.FindAll(DatabaseHelper.INVPAYMENTMETHOD_TABLE);

            if (allPaymentMethods != null)
            {
                // Create Grid 
                gridPaymentmethods.Rows.Add("Cash Receive", "");
                gridPaymentmethods.Rows[0].Cells[0].ToolTipText = "Cash";

                int i = 1;
                foreach (DataRow row in allPaymentMethods.Rows)
                {
                    gridPaymentmethods.Rows.Add(row["name"], "");
                    gridPaymentmethods.Rows[i].Cells[0].ToolTipText = row["id"].ToString();

                    i++;
                }

                int size = allPaymentMethods.Rows.Count;
                var panel = new TableLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                panel.RowCount = allPaymentMethods.Rows.Count / 3;
                panel.ColumnCount = 1;
                if (size <= 10)
                {
                    size = 10;
                }
                for (i = 0; i < size; i++)
                {
                    var percent = 100f;
                    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
                    panel.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
                }


                // Quick Buttons

                if (btnCashPayment == null)
                {
                    btnCashPayment = new Button();
                    pnlQuickPayment.Controls.Add(btnCashPayment);
                    btnCashPayment.Text = "Cash Receive";
                    btnCashPayment.Name = "btnCashPayment";
                    btnCashPayment.ForeColor = Color.Green;
                    btnCashPayment.Click += new System.EventHandler(btnPaymentCash_Click);
                    btnCashPayment.Dock = DockStyle.Fill;
                    btnCashPayment.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    panel.Controls.Add(btnCashPayment);
                }

                Button btn = new Button();
                i = 1;
                foreach (DataRow row in allPaymentMethods.Rows)
                {
                    btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Text = row["name"].ToString();
                    btn.Name = i.ToString();
                    btn.Click += new System.EventHandler(btnPaymentBtn_Click);
                    //btn.Height = 50;
                    // btn.Width = 235;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    panel.Controls.Add(btn);

                    i++;
                }
                pnlQuickPayment.Controls.Add(panel);
            }
        }

        private void btnPaymentBtn_Click(object sender, EventArgs e)
        {
            string s = (sender as Button).Name;

            String PaymentMethodId = gridPaymentmethods.Rows[Int32.Parse(s)].Cells[0].ToolTipText;

            DataRow itemRow = db.Find(DatabaseHelper.INVPAYMENTMETHOD_TABLE, "id='" + PaymentMethodId + "'");
            string spaymenttype = itemRow["spaymenttype"].ToString();


            if (spaymenttype.Equals("EC"))
            {
                utility.ShowMyECRPaymentDialog(itemRow, Invoice.getPayableReamin());

                if (!App.ECR_PAYMENT_VERIFIED.Equals("SUCCESSFUL"))
                {
                    MessageBox.Show("Sorry! Transaction is unsuccessful.");
                    return;
                }
            }

            gridPaymentmethods.Rows[Int32.Parse(s)].Cells[1].Value = Invoice.getPayableReamin();
            recalculateGrid();
            CompletePayment();
        }


        private void btnPaymentCash_Click(object sender, EventArgs e)
        {
            if (Invoice.getPayableReamin() <= 0)
            {
                return;
            }

            FrmCashFrom frmCashFrom = new FrmCashFrom(this, Invoice.getPayableReamin());
            frmCashFrom.ShowDialog(this);
            frmCashFrom.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtQty.Focus();
                return true;
            }
            else if (keyData == Keys.F2)
            {
                txtSearch.Focus();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                switchIndex2Grid();
                return true;
            }
            else if (keyData == Keys.F4)
            {
                focusPaymentReceieveInput();
                return true;
            }
            else if (keyData == Keys.F5)
            {
                Control control = FindFocusedControl(this);
                control.Text = Invoice.getPayableReamin().ToString();
                return true;
            }
            else if (keyData == Keys.F6)
            {
                txtTotalDiscount.Focus();
                return true;
            }
            else if (keyData == Keys.F7)
            {
                txtCustomer.Focus();
                return true;
            }
            else if (keyData == Keys.F9)
            {
                CompletePayment();
                return true;
            }
            else if (keyData == Keys.F11)
            {
                invoiceHoldandLoad();
            }
            else if (keyData == Keys.F12)
            {
                ClearInvoice();
                recalculateGrid();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        private void focusPaymentReceieveInput()
        {
            checkHasItemAdded();

            if (gridPaymentmethods.Rows.Count > 0)
            {


                if (gridPaymentmethods.Visible)
                {
                    gridPaymentmethods.ClearSelection();
                    gridPaymentmethods.Rows[0].Selected = true;
                    gridPaymentmethods.Rows[0].Cells[1].Selected = true;
                    gridPaymentmethods.BeginEdit(true);
                }
                else
                {
                    btnCashPayment.Focus();
                }
            }
        }

        private void checkHasItemAdded()
        {
            if (gridInvoice.RowCount <= 0)
            {
                MessageBox.Show("No Item Added!");

                return;
            }

        }

        public void removeItemInGrid(string barcode)
        {
            ERPCloud erpCloud = new ERPCloud();
            DataRow itemRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "' OR oldbarcode='" + barcode + "'");

            removeItemInGrid(itemRow);
        }

        private void removeItemInGrid(DataRow itemRow)
        {
            double qty;

            if (checkIsLock())
            {
                return;
            }

            qty = Convert.ToDouble(txtQty.Text);

            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    string code = gridInvoice.Rows[index].Cells[0].Value.ToString();
                    double Value = Convert.ToDouble(gridInvoice.Rows[index].Cells[3].Value);
                    string barcode = itemRow["barcode"].ToString();

                    if (code.Equals(barcode))
                    {
                        double newQty = Value - qty;
                        if (newQty <= 0)
                        {
                            gridInvoice.Rows.RemoveAt(index);
                            recalculateGrid();
                            return;
                        }
                        else
                        {
                            gridInvoice.Rows[index].Cells[3].Value = newQty;
                            gridInvoice.Rows[index].Cells[4].Value = itemRow["remain"].ToString();
                            recalculateGridRow(index);
                            return;
                        }
                    }
                }
            }
        }

        public void addItemInGrid(string barcode, double salesprice)
        {
            ERPCloud erpCloud = new ERPCloud();
            DataRow itemRow;
            string[] common = Utility.InvoiceCommonParameters();
            if (common.Contains("invoicequickmodedefault"))
            {
                itemRow = this.db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "' OR oldbarcode='" + barcode + "'");
            }
            else
            {
                itemRow = this.db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");
            }

            //MessageBox.Show("barcode='" + barcode + "' OR oldbarcode='" + barcode + "'");
            if (itemRow == null)
            {
                MessageBox.Show("Insufficient Quantity (001)");
                resetQtyBox();
            }
            else
            {
                addItemInGrid(itemRow, salesprice);
            }
        }


        private double itemAvailableQty(string itemid, double salesprice)
        {
            //String sql = "SELECT sum(remain) remain from " + DatabaseHelper.INVITEM_HISTORY_TABLE + " WHERE itemid=" + itemid + " and unitsalesprice = " + salesprice;
            String sql = "SELECT sum(remain) remain from " + DatabaseHelper.INVITEM_HISTORY_TABLE + " WHERE itemid=" + itemid;

            DataTable RecordSet = db.selectQuery(sql);

            if (RecordSet.Rows.Count <= 0)
            {
                return 0.0;
            }

            DataRow Record = RecordSet.Rows[0];

            String remain = Record["remain"].ToString();

            if (remain.Equals(""))
            {
                return 0;
            }

            return Convert.ToDouble(remain);
        }

        private void addItemInGrid(DataRow itemRow, double salesprice)
        {
            double remain, qty;

            if (checkIsLock())
            {
                return;
            }

            remain = itemAvailableQty(itemRow["id"].ToString(), salesprice);
            qty = Convert.ToDouble(txtQty.Text.Trim());

            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    double gridQty = Convert.ToDouble(gridInvoice.Rows[index].Cells[3].Value);
                    string gridBarcode = gridInvoice.Rows[index].Cells[0].Value.ToString();
                    string gridSaleprice = gridInvoice.Rows[index].Cells[5].Value.ToString();
                    string itemBarcode = itemRow["barcode"].ToString();

                    /* DO NOT MERGE AS CLOULD ACCEPT CLUBED ITEMS
                     */
                    if (gridBarcode.Equals(itemBarcode)) //&& gridSaleprice.Equals(salesprice.ToString())
                    {
                        double totalQty = gridQty + qty;

                        if (totalQty > remain)
                        {
                            MessageBox.Show("Insufficient Quantity (002)");
                            resetQtyBox();
                            return;
                        }
                        gridInvoice.Rows[index].Cells[3].Value = totalQty;
                        gridInvoice.Rows[index].Cells[4].Value = itemRow["remain"].ToString();
                        recalculateGridRow(index);

                        // Bring to top 
                        DataGridViewRow row = gridInvoice.Rows[index];
                        gridInvoice.Rows.RemoveAt(index);
                        gridInvoice.Rows.Insert(0, row);
                        gridInvoice.ClearSelection();
                        gridInvoice.Rows[0].Selected = true;
                        return;
                    }
                }
            }

            if (qty > remain)
            {
                MessageBox.Show("Insufficient Quantity " + " (*) (003)\n\nName: " + itemRow["name"].ToString() + "\nBarcode " + itemRow["barcode"].ToString() + "\nOld Barcode " + itemRow["oldbarcode"].ToString());
                resetQtyBox();
            }
            else
            {

                /**
                 * Add Frsit Item row in Grid 
                 */
                gridInvoice.Rows.Insert(
                    0,
                    new string[]{
                        itemRow["barcode"].ToString(),
                        itemRow["name"].ToString(),
                        "",
                        qty.ToString(),
                        remain.ToString(),
                        salesprice.ToString(),
                        "",
                        setUpToDiscount(itemRow),
                        "",
                        "",
                        itemRow["saleprice"].ToString()
                    }
                );

                // Recalculate the first Row of the grid 
                recalculateGridRow(0);

                recalculateGrid();
            }
        }

        private string definedMaxDiscountByBarcode(String barcode)
        {
            DataRow Item = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");

            if (Item == null) return "";

            if (Item["setup"].ToString().Trim().Equals("")) return "";

            try
            {
                JObject setupObject = JObject.Parse(Item["setup"].ToString());
                if (setupObject.ContainsKey("maxdiscount"))
                {
                    return setupObject.GetValue("maxdiscount").ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
            }



            return setUpToDiscount(Item);

        }

        private string setUpToDiscount(DataRow Item)
        {
            string setup = Item["setup"].ToString();

            if (string.IsNullOrEmpty(setup))
            {
                return "";
            }

            try
            {
                if (setup.Contains("{"))
                {
                    JObject json = JObject.Parse(setup);
                    if (!setup.Contains("autodiscount"))
                    {
                        return "";
                    }

                    return json["autodiscount"].ToString();
                }
                else
                {
                    return setup;
                }
            }
            catch (JsonReaderException ex)
            {
                return "";
            }
        }


        private async Task downloadItemRemove(string code, double qty, double salesprice)
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "fetchItem");
                nameValueCollection.Add("storeid", config.Setting("storeid"));
                nameValueCollection.Add("barcode", code);

                FormUrlEncodedContent content = new FormUrlEncodedContent(nameValueCollection);

                var response = await client.PostAsync(config.UrldataExchnage(), content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1") && state.Equals("100"))
                {
                    DataRow dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + code + "'");

                    String ItemData = Convert.ToString(dynObj["data"]);
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(ItemData);

                    values.Add("syncflag", "");

                    if (dataRow != null)
                    {
                        values.Remove("id");
                        values.Add("id", dataRow["id"].ToString());
                        db.Update(DatabaseHelper.INVITEMS_TABLE, values);
                    }
                    else
                    {
                        db.Insert(DatabaseHelper.INVITEMS_TABLE, values);
                    }

                    String stockData = Convert.ToString(dynObj["stock"]);
                    if (!stockData.Equals(""))
                    {
                        JArray jarray = JArray.Parse(stockData);

                        if (jarray != null)
                        {
                            db.execSQL("DELETE FROM " + DatabaseHelper.INVITEM_HISTORY_TABLE + " where itemid=" + dataRow["id"].ToString());
                            IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                            try
                            {
                                while (enumerator.MoveNext())
                                {
                                    values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                    values.Add("value", values["remain"]);
                                    db.Insert(DatabaseHelper.INVITEM_HISTORY_TABLE, values);

                                    db.execSQL(
                                        "UPDATE " + DatabaseHelper.INVITEMS_TABLE +
                                        "  SET remain = (SELECT sum(value) FROM " + DatabaseHelper.INVITEM_HISTORY_TABLE + " WHERE itemid = " + values["itemid"] + ") where id = " + values["itemid"]
                                    );
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        //System.Diagnostics.Debug.WriteLine(stockData);
                    }

                    DataRow itemRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + code + "' or oldbarcode='" + code + "'");

                    double remain = itemAvailableQty(itemRow["id"].ToString(), salesprice);

                    if (remain < qty)
                    {
                        MessageBox.Show("Insufficeint Qty (004)");
                    }
                    else
                    {
                        addItemInGrid(itemRow, salesprice);
                    }
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Item Found (Local Database)!\n\n" + ex.Message);
                return;
            }
        }

        private bool checkIsLock()
        {
            if (!this.Invoice.isLocked())
            {
                return false;
            }

            MessageBox.Show("Invoice Locekd");

            return true;
        }

        private void recalculateGridRow(int row)
        {
            double qty, salesprice, remain, subtotal, vatAmount, vatRate;
            string str;
            DataRow dataRow;

            try
            {
                qty = Convert.ToDouble(this.gridInvoice.Rows[row].Cells[3].Value);
                salesprice = Convert.ToDouble(this.gridInvoice.Rows[row].Cells[5].Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            str = Convert.ToString(this.gridInvoice.Rows[row].Cells[0].Value);
            dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + str + "'");

            remain = Convert.ToDouble(gridInvoice.Rows[row].Cells[4].Value);
            if (qty > remain)
            {
                MessageBox.Show("Insufficient Quantity (005)");
                return;
            }


            vatRate = Invoice.VatRate(dataRow["id"].ToString());
            gridInvoice.Rows[row].Cells[6].Value = Utility.Round(vatRate, 2);

            subtotal = qty * salesprice;

            vatAmount = calculateTax(row) * qty;
            subtotal += vatAmount;

            gridInvoice.Rows[row].Cells[10].Value = Utility.Round(subtotal, 2);

            lblMessage.Text = dataRow["name"]?.ToString() + " @ " + Utility.Round(salesprice, 2).ToString();

            recalculateGrid();
        }

        private double calculateTax(int row, bool multiply)
        {
            double vat, qty;

            vat = calculateTax(row);
            if (vat <= 0)
            {
                return 0;
            }

            qty = Convert.ToDouble(gridInvoice.Rows[row].Cells[3].Value);

            return vat * qty;
        }

        private double calculateTax(int row)
        {
            double vatRate, vat = 0, unitprice;

            try
            {
                if (gridInvoice.Rows[row].Cells[6].Value == null)
                {
                    return 0;
                }

                vatRate = Convert.ToDouble(gridInvoice.Rows[row].Cells[6].Value.ToString());

                if (vatRate <= 0)
                {
                    return 0;
                }

                unitprice = Convert.ToDouble(gridInvoice.Rows[row].Cells[5].Value);

                vat = (unitprice * vatRate) / 100;
            }
            catch (Exception ex)
            {
                return 0;
            }

            return vat;
        }

        public void loadInvoice(DataRow Invoice2Load)
        {
            DataTable all = db.FindAll(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + Invoice2Load["id"]?.ToString());
            gridInvoice.Rows.Clear();
            foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
            {
                DataRow dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "id=" + row["itemid"]?.ToString());
                if (dataRow != null)
                {
                    string str = row["discount"].ToString();
                    if (row["discountmethod"].ToString().Equals("P"))
                    {
                        str += "%";
                    }

                    gridInvoice.Rows.Add(
                            dataRow["barcode"],
                            dataRow["name"],
                            "",
                            row["value"],
                            dataRow["remain"],
                            row["unitprice"],
                            "",
                            str,
                            "",
                            row["returned"],
                            row["subtotal"]
                     );
                }
            }
            this.Invoice = new Invoice((long)Invoice2Load["id"]);
            this.recalculateGrid();
        }

        private void recalculateGrid()
        {
            //Recalculate Other Payment Grid
            double amount = 0;
            double amountTotal = 0;
            otherPaymentList = new JObject();
            try
            {
                for (int index = 1; index < gridPaymentmethods.Rows.Count; ++index)
                {
                    if (!gridPaymentmethods.Rows[index].Cells[1].Value.Equals(""))
                    {
                        amount = Convert.ToDouble(gridPaymentmethods.Rows[index].Cells[1].Value);
                        if (amount > 0)
                        {
                            amountTotal += amount;
                            otherPaymentList.Add(gridPaymentmethods.Rows[index].Cells[0].ToolTipText, amount.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Invoice.setOtherPaymentReceived(amountTotal);
            Invoice.setOtherPaymentReceivedList(otherPaymentList);
            //#////////////////////////////////

            double totalPayable = 0.0;
            double totalDiscount = 0.0;
            double totalVat = 0.0;
            double total = 0.0;
            double totalReturnAmount = 0.0;
            double totalRound = Invoice.getRound();
            double totalRedeemAmount = Invoice.getRedeemAmount();
            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    double returnQty = 0.0;
                    double rowTotal = Convert.ToDouble(this.gridInvoice.Rows[index].Cells[10].Value);
                    if (!this.gridInvoice.Rows[index].Cells[9].Value.Equals(""))
                    {
                        returnQty = Convert.ToDouble(gridInvoice.Rows[index].Cells[9].Value);
                    }

                    double salesprice = Convert.ToDouble(gridInvoice.Rows[index].Cells[5].Value);
                    double vat = calculateTax(index, true);
                    double discountamount = gridRowDiscount(index);

                    totalVat += vat;
                    total += rowTotal;
                    totalPayable += rowTotal - discountamount;
                    totalDiscount += discountamount;
                    totalReturnAmount += returnQty * salesprice;
                }
            }


            double returnToCustomer = Invoice.getReturnToCustomer();
            if (returnToCustomer > 0.0)
            {
                Label lblMessage = this.lblMessage;
                returnToCustomer = Math.Round(returnToCustomer, 2);
                string str = "CASH RETURN " + returnToCustomer + " " + config.Setting("currency");
                lblMessage.Text = str;
            }

            if (totalReturnAmount > 0.0)
            {
                Label lblMessage = this.lblMessage;
                totalReturnAmount = Math.Round(totalReturnAmount, 2);
                string str = "ITEM RETURN " + totalReturnAmount + " " + config.Setting("currency");
                lblMessage.Text = str;
            }

            totalPayable = totalPayable - totalRedeemAmount - totalRound;


            //Invoice.setCreditReceived(totalPayable);
            Invoice.setTotalDiscount(totalDiscount);
            Invoice.setTotalVat(totalVat);
            Invoice.setTotalPayable(totalPayable);
            Invoice.setTotal(total);
            double payableReamin = Invoice.getPayableReamin();



            if (payableReamin >= 0.0)
            {
                lblPayable.Text = Math.Round(payableReamin, 2).ToString();
            }

            if (Invoice.AdjustmentRecovery() > 0)
            {
                lblMessage.Text = "ADD MORE OF " + Math.Round(Invoice.AdjustmentRecovery(), 2) + " " + config.Setting("currency");
                lblPayable.Text = "-" + Math.Round(Invoice.AdjustmentRecovery(), 2);

            }
            else if (payableReamin <= 0.0 && gridInvoice.Rows.Count > 0)
            {
                lblPayable.Text = "Full Paid";
            }

            // Set common total values 
            lblTotalSale.Text = Utility.currencyFormat(Invoice.getTotalWithOutVat(), 2);
            lblTax.Text = Utility.currencyFormat(Invoice.getTotalVat(), 2);
            lblDiscount.Text = Utility.currencyFormat(Invoice.getTotalDiscount().ToString(), 2);
            lblTotalPayable.Text = Utility.currencyFormat(Invoice.getPayable().ToString(), 2);
            lblCredit.Text = Utility.currencyFormat(Invoice.getCreditReceived().ToString(), 2);

            setStatusLbl();
        }

        private double getInvoiceGridRowTotal(int row)
        {
            string total = gridInvoice.Rows[row].Cells[10].Value.ToString();

            if (total == null || total.Length == 0)
            {
                return 0.0;
            }

            return Convert.ToDouble(total);
        }

        private double gridRowDiscount(int rows)
        {
            return gridRowDiscount(rows, false);
        }

        private double gridRowDiscount(int rows, bool isUnitWise)
        {
            double TotalDiscount = 0;
            if (gridInvoice.Rows[rows].Cells[7].Value == null)
            {
                return 0.0;
            }

            string str = gridInvoice.Rows[rows].Cells[7].Value.ToString();

            if (str == null || str.Equals(""))
            {
                return 0.0;
            }

            double unitdiscount = 0;
            if (str.EndsWith("%"))
            {
                unitdiscount = Convert.ToDouble(gridInvoice.Rows[rows].Cells[5].Value) * Convert.ToDouble(str.Replace("%", "")) / 100.0;
            }
            else
            {
                unitdiscount = Convert.ToDouble(str);
            }

            if (isUnitWise)
            {
                return unitdiscount;
            }

            double qty = Convert.ToDouble(gridInvoice.Rows[rows].Cells[3].Value);

            TotalDiscount = unitdiscount * qty;

            gridInvoice.Rows[rows].Cells[5].ToolTipText = unitdiscount.ToString();


            return TotalDiscount;
        }

        public void setStatusLbl()
        {
            Invoice.setInvoiceCount(txtSettleButton);
            Text = "NEW INVOICE";
            if (Invoice.InvoiceId > 0)
            {
                Text = "INVOICE ID : " + Invoice.InvoiceId.ToString();
            }
            if (!tableId.Equals(""))
            {
                Text = Text + " | TABLE ID : " + tableId;
            }

            Text = Text + " | " + App.Config().Setting("site_title");

            gridInvoice.Columns["Qty"].ReadOnly = false;
            gridInvoice.Columns["discount"].ReadOnly = false;
            gridInvoice.Columns["warranty"].ReadOnly = false;
            gridInvoice.Columns["tax"].ReadOnly = false;
            gridInvoice.Columns["itemreturn"].ReadOnly = true;
            if (!Invoice.Status().Equals("Process"))
            {
                gridInvoice.Columns["Qty"].ReadOnly = true;
                gridInvoice.Columns["discount"].ReadOnly = true;
                gridInvoice.Columns["warranty"].ReadOnly = true;
                gridInvoice.Columns["tax"].ReadOnly = true;
            }
            if (Invoice.Status().Equals("Complete"))
            {
                gridInvoice.Columns["itemreturn"].ReadOnly = false;
            }
            lblInvoiceStatus.Text = Invoice.Status();

            double tQty = 0;
            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    double Value = Convert.ToDouble(gridInvoice.Rows[index].Cells[3].Value);
                    tQty += Value;
                }
            }

            lblItemCount.Text = gridInvoice.Rows.Count.ToString() + "/" + tQty.ToString(); ;

            resetQtyBox();
            displaySales();


        }

        private void displaySales()
        {
            if (lineDisplay == null)
            {
                return;
            }

            lineDisplay.displaySales(
                gridInvoice,
                Invoice.getTotalWithOutVat(),
                Invoice.getTotalVat(),
                Invoice.getTotalDiscount(),
                Invoice.getPayable()
            );
        }

        private void clearDisplaySales()
        {
            if (lineDisplay == null)
            {
                return;
            }

            lineDisplay.clearScreen();
        }

        private void resetQtyBox()
        {
            txtQty.Text = "1";
        }

        private void proceedToAddItem(String barcode)
        {
            string weightscale_barcode_prifix = App.Config().Setting("inventorysettings_weightscale_barcode_prifix");
            if (!weightscale_barcode_prifix.Equals(""))
            {
                try
                {
                    String[] result = weightscale_barcode_prifix.Split(',');
                    if (result.Length != 2)
                    {
                        MessageBox.Show("Weight Scale Wrongly Configured");
                        return;
                    }

                    String prefix = result[0];
                    int prefix_brcode_length = Convert.ToInt32(result[1]);

                    int weightscale_barcode_length = prefix.Length;

                    if (barcode.Substring(0, prefix.Length).Equals(prefix))
                    {

                        String barcodeTemp = barcode;
                        barcode = barcode.Substring(1, prefix_brcode_length - 1);

                        String qty = barcodeTemp.Substring(prefix_brcode_length, 6);

                        txtQty.Text = (Convert.ToDouble(qty) / 1000).ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }



            if (barcode.Equals(""))
            {
                if (gridInvoice.Rows.Count > 0)
                {
                    focusPaymentReceieveInput();
                }
                return;
            }
            txtSearch.Text = "";

            String condition = "(barcode = '" + barcode + "' OR oldbarcode = '" + barcode + "') and history.remain > 0";
            string query = "Select barcode,name,unitsalesprice,saleprice,modelno,history.remain remain from " + DatabaseHelper.INVITEMS_TABLE + " item left join " + DatabaseHelper.INVITEM_HISTORY_TABLE + " history on item.id=history.itemid where " + condition + " limit 0,2";

            DataTable all = db.selectQuery(query);

            if (all.Rows.Count == 0)
            {
                resetQtyBox();
                MessageBox.Show("No Item/Stock found!");
            }
            else if (all.Rows.Count == 1)
            {
                DataRow row = all.Rows[0];
                double salesprice = Convert.ToDouble(row["unitsalesprice"]);

                addItemInGrid(barcode, salesprice);
            }
            else
            {
                doSearch(barcode);
            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (!txtSearch.Text.Equals(""))
                {
                    if (gridItemSearch.Rows.Count > 0)
                    {
                        gridItemSearch.CurrentCell = gridItemSearch.Rows[0].Cells[5];
                        gridItemSearch.Focus();
                    }
                }
                else
                {
                    if (gridInvoice.Rows.Count > 0)
                    {
                        gridInvoice.CurrentCell = gridInvoice.Rows[0].Cells[3];
                        gridInvoice.Focus();
                    }
                }
            }

            if (e.KeyCode == Keys.Return)
            {
                string text = txtSearch.Text;
                if (text.Equals("") && gridInvoice.Rows.Count > 0)
                {
                    focusPaymentReceieveInput();
                    return;
                }

                proceedToAddItem(text);
            }

            if (e.KeyCode != Keys.F4)
            {
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            doSearch(txtSearch.Text);
        }

        private void doSearch(string text)
        {
            if (checkIsLock())
            {
                return;
            }
            gridItemSearch.Rows.Clear();

            if (text.Equals(""))
            {
                //gridItemSearch.Hide();
                showIndex2Grid();

            }
            else
            {
                gridItemSearch.Show();
                tblProducts.Hide();
                string condition = " (name like '%" + text + "%' OR barcode like '%" + text + "%' OR oldbarcode like '%" + text + "%')  and history.remain > 0 ";
                string query = "Select barcode,name,company,unitsalesprice,max(saleprice) saleprice,modelno,sum(history.remain) remain from app_invitems item left join app_invhistory history on item.id=history.itemid where " + condition + " group by name,unitsalesprice,modelno order by name ASC limit 0,20";

                DataTable all = db.selectQuery(query);

                if (all == null)
                {
                    return;
                }

                foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
                {
                    try
                    {
                        string historysales;
                        double remain = 0;
                        DataRow companyRow;
                        if (Convert.IsDBNull(row["unitsalesprice"]))
                        {
                            historysales = row["saleprice"].ToString();
                        }
                        else
                        {
                            historysales = row["unitsalesprice"].ToString();
                        }
                        if (!Convert.IsDBNull(row["remain"]))
                        {
                            remain = Convert.ToDouble(row["remain"]);
                        }

                        string[] common = Utility.ItemDisplayFormatParameters();
                        if (common.Contains("company"))
                        {
                            companyRow = db.Find(DatabaseHelper.INVITEMS_COMPANY_TABLE, "id=" + row["company"]);
                            if (companyRow != null)
                            {
                                row["name"] += " > " + companyRow["name"].ToString();
                            }
                        }


                        gridItemSearch.Rows.Add(
                            row["barcode"],
                            row["name"],
                            Utility.Round(Convert.ToDouble(historysales), 2),
                            row["modelno"],
                            Utility.Round(remain, 2),
                            "0"
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void gridItemSearch_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int index = 0; index < gridItemSearch.Rows.Count; ++index)
            {
                gridItemSearch.Rows[index].DefaultCellStyle.BackColor = Color.White;
            }

            gridItemSearch.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

        }
        private void gridItemSearch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (gridItemSearch.RowCount <= 0 || e.ColumnIndex != 5)
            {
                return;
            }

            try
            {
                double avl = Convert.ToDouble(gridItemSearch.Rows[e.RowIndex].Cells[4].Value);
                double Value = Convert.ToDouble(gridItemSearch.Rows[e.RowIndex].Cells[5].Value);
                double salesprice = Convert.ToDouble(gridItemSearch.Rows[e.RowIndex].Cells[2].Value);

                if (Value > avl)
                {
                    MessageBox.Show("Insufficient Quantity (006)");
                    resetQtyBox();
                    return;
                }

                txtQty.Text = Value.ToString();
                string barcode = Convert.ToString(gridItemSearch.Rows[e.RowIndex].Cells[0].Value);
                addItemInGrid(barcode, salesprice);
                gridItemSearch.Rows.Clear();
                txtSearch.Text = "";
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClearInvoice_Click(object sender, EventArgs e)
        {
            if (!App.Utility().checkPinVerified("ClearInvoice")) return;
            ClearInvoice();
            recalculateGrid();
        }

        private void ClearInvoice()
        {
            recalculateGrid();

            txtRemark.Text = "";
            lblPayable.Text = "0.00";
            lblInvoiceStatus.Text = "Process";
            txtTotalDiscount.Text = "";
            txtRedeem.Text = "";
            txtRound.Text = "";


            gridInvoice.Rows.Clear();
            Invoice = new Invoice();

            lblAdjustment.Text = App.Config().Setting("invoice_item_rtn_amount", "0.00");

            loadPaymentMethodGrid();

            itemIndex.clear().load();
            lblItemCount.Text = "0";
            lblTotalSale.Text = "0.00";
            lblTax.Text = "0.00";
            lblDiscount.Text = "0.00";
            lblTotalPayable.Text = "0.00";
            txtCustomer.Text = "";
            lblMRPoints.Text = ""; ;
            lblCustomerName.Text = "Guest";

            clearDisplaySales();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {

            new FrmManageItems().ShowDialog();
        }

        public void CompletePayment()
        {
            if (gridInvoice.Rows.Count <= 0 || checkIsLock())
            {
                return;
            }

            if (Invoice.getPayableReamin() > 0.0)
            {
                MessageBox.Show("Full Payment not completed yet!");
            }
            else
            {
                long Invoiceid = Invoice.saveInvoiceEntryByDataGrid(gridInvoice);

                printInvoice(Invoiceid);
                ClearInvoice();
                back2TableList("Open");
            }
        }

        private void printInvoice(long Invoiceid)
        {

            new PrintPOSInvoice(Invoiceid)
                .setLoyaltyBalance(Invoice.getLoyaltyBalance())
                .setCustomerName(Invoice.getCustomerName())
                .setTableId(tableId)
                .Print();


            JObject InvoiceJObject = getLocalInvoice();
            if (Convert.ToInt16(InvoiceJObject.GetValue("count")) > 0)
            {
                new PrintKOT(tableId).Print(InvoiceJObject.GetValue("body").ToString());
            }
        }

        private void printInvoice(long Invoiceid, String Status)
        {

            new PrintPOSInvoice(Invoiceid)
                .setLoyaltyBalance(Invoice.getLoyaltyBalance())
                .setCustomerName(Invoice.getCustomerName())
                .setPaymentStatus("Unpaid")
                .setTableId(tableId)
                .Print();


            JObject InvoiceJObject = getLocalInvoice();
            if (Convert.ToInt16(InvoiceJObject.GetValue("count")) > 0)
            {
                new PrintKOT(tableId).Print(InvoiceJObject.ToString());
            }
        }

        private void txtReceieve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                return;
            }

            CompletePayment();
        }

        public void CashReceive(double received)
        {
            Invoice.setCashReceived(received);
            recalculateGrid();
        }

        string cellOldValue = "";
        private void gridInvoice_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellOldValue = gridInvoice[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        private void gridInvoice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gridInvoice.RowCount <= 0)
            {
                return;
            }

            if (e.ColumnIndex == 7)
            {
                string code = gridInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                string gridSaleprice = gridInvoice.Rows[e.RowIndex].Cells[5].Value.ToString();
                string assignedDiscount = gridInvoice.Rows[e.RowIndex].Cells[7].Value.ToString();
                DataRow dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + code + "'");

                double purchaseprice = App.Common().itemAvailablePurchasePrice(dataRow["id"].ToString(), gridSaleprice);
                double discountamount = gridRowDiscount(e.RowIndex, true);
                double desiredsalesprice = Convert.ToDouble(gridSaleprice) - discountamount;

                String[] param = Utility.InvoiceCommonParameters();
                if (purchaseprice > desiredsalesprice && param.Contains("restrictdisbelwpp"))
                {
                    gridInvoice.Rows[e.RowIndex].Cells[7].Value = "0%";
                    MessageBox.Show("Discounts more than purchase price not permitted.\n\n (" + dataRow["name"].ToString() + ")");
                    return;
                }

                if (!dataRow["setup"].ToString().Equals(""))
                {
                    JObject setupObject = JObject.Parse(dataRow["setup"].ToString());
                    if (setupObject.ContainsKey("maxdiscount"))
                    {
                        String maxdiscount = setupObject.GetValue("maxdiscount").ToString().Replace("%", "");

                        assignedDiscount = assignedDiscount.Replace("%", "");

                        if (Convert.ToDouble(assignedDiscount) > Convert.ToDouble(maxdiscount))
                        {
                            gridInvoice.Rows[e.RowIndex].Cells[7].Value = "0%";
                            MessageBox.Show("Discounts of more than " + maxdiscount + "% are not permitted.\n\n (" + dataRow["name"].ToString() + ")");
                            return;
                        }
                    }
                }
            }


            if (e.ColumnIndex == 3)
            {

                double qty = Convert.ToDouble(gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (!string.IsNullOrEmpty(cellOldValue) && qty < Convert.ToDouble(cellOldValue))
                {
                    if (!utility.checkPinVerified("ChangeQty"))
                    {
                        gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellOldValue;
                        return;
                    }
                }


                string code = gridInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                double saleprice = Convert.ToDouble(gridInvoice.Rows[e.RowIndex].Cells[5].Value.ToString());
                double remain = Convert.ToDouble(gridInvoice.Rows[e.RowIndex].Cells[4].Value.ToString());
                DataRow dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + code + "'");

                if (qty <= 0.0)
                {
                    if (Invoice.InvoiceId > 0)
                    {
                        string condition = "invoiceid='" + Invoice.InvoiceId.ToString() + "' and itemid=" + dataRow["id"].ToString();
                        DataRow HistoryRecord = db.Find(DatabaseHelper.INVOICE_HISTORY_TABLE, condition);
                        Invoice.RemoveHistoryById(HistoryRecord["id"].ToString());
                    }
                    gridInvoice.Rows.RemoveAt(e.RowIndex);
                    recalculateGrid();
                    return;
                }

                if (qty > remain)
                {
                    MessageBox.Show("Insufficient Quantity \n\n" + dataRow["name"].ToString() + "(007)");
                    gridInvoice.Rows[e.RowIndex].Cells[3].Value = cellOldValue;
                    resetQtyBox();
                    return;
                }
            }

            if (e.ColumnIndex == 9)
            {
                if (Invoice.getOnlineId().Equals(""))
                {
                    MessageBox.Show("Upload all invoice before execute return");
                    return;
                }
                if (gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(""))
                {
                    return;
                }

                string barcode = Convert.ToString(gridInvoice.Rows[e.RowIndex].Cells[0].Value);
                double num1 = Convert.ToDouble(gridInvoice.Rows[e.RowIndex].Cells[3].Value);
                double rtnQty = Convert.ToDouble(gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (rtnQty > 0.0 && num1 < rtnQty)
                {
                    MessageBox.Show("Invalid Quantity");
                    gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    return;
                }

                if (!App.Utility().checkPinVerified("ItemReturn"))
                {
                    gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    return;
                }

                Invoice.updateReturn(barcode, rtnQty);
            }

            if (e.ColumnIndex == 5)
            {
                if (!utility.checkPinVerified("ChangePrice"))
                {
                    gridInvoice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellOldValue;
                    return;
                }
            }

            recalculateGridRow(e.RowIndex);
        }

        private void txtSettleButton_Click(object sender, EventArgs e)
        {
            killBackgroundProcess();
            db.execSQL("UPDATE " + DatabaseHelper.INVOICE_TABLE + "  SET syncflag = 1 WHERE syncflag > 1");
            erpCloud.UploadInvoice(txtSettleButton);
        }

        private void btnInquiry_Click(object sender, EventArgs e)
        {
            FrmPOSDialog frmPOSDialog = new FrmPOSDialog(this, "Search Invoice");
            frmPOSDialog.ShowDialog(this);
            frmPOSDialog.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private long findLastInvoiceId()
        {
            DataRow record = db.Find(DatabaseHelper.INVOICE_TABLE, "1=1 order by id desc");

            if (record != null)
            {
                return Convert.ToInt32(record["id"].ToString());
            }

            return 0;
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            long invoiceId;
            if (gridInvoice.Rows.Count > 0)
            {
                if (!Invoice.isLocked())
                {
                    MessageBox.Show("No qualified invoice found.");
                    return;
                }

                invoiceId = Invoice.getId();
            }
            else
            {
                invoiceId = findLastInvoiceId();

                if (invoiceId == 0)
                {
                    MessageBox.Show("No qualified invoice found.");
                    return;
                }
            }

            printInvoice(invoiceId);
            ClearInvoice();

        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

        private void gridInvoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            this.Invoice.setRemark(txtRemark.Text);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void gridPaymentmethods_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Invoice.getPayableReamin() <= 0)
                {
                    CompletePayment();
                }

            }
        }
        private void gridPaymentmethods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double received;

            if (gridInvoice.RowCount <= 0)
            {
                return;
            }

            if (e.ColumnIndex != 1)
            {
                return;
            }

            try
            {
                if (gridPaymentmethods.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    return;
                }

                string value = gridPaymentmethods.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (value.Equals(""))
                {
                    return;
                }

                if (!Double.TryParse(value, out Double amount))
                {
                    MessageBox.Show("Plase enter a correct value.");
                    return;
                }

                received = amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid value entered");
                return;
            }


            try
            {
                if (e.RowIndex == 0)
                {
                    CashReceive(received);
                }
                else
                {
                    if (received == 0)
                    {
                        recalculateGrid();
                        return;
                    }

                    String PaymentMethodId = gridPaymentmethods.Rows[e.RowIndex].Cells[0].ToolTipText;

                    DataRow itemRow = db.Find(DatabaseHelper.INVPAYMENTMETHOD_TABLE, "id='" + PaymentMethodId + "'");
                    string spaymenttype = itemRow["spaymenttype"].ToString();

                    if (spaymenttype.Equals("EC"))
                    {
                        utility.ShowMyECRPaymentDialog(itemRow, received);

                        if (!App.ECR_PAYMENT_VERIFIED.Equals("SUCCESSFUL"))
                        {
                            gridPaymentmethods.Rows[e.RowIndex].Cells[1].Value = 0;
                            MessageBox.Show("Sorry! Verification is unsuccessful.");
                            return;
                        }
                    }
                }

                recalculateGrid();

                // Set credit amount 
                setCreditAmount();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect data entry" + ex.Message);
            }
        }

        private void setCreditAmount()
        {
            string creditsale_disabled = App.Config().Setting("inventorysettings_credit_invoice_disabled");
            if (creditsale_disabled.Equals("No"))
            {
                string CmId = Invoice.getCustomerId();

                if (!string.IsNullOrEmpty(CmId))
                {
                    double getPayableReamin = Invoice.getPayableReaminExceptCredit();
                    Invoice.setCreditReceived(getPayableReamin);
                    recalculateGrid();
                }
            }
        }

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            ClearInvoice();
            recalculateGrid();
        }

        private void btnCashReturn_Click(object sender, EventArgs e)
        {
            if (!App.Utility().checkPinVerified("ItemReturn")) return;

            if (Invoice.getId() <= 0)
            {
                MessageBox.Show("Unprocessed invoice can not void!");
                return;
            }
            App.Config().Update("invoice_item_rtn_amount", "");
            App.Config().Update("invoice_item_rtn_invoice_id", "");
            printInvoice(Invoice.getId());
            ClearInvoice();
            recalculateGrid();
        }

        private void showIndex2Grid()
        {
            if (IndexMode.Equals("QSearch"))
            {
                setProductGirdDisplay();
            }
            else
            {
                tblProducts.Hide();
                gridInvoice.Left = 0;
                gridInvoice.Width = gridItemSearch.Width;
            }
        }

        private void switchIndex2Grid()
        {
            if (tblProducts.Visible)
            {
                IndexMode = "Search";
                tblProducts.Hide();
            }
            else
            {
                IndexMode = "QSearch";
                setProductGirdDisplay();
            }
        }

        private void showItemIndex()
        {
            tblProducts.Show();
            itemIndex.clear().load();
        }

        private void lblPayable_Click(object sender, EventArgs e)
        {
        }

        private void txtTotalDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                performDiscunt();
                focusPaymentReceieveInput();
                return;
            }
        }

        private void txtTotalDiscount_Leave(object sender, EventArgs e)
        {
            txtRound.Text = "";
        }

        private void performDiscunt()
        {
            performDiscunt(true);
        }

        private void performDiscunt(bool pinVerification)
        {
            if (gridInvoice.Rows.Count <= 0) return;

            try
            {
                String max_discount_pin_verification = config.Setting("inventorysettings_max_discount_pin_verification").Trim();
                String discount = txtTotalDiscount.Text;
                if (discount.Equals(""))
                {
                    discount = "0";
                }

                if (!discount.EndsWith("%"))
                {
                    double Total = Invoice.getTotalWithOutVat();
                    double discuntvalue = Convert.ToDouble(discount);
                    double percentage_desired = 100 * (discuntvalue / Total);
                    percentage_desired = Utility.Round(percentage_desired, 2);

                    discount = percentage_desired.ToString() + "%";
                }

                try
                {
                    if (!max_discount_pin_verification.Equals(""))
                    {
                        double discountstr = Convert.ToDouble(discount.Replace("%", ""));
                        double max_discount = Convert.ToDouble(max_discount_pin_verification.Replace("%", ""));
                        if (discountstr > 0 && discountstr > max_discount)
                        {
                            if (pinVerification && !App.Utility().checkPinVerified("Discount"))
                            {
                                discount = "0";
                                txtTotalDiscount.Text = discount;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    discount = "0";
                    txtTotalDiscount.Text = "0";
                    MessageBox.Show(ex.Message + " ERR DIS 001");
                }

                String barcode;
                String definediscount = "";
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    barcode = gridInvoice.Rows[index].Cells[0].Value.ToString();
                    definediscount = definedMaxDiscountByBarcode(barcode);

                    if (!definediscount.Equals(""))
                    {
                        gridInvoice.Rows[index].Cells[7].Value = definediscount;
                    }
                    else
                    {
                        gridInvoice.Rows[index].Cells[7].Value = discount;
                    }
                }


                recalculateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " ERR DIS 002");
            }
        }

        private bool checkSavedInvoce()
        {
            String saved_invoice_locally = App.Config().Setting("saved_invoice_locally");
            if (!saved_invoice_locally.Equals(""))
            {
                btnSave.Text = "Load";
                btnSave.ForeColor = Color.Red;
                return true;
            }
            btnSave.Text = "Hold";
            btnSave.ForeColor = Color.Black;

            return false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            invoiceHoldandLoad();
        }

        private void invoiceHoldandLoad()
        {
            if (checkSavedInvoce())
            {
                String localInvoice = App.Config().Setting("saved_invoice_locally");
                loadInvoiceLocallySaved(localInvoice);
                App.Config().Update("saved_invoice_locally", "");
            }
            else
            {
                saveInvoiceEntryLocallyByDataGrid();
                ClearInvoice();
            }

            // FX Called to reset the button title
            checkSavedInvoce();
        }

        public JObject getLocalInvoice()
        {

            JObject jInvoie = new JObject();
            JArray jarray = new JArray();
            if (gridInvoice.Rows.Count > 0)
            {
                for (int index = 0; index < gridInvoice.Rows.Count; ++index)
                {
                    JObject jobHistory = new JObject();
                    jobHistory.Add("barcode", gridInvoice.Rows[index].Cells[0].Value.ToString());
                    jobHistory.Add("value", gridInvoice.Rows[index].Cells[3].Value.ToString());
                    jobHistory.Add("warranty", gridInvoice.Rows[index].Cells[8].Value.ToString());
                    jobHistory.Add("discount", gridInvoice.Rows[index].Cells[7].Value.ToString());
                    jobHistory.Add("salesprice", gridInvoice.Rows[index].Cells[5].Value.ToString());

                    jarray.Add(jobHistory);
                }
            }

            JObject otherPRlist = new JObject();
            double received = 0;
            if (gridPaymentmethods.Rows.Count > 0)
            {
                for (int i = 0; i < gridPaymentmethods.Rows.Count; ++i)
                {
                    otherPRlist.Add(
                        gridPaymentmethods.Rows[i].Cells[0].ToolTipText,
                        gridPaymentmethods.Rows[i].Cells[1].Value.ToString()
                     );

                    string rcv = gridPaymentmethods.Rows[i].Cells[1].Value.ToString();

                    received += string.IsNullOrEmpty(rcv) ? 0 : Convert.ToDouble(gridPaymentmethods.Rows[i].Cells[1].Value.ToString());
                }
            }

            string remark = txtRemark.Text;

            string invoiceid = "";
            if(Invoice != null)
            {
                if (Invoice.InvoiceId > 0)
                {
                    invoiceid = Invoice.InvoiceId.ToString();
                }
            }

            JObject jInvoieHead = new JObject();
            jInvoieHead.Add("client", "");
            jInvoieHead.Add("invoiceid", invoiceid);
            jInvoieHead.Add("count", gridInvoice.Rows.Count);
            jInvoieHead.Add("received", received);
            jInvoieHead.Add("remark", remark);
            jInvoieHead.Add("paymentreceivedlist", otherPRlist);

            jInvoie.Add("head", jInvoieHead);
            jInvoie.Add("body", jarray);

            //MessageBox.Show(jInvoie.GetValue("body").ToString());

            return jInvoie;
        }

        public void saveInvoiceEntryLocallyByDataGrid()
        {
            JObject jOInvoice = getLocalInvoice();

            App.Config().Update("saved_invoice_locally", jOInvoice.ToString());
        }

        public void loadInvoiceLocallySaved(String saved_invoice_locally)
        {
            if (saved_invoice_locally.Equals("")) return;

            JObject InvoiceJObj = JObject.Parse(saved_invoice_locally);
            JObject InvioceHead = JObject.Parse(InvoiceJObj.GetValue("head").ToString());
            if (InvioceHead.ContainsKey("invoiceid"))
            {
                string invoiceidStr = InvioceHead.GetValue("invoiceid")?.ToString();

                if (!string.IsNullOrWhiteSpace(invoiceidStr) && int.TryParse(invoiceidStr, out int invoiceid)) { 
                    Invoice = new Invoice(Convert.ToInt32(invoiceid)); 
                 }
            }
            if (InvioceHead.ContainsKey("remark"))
            {
                string remark = InvioceHead.GetValue("remark").ToString();
                txtRemark.Text = remark;
            }


            dynamic History = JsonConvert.DeserializeObject(InvoiceJObj.GetValue("body").ToString());
            String discount = "";
            foreach (object jObject in History)
            {
                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jObject.ToString());
                String barcode = values.GetValueOrDefault("barcode");
                String value = values.GetValueOrDefault("value");
                discount = values.GetValueOrDefault("discount");
                Double salesprice = Convert.ToDouble(values.GetValueOrDefault("salesprice"));
                txtQty.Text = value;
                addItemInGrid(barcode, salesprice);
            }

            if (!string.IsNullOrEmpty(discount))
            {
                txtTotalDiscount.Text = discount;
                performDiscunt(false);
            }

           

            if (InvioceHead.ContainsKey("paymentreceivedlist"))
            {

                string otherPRListStr = InvioceHead.GetValue("paymentreceivedlist").ToString();
                if (!string.IsNullOrEmpty(otherPRListStr))
                {
                    JObject otherPRListJObj = JObject.Parse(InvioceHead.GetValue("paymentreceivedlist").ToString());


                    if (gridPaymentmethods.Rows.Count > 0)
                    {
                        for (int i = 0; i < gridPaymentmethods.Rows.Count; ++i)
                        {
                            try
                            {
                                gridPaymentmethods.Rows[i].Cells[1].Value = otherPRListJObj.GetValue(gridPaymentmethods.Rows[i].Cells[0].ToolTipText).ToString();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Missmatch found to load payment method" + e.Message);
                            }

                        }
                    }

                }
            }



        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "F1:\nSelect the quantity input field.\n\n" +
                "F2:\nThe barcode and search boxes can be chosen and switched between. \n\n" +
                "F3:\nThe invoice grid and the item index page can be switched between.\n\n" +
                "F4:\nReceive Payment\n\n" +
                "F5:\nThe outstanding balance should be copied into the targeted element. \n\n" +
                "F6:\nSelect the global discount input box.\n\n" +
                "F7:\nSelect customer field\n\n" +
                "F11:\nHold and Load invoice.\n\n" +
                "F12:\nClear Invoice");
        }

        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            new PrintPOSInvoice().testPrint();
            ClearInvoice();
        }

        private void btnQuickSync_Click(object sender, EventArgs e)
        {
            long maxid = App.Common().lastInsertId(DatabaseHelper.INVITEMS_TABLE);
            ERPCloud eRPCloud = new ERPCloud();
            eRPCloud.syncItemAsync(btnQuickSync, maxid);
            eRPCloud.callAriDrop();
        }

        private void txtTotalDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnProfit_Click(object sender, EventArgs e)
        {
            FrmPOSDialog frmPOSDialog = new FrmPOSDialog(gridInvoice, "PROFIT-LOSS");
            frmPOSDialog.ShowDialog(this);
            frmPOSDialog.Dispose();

            //back2TableList("Change");
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            fetchCustomer();
        }


        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                fetchCustomer();
            }
        }

        public async void fetchCustomer()
        {
            string srckey = txtCustomer.Text;

            Invoice.setCustomerId("");
            Invoice.setLoyaltyBalance(0);

            lblMRPoints.Text = "";
            lblCustomerName.Text = "Guest";
            var responseString = "";

            if (srckey.Equals(""))
            {
                return;
            }

            if (gridInvoice.RowCount <= 0)
            {
                txtCustomer.Text = "";
                MessageBox.Show("No Item Added!");
                return;
            }

            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "loyalty");
                nameValueCollection.Add("action", "Member");
                nameValueCollection.Add("srckey", srckey);

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(config.UrldataExchnage(), content);
                responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    if (state.Equals("100"))
                    {
                        String data = Convert.ToString(dynObj["data"]);
                        dynamic LoyaltyCustomer = JsonConvert.DeserializeObject(data);
                        double mrPoints = (double)LoyaltyCustomer["balance"];
                        string fname = (string)LoyaltyCustomer["fname"];
                        string lname = (string)LoyaltyCustomer["lname"];
                        string ClientId = (string)LoyaltyCustomer["id"];

                        Invoice.setCustomerId(ClientId);
                        Invoice.setLoyaltyBalance(mrPoints);
                        Invoice.setCustomerName(fname + " " + lname);

                        lblMRPoints.Text = Utility.currencyFormat(mrPoints, 2);

                        lblCustomerName.Text = fname + " " + lname;

                        focusPaymentReceieveInput();

                    }
                    else
                    {
                        MessageBox.Show((string)dynObj["message"]);
                    }
                }
                else
                {
                    MessageBox.Show("Error! Check Loyalty Configuration Status");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check your internet connection!\n\n" + ex.Message);
            }
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        private void txtRedeem_Leave(object sender, EventArgs e)
        {
            performRedeem();
        }

        private void txtRedeem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                performRedeem();
            }
        }

        private void performRedeem()
        {
            string text = txtRedeem.Text;
            if (text.Equals(""))
            {
                return;
            }

            if (Invoice.getCustomerId().Equals(""))
            {
                MessageBox.Show("No customer added.");
                return;
            }

            double pointToRedeem = 0;
            try
            {
                pointToRedeem = Convert.ToDouble(text);
                double payable = Invoice.getPayableReamin();

                if (pointToRedeem >= Utility.Round(payable, 0))
                {
                    MessageBox.Show("Point can not be less then payable amount");
                    return;
                }
                redeemPoint(pointToRedeem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void redeemPoint(double pointToRedeem)
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "loyalty");
                nameValueCollection.Add("action", "redeem");
                nameValueCollection.Add("customerid", Invoice.getCustomerId());
                nameValueCollection.Add("invoiceid", "");
                nameValueCollection.Add("point2redeem", pointToRedeem.ToString());

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(config.UrldataExchnage(), content);
                string responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    if (state.Equals("100"))
                    {
                        Invoice.setRedeemAmount(pointToRedeem);
                        recalculateGrid();

                    }
                    else
                    {
                        MessageBox.Show((string)dynObj["message"]);
                    }
                }
                else
                {
                    MessageBox.Show("Unknown Error!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check your internet connection!\n\n" + ex.Message);
            }
        }


        private void txtRound_Leave(object sender, EventArgs e)
        {
            performRound();
        }

        private void txtRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                performRound();
            }
        }

        private void performRound()
        {
            string txt = txtRound.Text;
            if (txt.Equals(""))
            {
                return;
            }

            try
            {
                double roundvalue = Convert.ToDouble(txt);

                if (roundvalue > 5)
                {
                    MessageBox.Show("Max Limit 5");
                    return;
                }

                //double totalDiscount = Invoice.getTotalDiscount(true);
                //double totalDiscount2Apply = totalDiscount + roundvalue - Invoice.roundApplied;
                //txtTotalDiscount.Text = totalDiscount2Apply.ToString();

                Invoice.setRound(roundvalue);
                recalculateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Amount");
            }
        }



        private void lblTotalPayable_Click(object sender, EventArgs e)
        {

        }


        private void btnPaymentPanelSelection_Click(object sender, EventArgs e)
        {

        }

        private void btnPaymentPanSelection_Click(object sender, EventArgs e)
        {
            setPaymentPanView();
        }

        private void setPaymentPanView()
        {
            gridPaymentmethods.Visible = !gridPaymentmethods.Visible;
            pnlQuickPayment.Visible = !pnlQuickPayment.Visible;
            btnPaymentPanSelection.Text = (gridPaymentmethods.Visible) ? "Quick Receive" : "Partial Receive";
        }

        private void btnShortcut_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "F1:\nSelect the quantity input field.\n\n" +
            "F2:\nThe barcode and search boxes can be chosen and switched between. \n\n" +
            "F3:\nThe invoice grid and the item index page can be switched between.\n\n" +
            "F4:\nReceive Payment\n\n" +
            "F5:\nThe outstanding balance should be copied into the targeted element. \n\n" +
            "F6:\nSelect the global discount input box.\n\n" +
            "F7:\nSelect customer field\n\n" +
            "F11:\nHold and Load invoice.\n\n" +
            "F12:\nClear Invoice");
        }

        private void gridItemSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tableId.Equals(""))
            {
                MessageBox.Show("No Table Selected");
                return;
            }

            try
            {
                if (gridInvoice.Rows.Count > 0)
                {
                    back2TableList("Dining");
                }
                else
                {
                    if (!TableReccord["lastinvoice"].ToString().Equals(""))
                    {
                        App.Log().Save("Executive", "KOT full invoice cleared", "KOT_ITEM_CLEARED", tableId, TableReccord["lastinvoice"].ToString());
                       
                    }

                    back2TableList("Open");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void back2TableList(String Status)
        {
            if (!tableId.Equals(""))
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values = new Dictionary<string, string>();
                values.Add("id", tableId);
                if (Status.Equals("Open"))
                {
                    values.Add("lastinvoice", "");
                    values.Add("qtdata", "");
                }
                else if (Status.Equals("Dining"))
                {
                    JObject InvoiceJObject = getLocalInvoice();
                    new PrintKOT(tableId).Print(InvoiceJObject.ToString());
                    values.Add("lastinvoice", InvoiceJObject.ToString());
                }
                else if (Status.Equals("Change"))
                {
                    JObject InvoiceJObject = getLocalInvoice();
                    new PrintKOT(tableId).Print(InvoiceJObject.ToString());
                    values.Add("lastinvoice", InvoiceJObject.ToString());
                    db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);

                    this.Dispose();
                    new FrmTable(tableId).ShowDialog();
                    return;
                }
                else if (Status.Equals("Billed"))
                {
                    JObject InvoiceJObject = getLocalInvoice();
                    values.Add("lastinvoice", InvoiceJObject.ToString());
                }
                values.Add("status", Status);
                db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);

                // Go to Table List
                this.Dispose();
                new FrmTable().ShowDialog();
            }
        }

        private void btnSendbill_Click(object sender, EventArgs e)
        {
            if (tableId.Equals(""))
            {
                MessageBox.Show("No Table Selected");
                return;
            }

            if (gridInvoice.Rows.Count <= 0)
            {
                MessageBox.Show("Invalid Invoice");
                return;
            }
            long Invoiceid = Invoice.InvoiceId;

            if (Invoiceid > 0)
            {
                db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + Invoiceid);
            }

            Invoiceid = Invoice.saveInvoiceEntryByDataGrid(gridInvoice,true);
            printInvoice(Invoiceid, "Unpaid");

            Invoice = new Invoice(Invoiceid);


           //  db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + Invoiceid);
           //  db.Delete(DatabaseHelper.INVOICE_TABLE, "id=" + Invoiceid);

            back2TableList("Billed");
        }

        private void btnChangeTable_Click(object sender, EventArgs e)
        {
            back2TableList("Change");
        }

        private void btnViewReportWindow_Click(object sender, EventArgs e)
        {
            new FrmSales().ShowDialog();
        }

        private void txtRound_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDiscount_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            new FrmAddCustomer().ShowDialog();
        }
    }
}
