using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;

namespace ApprainERPTerminal.Modules
{
    internal class BackgroundProcess
    {
        ERPCloud erpCloud; 
        Thread childThread;
        Button UIBtn;
        DatabaseHelper db;
        bool isRun = false;

        public BackgroundProcess()
        {
            db = new DatabaseHelper();
            erpCloud = new ERPCloud();
        }

        public void End() {
            isRun = false;
        }
        string _token = string.Empty;
        public void Start(Button btn,string token) {
            _token = token;
            UIBtn = btn;
            isRun = true;
            ThreadStart childref = new ThreadStart(()=> CallUploadInvoice(btn));            
            childThread = new Thread(childref);
            childThread.Start();

        }

        int cnt = 0;
        public void CallUploadInvoice(Button btn)
        {
            try
            {
                while (isRun)
                {
                    Thread.Sleep(1000 * 60 * 1);
                    //Thread.Sleep(1000 * 5);

                    DataRow dataRowH = db.Find(DatabaseHelper.INVOICE_HISTORY_TABLE, "syncflag > 0 and returned > 0");
                    DataRow dataRowI = db.Find(DatabaseHelper.INVOICE_TABLE, "syncflag > 0");
                    if (dataRowH == null && dataRowI == null)
                    {
                        if (cnt >= 3)
                        {
                            cnt = 0;
                            Debug.WriteLine("Calling Air Drop...");
                            erpCloud.callAriDrop();
                        }
                        else
                        {
                            cnt++;
                        }
                    }
                    else
                    {
                        cnt = 0;
                        erpCloud.UploadInvoice(null);
                    }
                    Debug.WriteLine(cnt + ": Thread running.." + _token);
                }

            }
            catch (ThreadAbortException e)
            {
               Debug.WriteLine(e.Message);
            }
        }
    }
}
