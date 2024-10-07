using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AxWMPLib;
using blackjax;
using Probz_Blackjack; // Ensure this is the correct namespace for Form1

namespace Probz_Blackjack
{
    public partial class Form2 : Form
    {
        private Rectangle rectangleA;
        private Rectangle rectangleB;
        private Rectangle rectangleC;

        public Form2()
        {
            InitializeComponent();
            this.Resize += Form2_Resize; // subscribe to the resize event

            // define the rectangle areas
            rectangleA = new Rectangle(new Point(180, 485), new Size(665 - 180, 643 - 485));
            rectangleB = new Rectangle(new Point(351, 219), new Size(811 - 351, 462 - 219));
            rectangleC = new Rectangle(new Point(851, 175), new Size(1223 - 851, 545 - 175));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                // Attempt to use the relative path
                string mediaFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"inventory\f2.mp4");

                if (!File.Exists(mediaFilePath))
                {
                    // If the relative path does not work, try the absolute path
                    mediaFilePath = @"E:\\A5EV5C\\a5ev5c-main\\blackjax\\blackjax\\bin\\Debug\\inventory\\f2..mp4";
                }

                if (File.Exists(mediaFilePath))
                {
                    axWindowsMediaPlayer1.URL = mediaFilePath;
                    axWindowsMediaPlayer1.settings.setMode("loop", true);
                    axWindowsMediaPlayer1.uiMode = "none"; // hide video UI
                    axWindowsMediaPlayer1.stretchToFit = true; // stretch video to fit the form
                }
                else
                {
                    MessageBox.Show("Media file not found: " + mediaFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Set form properties
                this.FormBorderStyle = FormBorderStyle.None; // remove form border
                this.WindowState = FormWindowState.Maximized; // maximize the form
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form2_Resize(object sender, EventArgs e)
        {
            // adjust any controls if needed when the form is resized
            // if not needed, you can remove this method
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            // handle the media player click event
            Point clickPoint = this.PointToClient(Cursor.Position);

            if (rectangleA.Contains(clickPoint))
            {
                form1 form1 = new form1(); // Correctly open Form1
                form1.Show();
                this.Hide();
            }
            else if (rectangleB.Contains(clickPoint))
            {
                Form4 form4 = new Form4();
                form4.Show();
                this.Hide();
            }
            else if (rectangleC.Contains(clickPoint))
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Point clickPoint = e.Location;

            // check if the click is within any defined rectangles
            if (rectangleA.Contains(clickPoint))
            {
                form1 form1 = new form1(); // Correctly open Form1
                form1.Show();
                this.Hide();
            }
            else if (rectangleB.Contains(clickPoint))
            {
                Form4 form4 = new Form4();
                form4.Show();
                this.Hide();
            }
            else if (rectangleC.Contains(clickPoint))
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
        }
    }
}