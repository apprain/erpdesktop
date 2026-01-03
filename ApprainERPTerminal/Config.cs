using Newtonsoft.Json;
using System.Data;
namespace ApprainERPTerminal
{

    public class Config
    {
        private string basePath = "c:/data";
        private dynamic responseObject;
        private DatabaseHelper db;
        public Config()
        {
            compileResource();
        }

        private void compileResource()
        {
            if (!Directory.Exists(this.basePath))
            {
                Directory.CreateDirectory(this.basePath);
            }

            if (File.Exists(this.getBootFilePath()))
            {
                responseObject = JsonConvert.DeserializeObject(File.ReadAllText(getBootFilePath()));
            }

            this.db = new DatabaseHelper();
        }

        public string Token()
        {
            if (responseObject == null) return "";

            return responseObject.token;

        }

        public string baseUrl()
        {            
            return Token();
        }

        public string AuthUrl()
        {
            if (responseObject == null) return "";

            return responseObject.url;

        }

        public string UrldataExchnage()
        {
            if (responseObject == null) return "";

            return responseObject.urldataExchnage;

        }

        public void WriteLog(string logStr)
        {
            string str = this.Setting("username");
            File.AppendAllText(this.basePath + "/audit_trail_log.txt", string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", (object)DateTime.Now) + " " + str + " " + logStr + Environment.NewLine);
        }

        public string ReadLog() => File.ReadAllText(this.basePath + "/audit_trail_log.txt");

        public string Setting(string SOption)
        {
            DataRow dataRow = this.db.Find(DatabaseHelper.SCONFIG_TABLE, "soption='" + SOption + "'");
            return dataRow == null ? "" : dataRow["svalue"].ToString();
        }

        public string Setting(string SOption, string DefaultValue)
        {
            DataRow dataRow = this.db.Find(DatabaseHelper.SCONFIG_TABLE, "soption='" + SOption + "'");
            if (dataRow == null)
            {
                return DefaultValue;
            }

            string str = dataRow["svalue"].ToString();

            return (str == null || str.Equals("")) && DefaultValue != null ? DefaultValue : str;
        }

        public bool NavAccess(string opt)
        {
            string[] access = App.Config().NavAccess();
            return access.Contains(opt);

        }
        public string[] NavAccess()
        {
            string jsonString = Setting("authuser");

            if (jsonString.Equals("")) return Array.Empty<string>();

            var root = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            var aclobjectJson = root["aclobject"].ToString();
            var aclobject = JsonConvert.DeserializeObject<Dictionary<string, object>>(aclobjectJson);

            // Extract the ethicalopts dictionary
            if (aclobject.TryGetValue("ethicalopts", out var ethicaloptsJson))
            {
                var ethicalopts = JsonConvert.DeserializeObject<Dictionary<string, object>>(ethicaloptsJson.ToString());

                if (ethicalopts.TryGetValue("app_nav_access", out var appNavAccessJson))
                {
                    var appNavAccess = JsonConvert.DeserializeObject<List<string>>(appNavAccessJson.ToString());

                    // Return app_nav_access as a string array
                    return appNavAccess.ToArray();
                }
            }

            return Array.Empty<string>();

        }

        public void Update(string SOption, string SValue)
        {
            DataRow dataRow = this.db.Find(DatabaseHelper.SCONFIG_TABLE, "soption='" + SOption + "'");
            Dictionary<string, string> values = new Dictionary<string, string>();
            if (dataRow != null)
            {
                values.Add("id", Convert.ToString(dataRow["id"]));
            }
            values.Add("soption", SOption);
            values.Add("svalue", SValue);
            values.Add("fkey", "");
            values.Add("sort_order", "");
            values.Add("section", "general");
            this.db.Save(DatabaseHelper.SCONFIG_TABLE, values);
        }

        public string getBootFilePath()
        {
            return this.getbasePath("/config.txt");
        }

        public string getbasePath(string p)
        {
            return this.basePath + p;
        }
    }

}
