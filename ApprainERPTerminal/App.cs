using ApprainERPTerminal.Modules;

namespace ApprainERPTerminal
{
    public static class App
    {

        public static string DATA_PATH = "c:\\data";

        public static string DS = "\\";
        public static string TOKEN = "";
        public static string TIMESTAMP = "";
        public static string PIN_VERIFIED = "";
        public static string ECR_PAYMENT_VERIFIED = "";
        public static Boolean IS_Thread_Running = false;

        public static void setToken(string token)
        {
            App.TOKEN = token;
        }

        public static void setTimestamp(string timestamp)
        {
            App.TIMESTAMP = timestamp;
        }

        public static bool isLoggedin() => !App.TOKEN.Equals("");

        public static bool startBackgroundThread()
        {
            if (App.IS_Thread_Running == false)
            {
                IS_Thread_Running = true;

                return true;
            }
            else
            {
                return false;
            }

        }

        public static Log Log()
        {
            return new Log();
        }


        public static Config Config()
        {
            return new Config();
        }

        public static Utility Utility()
        {
            return new Utility();
        }

        public static Common Common()
        {
            return new Common();
        }
    }
}
