using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApprainERPTerminal.Modules
{
    public class Log
    {
        private string m_exePath = string.Empty;

        public void Write(string logMessage, bool force)
        {
            if (!force)
            {
                return;
            }
            
            m_exePath = App.DATA_PATH;

            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Format(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Write(string logMessage)
        {
            if (!App.Config().Setting("write_event_log").Equals("Enabled"))
            {
                return;
            }

            Write(logMessage, true);
        }

        public void Format(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                txtWriter.WriteLine("  :{0}", logMessage);

            }
            catch (Exception ex)
            {
            }
        }

        public void Save(string type, string message)
        {
            Save(type, message, "", "","");
        }


        /*
         * Action = Executive/System/Other
         */
        public void Save(string action, string message, string fkeytype, string fkey,string data)
        {

            DatabaseHelper db = new DatabaseHelper();
            Dictionary<string, string> Records = new Dictionary<string, string>();
            string adminref = App.Config().Setting("adminref");

            string entrydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Records.Add("action", action);
            Records.Add("fkey", fkey);
            Records.Add("fkeytype", fkeytype);
            Records.Add("message", message);
            Records.Add("entrydate", entrydate);
            Records.Add("operator", adminref);
            Records.Add("data", data);
            db.Save(DatabaseHelper.LOG_TABLE, Records);
        }
    }
}
