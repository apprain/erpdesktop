using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using System.ComponentModel;
using ApprainERPTerminal.UI.POS;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace ApprainERPTerminal.Modules.ECR
{
    internal class NEXGO
    {
        public async void sendCMD(FrmECRDialog frmECRDialog, double amountGiven, dynamic txndataObj)
        {

            string terminalIp = "169.254.48.101";
            int terminalPort = 139;

            // Define the transaction data (this will vary based on the protocol being used)
            //$"00<FS>0011000"
            string txnData = $"<STX>00<FS>001100<ETX><LRC>";// BuildTransactionRequest("100.00"); // Example transaction data
            Debug.WriteLine("Started.......");
            try
            {
                // Create a TCP client and connect to the Ingenico terminal
                using (TcpClient client = new TcpClient(terminalIp, terminalPort))
                {
                    // Get the network stream to send and receive data
                    NetworkStream stream = client.GetStream();

                    // Convert the transaction data to bytes
                    byte[] data = Encoding.ASCII.GetBytes(txnData);

                    // Send the transaction data to the terminal
                    stream.Write(data, 0, data.Length);
                    Debug.WriteLine("Transaction data sent to Desk/5000 terminal");

                    // Buffer to store the response from the terminal
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    Debug.WriteLine("Raw response: " + BitConverter.ToString(buffer, 0, bytesRead));


                    // Convert the response to a string and display it
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Debug.WriteLine("Response from terminal: " + response);
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("SocketException: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }


            /*
            SerialPort serialPort = new SerialPort()
            {
                PortName = "COM3",     // Replace with the correct COM port
                BaudRate = 19200,      // Default baud rate
                DataBits = 7,          // 7-bit ASCII communication
                Parity = Parity.Even,  // Even parity
                StopBits = StopBits.One, // 1 stop bit
                Handshake = Handshake.None,
                Encoding = Encoding.ASCII
            };

            
            try
            {
               
                // Open the serial port
                serialPort.Open();
                Console.WriteLine(" Open the serial port.");

                // Create a transaction request string (example command)
                // Replace this with the actual command based on Desk-5000 API/Protocol
                string transactionRequest = BuildTransactionRequest("100.00"); // Example transaction of $100.00
                Debug.WriteLine(transactionRequest);
                // Convert the transaction request to a byte array
                byte[] requestBytes = Encoding.ASCII.GetBytes(transactionRequest);

                // Send the transaction request
                serialPort.Write(requestBytes, 0, requestBytes.Length);

                Console.WriteLine("Transaction request sent.");

                // Optional: Read the response from the Desk-5000
                string response = serialPort.ReadLine();  // Reading the response from the device
                Console.WriteLine("Response from Desk-5000: " + response);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the port when done
                if (serialPort.IsOpen)
                    serialPort.Close();
            }

            */


            /*
          //  App.ECR_PAYMENT_VERIFIED = "SUCCESSFUL";
            string message = "";

             //await Task.Delay(2000);
             String port = txndataObj["port"];
             //string message = "";

             //######### TXN PROCESSING
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
             SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
             serialPort.ReadTimeout = 5000;
             serialPort.WriteTimeout = 5000;
             serialPort.Open();
             try
             {
                 byte[] command = StringToByteArray(commandStr);
                 serialPort.Write(command, 0, command.Length);

                 var sw = new Stopwatch();
                 sw.Start();
                 while (serialPort.BytesToRead == 0 && sw.Elapsed < TimeSpan.FromSeconds(1));
                 sw.Stop();

                 byte[] buffer = new byte[serialPort.BytesToRead];
                 serialPort.Read(buffer, 0, buffer.Length);
                  //string response = ByteArrayToString(buffer);
                  string response = "020001010301";

                 bool containsAck = response.Contains("02");

                 if (containsAck)
                 {
                     sw.Start();
                     while (serialPort.BytesToRead == 0 && sw.Elapsed < TimeSpan.FromSeconds(3)) ;
                     sw.Stop();

                     //  while (serialPort.BytesToRead == 0) ;

                     //# byte[] buffer2 = new byte[serialPort.BytesToRead];
                     //# serialPort.Read(buffer2, 0, buffer2.Length);
                     //# string response2 = ByteArrayToString(buffer2);

                     string response2 = "A00000000020000|B00TK|B01050|B02NFC|B03VISA|F01|R0000|Y0090|Q00000019|Q01462870******1039|Q022023.09.19 19:23:40|Q0351004361|Q04689137|Q05000018043933|Q06000009100120112|Q07000010";
                     //string response2 = "A00000000020000|B00TK|F00|R00H0|Y0090|E00TRANSACTION OTHER ERROR|Q022023.09.19 19:33:41|Q0351004361|Q06000009100120112";

                     string FieldValue = messageValue(response2, "R00");
                     if (FieldValue.Equals("00"))
                     {

                         App.ECR_PAYMENT_VERIFIED = "SUCCESSFUL";
                         Debug.WriteLine("Transaction Approved");

                         message = "Approval Code: " + messageValue(response2, "Q04");

                        // Debug.WriteLine(messageValue(response2, "Q01")); // Card No
                        // Debug.WriteLine(messageValue(response2, "Q04")); // Response Code
                     }
                     else
                     {
                         message = messageValue(response2, "E00");
                         //Debug.WriteLine(messageValue(response2, "E00")); // 
                     }
                 }
                 else
                 {
                     message = "The response does not contain ACK";
                     //Debug.WriteLine("The response does not contain ACK");

                 }

             }
             catch (TimeoutException ex)
             {
                 message = ex.Message;
                 // MessageBox.Show(ex.Message);
             }
             catch (Exception ex)
             {
                 message = ex.Message;
                    MessageBox.Show(ex.Message);

            }
             finally
             {
                  serialPort.Close();
             }
             //######### TXN PROCESSING
            

            try
            {
                string contents = File.ReadAllText(@"C:\data\ecr-result.txt");
                App.ECR_PAYMENT_VERIFIED = contents;
            }
            catch { }

            JObject jObj = new JObject();
                jObj.Add("status", App.ECR_PAYMENT_VERIFIED);
                jObj.Add("message", message); 
                frmECRDialog.updateStatus(jObj);*/
        }

        static string BuildTransactionRequest(string amount)
        {
            // This would be specific to the Desk-5000 protocol
            // Example format: "TRX,100.00\n" where TRX is a hypothetical command
          //  return $"TRX,{amount}\n";
            return $"00<FS>0011000";
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
