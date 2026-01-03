using ApprainERPTerminal.Modules;
using ApprainERPTerminal.Modules.Printer;
using ApprainERPTerminal.UI;
using ApprainERPTerminal.UI.Common;
using ApprainERPTerminal.UI.POS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Text;


namespace ApprainERPTerminal
{
    public partial class FrmLogin : Form
    {
        private HttpClient client;
        private string UrlAuth;
        private string UrlDataExchnage;
        private ERPCloud eRPCloud;
        private Config config;

        public FrmLogin()
        {
            UrlAuth = "";
            UrlDataExchnage = "";

            InitializeComponent();

            eRPCloud = new ERPCloud();
            config = new Config();
            if (File.Exists(this.config.getBootFilePath()))
            {
                string str1 = File.ReadAllText(this.config.getBootFilePath());
                if (!str1.Equals(""))
                {
                    txtToken.Text = config.Token();
                    UrlAuth = config.AuthUrl();
                    UrlDataExchnage = config.UrldataExchnage();

                }
                else
                {
                    this.panelLogin.Hide();
                    this.panelConnection.Show();
                }
            }
            else
            {
                this.panelLogin.Hide();
                this.panelConnection.Show();
            }
        }
        private void lblConnection_Click(object sender, EventArgs e)
        {
            panelLogin.Hide();
            panelConnection.Show();
        }


        private void FrmLogin_Load(object sender, EventArgs e)
        {

            this.Text = "Login, Apprain ERP";
            this.lblSitetitle.Text = this.config.Setting("site_title", "Continue");
            txtUsername.Text = this.config.Setting("username");
            lblVersion.Text = "Version 2.1.57";

            String logo =  App.Config().Setting("poslogo");
            String path = App.Config().getbasePath("\\" + logo);

           
            if (File.Exists(path))
            {
                picLogo.Image = new Bitmap(Image.FromFile(@path), new Size(80, 80));
            }

        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            string text = txtToken.Text;
            if (text.Equals(""))
            {
                MessageBox.Show("Enter a qualified token.\nContact your account manager for details.");
                return;
            }
            JObject jobject = new JObject();
            jobject.Add("token", (JToken)text);
            jobject.Add("url", (JToken)(text + "/ethical/auth"));
            jobject.Add("urldataExchnage", (JToken)(text + "/ethical/exchange"));
            File.WriteAllText(this.config.getBootFilePath(), jobject.ToString());
            MessageBox.Show("Connection updated successfull.");
            this.panelLogin.Show();
            this.panelConnection.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelLogin.Show();
            panelConnection.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           // AesEncryption encryption = new AesEncryption();
           /* string password = "your-password";
            string plaintext = "Hello AES with HMAC!";

            byte[] encrypted = AesEncryption.Encrypt(plaintext, password);
            string decrypted = AesEncryption.Decrypt(encrypted, password);

            Debug.WriteLine("Encrypted (base64): " + Convert.ToBase64String(encrypted));
            Debug.WriteLine("Decrypted: " + decrypted);

            new PrintTicket().Print("4777");
           */
            doLogin();
        }

        private void doLogin()
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string dbusername = config.Setting("username");
            string dbpassword = config.Setting("password");

            if (!chkLocallogin.Checked)
            {
                DatabaseHelper db = new DatabaseHelper();
                DataRow AdminRecord = db.Find(DatabaseHelper.ADMIN_TABLE, "username='" + username + "'");

                if (AdminRecord == null)
                {
                    MessageBox.Show("Invalid User Name.");
                    return;
                }

                dbpassword = AdminRecord["password"].ToString();
                if (CreateMD5(password).Equals(dbpassword))
                {
                    Utility utility = new Utility();
                    string authuser = JsonConvert.SerializeObject(AdminRecord);
                    string UID = AdminRecord["id"].ToString();
                    string token = utility.UID2Token(UID);

                    config.Update("token", token);
                    config.Update("username", username);
                    config.Update("password", dbpassword);
                    config.Update("adminref", UID);

                    App.Log().Write("Local Login By " + username, true);

                    App.TOKEN = token;
                    this.Hide();
                    new FrmMDI().ShowDialog();



                }
                else
                {
                    MessageBox.Show("Invalid Login!");
                }
            }
            else
            {
                ConnectAsync(username, password, this);
            }
        }

        public async Task ConnectAsync(string user, string password, FrmLogin frm)
        {

            try
            {
                config = new Config();
                client = new HttpClient();

                Dictionary<string, string> nameValueCollection = new Dictionary<string, string>();
                nameValueCollection.Add("username", user);
                nameValueCollection.Add("password", password);
                FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)nameValueCollection);
                var response = await client.PostAsync(config.AuthUrl(), content);
                var responseString = await response.Content.ReadAsStringAsync();
              //  Debug.WriteLine(responseString);
                try
                {
                    dynamic dynObj = JsonConvert.DeserializeObject(responseString);

                    String status = dynObj["status"];
                    String message = dynObj["message"];
                    if (status.Equals("1"))
                    {
                        if (!message.Equals(""))
                        {
                            MessageBox.Show(message);
                        }

                        config.Update("username", user);
                        config.Update("password", CreateMD5(password));
                        config.Update("token", (String)dynObj["token"]);
                        config.Update("timestamp", (String)dynObj["timestamp"]);
                        String auth = Convert.ToString(dynObj["auth"]);
                        String adminref = Convert.ToString(dynObj["auth"]["adminref"]);
                        config.Update("authuser", auth);

                        String stationInfo = Convert.ToString(dynObj["station"]);

                        dynamic stationObject = JsonConvert.DeserializeObject(stationInfo);

                        config.Update("adminref", adminref);
                        config.Update("storeid", (String)stationObject["storeid"]);
                        config.Update("storename", (String)stationObject["storename"]);
                        config.Update("terminalcode", (String)stationObject["terminalcode"]);
                        config.Update("storelocation", (String)stationObject["storelocation"]);
                        config.Update("storephoneno", (String)stationObject["storephoneno"]);
                        config.Update("storeemailaddress", (String)stationObject["storeemailaddress"]);

                        try
                        {
                            String settingInfo = Convert.ToString(dynObj["setting"]);
                            Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(settingInfo);
                            if (dictionary != null)
                            {
                                Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator();
                                try
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        KeyValuePair<string, string> current = enumerator.Current;
                                        config.Update(current.Key, current.Value);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        App.Log().Write("Cloud Login By " + user, true);

                        App.TOKEN = (String)dynObj["token"];
                        frm.Hide();
                        new FrmMDI().ShowDialog();


                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check your Internet connection.\n\n" + ex.Message);
            }
        }

        private string CreateMD5(string myText)
        {
            var hash = System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.ASCII.GetBytes(myText ?? ""));
            return string.Join("", Enumerable.Range(0, hash.Length).Select(i => hash[i].ToString("x2")));
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                doLogin();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FrmPaymentMethods().ShowDialog();
            /*
            double amountGiven = 200;
            string messageType = "A00" + (amountGiven * 100).ToString() + "|B00TK|B01156|Y0090|U0001";// amount *100. e.g 300*100= 30000;  "A00"+(amount*100).toString()+"|B00TK|Y0090|U0001";
            int messageLength = messageType.Length;
            string lenValue = messageLength.ToString("X2");

            byte[] bytes = Encoding.ASCII.GetBytes($"<STX>{lenValue}<TYPE>{messageType}<ETX>");

            byte lrc = 0;
            for (int i = 1; i < bytes.Length; i++)
            {
                lrc ^= bytes[i];
            }

            string lrcValue = lrc.ToString("X2");

            string hexString = ASCIItoHex(messageType); 
            string header = "02002200";
            string etx = "03";
            string commandStr = header + hexString + etx + lrcValue;

            //Debug.WriteLine(commandStr);

            //# SerialPort serialPort = new SerialPort("COM11", 9600, Parity.None, 8, StopBits.One); 
            //# serialPort.ReadTimeout = 5000;
            //# serialPort.WriteTimeout = 5000;
            //# serialPort.Open();

            try
            {

                //# byte[] command = StringToByteArray(commandStr);
                //# serialPort.Write(command, 0, command.Length);

                //# while (serialPort.BytesToRead == 0);

                //# byte[] buffer = new byte[serialPort.BytesToRead];
                //# serialPort.Read(buffer, 0, buffer.Length);
                //# string response = ByteArrayToString(buffer);
                string response = "020001010301";

                bool containsAck = response.Contains("02");

                if (containsAck)
                {
                    //# while (serialPort.BytesToRead == 0) ;

                    //# byte[] buffer2 = new byte[serialPort.BytesToRead];
                    //# serialPort.Read(buffer2, 0, buffer2.Length);
                    //# string response2 = ByteArrayToString(buffer2);

                    //string response2 = "A00000000020000|B00TK|B01050|B02NFC|B03VISA|F01|R0000|Y0090|Q00000019|Q01462870******1039|Q022023.09.19 19:23:40|Q0351004361|Q04689137|Q05000018043933|Q06000009100120112|Q07000010";
                    string response2 = "A00000000020000|B00TK|F00|R00H0|Y0090|E00TRANSACTION OTHER ERROR|Q022023.09.19 19:33:41|Q0351004361|Q06000009100120112";

                    string FieldValue = messageValue(response2, "R00");
                    if (FieldValue.Equals("00"))
                    {
                        Debug.WriteLine("Transaction Approved");
                        Debug.WriteLine(messageValue(response2, "Q01")); // Card No
                        Debug.WriteLine(messageValue(response2, "Q04")); // Response Code
                    }
                    else
                    {
                        Debug.WriteLine(messageValue(response2, "E00")); // 
                    }
                }
                else
                {
                    Debug.WriteLine("The response does not contain ACK");

                }

            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //# serialPort.Close();
            }*/
        }

        public string messageValue(string message, string field)
        {
            string[] messageArray = message.Split("|");
            string result = string.Empty;

            for (int i = 0; i < messageArray.Length; i++)
            {
                if (messageArray[i].Substring(0, 3).Equals(field))
                {
                    result = messageArray[i].Substring(3, messageArray[i].Length - 3);
                    break;
                }

            }

            return result;
        }

        public string ASCIItoHex(string Value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in Value)
            {
                sb.Append(string.Format("{0:x2}", b));
            }

            return sb.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


    }
}