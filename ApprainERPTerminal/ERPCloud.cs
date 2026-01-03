using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Inventory;
using ApprainERPTerminal.UI.POS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace ApprainERPTerminal
{
    public class ERPCloud
    {
        private HttpClient client;
        private Config config;
        private string UrlAuth;
        private string UrlDataExchnage;
        private DatabaseHelper db;
        private Utility utility;

        public ERPCloud()
        {
            this.UrlAuth = "";
            this.UrlDataExchnage = "";
            this.config = new Config();
            this.client = new HttpClient();
            this.UrlAuth = this.config.AuthUrl();
            this.UrlDataExchnage = this.config.UrldataExchnage();
            this.db = new DatabaseHelper();
            this.utility = new Utility();
        }

        bool synceBatchFirst = true;
        public async Task syncItemAsync(Button btnItemFull)
        {
            long item_last_insert_id = 0;
            if (!synceBatchFirst)
            {
                item_last_insert_id = App.Common().lastInsertId(DatabaseHelper.INVITEMS_TABLE);
            }
            synceBatchFirst = false;
            syncItemAsync(btnItemFull, item_last_insert_id);
        }

        string btnText = "";
        public async Task syncItemAsync(Button btnItemFull, long maxid)
        {
            try
            {
                btnText = btnItemFull.Text;
                btnItemFull.Text = "Wait...";
                client = new HttpClient();

                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "fetchItems");
                nameValueCollection.Add("maxid", maxid.ToString());
                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String data = Convert.ToString(dynObj["data"]["data"]);

                if (!data.Equals(""))
                {
                    JArray jarray = JArray.Parse(data);

                    if (jarray != null)
                    {
                        if (maxid == 0)
                        {
                            db.execSQL("DELETE FROM " + DatabaseHelper.INVITEMS_TABLE);
                        }

                        IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                        try
                        {
                            while (enumerator.MoveNext())
                            {
                                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                values.Add("syncflag", "");

                                db.Insert(DatabaseHelper.INVITEMS_TABLE, values);
                            }
                                                        
                            resetItemButtonTitle(btnItemFull);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            resetItemButtonTitle(btnItemFull);
                        }
                        if (jarray.Count > 0)
                        {
                            MessageBox.Show(jarray.Count + " items update successfully.\nPlease click again for next batch.");
                        }
                        else
                        {
                            MessageBox.Show("All data update completed successfully!");

                        }
                    }
                    else
                    {
                        MessageBox.Show("All data update completed successfully!");
                    }
                   
                    resetItemButtonTitle(btnItemFull);
                }
                else
                {
                    MessageBox.Show("No Item found");
                    resetItemButtonTitle(btnItemFull);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resetItemButtonTitle(btnItemFull);
            }
        }

        private void resetItemButtonTitle(Button btnItemFull)
        {
            btnItemFull.Text = btnText;
        }

        public async Task syncStockByAttribute()
        {
            try
            {
                client = new HttpClient();

                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "fetchItemsStockGroupByAttribute");
                nameValueCollection.Add("storeid", config.Setting("storeid"));

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];
                if (status.Equals("1"))
                { 
                    if (state.Equals("100"))
                    {
                        String data = Convert.ToString(dynObj["data"]["data"]);

                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);

                            if (jarray != null)
                            {
                                db.execSQL("DELETE FROM " + DatabaseHelper.INVITEM_HISTORY_TABLE);
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        values.Add("entrytype", "IN");
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
                            MessageBox.Show("Operation Successfull");
                            config.Update("servertime_lastsync", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    else
                    {
                        String msg = dynObj["message"];
                        MessageBox.Show("Error: " + msg);
                    }
                }
                else
                {
                    MessageBox.Show("Unexpected Error");
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public async Task UploadInvoice(Button btn)
        {
            DataRow dataRow = db.Find(DatabaseHelper.INVOICE_TABLE, "syncflag=1");
            if (dataRow == null)
            {
                new Invoice().setInvoiceCount(btn);
                UploadItemReturn(btn);
                return;
            }

            String InvoiceId = dataRow["id"].ToString();
            String UID = dataRow["operator"].ToString();

            try
            {
                new Invoice().setInvoiceCount(btn);
     
                if (dataRow == null)
                {
                    UploadItemReturn(btn);
                    return;
                }

                Debug.WriteLine("Trying for " + InvoiceId);
                long syncflag = Convert.ToInt32(dataRow["syncflag"]);

                DataTable all = db.FindAll(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + InvoiceId);

                Dictionary<string, string> values = new Dictionary<string, string>();
                ++syncflag;
                values.Add("id", InvoiceId);
                values.Add("syncflag", syncflag.ToString());
                db.Save(DatabaseHelper.INVOICE_TABLE, values);

                double cashRecied = Convert.ToDouble(dataRow["received"]) - Convert.ToDouble(dataRow["otherpaymentreceived"]);

                JObject jobjInvoice;
                jobjInvoice = new JObject();
                jobjInvoice.Add("type", dataRow["type"].ToString());
                jobjInvoice.Add("customerid", dataRow["client"].ToString());
                jobjInvoice.Add("fkey", dataRow["fkey"].ToString());
                jobjInvoice.Add("fkeytype", dataRow["fkeytype"].ToString());
                jobjInvoice.Add("status", dataRow["status"].ToString());
                jobjInvoice.Add("onlineid", dataRow["onlineid"].ToString());
                jobjInvoice.Add("received", dataRow["received"].ToString());
                jobjInvoice.Add("vat", dataRow["vat"].ToString());
                jobjInvoice.Add("servicecharge", dataRow["servicecharge"].ToString());
                jobjInvoice.Add("rounded", dataRow["rounded"].ToString());
                jobjInvoice.Add("otherpaymentreceived", dataRow["otherpaymentreceived"].ToString());
                jobjInvoice.Add("otherpaymentreceivedlist", dataRow["otherpaymentreceivedlist"].ToString());
                jobjInvoice.Add("returned", dataRow["returned"].ToString());
                jobjInvoice.Add("discount", dataRow["discount"].ToString());
                jobjInvoice.Add("adjustment", dataRow["adjustment"].ToString());
                jobjInvoice.Add("cashreceived", cashRecied);
                jobjInvoice.Add("creditreceived", dataRow["creditreceived"].ToString());
                jobjInvoice.Add("total", dataRow["total"].ToString());
                jobjInvoice.Add("remark", dataRow["remark"].ToString());
                jobjInvoice.Add("entrydate", dataRow["entrydate"].ToString());

                JArray jarray = new JArray();
                DataTable dataTable = db.FindAll(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + InvoiceId);
                IEnumerator enumerator = dataTable.Rows.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    DataRow current = (DataRow)enumerator.Current;
                    JObject jobHistory = new JObject();
                    jobHistory.Add("id", (JToken)current["itemid"].ToString());
                    jobHistory.Add("discount", (JToken)current["discount"].ToString());
                    jobHistory.Add("unitprice", (JToken)current["unitprice"].ToString());
                    jobHistory.Add("vat", (JToken)current["vat"].ToString());
                    jobHistory.Add("vatrate", (JToken)current["vatrate"].ToString());
                    jobHistory.Add("discountmethod", (JToken)current["discountmethod"].ToString());
                    jobHistory.Add("additionals", (JToken)current["additionals"].ToString());
                    jobHistory.Add("storeid", (JToken)App.Config().Setting("storeid"));
                    jobHistory.Add("qty", (JToken)current["value"].ToString());
                    jobHistory.Add("subtotal", (JToken)current["subtotal"].ToString());
                    jobHistory.Add("totaleprice", (JToken)current["totaleprice"].ToString());

                    jarray.Add(jobHistory);
                }

                long item_last_insert_id = App.Common().lastInsertId(DatabaseHelper.INVITEMS_TABLE);
                string token = utility.UID2Token(UID);
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", token);
                nameValueCollection.Add("timestamp", config.Setting("token"));               
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "transportInvoiceStatic");
                //nameValueCollection.Add("action", "transportInvoice");
                nameValueCollection.Add("itemmaxid", item_last_insert_id.ToString());
                nameValueCollection.Add("stockmaxid", "0");
                nameValueCollection.Add("servertime_lastsync", config.Setting("servertime_lastsync"));
                nameValueCollection.Add("invoiceHead", jobjInvoice.ToString());
                nameValueCollection.Add("invoiceBody", jarray.ToString());

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);
                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];

                if (status.Equals("1"))
                {
                    String onlineid = dynObj["invoiceid"];
                    updateSyncFlag(InvoiceId, onlineid, "-1");


                    //db.Delete(DatabaseHelper.INVOICE_HISTORY_TABLE, "invoiceid=" + InvoiceId);
                    //db.Delete(DatabaseHelper.INVOICE_TABLE, "id=" + InvoiceId);

                }
                App.Log().Write("Response : " + responseString);

                // Recurtion 
                UploadInvoice(btn);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Try Faild for " + InvoiceId);
                updateSyncFlag(InvoiceId, "1");

                if (btn != null)
                {
                    MessageBox.Show("Check Internet connection\n\n" + ex.Message);
                }
            }

        }

        private void updateSyncFlag(string InvoiceId, String syncflag)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values = new Dictionary<string, string>();
            values.Add("id", InvoiceId);            
            values.Add("syncflag", syncflag);
            db.Save(DatabaseHelper.INVOICE_TABLE, values);
        }

        private void updateSyncFlag(string InvoiceId, String onlineid, String syncflag)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values = new Dictionary<string, string>();
            values.Add("id", InvoiceId);
            values.Add("onlineid", onlineid);
            values.Add("syncflag", syncflag);
            db.Save(DatabaseHelper.INVOICE_TABLE, values);
        }


        public async Task callAriDrop()
        {
            try
            {
                
                long item_last_insert_id = App.Common().lastInsertId(DatabaseHelper.INVITEMS_TABLE);
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("timestamp"));               
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "callAirDrop");
                nameValueCollection.Add("itemmaxid", item_last_insert_id.ToString());
                nameValueCollection.Add("servertime_lastsync",  config.Setting("servertime_lastsync"));

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic dynObj = JsonConvert.DeserializeObject(responseString);
                String status = dynObj["status"];
                if (status.Equals("1"))
                {                   
                    unpackAridrop(responseString);
                }
            }
            catch (Exception ex) {}

        }

        public void unpackAridrop(string str)
        {
                
                dynamic dynObj = JsonConvert.DeserializeObject(str);

                String status = dynObj["status"];

                if (status.Equals("1"))
                {
                    String aridrop = dynObj["aridrop"].ToString();
                    if (aridrop != null)
                    {
                        String itemdata = dynObj["aridrop"]["itemdata"].ToString();
                        
                        String servertime = dynObj["aridrop"]["servertime"].ToString();
                        JArray jarray = JArray.Parse(itemdata);

                        if (jarray != null)
                        {
                            IEnumerator<JToken> enumerator = jarray.GetEnumerator();

                            while (enumerator.MoveNext())
                            {
                                try
                                {
                                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                    
                                    //## DELETING PREVIOUS IF ANY
                                    string query = "DELETE FROM " + DatabaseHelper.INVITEMS_TABLE + " where id=" + values["id"].ToString();
                                    db.execSQL(query);
                                    //Debug.WriteLine(query + "");

                                    // Inserting new value
                                    values.Add("syncflag", "");
                                    db.Insert(DatabaseHelper.INVITEMS_TABLE, values);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        

                        String stockdata = dynObj["aridrop"]["stockdata"].ToString();
                        jarray = JArray.Parse(stockdata);

                        if (jarray != null)
                        {
                            IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                    string query = "DELETE FROM " + DatabaseHelper.INVITEM_HISTORY_TABLE + " where itemid=" + values["itemid"].ToString();
                                    db.execSQL(query);                              
                            }

                            enumerator = jarray.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                values.Add("entrytype","IN");
                                values.Add("value", values["remain"]);
                                db.Insert(DatabaseHelper.INVITEM_HISTORY_TABLE, values);

                                db.execSQL(
                                    "UPDATE " + DatabaseHelper.INVITEMS_TABLE +
                                    "  SET remain = (SELECT sum(value) FROM " + DatabaseHelper.INVITEM_HISTORY_TABLE + " WHERE itemid = " + values["itemid"] + ") where id = " + values["itemid"]
                                );
                            }
                            
                        }
                        config.Update("servertime_lastsync", servertime);
                    }
                }
            
        }

        private async Task UploadItemReturn(Button btn)
        {
            new Invoice().setInvoiceCount(btn);

            DataRow dataRow = db.Find(DatabaseHelper.INVOICE_HISTORY_TABLE, "syncflag > 0 and returned > 0");
            if (dataRow == null)
            {
                if (btn != null)
                {
                    MessageBox.Show("Upload Completed!\n\n** All local invoice uploaded in cloud!");
                }
                return;
            }

            String HistoryId = dataRow["id"].ToString();
            String ItemId = dataRow["itemid"].ToString();
            String Returned = dataRow["returned"].ToString();
            String InvoiceId = dataRow["invoiceid"].ToString();
            String onlineid = null;
            DataRow dataRowInvoice = db.Find(DatabaseHelper.INVOICE_TABLE, "id=" + InvoiceId);

            if (dataRowInvoice != null)
            {
                onlineid = dataRowInvoice["onlineid"].ToString();
                long syncflag = (long)Convert.ToInt32(dataRow["syncflag"]);
                syncflag++;
                Dictionary<string, string> values = new Dictionary<string, string>();

                values.Add("id", HistoryId);
                values.Add("syncflag", syncflag.ToString());
                db.Save(DatabaseHelper.INVOICE_HISTORY_TABLE, values);
            }

            if(string.IsNullOrEmpty(onlineid) || onlineid.Equals("0"))
            {
                MessageBox.Show("Error: Invoice not found online for local id: " + InvoiceId + "\nPlease upliad all invoice or settle manaully.");              
            }
            

            Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
            nameValueCollection.Add("token", App.Config().Setting("token"));
            nameValueCollection.Add("timestamp", App.Config().Setting("token"));
            nameValueCollection.Add("com", "Inventory");
            nameValueCollection.Add("action", "itemReturn");
            nameValueCollection.Add("invoiceid", onlineid);
            nameValueCollection.Add("itemid", ItemId);
            nameValueCollection.Add("qty", Returned);

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

            var response = await client.PostAsync(UrlDataExchnage, content);
            String responseString = await response.Content.ReadAsStringAsync();
            dynamic dynObj = JsonConvert.DeserializeObject(responseString);

            String status = dynObj["status"];
            String state = dynObj["state"];
            
            if (status.Equals("1"))
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("id", HistoryId);
                if (!state.Equals("100"))
                {
                    String message = dynObj["message"];
                    MessageBox.Show("Invoice Id: " + InvoiceId + "\n" + message);
                    values.Add("syncflag", "0");
                }
                else
                {
                    values.Add("syncflag", "-1");
                }
                db.Save(DatabaseHelper.INVOICE_HISTORY_TABLE, values);

                UploadItemReturn(btn);
            }
            else
            {
                MessageBox.Show("Internal System error");
            }
        }


        

        public async Task DownloadInvoice(string invoiceId, string idtype, FrmPOS frmPOS, FrmPOSDialog frmPOSDialog)
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Inventory");
                nameValueCollection.Add("action", "fetchInvoice");
                nameValueCollection.Add("idtype", idtype);
                nameValueCollection.Add("invoiceId", invoiceId);

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1") && state.Equals("100"))
                {
                    try
                    {
                        Invoice invoice = new Invoice();
                        String invoiceHead = Convert.ToString(dynObj["data"]["invoiceHead"]);
                        JArray invoiceBody = dynObj["data"]["invoiceBody"];

                        long InvoiceIdNew = invoice.saveInvoiceEntry(invoiceHead);
                        invoice.saveInvoiceHistory(InvoiceIdNew, invoiceBody);

                        frmPOS.loadInvoice(db.Find(DatabaseHelper.INVOICE_TABLE, "id=" + InvoiceIdNew.ToString()));
                        frmPOSDialog.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error found:\n1) Upload all Invoices in clould.\n2) Check Internet connection.");
                    }
                }
                else
                {
                    MessageBox.Show("Invoice not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async void syncCategory()
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Dataexpert");
                nameValueCollection.Add("action", "fetchFromModel");
                nameValueCollection.Add("method", "findAll");
                nameValueCollection.Add("sourcename", "Category");
                nameValueCollection.Add("condition", "");

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                // Debug.WriteLine(responseString);

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    try
                    {
                        String data = Convert.ToString(dynObj["data"]["data"]);

                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);
                            if (jarray != null)
                            {
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    db.execSQL("DELETE FROM " + DatabaseHelper.CATEGORY_TABLE);
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        db.Insert(DatabaseHelper.CATEGORY_TABLE, values);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            MessageBox.Show("Operation Successfull");
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async void syncUsers()
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Dataexpert");
                nameValueCollection.Add("action", "fetchUsers");
               //# nameValueCollection.Add("action", "fetchFromModel");
               //# nameValueCollection.Add("method", "findAll");
               //# nameValueCollection.Add("sourcename", "Admin");
                nameValueCollection.Add("condition", "type!='Super'");

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    try
                    {
                        String data = Convert.ToString(dynObj["data"]);
                      
                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);
                            if (jarray != null)
                            {
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    db.execSQL("DELETE FROM " + DatabaseHelper.ADMIN_TABLE);
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        db.Insert(DatabaseHelper.ADMIN_TABLE, values);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            MessageBox.Show("Operation Successfull");
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Auth Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public async void syncPaymentMethods()
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "Extpaymentmethods");
                nameValueCollection.Add("action", "fetchList");
                nameValueCollection.Add("storeid", config.Setting("storeid"));

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                
                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1") && state.Equals("100"))
                {
                    try
                    {
                        String data = Convert.ToString(dynObj["data"]);

                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);
                            if (jarray != null)
                            {
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    db.execSQL("DELETE FROM " + DatabaseHelper.INVPAYMENTMETHOD_TABLE);
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        db.Insert(DatabaseHelper.INVPAYMENTMETHOD_TABLE, values);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            MessageBox.Show("Operation Successfull");
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Invoice not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void syncProductCompanies()
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "DataExpert");
                nameValueCollection.Add("action", "fetchFromInformationSet");
                nameValueCollection.Add("sourcename", "invprodcompany");
                nameValueCollection.Add("condition", "");
                nameValueCollection.Add("method", "findAll");
               
                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    try
                    {
                        String data = Convert.ToString(dynObj["data"]["data"]);

                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);
                            if (jarray != null)
                            {
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    db.execSQL("DELETE FROM " + DatabaseHelper.INVITEMS_COMPANY_TABLE);
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        db.Insert(DatabaseHelper.INVITEMS_COMPANY_TABLE, values);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            MessageBox.Show("Operation Successfull");
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Invoice not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async void syncRestaurant()
        {
            try
            {
                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("token", config.Setting("token"));
                nameValueCollection.Add("timestamp", config.Setting("token"));
                nameValueCollection.Add("com", "DataExpert");
                nameValueCollection.Add("action", "fetchFromInformationSet");
                nameValueCollection.Add("sourcename", "restauranttable");
                nameValueCollection.Add("condition", "");
                nameValueCollection.Add("method", "findAll");

                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);

                var response = await client.PostAsync(UrlDataExchnage, content);
                var responseString = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(responseString);

                dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                String status = dynObj["status"];
                String state = dynObj["state"];

                if (status.Equals("1"))
                {
                    try
                    {
                        String data = Convert.ToString(dynObj["data"]["data"]);

                        if (!data.Equals(""))
                        {
                            JArray jarray = JArray.Parse(data);
                            if (jarray != null)
                            {
                                IEnumerator<JToken> enumerator = jarray.GetEnumerator();
                                try
                                {
                                    db.execSQL("DELETE FROM " + DatabaseHelper.RESTAURANT_TABLE_TABLE);
                                    while (enumerator.MoveNext())
                                    {
                                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(enumerator.Current.ToString());
                                        db.Insert(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);
                                    }

                                    db.execSQL("UPDATE " + DatabaseHelper.RESTAURANT_TABLE_TABLE + " SET qtdata='' , lastinvoice=''");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            MessageBox.Show("Operation Successfull");
                        }
                        else
                        {
                            MessageBox.Show("No Item found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Data not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
