using ApprainERPTerminal.UI.POS;
using DocumentFormat.OpenXml.Office.MetaAttributes;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;


namespace ApprainERPTerminal.Modules.Inventory
{
    internal class Invoice
    {
        
        private double TotalPayable;
        private double TotalVat = 0;
        private string AdjustmentRefId;
        private double AdjustmentAmount;
        private double CashReceived;
        private double OtherPaymentReceived;
        private JObject OtherPaymentReceivedList;
        private double CreditReceived;
        private double TotalDiscount;
        private double Total;
       
        private string remark;
        private string CustomerId = "";
        private string CustomerName = "";
        private double LoyaltyBalance;
        private double redeemAmount;
        public long InvoiceId;
        private DatabaseHelper db;
        private DataRow InvoiceRecord;
        public double roundApplied = 0;
        public bool isDummy = false;
     

        public Invoice()
        {
            remark = "";
            db = new DatabaseHelper();

            string amount = App.Config().Setting("invoice_item_rtn_amount");
            AdjustmentRefId = App.Config().Setting("invoice_item_rtn_invoice_id");

            if(!amount.Equals(""))
            {
                AdjustmentAmount = Convert.ToDouble(amount);
            }
        }

        public Invoice(long id)
        {
            remark = "";
            db = new DatabaseHelper();
            InvoiceId = id;
            InvoiceRecord = db.Find(DatabaseHelper.INVOICE_TABLE, "id=" + id.ToString());
            
        }
        public string Status()
        {
            return InvoiceRecord == null ? "Process" : InvoiceRecord["status"].ToString();
        }

        public bool isLocked()
        {
            return Status() != "Process";
        }

        public void setInvoiceCount(Button btn)
        {
            if(btn == null) return ;
            long num = db.countEntry(DatabaseHelper.INVOICE_TABLE, "syncflag > 0") + db.countEntry(DatabaseHelper.INVOICE_HISTORY_TABLE, "syncflag > 0 and returned > 0");
            btn.Text = "(" + Convert.ToString(num) + ")";
        }

        public long saveInvoiceEntryByDataGrid(DataGridView grid)
        {
            return saveInvoiceEntryByDataGrid(grid, false);
        }

        public long saveInvoiceEntryByDataGrid(DataGridView grid, bool asDummy)
        {       
            isDummy = asDummy;
            string fkey = App.Config().Setting("terminalcode") + "-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            string received = Convert.ToString(getReceiedFromCustomer());
            //string received = Convert.ToString(getTotalReceievedExceptCredit());

            string creditreceived = Convert.ToString(getCreditReceived());
            string returned = Convert.ToString(getReturnToCustomer());
            string discount = Convert.ToString(getTotalDiscount());
            string rounded = Convert.ToString(getRound());
            string total = Convert.ToString(getTotal());
            string totalVat = Convert.ToString(getTotalVat());
            string CashReceived = Convert.ToString(getCashReceived());
            string OtherPaymentReceived = Convert.ToString(getOtherPaymentReceived());
            string entrydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string storeid = App.Config().Setting("storeid");
            string adminref = App.Config().Setting("adminref");
            string terminal = App.Config().Setting("inventorysettings_terminal_code");
            string OtherPaymentReceivedliststr = (OtherPaymentReceivedList!= null) ?  OtherPaymentReceivedList.ToString() : "";

            Dictionary<string, string> values1 = new Dictionary<string, string>();
            if (InvoiceId > 0)
            {
                db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + InvoiceId);
                // MessageBox.Show(InvoiceId.ToString());
                values1.Add("id", InvoiceId.ToString());
            }

            values1.Add("fkey", fkey);
            values1.Add("fkeytype", "erpditcY");
            values1.Add("storeid", storeid);
            values1.Add("terminal", terminal);
            values1.Add("received", received);
            values1.Add("creditreceived", creditreceived);
            values1.Add("total", total);
            values1.Add("discount", discount);
            values1.Add("cashreceived", CashReceived);
            values1.Add("otherpaymentreceived", OtherPaymentReceived);
            values1.Add("OtherPaymentReceivedlist", OtherPaymentReceivedliststr.ToString());
            values1.Add("vat", totalVat);
            values1.Add("servicecharge", "");
            values1.Add("rounded", rounded);
            values1.Add("adjustment", getAdjustmentAmount().ToString());
            values1.Add("adjustmentrefid", getAdjustmentRefId());
            values1.Add("returned", returned);
            values1.Add("remark", remark);
            values1.Add("client", getCustomerId());
            values1.Add("type", "Cash");
            
            values1.Add("entrydate", entrydate);
            values1.Add("operator", adminref);

            if (isDummy)
            {
                values1.Add("status", "Process");
                values1.Add("syncflag", "0"); // Dummy Invoice
            }
            else
            {
                values1.Add("status", "Complete");
                values1.Add("syncflag", "1");
            }
           

            InvoiceId = db.Save(DatabaseHelper.INVOICE_TABLE, values1);

            App.Config().Update("invoice_item_rtn_amount", "");
            App.Config().Update("invoice_item_rtn_invoice_id", "");

            for (int index = 0; index < grid.Rows.Count; ++index)
            {
               
                string barcode = grid.Rows[index].Cells[0].Value.ToString();
                string value = grid.Rows[index].Cells[3].Value.ToString();
                string unitprice = grid.Rows[index].Cells[5].Value.ToString();
                string vat = grid.Rows[index].Cells[6].Value.ToString();
                string totaleprice = grid.Rows[index].Cells[10].Value.ToString();
                string warranty = grid.Rows[index].Cells[8].Value.ToString();
                string ItemwiseDiscount = grid.Rows[index].Cells[7].Value.ToString();

                DataRow itemRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");
                double vatRate = VatRate(itemRow["id"].ToString());

                Dictionary<string, string> values2 = new Dictionary<string, string>();

                string discountmethod = "F";
                if (ItemwiseDiscount.EndsWith("%"))
                {
                    ItemwiseDiscount = ItemwiseDiscount.Replace("%", "");
                    discountmethod = "P";
                }

                values2.Add("itemid", itemRow["id"].ToString());
                values2.Add("storeid", storeid);
                values2.Add("value", value);
                values2.Add("presentstock", "");
                values2.Add("discount", ItemwiseDiscount);
                values2.Add("discountmethod", discountmethod);
                values2.Add("unitprice", unitprice);
                values2.Add("unitpurchaseprice", "");
                values2.Add("vatrate", vatRate.ToString());
                values2.Add("vat", vat);
                values2.Add("subtotal", totaleprice);
                values2.Add("totaleprice", totaleprice);
                values2.Add("totalpurchaseprice", "");
                values2.Add("invoiceid", InvoiceId.ToString());
                values2.Add("lotno", "");
                values2.Add("productcode", "");
                values2.Add("color", "");
                values2.Add("sizes", "");
                values2.Add("attributes", "");
                values2.Add("supplier", "");
                values2.Add("warranty", warranty);
                values2.Add("track", "");
                values2.Add("returned", "");
                values2.Add("entrydate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                values2.Add("additionals", "");

                db.Save(DatabaseHelper.INVOICE_HISTORY_TABLE, values2);
                if (!isDummy)
                {
                    decreaseStock(itemRow, Convert.ToDouble(value), Convert.ToDouble(unitprice));
                }
            }
            return InvoiceId;
        }

        public double VatRate(string id)
        {
            string rate = "0";

            if (!id.Equals(""))
            {
                DataRow dataRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "id='" + id + "'");

                if (!dataRow["vat"].Equals(""))
                {
                    rate = dataRow["vat"].ToString();
                }
                else
                {
                    rate = App.Config().Setting("inventorysettings_vat");
                }
            }
            else
            {
                rate = App.Config().Setting("inventorysettings_vat");
            }

            if (rate.Equals(""))
            {
                return 0.0;
            }
            else
            {
                return Convert.ToDouble(rate);
            }
        }
        

        public void decreaseStock(DataRow itemRow, double qty,double salesprice)
        {
            db.execSQL("UPDATE " + DatabaseHelper.INVITEM_HISTORY_TABLE + "  SET remain = remain - " + qty + " WHERE itemid=" + itemRow["id"].ToString() + " and unitsalesprice =  " + salesprice);
        }

        public void increaseStock(DataRow itemRow, double qty, double salesprice)
        {

            db.execSQL("UPDATE " + DatabaseHelper.INVITEM_HISTORY_TABLE + "  SET remain = remain + " + qty + " WHERE itemid=" + itemRow["id"].ToString() + " and unitsalesprice =  " + salesprice);

        }

        public string getOnlineId()
        {
            return InvoiceRecord == null ? "" : InvoiceRecord["onlineid"].ToString();
        }

        public void updateReturn(string barcode, double rtnQty)
        {
            DataRow itemRow = db.Find(DatabaseHelper.INVITEMS_TABLE, "barcode='" + barcode + "'");
            DataRow dataRow = db.Find(DatabaseHelper.INVOICE_HISTORY_TABLE, "itemid=" + itemRow["id"].ToString() + " and invoiceid=" + InvoiceId.ToString());
            double qty = rtnQty - Convert.ToDouble(dataRow["returned"]);
            double unitprice = Convert.ToDouble(dataRow["unitprice"]);


            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("id", dataRow["id"].ToString());
            values.Add("returned", rtnQty.ToString());
            values.Add("entrydate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            values.Add("syncflag", "1");
            db.Save(DatabaseHelper.INVOICE_HISTORY_TABLE, values);

            //# Update return amount in device common table
            double returnamouint = HistoryWiseReturnAmount(dataRow["id"].ToString(), qty);
            string item_rtn_amount = App.Config().Setting("invoice_item_rtn_amount");
            if (!item_rtn_amount.Equals(""))
            {
                returnamouint += Convert.ToDouble(item_rtn_amount);
            }
            App.Config().Update("invoice_item_rtn_amount", returnamouint.ToString());
            App.Config().Update("invoice_item_rtn_invoice_id", InvoiceId.ToString());
            //#

            increaseStock(itemRow, qty, unitprice);
        }

        public void setCustomerId(string cmId)
        {
            CustomerId = cmId;
        }

        public string getCustomerId()
        {
            return CustomerId;
        }

        public string getCustomerName()
        {
            return CustomerName;
        }

        public void setCustomerName(string value)
        {
            CustomerName = value;
        }

        public void setLoyaltyBalance(double balance)
        {
            LoyaltyBalance = balance;
        }

        public double getLoyaltyBalance()
        {
            return LoyaltyBalance;
        }
        
        public void setRedeemAmount(double amount)
        {
            redeemAmount = amount;
        }

        public double getRedeemAmount()
        {
            return redeemAmount;
        }


        public void setTotalPayable(double total)
        {
            TotalPayable = total - AdjustmentAmount;
        }

        internal void RemoveHistoryById(string id)
        {
            db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "id=" + id);
        }

        public void setCashReceived(double received)
        {
            CashReceived = received;
        }

        public double getCashReceived()
        {
            return CashReceived;
        }

        public void setOtherPaymentReceived(double received)
        {
            OtherPaymentReceived = received;
        }
        
        public void setOtherPaymentReceivedList(JObject list)
        {
            OtherPaymentReceivedList = list;
        }
        
        public double getOtherPaymentReceived()
        {
            return OtherPaymentReceived;
        }

        public JObject getOtherPaymentReceivedList()
        {
            return OtherPaymentReceivedList;
        }

        public double getAdjustmentAmount()
        {
            return AdjustmentAmount;
        }

        public double AdjustmentRecovery()
        {
            return getAdjustmentAmount() - getTotal();
        }

        public string getAdjustmentRefId()
        {
            return AdjustmentRefId;
        }

        public void setCreditReceived(double received)
        {
            CreditReceived = received;
        }

        public double getCreditReceived()
        {
            return CreditReceived;
        }

        public void setTotal(double total)
        {
            Total = total;
        }

        public double getTotal()
        {
            return Total;
        }

        public double getTotalWithOutVat()
        {
            return Math.Round(Total - TotalVat,2);
        }

        public void setTotalDiscount(double totalDiscount)
        {
            TotalDiscount = totalDiscount;
        }

        public void setRound(double value)
        {
            roundApplied = value;
        }

        public double getRound()
        {
            return roundApplied;
        }



        public void setTotalVat(double totalVat)
        {
            TotalVat = totalVat;
        }

        public double getTotalVat()
        {
            return TotalVat;
        }

        public double getTotalDiscount()
        {
            return Math.Round(TotalDiscount + redeemAmount, Utility.getPrecision());
        }

        public double getTotalDiscount(bool onlydiscunt)
        {
            if (onlydiscunt)
            {
                return TotalDiscount;
            }

            return getTotalDiscount();
            
        }

        public void setRemark(string comments)
        {
            remark = comments;
        }

        public double getTotalReceievedExceptCredit()
        {
            return Math.Round(CashReceived + OtherPaymentReceived, 2);
        }

        public double getPayableReaminExceptCredit()
        {
            double num = Math.Round(TotalPayable - getTotalReceievedExceptCredit(), 2);

            return num <= 0.0 ? 0.0 : Math.Round(num, Utility.getPrecision());
        }

        public double getPayableReamin()
        {
            double num = Math.Round(TotalPayable - getTotalReceieved(),2);

            return num <= 0.0 ? 0.0 : Math.Round(num, Utility.getPrecision());
        }

        public double getPayable()
        {
            double num = TotalPayable;
            return num <= 0.0 ? 0.0 : Math.Round(num,Utility.getPrecision());
        }

        public double getTotalReceieved()
        {
            return Math.Round(CashReceived + OtherPaymentReceived + CreditReceived, 2);
        }

        public double getReturnToCustomer()
        {
            double value = getTotalReceieved() >   TotalPayable ? getTotalReceieved() - TotalPayable : 0.0;

            return Math.Round(value,2);
        }

        public double getReceiedFromCustomer()
        {
            //return TotalPayable;
            return Math.Round(CashReceived + OtherPaymentReceived, 2);
        }

        public long getId()
        {
            return InvoiceId;
        }

        public long saveInvoiceEntry(String invoiceHead)
        {
                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(invoiceHead);
                
                values.Add("cashreceived", "");
                values.Add("otherpaymentreceived", "");
                values.Add("otherpaymentreceivedlist", "");
                values.Add("onlineid", Convert.ToString(values.GetValueOrDefault("id")));
                
                return db.Insert(DatabaseHelper.INVOICE_TABLE, values);
        }

        public void saveInvoiceHistory(long invoiceId, JArray invoiceBody)
        {
            foreach (object obj1 in invoiceBody)
            {
                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj1.ToString());
                db.Insert(DatabaseHelper.INVOICE_HISTORY_TABLE, values);
            }
            
        }
		
		public double HistoryWiseReturnAmount(string id, double qty)
        {			
            DataRow dataRow = db.Find(DatabaseHelper.INVOICE_HISTORY_TABLE, "id=" + id);

            double unitprice = Convert.ToDouble(dataRow["unitprice"]);
            double value = Convert.ToDouble(dataRow["value"]);
            double returnAmount = (unitprice * qty);

            double discount = invoiceentrywisediscount(dataRow);

            if (discount > 0){
			    double unitdiscount = discount / value;
			    returnAmount = returnAmount - (unitdiscount * qty);
            }
		
            return Math.Round(returnAmount, 2);
        }

        public double invoiceentrywisediscount(DataRow row)
        {
            double discount = Convert.ToDouble(row["discount"]);
            double unitprice = Convert.ToDouble(row["unitprice"]);
            double value = Convert.ToDouble(row["value"]);

            if (discount <= 0) {
                return 0;
            }

            if (Convert.ToString(row["discountmethod"]) == "P") {
                return Math.Round((unitprice * discount / 100) * value, 2);
            } else
            {
                return Math.Round((discount * value), 2);
            }
        }
    }
}
