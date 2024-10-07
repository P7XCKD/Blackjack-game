using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AxWMPLib;
using blackjax; // include this for AxWindowsMediaPlayer

namespace Probz_Blackjack
{
    public partial class form1 : Form
    {
        private Rectangle targetArea;

        public form1()
        {
            InitializeComponent();
            this.Resize += form1_resize; // subscribe to the resize event

            // define the target area rectangle
            targetArea = new Rectangle(new Point(635, 237), new Size(1183 - 635, 631 - 237));
        }

        private void form1_load(object sender, EventArgs e)
        {
            try
            {
                // set up the media player with the relative and absolute paths
                string oldMediaFilePath = Path.Combine(Application.StartupPath, "inventory", "intro.mp4");
                string newMediaFilePath = @"E:\A5EV5C\blackjax\blackjax\bin\Debug\inventory\intro.mp4";

                string mediaFilePath = File.Exists(oldMediaFilePath) ? oldMediaFilePath : newMediaFilePath;

                if (File.Exists(mediaFilePath))
                {
                    axWindowsMediaPlayer1.URL = mediaFilePath;
                    axWindowsMediaPlayer1.settings.setMode("loop", true);
                    axWindowsMediaPlayer1.uiMode = "none"; // hide video ui
                    axWindowsMediaPlayer1.stretchToFit = true; // stretch video to fit the form
                }
                else
                {
                    MessageBox.Show("Media file not found: " + mediaFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // set form properties
                this.FormBorderStyle = FormBorderStyle.None; // remove form border
                this.WindowState = FormWindowState.Maximized; // maximize the form
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void form1_resize(object sender, EventArgs e)
        {
            // adjust button position when the form is resized, if needed
            // if not needed, you can remove this method
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            // get the mouse position relative to the form
            Point clickPoint = this.PointToClient(Cursor.Position);

            // check if the click is within the target area
            if (targetArea.Contains(clickPoint))
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
            }
        }
    }
}
