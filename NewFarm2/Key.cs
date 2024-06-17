using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace NewFarm2
{
    public partial class Key : Form
    {
        public Key()
        {
            InitializeComponent();
           // Console.WriteLine("Hallo");
           // Console.WriteLine(Properties.Settings.Default.botKey);
            textBox1.Text = Properties.Settings.Default.botKey;
            textBox1.Update();
        }
        private static string getUniqueID(string drive)
        {
            try
            {
                if (drive == string.Empty)
                {
                    //Find first drive
                    foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                    {
                        if (compDrive.IsReady)
                        {
                            drive = compDrive.RootDirectory.ToString();
                            break;
                        }
                    }
                }

                if (drive.EndsWith(":\\"))
                {
                    //C:\ -> C
                    drive = drive.Substring(0, drive.Length - 2);
                }

                string volumeSerial = getVolumeSerial(drive);
                string cpuID = getCPUID();

                //Mix them up and remove some useless 0's
                return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
            }
            catch { }

            return null;

        }

        private static string getVolumeSerial(string drive)
        {
            try
            {
                ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                disk.Get();

                string volumeSerial = disk["VolumeSerialNumber"].ToString();
                disk.Dispose();

                return volumeSerial;
            }
            catch { }

            return null;
        }

        private static string getCPUID()
        {
            try
            {
                string cpuInfo = "";

                ManagementClass managClass = new ManagementClass("win32_processor");
                ManagementObjectCollection managCollec = managClass.GetInstances();

                foreach (ManagementObject managObj in managCollec)
                {
                    if (cpuInfo == "")
                    {
                        //Get only the first CPU's ID
                        cpuInfo = managObj.Properties["processorID"].Value.ToString();
                        break;
                    }
                }

                return cpuInfo;
            }
            catch { }
            return null;
        }
        public static string getK()
        {
            try
            {

                return getUniqueID("C:\\");

            }
            catch { }

            return null;

        }
        public static bool StartClient(string key)
        {

            bool isValid = false;
            byte[] bytes = new byte[1024];

            try
            {
                // Connect to a Remote server  
                // Get Host IP Address that is used to establish a connection  
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
                // If a host has multiple addresses, you will get a list of addresses  
                //IPHostEntry host = Dns.GetHostEntry("45.89.127.116");
                IPAddress ipAddress;
                IPAddress.TryParse("45.89.127.116", out ipAddress);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 9999);

                // Create a TCP/IP  socket.    
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.    
                try
                {
                    // Connect to Remote EndPoint  
                    sender.Connect(remoteEP);

                    // Console.WriteLine("Socket connected to {0}",
                    //sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    //
                    string id = getK();
                    if (id != null)
                    {
                        key += "|" + id;
                    }
                    else
                    {
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                        return isValid;
                    }
                    byte[] msg = Encoding.ASCII.GetBytes(key);

                    // Send the data through the socket.    
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.    
                    int bytesRec = sender.Receive(bytes);
                    //Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    if (Encoding.ASCII.GetString(bytes, 0, bytesRec) == "Ok!")
                    {
                        isValid = true;
                    }
                    if (Encoding.ASCII.GetString(bytes, 0, bytesRec) == "Fail!")
                    {
                        //Console.WriteLine("Key Not Accepted..");
                    }

                    // Release the socket.    
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException )
                {
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException )
                {
                    // Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception )
                {
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception )
            {
                //Console.WriteLine(e.ToString());
            }
            return isValid;
        }
        private bool okButton = false;
        public bool OKButtonClicked
        {
            get { return okButton; }
        }
        public string setText
        {
            set { textBox1.Text = value; }
        }
        public string BotKey
        {
            get { return textBox1.Text; }
        }
        private bool isOk = false;
        public bool ok
        {
            get { return isOk; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (BotKey.Length > 0)
                if(StartClient(BotKey))
                {
                    MessageBox.Show("Key Saved");
                    isOk = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went Wrong ???");
                }
            
        }
    }
}
