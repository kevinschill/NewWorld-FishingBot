using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static NewFarm2.Funktions;


namespace NewFarm2
{
    public partial class InfoRepair : Form
    {
        public InfoRepair()
        {
            InitializeComponent();

            pictureBox1.Image = Properties.Resources.invFertig;
        }
        public Point loc = new Point();

        public Point getPosition
        {
            get { return loc; }
        }
        private bool okButton = false;
        public bool OKButtonClicked
        {
            get { return okButton; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            loc = GetCursorPosition();

            MessageBox.Show("Repair Button set.");
            okButton = true;

            this.Close();
        }
    }
}
