using ApprainERPTerminal.Modules.Inventory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


namespace ApprainERPTerminal.UI.Inventory
{
    public partial class FrmManageItems : Form
    {
        private DatabaseHelper db;
        private String currentItemId;
        Config config;
        HttpClient client;

        public FrmManageItems()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            loadDefaultValue();
            loadItemInList("");

        }

        private void FrmManageItems_Load(object sender, EventArgs e)
        {
            config = new Config();
            client = new HttpClient();

            switcher(true);
        }

        private void loadDefaultValue()
        {

            String str = App.Config().Setting("inventorysettings_item_colors");
            string[] result = str.Split(',');
            foreach (string s in result)
            {
                if (s.Trim() != "") cmbItmColor.Items.Add(s);
            }

            str = App.Config().Setting("inventorysettings_item_size");
            result = str.Split(',');
            foreach (string s in result)
            {
                if (s.Trim() != "") cmbItmSize.Items.Add(s);
            }

            DataTable Records = db.FindAll(DatabaseHelper.CATEGORY_TABLE, "type = 'invitemcat' ORDER BY title ASC");

            foreach (DataRow row in (InternalDataCollectionBase)Records.Rows)
            {
                cmbItmCategory.Items.Add(new ComboBoxItem(Convert.ToInt16(row["id"]), row["title"].ToString()));

            }

        }

        private void txtProductSearchBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtProductSearchBarcode.Text.ToString().Equals(""))
            {
                return;
            }

            loadItemInList(txtProductSearchBarcode.Text.ToString());
        }



        private void loadItemInList(String str)
        {
            DataTable all;

            if (str.Equals(""))
            {
                all = db.FindAll(DatabaseHelper.INVITEMS_TABLE, "id > 0 LIMIT 0,50");
            }
            else
            {
                all = db.FindAll(DatabaseHelper.INVITEMS_TABLE, "barcode LIKE '%" + str + "%' OR  name like'%" + str + "%' LIMIT 0,50");
            }

            dataGridProductList.Rows.Clear();
            foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
            {
                dataGridProductList.Rows.Add(row["barcode"], row["name"], "View");
            }
        }



        private void dataGridProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                displayItemForm(dataGridProductList.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void dataGridViewItemStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                updatestock(e.RowIndex);
            }

        }

        private async Task updatestock(int RowIndex)
        {
            String qty = "", sprice = "", color = "", size = "";

            if (dataGridViewItemStock.Rows[RowIndex].Cells[0].Value != null)
            {
                qty = dataGridViewItemStock.Rows[RowIndex].Cells[0].Value.ToString();
            }
            else
            {
                return;
            }


            if (dataGridViewItemStock.Rows[RowIndex].Cells[3].Value != null)
            {
                sprice = dataGridViewItemStock.Rows[RowIndex].Cells[3].Value.ToString();
            }


            if (dataGridViewItemStock.Rows[RowIndex].Cells[5].Value != null)
            {
                color = dataGridViewItemStock.Rows[RowIndex].Cells[5].Value.ToString();
            }

            if (dataGridViewItemStock.Rows[RowIndex].Cells[6].Value != null)
            {
                size = dataGridViewItemStock.Rows[RowIndex].Cells[6].Value.ToString();
            }

            Config config = new Config();
            HttpClient client = new HttpClient();

            Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
            nameValueCollection.Add("token", config.Setting("token"));
            nameValueCollection.Add("timestamp", config.Setting("token"));
            nameValueCollection.Add("com", "Inventory");
            nameValueCollection.Add("action", "StockIn");

            nameValueCollection.Add("itemid", currentItemId);
            nameValueCollection.Add("value", qty);
            nameValueCollection.Add("storeid", config.Setting("storeid"));
            nameValueCollection.Add("unitsalesprice", sprice);
            nameValueCollection.Add("unitprice", "");
            nameValueCollection.Add("lotno", "");
            nameValueCollection.Add("productcode", "");
            nameValueCollection.Add("remark", "");
            nameValueCollection.Add("attributes", "");
            nameValueCollection.Add("color", color);
            nameValueCollection.Add("sizes", size);

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

            var response = await client.PostAsync(config.UrldataExchnage(), content);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(responseString);

            String status = dynObj["status"];

            if (status.Equals("1"))
            {
                MessageBox.Show("Quick Sync is must aftar stock update.\n\nClick on Quick Sync button after all stock posting done.");
            }
            else
            {
                MessageBox.Show("Invaid request!");
            }
        }

        private void displayItemForm(String barcode)
        {
            clearInputFields();
            DataRow itemRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");

            if (itemRow == null)
            {
                return;
            }

            switcher(false);
            currentItemId = itemRow["id"].ToString();
            setInputFields(itemRow);
            setDisplayStock(currentItemId);
        }

        private void setDisplayStock(String itemid)
        {
            DataTable all = db.FindAll(DatabaseHelper.INVITEM_HISTORY_TABLE, "itemid =" + itemid + " ORDER BY id DESC");

            dataGridViewItemStock.Rows.Clear();
            double total = 0;
            foreach (DataRow row in (InternalDataCollectionBase)all.Rows)
            {
                DataGridViewButtonColumn View = new DataGridViewButtonColumn();

                dataGridViewItemStock.Rows.Add(
                            row["value"],
                            row["remain"],
                            row["entrytype"],
                            row["unitsalesprice"],
                            row["entrydate"],
                            row["color"],
                            row["sizes"],
                            "Submit"
                     );

                total += Convert.ToDouble(row["remain"]);
            }

            lblTotalStock.Text = "Total Stock: " + total.ToString();
        }

        private void clearInputFields()
        {
            dataGridProductList.Rows.Clear();
            currentItemId = null;
            txtItmOldBarcode.Text = "";
            txtItmName.Text = "";
            txtItmTax.Text = "";
            cmbItmCategory.Text = "";
            cmbItmColor.Text = "";
            cmbItmSize.Text = "";
            txtItmFeatured.Text = "";
            txtItmRemark.Text = "";
            dataGridViewItemStock.Rows.Clear();
        }


        private void setInputFields(DataRow itemRow)
        {
            if (itemRow == null) return;
            currentItemId = itemRow["id"].ToString();
            txtItmBarcode.Text = itemRow["barcode"].ToString();
            txtItmOldBarcode.Text = itemRow["oldbarcode"].ToString();
            txtItmName.Text = itemRow["name"].ToString();
            txtItmTax.Text = itemRow["vat"].ToString();
            cmbItmCategory.Text = itemRow["vat"].ToString();
            cmbItmColor.Text = itemRow["color"].ToString();
            cmbItmSize.Text = itemRow["sizes"].ToString();
            txtItmFeatured.Text = itemRow["featured"].ToString();
            txtItmRemark.Text = itemRow["remark"].ToString();

            SetSelectedItemById(itemRow["category"].ToString());

        }

        private void switcher(bool isItemList)
        {
            if (isItemList == true)
            {
                pnlItemSingle.Visible = false;
                pnlProductList.Visible = true;
            }
            else
            {
                pnlItemSingle.Visible = true;
                pnlProductList.Visible = false;
            }

        }

        private void txtItmBarcode_TextChanged(object sender, EventArgs e)
        {
            String barcode = txtItmBarcode.Text;
            if (barcode.Equals(""))
            {
                clearInputFields();
                return;
            }
            displayItemForm(txtItmBarcode.Text);
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            createnewItem();
        }

        private void createnewItem()
        {
            switcher(false);
            txtItmBarcode.Text = "";
            clearInputFields();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            uploadItem(currentItemId);

        }

        public void SetSelectedItemById(String id)
        {
            if (id.Equals("")) return;

            foreach (var item in cmbItmCategory.Items)
            {
                if (item is ComboBoxItem comboBoxItem && comboBoxItem.Id == Convert.ToInt32(id))
                {
                    cmbItmCategory.SelectedItem = comboBoxItem;

                    DataRow Records = db.Find(DatabaseHelper.CATEGORY_TABLE, "id=" + id);
                    if (Records != null) {
                        cmbItmCategory.Text = Records["title"].ToString();
                    }
                    break;
                }
            }
        }

        public String GetSelectedId()
        {
            if (cmbItmCategory.SelectedItem is ComboBoxItem selectedItem)
            {
                return selectedItem.Id.ToString();
            }
            return "";
        }

        public async Task uploadItem(String Itemid)
        {
            Config config = new Config();
            HttpClient client = new HttpClient();

            Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
            nameValueCollection.Add("token", config.Setting("token"));
            nameValueCollection.Add("timestamp", config.Setting("token"));
            nameValueCollection.Add("com", "Inventory");
            nameValueCollection.Add("action", "saveItem");

            if (Itemid != null) nameValueCollection.Add("id", Itemid);

            nameValueCollection.Add("barcode", txtItmBarcode.Text.ToString());
            nameValueCollection.Add("oldbarcode", txtItmOldBarcode.Text.ToString());
            nameValueCollection.Add("category", GetSelectedId());
            nameValueCollection.Add("name", txtItmName.Text.ToString());
            nameValueCollection.Add("vat", txtItmTax.Text.ToString());
            nameValueCollection.Add("color", cmbItmColor.Text.ToString());
            nameValueCollection.Add("sizes", cmbItmSize.Text.ToString());
            nameValueCollection.Add("featured", txtItmFeatured.Text.ToString());
            nameValueCollection.Add("remark", txtItmRemark.Text.ToString());

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

            var response = await client.PostAsync(config.UrldataExchnage(), content);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(responseString);

            String state = dynObj["state"];
            if (state.Equals("100"))
            {
                currentItemId = dynObj["id"];
                Dictionary<string, string> values = new Dictionary<string, string>();
                values = new Dictionary<string, string>();
                values.Add("id", currentItemId);
                values.Add("barcode", txtItmBarcode.Text.ToString());
                values.Add("oldbarcode", txtItmOldBarcode.Text.ToString());
                values.Add("category", GetSelectedId());
                values.Add("name", txtItmName.Text.ToString());
                values.Add("vat", txtItmTax.Text.ToString());
                values.Add("color", cmbItmColor.Text.ToString());
                values.Add("sizes", cmbItmSize.Text.ToString());
                values.Add("featured", txtItmFeatured.Text.ToString());
                values.Add("remark", txtItmRemark.Text.ToString());

                if (Itemid == null)
                {
                    db.Insert(DatabaseHelper.INVITEMS_TABLE, values);
                    MessageBox.Show("Item added succseefully!");
                }
                else
                {
                    db.Save(DatabaseHelper.INVITEMS_TABLE, values);
                    MessageBox.Show("Saved Successfully!");
                }


            }
            else
            {
                MessageBox.Show("Invaid request!");
            }

            //MessageBox.Show(responseString);
        }

        private void btnCreateNewItem2_Click(object sender, EventArgs e)
        {
            createnewItem();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switcher(true);
        }

        private void btnQuickSunc_Click(object sender, EventArgs e)
        {
            ERPCloud eRPCloud = new ERPCloud();

            eRPCloud.syncStockByAttribute();
            setDisplayStock(currentItemId);
        }

        private void btnRefreshItem_Click(object sender, EventArgs e)
        {
            if (txtItmBarcode.Text.Equals("")) return;

            displayItemForm(txtItmBarcode.Text);
        }
    }

    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ComboBoxItem(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }

}

