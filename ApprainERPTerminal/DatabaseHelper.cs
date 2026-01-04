using System.Data;
using System.Data.SQLite;

namespace ApprainERPTerminal
{
    internal class DatabaseHelper
    {
        public static string DATABASE_NAME = "appRainERP.db";
        public static string TABLE_PREFIX = "app_";
        public static string INVITEMS_TABLE = DatabaseHelper.TABLE_PREFIX + "invitems";
        public static string INVITEM_HISTORY_TABLE = DatabaseHelper.TABLE_PREFIX + "invhistory";
        public static string INVITEMS_COMPANY_TABLE = DatabaseHelper.TABLE_PREFIX + "invprodcompany";
        public static string RESTAURANT_TABLE_TABLE = DatabaseHelper.TABLE_PREFIX + "restauranttable";
        public static string SCONFIG_TABLE = DatabaseHelper.TABLE_PREFIX + "sconfigs";
        public static string ADMIN_TABLE = DatabaseHelper.TABLE_PREFIX + "administrators";
        public static string CATEGORY_TABLE = DatabaseHelper.TABLE_PREFIX + "categories";
        public static string INVOICE_TABLE = DatabaseHelper.TABLE_PREFIX + "invoice";
        public static string INVOICE_HISTORY_TABLE = DatabaseHelper.TABLE_PREFIX + "invinvoicehistory";
        public static string MESSAGEBOARD_TABLE = DatabaseHelper.TABLE_PREFIX + "messageboard";
        public static string INVPAYMENTMETHOD_TABLE = DatabaseHelper.TABLE_PREFIX + "invpaymentmethod";
        public static string INVEXTTXNENTRY_TABLE = DatabaseHelper.TABLE_PREFIX + "invexttxnentry ";
        public static string WORKPERIOD_TABLE = DatabaseHelper.TABLE_PREFIX + "workperiod";
        public static string LOG_TABLE = DatabaseHelper.TABLE_PREFIX + "log";
        public static string VOUCHER_TABLE = DatabaseHelper.TABLE_PREFIX + "voucher";


        private readonly SQLiteConnection Connection;

        public DatabaseHelper()
        {
            this.Connection = new SQLiteConnection("Data Source=" + App.DATA_PATH + "\\" + DatabaseHelper.DATABASE_NAME);
            
            


             if (File.Exists(DatabaseHelper.DATABASE_NAME))
             {
                return;
             }

            this.Connection.Open();
            using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this.Connection))
            {
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.SCONFIG_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,fkey INTEGER,soption TEXT, svalue TEXT, sort_order TEXT, section TEXT )";
                sqLiteCommand.ExecuteNonQuery();                
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.ADMIN_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,groupid INTEGER,f_name TEXT,l_name TEXT,username TEXT,password TEXT,email TEXT,createdate TEXT,latestlogin INTEGER,lastlogin INTEGER,status TEXT,type TEXT,acl TEXT,aclobject TEXT,description TEXT,resetsid TEXT,lastresettime TEXT)";
                sqLiteCommand.ExecuteNonQuery();                
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVITEMS_TABLE + "(id INTEGER PRIMARY KEY AUTOINCREMENT,image TEXT,moreimages TEXT, name TEXT,description TEXT, company INTEGER, category INTEGER,remain REAL,criticalqty REAL,minorderqty REAL, unit TEXT, color TEXT, sizes TEXT, modelno TEXT ,remark TEXT, barcode TEXT, oldbarcode TEXT, oldprice REAL, saleprice REAL,altsellingchannel TEXT, defpurchaseprice REAL,entrydate TEXT,lastupdate TEXT,isfinancial TEXT,vat TEXT,featured TEXT,setup TEXT,depreciation TEXT,purchaseaccount TEXT,saleaccount TEXT,createdby TEXT,syncflag INTEGER,score REAL,poquickqty REAL)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVOICE_TABLE + "(id INTEGER PRIMARY KEY AUTOINCREMENT,fkey TEXT,fkeytype TEXT,onlineid INTEGER,storeid INTEGER,client INTEGER,ref TEXT, terminal TEXT, promoref TEXT,adjustmentrefid TEXT,unsettled REAL, received REAL, creditunsettled REAL, creditreceived REAL, total REAL, purchaseprice REAL, discount REAL, adjustment REAL,cashreceived REAL,otherpaymentreceived REAL,otherpaymentreceivedlist TEXT,returned REAL, vat REAL, servicecharge REAL,rounded REAL, entrydate TEXT,operator INTEGER,remark TEXT,type TEXT,followup TEXT,hasreturn TEXT,syncflag INTEGER,status TEXT)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVOICE_HISTORY_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,itemid INTEGER,storeid INTEGER,value REAL,presentstock REAL,discount REAL,discountmethod TEXT,unitprice REAL,unitpurchaseprice REAL,vatrate REAL,vat REAL,subtotal REAL,totaleprice REAL,totalpurchaseprice REAL,invoiceid INTEGER,lotno TEXT,productcode TEXT,color TEXT,sizes TEXT,attributes TEXT,supplier INTEGER,warranty TEXT,track TEXT,returned REAL,additionals TEXT,entrydate TEXT,syncflag INTEGER)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE INDEX IF NOT EXISTS invhistory_itemid ON " + DatabaseHelper.INVOICE_HISTORY_TABLE + "(itemid);";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVITEMS_COMPANY_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,adminref REAL,entrydate TEXT,lastmodified TEXT,name TEXT,address TEXT,remark TEXT,contactperson TEXT,logo TEXT,status TEXT)";
                sqLiteCommand.ExecuteNonQuery();

                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.RESTAURANT_TABLE_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,adminref REAL,entrydate TEXT,lastmodified TEXT,name TEXT,store INTEGER,section TEXT,lastinvoice TEXT,servername TEXT,note TEXT,status TEXT,qtdata TEXT)";
                sqLiteCommand.ExecuteNonQuery();
                                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.CATEGORY_TABLE + " (  id INTEGER PRIMARY KEY AUTOINCREMENT,fkey REAL,adminref REAL,parentid REAL,title TEXT,image TEXT,description TEXT,type TEXT,generic TEXT,entrydate TEXT,lastmodified TEXT)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVITEM_HISTORY_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,orderid INTEGER,batchid TEXT,supplierid INTEGER,storeid INTEGER,itemid INTEGER,entrytype TEXT,value REAL,remain REAL,presentstock REAL,finentryid INTEGER,unitprice REAL,shadowunitprice TEXT,totaleprice REAL,unitsalesprice REAL,paidondate REAL,entrydate TEXT,lotno TEXT,productcode TEXT,color TEXT,sizes TEXT,attributes TEXT,remark TEXT,syncflag INTEGER,operator INTEGER)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.MESSAGEBOARD_TABLE + " (  id INTEGER PRIMARY KEY AUTOINCREMENT,parent INTEGER,sendertitle TEXT,senderid TEXT,receivedid TEXT,session TEXT,message TEXT,imagelink TEXT,readerstatus TEXT,timestamp TEXT,entrydate TEXT,type TEXT)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVPAYMENTMETHOD_TABLE + " ( id INTEGER PRIMARY KEY AUTOINCREMENT,adminref INTEGER,name TEXT,location TEXT,spaymenttype TEXT,txndata TEXT,receiveableacc TEXT,txnentrycode TEXT,store TEXT,entrydate TEXT,lastmodified TEXT,sortorder TEXT)";
                sqLiteCommand.ExecuteNonQuery();
                
                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.INVEXTTXNENTRY_TABLE + " ( id INTEGER PRIMARY KEY AUTOINCREMENT,invoiceid INTEGER,txnid INTEGER,methodid INTEGER,txnident TEXT,typecode TEXT,appcode TEXT,amount REAL,entrydate TEXT,operator INTEGER,status TEXT,additional TEXT)";
                sqLiteCommand.ExecuteNonQuery();

                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.WORKPERIOD_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,operator INTEGER,starttime TEXT, endtime TEXT,timespent INTEGER,cashcollected REAL,note TEXT)";
                sqLiteCommand.ExecuteNonQuery();

                sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.LOG_TABLE + " (id INTEGER PRIMARY KEY AUTOINCREMENT,action TEXT, fkey TEXT,fkeytype TEXT,message TEXT,data TEXT,entrydate TEXT,operator INTEGER)";
                sqLiteCommand.ExecuteNonQuery();

                sqLiteCommand.CommandText =
                    "CREATE TABLE IF NOT EXISTS " + DatabaseHelper.VOUCHER_TABLE + " (" +
                    " id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    " local_voucher_id TEXT UNIQUE," +
                    " clientid INTEGER," +
                    " action TEXT," +
                    " voucherdate TEXT," +
                    " total REAL," +
                    " subject TEXT," +
                    " note TEXT," +
                    " trows TEXT," +
                    " entrycode TEXT," +
                    " companyac TEXT," +
                    " operator INTEGER," +
                    " status TEXT," +
                    " sync_status TEXT," +
                    " sync_retry INTEGER DEFAULT 0," +
                    " createdate TEXT" +
                    ")";
                sqLiteCommand.ExecuteNonQuery();


                this.Connection.Close();
            }
        }

        public DataTable selectQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Connection.Open();
                SQLiteCommand command = this.Connection.CreateCommand();
                command.CommandText = query;
                new SQLiteDataAdapter(command).Fill(dataTable);
            }
            catch (SQLiteException ex)
            {
            }
            this.Connection.Close();
            return dataTable;
        }

        public long countEntry(string TABLE, string condition)
        {
            this.Connection.Open();
            SQLiteCommand command = this.Connection.CreateCommand();
            string str = "SELECT count(*) cnt FROM " + TABLE;
            if (!condition.Equals(""))
                str = str + " WHERE " + condition;
            command.CommandText = str;
            command.CommandType = CommandType.Text;
            long int32 = (long)Convert.ToInt32(command.ExecuteScalar());
            this.Connection.Close();
            return int32;
        }

        public DataRow? Find(string TABLE, string condition)
        {
            string str = "SELECT * FROM " + TABLE;
            if (!condition.Equals(""))
                str = str + " WHERE " + condition;
            DataTable dataTable = this.selectQuery(str + " LIMIT 0,1");
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : (DataRow)null;
        }

        public bool Delete(string TABLE, string condition)
        {
            try
            {
                string SQL = "DELETE FROM " + TABLE;
                if (!condition.Equals(""))
                    SQL = SQL + " WHERE " + condition;
                this.execSQL(SQL);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public DataTable FindAll(string TABLE)
        {
            return FindAll(TABLE, "");
        }

        public DataTable FindAll(string TABLE, string condition)
        {
            string query = "SELECT * FROM " + TABLE;
            if (!condition.Equals(""))
                query = query + " WHERE " + condition;
            return this.selectQuery(query);
        }

        public long Save(string TABLE, Dictionary<string, string> values)
        {
            if (!values.ContainsKey("id"))
            {
                return Insert(TABLE, values);
            }
            else
            {
                return Update(TABLE, values);
            }
        }

        public long Update(string TABLE, Dictionary<string, string> values)
        {
            this.Connection.Open();
            using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this.Connection))
            {
                string str1 = "";
                string str2 = "";
                foreach (KeyValuePair<string, string> keyValuePair in values)
                {
                    sqLiteCommand.Parameters.AddWithValue("@" + keyValuePair.Key, (object)keyValuePair.Value);
                    if (keyValuePair.Key == "id")
                    {
                        str2 = keyValuePair.Value;
                    }
                    else
                    {
                        if (!str1.Equals(""))
                            str1 += ",";
                        str1 = str1 + keyValuePair.Key + "=@" + keyValuePair.Key;
                    }
                }
                sqLiteCommand.CommandText = "UPDATE " + TABLE + " SET " + str1 + " WHERE id=@id";
                sqLiteCommand.Prepare();
                sqLiteCommand.ExecuteNonQuery();
                this.Connection.Close();

                return Convert.ToInt64(str2);
            }
        }

        public long Insert(string TABLE, Dictionary<string, string> values)
        {
            this.Connection.Open();
            using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this.Connection))
            {
                string str1 = "";
                string str2 = "";
                foreach (KeyValuePair<string, string> keyValuePair in values)
                {
                    if (!str1.Equals(""))
                    {
                        str1 += ",";
                        str2 += ",";
                    }
                    str1 += keyValuePair.Key;
                    str2 = str2 + "@" + keyValuePair.Key;
                    sqLiteCommand.Parameters.AddWithValue("@" + keyValuePair.Key, (object)keyValuePair.Value);
                }

                sqLiteCommand.CommandText = "INSERT INTO " + TABLE + "(" + str1 + ") VALUES(" + str2 + ")";

                try
                {
                    sqLiteCommand.Prepare();
                    sqLiteCommand.ExecuteNonQuery();
                    long lastInsertRowId = this.Connection.LastInsertRowId;
                    this.Connection.Close();
                    return lastInsertRowId;
                }
                catch (Exception ex)
                {
                    this.Connection.Close();
                    return 0;
                }
            }
        }

        public void execSQL(string SQL)
        {
            this.Connection.Open();
            using (SQLiteCommand sqLiteCommand = new SQLiteCommand(this.Connection))
            {
                sqLiteCommand.CommandText = SQL;
                sqLiteCommand.ExecuteNonQuery();
                this.Connection.Close();
            }
        }
    }
}
