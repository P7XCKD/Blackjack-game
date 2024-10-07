using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AxWMPLib;
using Probz_Blackjack;

namespace blackjax
{
    public partial class Form3 : Form
    {
        private string oldMediaFilePath = Path.Combine(Application.StartupPath, "inventory", "tutorial.mp4");
        private string newMediaFilePath = @"E:\A5EV5C\blackjax\blackjax\bin\Debug\inventory\tutori.mp4";
        private Rectangle rectangleA = new Rectangle(927, 9, 344, 101); // Define Rectangle A's coordinates
        private bool isForm4Opened = false; // Flag to prevent multiple openings of Form4

        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
            this.FormBorderStyle = FormBorderStyle.None; // remove border
            this.WindowState = FormWindowState.Maximized; // maximize form
            axWindowsMediaPlayer1.ClickEvent += axWindowsMediaPlayer1_ClickEvent; // Add ClickEvent handler for the media player
            axWindowsMediaPlayer1.PlayStateChange += axWindowsMediaPlayer1_PlayStateChange; // Add PlayStateChange handler
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                string mediaFilePath = File.Exists(oldMediaFilePath) ? oldMediaFilePath : newMediaFilePath;

                if (File.Exists(mediaFilePath))
                {
                    axWindowsMediaPlayer1.URL = mediaFilePath;
                    axWindowsMediaPlayer1.settings.setMode("loop", false); // disable loop
                    axWindowsMediaPlayer1.uiMode = "none"; // hide video ui
                    axWindowsMediaPlayer1.stretchToFit = true; // stretch video to fit the form
                }
                else
                {
                    MessageBox.Show("Media file not found: " + mediaFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            if (isForm4Opened) return; // Check if Form4 has already been opened

            // Check if the click is within Rectangle A
            Point clickPoint = new Point(e.fX, e.fY);
            if (rectangleA.Contains(clickPoint))
            {
                OpenForm4();
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                OpenForm4();
            }
        }

        private void OpenForm4()
        {
            if (isForm4Opened) return; // Prevent opening multiple instances of Form4

            isForm4Opened = true; // Set the flag to true to indicate Form4 is opened
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide(); // Hide Form3
        }
    }
}
