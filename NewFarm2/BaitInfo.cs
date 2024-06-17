using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static NewFarm2.Funktions;

namespace NewFarm2
{
    public partial class BaitInfo : Form
    {
        public BaitInfo()
        {
            InitializeComponent();

        }


        private Point baitP = new Point();
        private Point baitB = new Point();
        private bool okButton = false;
        public bool OKButtonClicked
        {
            get { return okButton; }
        }
        public (Point, Point) positions
        {
            get { return (baitP, baitB); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            baitP = GetCursorPosition();
            MessageBox.Show("Bait Position Set");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            baitB = GetCursorPosition();
            MessageBox.Show("Bait Button Position Set");
            okButton = true;
            this.Close();
        }
    }
}
