using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprainERPTerminal.Modules
{
    public class Common
    {
        DatabaseHelper db = new DatabaseHelper();
        public long lastInsertId(string Table)
        {
            if (string.IsNullOrEmpty(Table))
            {
                return 0;
            }

            DataRow record = db.Find(Table, "1=1 order by id desc");

            if (record == null)
            {
                return 0;
            }

            return Convert.ToInt32(record["id"].ToString());
        }

        public double itemAvailablePurchasePrice(string itemid, string salesprice)
        {
           // DataRow Record = db.Find(DatabaseHelper.INVITEM_HISTORY_TABLE, "itemid=" + itemid + " and unitsalesprice = " + salesprice);
            DataRow Record = db.Find(DatabaseHelper.INVITEM_HISTORY_TABLE, "itemid=" + itemid + " ORDER BY id DESC ");

            if (Record == null) return 0.0;

            return Convert.ToDouble(Record["unitprice"].ToString());
        }

        public double itemSalesPrice(string itemid)
        {
            DataRow Record = db.Find(DatabaseHelper.INVITEM_HISTORY_TABLE, "itemid=" + itemid + " ORDER BY id DESC");

            if (Record == null) return 0.0;

            return Convert.ToDouble(Record["unitsalesprice"].ToString());
        }


        public string FormatItemName(DataRow item)
        {
            if (item == null || item["name"] == null || item["id"] == null)
            {
                return "";
            }

            string name = item["name"].ToString().ToUpper();
            string configValue = App.Config().Setting("inventorysettings_item_name_dis_format");

            if (string.IsNullOrWhiteSpace(configValue))
            {
                return name;
            }

            string[] configOptions = configValue.Split(',');
            if (configOptions.Contains("saleprice"))
            {
                string salePrice = itemSalesPrice(item["id"].ToString()).ToString();
                string currency = App.Config().Setting("currency");
                name += $"~{salePrice}{currency}";
            }

            return name;
        }

        public string tableId2Name(string tableId)
        {
            if (tableId.Equals("")) return "";

            DataRow TableRecord = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableId);
            if (TableRecord == null) return "";

           return TableRecord["name"].ToString();
        }

        public string tableId2ServerName(string tableId)
        {

            if (tableId.Equals("")) return "";
            
            DataRow TableRecord = db.Find(DatabaseHelper.RESTAURANT_TABLE_TABLE, "id=" + tableId);
            if (TableRecord == null) return "";

            string serverId = TableRecord["servername"].ToString();

            if (string.IsNullOrEmpty(serverId))
            {
                return "";
            }


            DataRow itemRow = db.Find(DatabaseHelper.ADMIN_TABLE, "id='" + serverId + "'");

            if (itemRow == null) return "";

            return itemRow["f_name"].ToString() + itemRow["l_name"].ToString();
            
        }

        public void setWaiter(string TableId, string UserId)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "id", TableId },
                { "servername", UserId}
            };
            db.Save(DatabaseHelper.RESTAURANT_TABLE_TABLE, values);
        }

       
    }
}
