using ApprainERPTerminal.UI.POS;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;

namespace ApprainERPTerminal.Modules
{
    public class Utility
    {


        public static string GetLine(string text, int start, int maxLength)
        {
            if (start >= text.Length) return "";

            string substring = text.Substring(start);
            if (substring.Length <= maxLength) return substring;

            int lastSpaceIndex = substring.LastIndexOf(' ', maxLength);
            return lastSpaceIndex == -1 ? substring.Substring(0, maxLength).Trim() : substring.Substring(0, lastSpaceIndex);
        }

        public static double Round(double value, int precitionTimes)
        {
            if (value == 0)
            {
                return 0.00;
            }

            double precition = Math.Pow(10, precitionTimes);

            value += (.5 / precition);

            value = (((int)(value * precition)) / precition);


            return value;
        }
        
        public static string currencyFormat(double myMoneyString, int precition)
        {
            if (myMoneyString == 0)
            {
                return "0.00";
            }

            return currencyFormat(Convert.ToString(myMoneyString), precition);
        }
        public static string currencyFormat(string myMoneyString, int precition)
        {
            if (myMoneyString.Equals("0") || myMoneyString.Equals("0"))
            {
                return "0.00";
            }

            try
            {
                return string.Format("{0:#.00}", Convert.ToDecimal(myMoneyString));
            }
            catch
            {
                try
                {
                    myMoneyString = myMoneyString.Substring(0, 10);
                    return string.Format("{0:#.00}", Convert.ToDecimal(myMoneyString));
                }
                catch
                {
                    return myMoneyString;
                }
            }
        }

        public static string getDate(string v)
        {
            return v == null || v.Equals("") ? DateTime.Now.ToString(v) : DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        public static string Center(string s, int width)
        {
            if (s.Length >= width)
            {
                return s;
            }

            int count1 = (width - s.Length) / 2;
            int count2 = width - s.Length - count1;
            return new string(' ', count1) + s + new string(' ', count2);
        }

        public static int getPrecision()
        {
            Config config = new Config();

            string parem = config.Setting("inventorysettings_precision");

            if (parem.Equals(""))
            {
                return 0;
            }

            try
            {
                return Convert.ToInt16(parem);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static string[] InvoiceCommonParameters()
        {
            Config config = new Config();

            string parem = config.Setting("inventorysettings_invoice_common_parameters");

            if (parem.Equals(""))
            {
                return new string[] {};
            }

            return parem.Split(",");
        }
        
        public static string[] ItemDisplayFormatParameters()
        {
            Config config = new Config();

            string parem = config.Setting("inventorysettings_item_name_dis_format");

            if (parem.Equals(""))
            {
                return new string[] {};
            }

            return parem.Split(",");
        }
        
        public static string[] PinList(String Profile)
        {
            Config config = new Config();

            string jsonData = config.Setting("inventorysettings_pinlist");

            if (jsonData.Equals(""))
            {
                return new string[] {};
            }

            if (IsValidJson(jsonData))
            {
                JObject jsonObject = JObject.Parse(jsonData);

                if (jsonObject[Profile] is JArray reportArray && reportArray.Count > 0)
                {
                    return reportArray.Select(num => num.ToString()).ToArray();
                }
                else
                {
                    return new string[] { }; 
                }

            }
            else
            {
                return jsonData.Split(',')
                    .Select(item => item.Trim()) 
                    .ToArray();
            }
        }

        public static bool IsValidJson(string input)
        {
            try
            {
                JToken.Parse(input);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }


        public void ShowPINDialogBox(string pinProfile)
        {
            string[] pinlist = Utility.PinList(pinProfile);
            if (pinlist.Count() > 0)
            {
                App.PIN_VERIFIED = "UNSUCCESSFUL";
                FrmPinVerification frmPinVerification = new FrmPinVerification(pinProfile);
                frmPinVerification.ShowDialog();
                frmPinVerification.Dispose();
            }
            else
            {
                App.PIN_VERIFIED = "SUCCESSFUL";
            }
        }

        public bool checkPinVerified(string pinProfile)
        {
            ShowPINDialogBox(pinProfile);
            if (App.PIN_VERIFIED.Equals("SUCCESSFUL"))
            {

                return true;
            }

            MessageBox.Show("Sorry! Verification is unsuccessful.");

            return false;
        }

        public void ShowMyECRPaymentDialog(DataRow itemRow, double amount)
        {
            //App.ECR_PAYMENT_VERIFIED = "SUCCESSFUL";
            FrmECRDialog frmECRDialog = new FrmECRDialog(itemRow, amount);
            frmECRDialog.ShowDialog();
            frmECRDialog.Dispose();
        }

        public string UID2Token(string UID)
        {
            var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            string token_stirng = "{ERPDESKTOP:" + UID + ":" + Timestamp + "}";
            //Debug.WriteLine(token_stirng);
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token_stirng));

        }
    }
}
