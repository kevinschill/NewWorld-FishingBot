using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static NewFarm2.Funktions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Management;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace NewFarm2
{
    
    public partial class Form1 : Form
    {
        class HardDrive
        {
            private string model = null;
            private string type = null;
            private string serialNo = null;
            public string Model
            {
                get { return model; }
                set { model = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public string SerialNo
            {
                get { return serialNo; }
                set { serialNo = value; }
            }
        }
        public Random rnd = new Random();

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
        public static string Reso()
        {
            try
            {
                string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
                string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();
                return screenWidth + "x" + screenHeight;
            }
            catch {}
            return "";
            
        }
        public static double getAvgFps()
        {
            try
            {
                return Math.Round(Queryable.Average(avg_fps.AsQueryable()));
            }
            catch {}
            return 0;
        }
        public static bool StartClient(string key,string why)
        {
            return true;

            bool isValid = false;
            byte[] bytes = new byte[1024];

            try
            {
                IPAddress ipAddress;
                IPAddress.TryParse("bla", out ipAddress);
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
                    string id = getK();
                    if (id != null)
                    {
                        key += "|" + id +"#"+getAvgFps().ToString()+"!"+Reso()+"?"+why;
                    }
                    else
                    {
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                        return isValid;
                    }
                    // Encode the data string into a byte array.    
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
                        isValid = false;
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
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                if (key.ToString() == "F8")
                {
                    if (StartClient(Properties.Settings.Default.botKey,"hotkey"))
                    {
                        if(t != null)
                            if (!t.IsAlive)
                            {
                                t = new Thread(new ThreadStart(ThreadProc));
                                t.Start();
                            }
                            //t = new Thread(new ThreadStart(ThreadProc));
                            //t.Start();
                    }
                    else
                    {
                        MessageBox.Show("Key Invalid...");
                    }
                   // t = new Thread(new ThreadStart(ThreadProc));
                   // t.Start();
                }
                if (key.ToString() == "F9")
                {
                    t.Abort();
                }

            }
            base.WndProc(ref m);
        }

        public Thread t;
        public Form1()
        {
            InitializeComponent();


            RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, 0x77);
            RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, 0x78);


        }
        static Image<Gray, byte> rButton_img;// = Properties.Resources.rbutton_window_1920_1080_21_9.ToImage<Gray, byte>();
        static Image<Gray, byte> isCasted_img;// = Properties.Resources.isCasted_window_1920_1080_21_9.ToImage<Gray, byte>();
        static Image<Gray, byte> stopFishing;// = Properties.Resources.ab_klein_window_1920_1080_21_9.ToImage<Gray, byte>();
        static Image<Gray, byte> koeder_img;// = Properties.Resources.koeder_window_1920_1080_21_9.ToImage<Gray, byte>();
        static Image<Gray, byte> findFish_img;// = Properties.Resources.fish_klein_window_1920_1080_21_9.ToImage<Gray, byte>();
        public void LoadImages(int index)
        {
            if (index == 0)//1920x1080 Windowed 21:9
            {
                   rButton_img = Properties.Resources.rbutton_window_1920_1080_21_9.ToImage<Gray, byte>();
                   isCasted_img = Properties.Resources.isCasted_window_1920_1080_21_9.ToImage<Gray, byte>();
                   stopFishing = Properties.Resources.ab_klein_window_1920_1080_21_9.ToImage<Gray, byte>();
                   koeder_img = Properties.Resources.koeder_window_1920_1080_21_9.ToImage<Gray, byte>();
                   findFish_img = Properties.Resources.fish_klein_window_1920_1080_21_9.ToImage<Gray, byte>();
            }
            if(index == 1)
            {
                rButton_img = Properties.Resources.rKey_1920_1080_21_9.ToImage<Gray, byte>();
                isCasted_img = Properties.Resources.isCasted_1920_1080_21_9.ToImage<Gray, byte>();
                stopFishing = Properties.Resources.ab_klein_1920_1080_21_9.ToImage<Gray, byte>();
                koeder_img = Properties.Resources.koeder_1920_1080_21_9.ToImage<Gray, byte>();
                findFish_img = Properties.Resources.fish_1920_1080_21_9.ToImage<Gray, byte>();
            }
            if(index == 2)
            {
                rButton_img = Properties.Resources.rKey_1280_720.ToImage<Gray, byte>();
                isCasted_img = Properties.Resources.isCasted_1280_720.ToImage<Gray, byte>();
                stopFishing = Properties.Resources.ab_klein_1280_720.ToImage<Gray, byte>();
                koeder_img = Properties.Resources.koeder_1280_720.ToImage<Gray, byte>();
                findFish_img = Properties.Resources.fish_1280_720.ToImage<Gray, byte>();
            }
        }
        
        public static bool IsRButton(Image<Gray, byte> source)
        {
            using (Image<Gray, float> result = source.MatchTemplate(rButton_img, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.8)
                    return true;
            }
            return false;
        }
        
        public static bool IsCAsted(Image<Gray, byte> source)
        {
            using (Image<Gray, float> result = source.MatchTemplate(isCasted_img, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.8)
                    return true;
            }
            return false;
        }
        
        public static bool CheckStopFishing(Image<Gray, byte> source)
        {
            using (Image<Gray, float> result = source.MatchTemplate(stopFishing, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.8)
                    return true;
            }
            return false;
        }

        
        public static bool FindKoeder(Image<Gray, byte> source)
        {
            using (Image<Gray, float> result = source.MatchTemplate(koeder_img, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.9)
                    return true;
            }
            return false;
        }
        
        public static bool FindFish(Image<Gray, byte> source)
        {
            using (Image<Gray, float> result = source.MatchTemplate(findFish_img, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                if (maxValues[0] > 0.9)
                    return true;
            }
            return false;
        }
        public void SetCastAmount(string s)
        {
            try
            {
                totalCastCounter_label.Invoke(new Action(() =>
                {
                    totalCastCounter_label.Text = s;
                }));
            }
            catch (Exception)
            {

            }
        }
        public void SetTitle(string s)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    this.Text = s;
                }));
            }
            catch (Exception)
            {

            }


        }
        public void UseBaitLabelSet()
        {
            try
            {
                outofbait_label.Invoke(new Action(() =>
                {
                    outofbait_label.Visible = true;
                }));
                checkBox1.Invoke(new Action(() =>
                {
                    checkBox1.Checked = false;
                }));
            }
            catch (Exception)
            {

            }


        }

        public int repairAmount_Value = 0;
        public bool randomCastRod = false;
        public int castPowerValue = 0;
        public void CastRod()
        {
            if (randomCastRod)
            {
                LeftMouseUp();
                Thread.Sleep(50);
                LeftMouseDown();
                Thread.Sleep((rnd.Next(10, 100) * 20));
                LeftMouseUp();
            }
            else
            {
                LeftMouseUp();
                Thread.Sleep(50);
                LeftMouseDown();
                Thread.Sleep(castPowerValue * 20);
                LeftMouseUp();
            }
        }
        public Point repairPosition = new Point();
        public Point baitPosition = new Point();
        public Point baitButtonPositon = new Point();

        public void repairRod()
        {
            if (!StartClient(Properties.Settings.Default.botKey,"repair"))
            {
                MessageBox.Show("Problem with Auth...?");
                this.Close();
            }
            pressKey(KeyCodes.TAB);

            Thread.Sleep(1000);
            SetCursorPosition(repairPosition.X, repairPosition.Y);
            Thread.Sleep(1000);
            holdKey(KeyCodes.R);
            Thread.Sleep(1000);
            LeftMouseDown();
            Thread.Sleep(20);
            LeftMouseUp();
            Thread.Sleep(20);
            LeftMouseDown();
            Thread.Sleep(20);
            LeftMouseUp();
            Thread.Sleep(1000);
            upKey(KeyCodes.R);
            Thread.Sleep(1000);
            pressKey(KeyCodes.E);
            Thread.Sleep(1000);
            pressKey(KeyCodes.TAB);
            Thread.Sleep(1000);
            pressKey(KeyCodes.F3);
        }
        public void UseBait()
        {
            Thread.Sleep(1000);
            pressKey(KeyCodes.R);
            Thread.Sleep(500);
            SetCursorPosition(baitPosition.X, baitPosition.Y);
            Thread.Sleep(200);
            LeftMouseClick();
            Thread.Sleep(600);
            SetCursorPosition(baitButtonPositon.X, baitButtonPositon.Y);
            Thread.Sleep(100);
            LeftMouseClick();
            Thread.Sleep(500);
        }
        public void Moverandom()
        {

            if (rnd.Next(1, 100) > 80)
            {
                if (rnd.Next(1, 100) > 50)
                {
                    pressKey(KeyCodes.W, 180);
                    Thread.Sleep(500);
                    pressKey(KeyCodes.S, 180);
                }
                else
                {
                    pressKey(KeyCodes.A, 180);
                    Thread.Sleep(500);
                    pressKey(KeyCodes.D, 180);
                }
            }
        }
        public void randomAsk()
        {
            int nr = rnd.Next(1, 40000);
            if(nr > 39990)
            {
                nr = rnd.Next(1, 10000);
                if (nr < 40)
                {
                    if (!StartClient(Properties.Settings.Default.botKey,"rnd"))
                    {
                        MessageBox.Show("Key not Valid???");
                        this.Close();
                    }
                }

            }

        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        public static IntPtr GetWindowRectA(IntPtr hWnd, ref Rect rect)
        {
            return GetWindowRect(hWnd, ref rect);
        }
        public bool repaidRod_b = false;
        public bool useBait_b = false;
        public bool antiAfk_b = false;
        public static List<double> avg_fps = new List<double>();
        public void ThreadProc()
        {
            // string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            // string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();
            // string reso = screenWidth + "x" + screenHeight;
            // Console.WriteLine(reso);
            // int scrape_offset = 0;
            // if(reso == "1920x1080")
            //    scrape_offset = 250;
            // if (reso == "3440x1440")
            //    scrape_offset = 500;
           // MessageBox.Show("Hello");
            Thread.Sleep(2000);
            avg_fps.Clear();
            DateTime _lastCheckTime = DateTime.Now;
            long _frameCount = 0;
            IntPtr hd = FindWindowA("New World");
            if(hd == IntPtr.Zero)
            {
                MessageBox.Show("cant find New World Window ??\nTry Again.");
                t.Abort();
            }
            bool search_fish = true;
            bool finished_fishing = false;
            bool castRod = true;
            bool isCasted_b = false;
            DateTime casting_time_start = DateTime.UtcNow;
            int total_casts = 0;
            int repairCounter = 0;

            while (true)
            {

                try
                {
                    randomAsk();
                    var screenshot_temp = ScreenCapture.CaptureWindow(hd, 300, 600).ToImage<Gray, byte>();

                   // CvInvoke.Imshow("s", screenshot_temp); //Show the image
                  //  CvInvoke.WaitKey(10);  //Wait for the key pressing event
                    if (useBait_b)
                    {
                        if (FindKoeder(screenshot_temp))
                        {
                            UseBait();
                            Thread.Sleep(2000);
                            screenshot_temp = ScreenCapture.CaptureWindow(hd, 300, 600).ToImage<Gray, byte>();
                            if (FindKoeder(screenshot_temp))
                            {
                                useBait_b = false;
                                UseBaitLabelSet();
                            }
                        }

                    }
                    if (castRod)
                    {
                        if (antiAfk_b)
                        {
                            Moverandom();
                            Thread.Sleep(1000);
                        }

                        isCasted_b = false;
                        keybd_event(0x56, 0, 0x0001 | 0, 0);
                        Thread.Sleep(100);
                        CastRod();
                        casting_time_start = DateTime.UtcNow;
                        castRod = false;
                    }


                    if (isCasted_b == false)
                    {

                        //Debug.Print("Check for Cast...");
                        if (IsCAsted(screenshot_temp))
                        {
                            isCasted_b = true;
                            //Debug.Print("Found Cat...");
                        }
                        else
                        {
                            DateTime otherTime = DateTime.UtcNow;
                            var seconds = (casting_time_start < otherTime) ? (otherTime - casting_time_start).TotalSeconds : (casting_time_start - otherTime).TotalSeconds;
                            if (seconds > 3)
                            {
                                castRod = true;
                            }
                        }
                    }
                    else
                    {

                        if (search_fish && castRod == false)
                        {
                            //Debug.Print("Serach Fish");
                            if (FindFish(screenshot_temp))
                            {
                                //Debug.Print("Found Fish");
                                LeftMouseClick();
                                search_fish = false;
                                Thread.Sleep(rnd.Next(500, 1000));
                            }
                        }
                        if (!search_fish && castRod == false)
                        {
                            if (IsRButton(screenshot_temp))
                            {
                                //Debug.Print("Found End");
                                finished_fishing = true;
                            }
                            if (!finished_fishing)
                            {
                                //Debug.Print("Reel Loop");
                                if (!CheckStopFishing(screenshot_temp))
                                {
                                    LeftMouseDown();
                                }
                                else
                                {
                                    LeftMouseUp();
                                    Thread.Sleep(rnd.Next(800, 1500));
                                }

                            }
                            else
                            {
                                //Debug.Print("End this shit");
                                total_casts += 1;
                                if (repaidRod_b)
                                    repairCounter += 1;

                                SetCastAmount("Total Casts: " + total_casts.ToString());
                                castRod = true;
                                search_fish = true;

                                Thread.Sleep(rnd.Next(3000, 3400));
                                keybd_event(0x56, 0, 0x0002 | 0, 0);
                                finished_fishing = false;

                                if (repaidRod_b)
                                {
                                    if (repairCounter >= repairAmount_Value)
                                    {
                                        Thread.Sleep(1500);
                                        repairRod();
                                        Thread.Sleep(rnd.Next(500, 1000));
                                        repairCounter = 0;
                                    }
                                }


                            }
                        }
                    }

                    Thread.Sleep(10);

                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }

                Interlocked.Increment(ref _frameCount);
                double secondsElapsed = (DateTime.Now - _lastCheckTime).TotalSeconds;
                long count = Interlocked.Exchange(ref _frameCount, 0);
                double fps = Math.Round(count / secondsElapsed);
                _lastCheckTime = DateTime.Now;
                //SetTitle("FPS: " + fps.ToString());
                avg_fps.Add(fps);
                

            }

        }
        private void scanArea_button_Click(object sender, EventArgs e)
        {
            if (!t.IsAlive)
            {
                t.Start();
                t = new Thread(new ThreadStart(ThreadProc));
            }


        }

        private void startBotButton_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                if (!t.IsAlive)
                {
                    t = new Thread(new ThreadStart(ThreadProc));
                    t.Start();
                }
            }
            else
            {
                t = new Thread(new ThreadStart(ThreadProc));
                t.Start();
            }

           // if (StartClient(Properties.Settings.Default.botKey))
           // {
               
           // }
           // else
           // {
            //    MessageBox.Show("Key Invalid...");
            //}

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            castPowerLabel.Text = "Power: " + trackBar1.Value.ToString() + "%";
            castPowerValue = trackBar1.Value;
            Properties.Settings.Default.castPower = castPowerValue;
            Properties.Settings.Default.Save();
        }

        private void castPowerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (castPowerCheckBox.Checked)
            {
                randomCastRod = true;
            }
            else
            {
                randomCastRod = false;
            }
        }

        private void stopBotButton_Click(object sender, EventArgs e)
        {
            t.Abort();
        }
        public void go()
        {
            this.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            string[] files = Directory.GetFiles("./");
            foreach (string file in files)
            {
                if (file.EndsWith(".exe"))
                {
                    SetTitle(file.Substring(2, file.Length - 6));
                }
            }
            

            this.baitSetupButton.Enabled = false;
            this.repairSetupButton.Enabled=false;
            this.startBotButton.Enabled=false;
            this.stopBotButton.Enabled=false;
            this.checkBox1.Enabled=false;
            this.antiAfkCheckBox.Enabled=false;
            this.repair_CheckBox.Enabled=false;
            this.trackBar1.Enabled=false;
            this.trackBar2.Enabled=false;
            this.castPowerCheckBox.Enabled=false;
            
            //this.Enabled = false;
            Thread.Sleep(80);
            this.keyButton.Enabled = true;
            if (Properties.Settings.Default.botKey != null)
            {
                if (StartClient(Properties.Settings.Default.botKey,"load"))
                    EnableControls();
            }


            trackBar1.Value = Properties.Settings.Default.castPower;
            castPowerValue = Properties.Settings.Default.castPower;
            repairAmount_Value = Properties.Settings.Default.repairAmount;
            castPowerLabel.Text = "Power: " + Properties.Settings.Default.castPower.ToString() + "%";
            trackBar2.Value = Properties.Settings.Default.repairAmount;
            repairAmount_label.Text = "Amount: " + Properties.Settings.Default.repairAmount.ToString();

            baitPosition = Properties.Settings.Default.baitPosition;
            baitButtonPositon = Properties.Settings.Default.baitButtonPosition;

            repairPosition = Properties.Settings.Default.repairPosition;
            comboBox1.SelectedIndex = Properties.Settings.Default.lastResolution;

            LoadImages(Properties.Settings.Default.lastResolution);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            repairAmount_Value = trackBar2.Value;
            repairAmount_label.Text = "Amount: " + trackBar2.Value.ToString();
            Properties.Settings.Default.repairAmount = trackBar2.Value;
            Properties.Settings.Default.Save();
        }

        private void repairSetupButton_Click(object sender, EventArgs e)
        {

            InfoRepair m = new InfoRepair();
            m.ShowDialog();

            bool okButtonClicked = m.OKButtonClicked;
            if (okButtonClicked)
            {
                //Thread.Sleep(3000);
                repairPosition = m.getPosition;
                Properties.Settings.Default.repairPosition = repairPosition;
                Properties.Settings.Default.Save();
                Thread.Sleep(500);
            }
        }

        private void repair_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (repair_CheckBox.Checked)
            {
                repaidRod_b = true;
            }
            else
            {
                repaidRod_b = false;
            }
        }

        private void baitSetupButton_Click(object sender, EventArgs e)
        {
            BaitInfo m = new BaitInfo();
            m.ShowDialog();

            (Point a, Point b) = m.positions;

            bool okButtonClicked = m.OKButtonClicked;
            if (okButtonClicked)
            {
                baitPosition = a;
                baitButtonPositon = b;
                Properties.Settings.Default.baitPosition = a;
                Properties.Settings.Default.baitButtonPosition = b;
                Properties.Settings.Default.Save();
                Thread.Sleep(500);
                //MessageBox.Show("Bait: " + baitPosition.ToString() + " - - - Button: " + baitButtonPositon.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                useBait_b = true;
                outofbait_label.Visible = false;
            }
            else
            {
                useBait_b = false;
            }

        }

        private void antiAfkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (antiAfkCheckBox.Checked)
            {
                antiAfk_b = true;

            }
            else
            {
                antiAfk_b = false;
            }
        }
        private void EnableControls()
        {
            this.baitSetupButton.Enabled = true;
            this.repairSetupButton.Enabled = true;
            this.startBotButton.Enabled = true;
            this.stopBotButton.Enabled = true;
            this.checkBox1.Enabled = true;
            this.antiAfkCheckBox.Enabled = true;
            this.repair_CheckBox.Enabled = true;
            this.trackBar1.Enabled = true;
            this.trackBar2.Enabled = true;
            this.castPowerCheckBox.Enabled = true;
        }
        private void keyButton_Click(object sender, EventArgs e)
        {
            Key m = new Key();
            m.setText = Properties.Settings.Default.botKey = m.BotKey;
            m.ShowDialog();

            Properties.Settings.Default.botKey = m.BotKey;
            Properties.Settings.Default.Save();

            if(m.ok)
            {
                EnableControls();
                //foreach(Control c in this.Controls)
                 //   c.Enabled = true;
            }
            bool okButtonClicked = m.OKButtonClicked;
            if (okButtonClicked)
            {
                
                Thread.Sleep(500);
                //MessageBox.Show("Bait: " + baitPosition.ToString() + " - - - Button: " + baitButtonPositon.ToString());
            }
        }
        public static List<string> progNames = new List<string>
        {
            "Firefox.exe",
            "Chrome.exe",
            "IE.exe",
            "Discord.exe",
            "explorer.exe",
            "excel.exe",
            "powerpoint.exe",
            "AISuite3.exe",
            "LogiOptions.exe",
            "RadeonSoftware.exe",
            "RTSS.exe",
            "steam.exe",
            "sqlwriter.exe",
            "upc.exe",
            "UplayWebCore.exe",
            "ControlCenter.exe"
        };
        public static Dictionary<string, string> ProgFakes = new Dictionary<string, string>
        {
            {"Firefox.exe","Google - Mozilla Firefox" },
            {"Chrome.exe","Google - Google Chromebrowser" },
            {"IE.exe","" },
            {"Discord.exe","#support | GamingLounge" },
            {"explorer.exe","Computer" },
            {"excel.exe","" },
            {"powerpoint.exe","" },
            {"AISuite3.exe","" },
            {"LogiOptions.exe","" },
            {"RadeonSoftware.exe","" },
            {"RTSS.exe","" },
            {"steam.exe","Steam" },
            {"sqlwriter.exe","" },
            {"upc.exe","" },
            {"UplayWebCore.exe","" },
            {"ControlCenter.exe","ControlCenter" }
        };
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImages(comboBox1.SelectedIndex);

            Properties.Settings.Default.lastResolution = comboBox1.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string new_filename = progNames[rnd.Next(0, 15)];
            string[] files = Directory.GetFiles("./");
            foreach(string file in files)
            {
                if(file.EndsWith(".exe"))
                {
                    string strCmdText;
                    strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    System.IO.File.Move("./" + file, "./" + new_filename);
                }
            }
        }
    }
}