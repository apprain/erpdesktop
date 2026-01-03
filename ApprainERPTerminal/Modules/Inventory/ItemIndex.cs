using ApprainERPTerminal.UI.POS;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace ApprainERPTerminal.Modules.Inventory
{
    internal class ItemIndex
    {
        private Panel pnlProductListBody;
        private Panel pnlProductListHead;
        private FrmPOS frmPOS;
        private Config config;
        DatabaseHelper db = new DatabaseHelper();

        public ItemIndex(FrmPOS form,Panel pnlProductListBody, Panel pnlProductListHead)
        {
            this.frmPOS = form;
            
            this.pnlProductListBody = pnlProductListBody;
            this.pnlProductListHead = pnlProductListHead;
            config = new Config(); 
        }

        public ItemIndex clear()
        {
            pnlProductListBody.Controls.Clear();
            pnlProductListHead.Controls.Clear();
            return this;
        }
        
        TabPage tabPage;
        internal void load()
        {
            //String sql = "SELECT distinct(I.category) category FROM app_invitems I left join app_invhistory H on I.id=H.itemid where H.remain > 0 and featured <> '' GROUP by itemid,saleprice";
            String sql = "SELECT DISTINCT I.category AS category FROM app_invitems I LEFT JOIN app_invhistory H on I.id=H.itemid LEFT JOIN app_categories C ON c.type='invitemcat' and I.category = C.id WHERE  H.remain > 0 and I.featured <> ''  ORDER BY CAST(C.generic AS INTEGER) DESC";

            DataTable ItemDataTable = db.selectQuery(sql);
            IEnumerator enumerator = ItemDataTable.Rows.GetEnumerator();
            
            int i = 0;
            while (enumerator.MoveNext())
            {
                DataRow current = (DataRow)enumerator.Current;
                addButton2Head(current["category"].ToString());

                if (i == 0)
                {
                    loadByCat(current["category"].ToString());
                }
                i++;
            }

            if (i == 0)
            {
                Label lbl = new Label();
                lbl.Text = "No item found in quick book.";
                lbl.AutoSize = false;
                lbl.Padding = new Padding(5);
                lbl.Height = 50;
                lbl.Width = 400;
                lbl.ForeColor = Color.Gray;
                lbl.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point);
                pnlProductListBody.Controls.Add(lbl);

            }

        }

        private void addButton2Head(String CatId)
        {
            DataRow Record = db.Find(DatabaseHelper.CATEGORY_TABLE, "id='" + CatId + "'");

            if (Record == null) return;

                Button btn = new Button();
                btn.Text = Record["title"].ToString().ToUpper(); 
                btn.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point);

                String color = "#61BC76";

                btn.BackColor = Color.Yellow;
                btn.FlatAppearance.BorderColor = ColorTranslator.FromHtml(color);
                btn.Dock = DockStyle.Top; ;
                btn.Height = 80;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Click += (sender, EventArgs) => { loadByCat(CatId); };

                pnlProductListHead.Controls.Add(btn);

        }


        TableLayoutPanel panel;
        private void loadByCat(String CatId)
        {
            pnlProductListBody.Controls.Clear();

            String sql = "SELECT I.id,I.name,I.isfinancial,I.depreciation,I.barcode,sum(H.remain) remain, max(H.unitsalesprice) saleprice FROM app_invitems I left join app_invhistory H on I.id=H.itemid where I.category=" + CatId + " and H.remain > 0 and featured <> '' GROUP by name,unitsalesprice ORDER BY featured ASC LIMIT 0,50";
            DataTable ItemDataTable = db.selectQuery(sql);
            int entryCount = ItemDataTable.Rows.Count;

           // DataRow itemRow = db.Find(DatabaseHelper.CATEGORY_TABLE, "id='" + CatId + "'");

           
            panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.ColumnCount = 3;
            panel.RowCount = entryCount / panel.ColumnCount;
            if ((entryCount % panel.ColumnCount) > 0 )
            {
                panel.RowCount++;
            }
            IEnumerator enumerator = ItemDataTable.Rows.GetEnumerator();
            for (int j = 0; j < panel.RowCount; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    enumerator.MoveNext();

                    if (enumerator.Current != null)
                    {
                        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
                        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
                        panel.Controls.Add(addPanel((DataRow)enumerator.Current), i, j);
                    }
                }
            }
            
            pnlProductListBody.Controls.Add(panel); 
        }

        private void loadByItemList(string[] mapItems)
        {
            pnlProductListBody.Controls.Clear();

            String sql = "SELECT I.id,I.name,I.isfinancial,I.depreciation,I.barcode,sum(H.remain) remain, max(H.unitsalesprice) saleprice FROM app_invitems I left join app_invhistory H on I.id=H.itemid where I.id in (" + string.Join(",", mapItems) + ") and H.remain > 0  GROUP by name,unitsalesprice ORDER BY featured ASC LIMIT 0,50";

            Debug.WriteLine(sql);
            DataTable ItemDataTable = db.selectQuery(sql);
            int entryCount = ItemDataTable.Rows.Count;

           // DataRow itemRow = db.Find(DatabaseHelper.CATEGORY_TABLE, "id='" + CatId + "'");


            panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.ColumnCount = 3;
            panel.RowCount = entryCount / panel.ColumnCount;
            if ((entryCount % panel.ColumnCount) > 0)
            {
                panel.RowCount++;
            }
            IEnumerator enumerator = ItemDataTable.Rows.GetEnumerator();
            for (int j = 0; j < panel.RowCount; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    enumerator.MoveNext();

                    if (enumerator.Current != null)
                    {
                        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
                        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
                        panel.Controls.Add(addPanel((DataRow)enumerator.Current), i, j);
                    }
                }
            }

            pnlProductListBody.Controls.Add(panel);
        }

        private Button addPanel(DataRow Item)
        {         
            // Top Title
            Button ItemName = new Button();
            ItemName.Text = App.Common().FormatItemName(Item);// ["name"].ToString().ToUpper();
            ItemName.Font = new Font("Segoe UI Black", 10F, FontStyle.Regular, GraphicsUnit.Point);

            String color = "#61BC76";
            int itemid = Convert.ToInt32(Item["id"]);
            if (itemid % 5 == 0) color = "#6381db";
            else if (itemid % 5 == 1)color = "#CC9999";
            else if (itemid % 5 == 2)color = "#33CCCC";
            else if (itemid % 5 == 3)color = "#B09FCF";
            else if (itemid % 5 == 4)color = "#0099CC";
            

            ItemName.BackColor = ColorTranslator.FromHtml(color);
            ItemName.FlatStyle = FlatStyle.Flat;
            ItemName.FlatAppearance.BorderColor = ColorTranslator.FromHtml(color);
            ItemName.Dock = DockStyle.Top; ;
            ItemName.Height = 100;
            ItemName.TextAlign = ContentAlignment.MiddleCenter;
            ItemName.Click += (sender, EventArgs) => { BtnInc_Click(sender, EventArgs, Item);};

            return ItemName;
        }
        private void BtnInc_Click(object sender, EventArgs e, DataRow item)
        {

            string orientation = item["isfinancial"]?.ToString() ?? string.Empty;

            if (orientation == "3")
            {
                string map = item["depreciation"]?.ToString() ?? "";
                if (map.Equals(""))
                {
                    MessageBox.Show("Item map is not set"); ;
                    return;
                }

                string[] mapItems = map.Split(',');

                loadByItemList(mapItems);


            }
            else
            {
                string barcode = item["barcode"]?.ToString() ?? string.Empty;
                double salesPrice = double.TryParse(item["saleprice"]?.ToString(), out double parsedPrice) ? parsedPrice : 0;

                frmPOS.addItemInGrid(barcode, salesPrice);
            }

        }

    }
}

