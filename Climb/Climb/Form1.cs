using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Climb.Util;

namespace Climb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // This is the current resolution of the persons computer (like windows)
            // We need to get rid of the options that are smaller
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.PrimaryScreen;
            int S_width = screen.Bounds.Width;
            int S_height = screen.Bounds.Height;

            // These are all the possible resolutions I thought of
            Point [] sizes = {  new Point(1600,1200), 
                                 new Point(1280, 1024), new Point(1280, 720),
                                 new Point(1024, 768), new Point(1024, 576),
                                  new Point(800, 600), new Point (800, 480) };

            // This list is all the resolutions that you will get to pick of
            List<string> strs = new List<string>();

            // This loop goes through and removes all the points that are out of range of your PC
            foreach (Point pt in sizes)
            {
                if (pt.X > S_width || pt.Y > S_height)
                {
                    // dont add to list of reses
                }
                else
                {
                    strs.Add(pt.X + " x " + pt.Y);
                }
            }
            //"1600 x 1200", "1280 x 1024", "1280 x 720", "1024x768", "1024 x 576" , "800 x 480", "800 x 600"
            
            cmbResolution.DataSource = strs;

            CUtil.FullScreenResolution.X = S_width;
            CUtil.FullScreenResolution.Y = S_height;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            string res = cmbResolution.SelectedItem as string;
            int w = int.Parse(res.Remove(res.IndexOf('x') - 1));
            int h = int.Parse(res.Substring(res.IndexOf('x') + 1));
            CUtil.InitalResolution.X = w;
            CUtil.InitalResolution.Y = h;
            Options.IsMusicOn = chkMusic.Checked;
            Options.IsSFXOn = chkSFX.Checked;
            if (windowedRadial.Checked)
            {
                Options.IsFullScreen = false;
                Options.IsLetterBox = false;
            }
            else if (letterBoxRadial.Checked)
            {
                Options.IsFullScreen = true;
                Options.IsLetterBox = true;
            }
            else if (fullscreenRadial.Checked)
            {
                Options.IsFullScreen = true;
                Options.IsLetterBox = false;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
