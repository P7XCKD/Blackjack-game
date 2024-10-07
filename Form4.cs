using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AxWMPLib;
using blackjax;

namespace Probz_Blackjack
{
    public partial class Form4 : Form
    {
        private Rectangle hitArea;
        private Rectangle standArea;
        private Rectangle quitArea;
       public int roundNumber = 1; // Public property for tracking the round number
    private blackjack game; // Reference to the blackjack game
        private Timer loadTimer;
        private Timer messageBoxTimer; // Timer for delaying message box
        private Timer dealerUpdateTimer; // Timer for dealer card updates
        private Timer userUpdateTimer; // Timer for user card updates
        private List<PictureBox> dealerPictureBoxes;
        private List<PictureBox> userPictureBoxes;
        private int dealerCardIndex = 0; // To keep track of which card to display next for dealer
        private int userCardIndex = 0; // To keep track of which card to display next for user
        private bool isMessageBoxDisplayed = false; // Flag to control MessageBox display

        public Form4()
        {

            InitializeComponent();
            InitializeGame();
        }
     

        private void InitializeGame()
        {
            loadTimer = new Timer
            {
                Interval = 1000
            };
            loadTimer.Tick += LoadTimer_Tick;

            messageBoxTimer = new Timer
            {
                Interval = 1000 // 1 second delay
            };
            messageBoxTimer.Tick += MessageBoxTimer_Tick;

            dealerUpdateTimer = new Timer
            {
                Interval = 1000 // Adjust as needed for the delay between dealer card updates
            };
            dealerUpdateTimer.Tick += DealerUpdateTimer_Tick;

            userUpdateTimer = new Timer
            {
                Interval = 1000 // Adjust as needed for the delay between user card updates
            };
            userUpdateTimer.Tick += UserUpdateTimer_Tick;

            this.KeyDown += Form4_KeyDown;
            this.KeyPreview = true;

            // Pass label2 (or the appropriate label you want to use) to the blackjack constructor
            game = new blackjack(label2); // Replace 'label2' with your player total label
            dealerPictureBoxes = new List<PictureBox>
    {
        pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7
    };
            userPictureBoxes = new List<PictureBox>
    {
        pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14
    };
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            ConfigureFormAppearance();
            ConfigurePictureBoxes();
            loadTimer.Start();
        }

        private void ConfigureFormAppearance()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void ConfigurePictureBoxes()
        {
            // Configure dealer PictureBoxes
            pictureBox1.Size = new Size(103, 203);
            pictureBox1.Location = new Point(95, 413);
            SetPictureBoxProperties(pictureBox1, new Point(95, 413));

            int originalSpacing = 120;
            int shiftAmount = (int)(originalSpacing * -0.83); // -83% of 120 for dealer

            for (int i = 0; i < dealerPictureBoxes.Count; i++)
            {
                int newX = 95 + (i * originalSpacing) + (i > 0 ? (i * shiftAmount) : 0); // Shift by -83% more for i > 0
                SetPictureBoxProperties(dealerPictureBoxes[i], new Point(newX, 413));
                dealerPictureBoxes[i].Visible = false; // Initially hidden
            }

            // Configure user PictureBoxes with the specified coordinates
            Point userStartPoint = new Point(606, 278);
            Point userEndPoint = new Point(770, 581);
            int userOriginalSpacing = 120;
            int userShiftAmount = (int)(userOriginalSpacing * -0.79); // 79 of 120

            int userPictureBoxWidth = userEndPoint.X - userStartPoint.X;
            int userPictureBoxHeight = userEndPoint.Y - userStartPoint.Y;

            for (int i = 0; i < userPictureBoxes.Count; i++)
            {
                int newX = userStartPoint.X + (i * userOriginalSpacing) + (i > 0 ? (i * userShiftAmount) : 0);
                SetPictureBoxProperties(userPictureBoxes[i], new Point(newX, userStartPoint.Y), new Size(userPictureBoxWidth, userPictureBoxHeight));
                userPictureBoxes[i].Visible = false; // Initially hidden
            }
        }

        private void SetPictureBoxProperties(PictureBox pictureBox, Point location, Size? size = null)
        {
            pictureBox.Location = location;
            pictureBox.Size = size ?? pictureBox1.Size;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            PerformPostLoadActions();
        }

        private void PerformPostLoadActions()
        {
            try
            {
                ConfigureMediaPlayer();
                DefineClickableAreas();
                UpdateRoundNumber(roundNumber);
                ConfigureLabels();

                StartNewGameRound();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred: " + ex.Message);
            }
        }

        private void ConfigureMediaPlayer()
        {
            string mediaFilePath = Path.Combine(Application.StartupPath, "inventory", "gameplay.mp4");
            if (File.Exists(mediaFilePath))
            {
                axWindowsMediaPlayer1.URL = mediaFilePath;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                axWindowsMediaPlayer1.uiMode = "none";
                axWindowsMediaPlayer1.stretchToFit = true;
            }
            else
            {
                ShowErrorMessage("Media file not found: " + mediaFilePath);
            }
        }

        private void DefineClickableAreas()
        {
            hitArea = new Rectangle(new Point(580, 600), new Size(288, 137));
            standArea = new Rectangle(new Point(1001, 597), new Size(310, 139));
            quitArea = new Rectangle(new Point(576, 33), new Size(307, 119));

            axWindowsMediaPlayer1.ClickEvent += axWindowsMediaPlayer1_ClickEvent;
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            Point clickPoint = new Point(e.fX, e.fY);

            if (hitArea.Contains(clickPoint))
            {
                game.PlayerHit();
                ShowGameStatus();
            }
            else if (standArea.Contains(clickPoint))
            {
                game.PlayerStand();
                ShowGameStatus(); // Ensure this is called after PlayerStand
            }
            else if (quitArea.Contains(clickPoint))
            {
                ResetAndReturnToForm2();
            }
        }

    private void Form4_KeyDown(object sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Escape)
    {
        ResetAndReturnToForm2();
    }
    else if (e.KeyCode == Keys.S) // Check for 'S' key press
    {
        this.Hide();
                Form5 ff = new Form5();
        ff.Show();
    }
}

        private void ResetAndReturnToForm2()
        {
            game.StartNewRound();
            UpdateGameUI();

            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void UpdateRoundNumber(int round)
        {
            roundNumber = round;
            label1.Text = roundNumber.ToString();
        }

        private void StartNewGameRound()
        {
            game.StartNewRound();
            UpdateGameUI();
        }

        private void UpdateGameUI()
        {
            var gameState = game.GetGameState();
            label2.Text = gameState.playerTotal.ToString();

            dealerCardIndex = 0; // Reset the dealer card index
            userCardIndex = 0; // Reset the user card index

            // Start updating dealer's cards one by one
            dealerUpdateTimer.Start();

            // Start updating user's cards one by one
            userUpdateTimer.Start();

            // Optionally update all cards at once (if you want to ensure they are displayed immediately)
            for (int i = 0; i < gameState.playerCards.Count && i < 7; i++)
            {
                UpdateUserCardImage(i, game.get_card_image(gameState.playerCards[i]));
            }

            for (int i = 0; i < gameState.dealerCards.Count && i < 6; i++) // Dealer has 6 picture boxes
            {
                UpdateDealerCardImage(i, game.get_card_image(gameState.dealerCards[i]));
            }
        }

        private void ConfigureLabels()
        {
            SetLabelProperties(label1, 1222, 1274, 50, 116, "#262624", 40, FontStyle.Bold);
            SetLabelProperties(label2, 1075, 1122, 171, 216, "#382c5c", 36, FontStyle.Bold);
        }

        private void SetLabelProperties(Label label, int startX, int endX, int startY, int endY, string bgColorHex, int fontSize, FontStyle fontStyle)
        {
            Color bgColor = ColorTranslator.FromHtml(bgColorHex);
            label.BackColor = bgColor;
            label.ForeColor = Color.White;
            label.Font = new Font(label.Font.FontFamily, fontSize, fontStyle);

            int centerX = (startX + endX) / 2;
            int centerY = (startY + endY) / 2;

            label.Location = new Point(centerX - (label.Width / 2), centerY - (label.Height / 2));
        }

        private void ShowGameStatus()
        {
            if (isMessageBoxDisplayed) return; // Prevent multiple displays

            isMessageBoxDisplayed = true;

            var gameState = game.GetGameState();
            string message = $"Round {roundNumber} Status:\n";
            message += $"Player Total: {gameState.playerTotal}\n";
            message += $"Dealer Total: {gameState.dealerTotal}\n";
           
                                           // Check for game over conditions
        bool isGameOver = gameState.playerBust || gameState.dealerBust || gameState.playerBlackjack || gameState.dealerBlackjack;

           /* if (isGameOver)
            {
                message += gameState.playerTotal > 21 ? "You bust!" : "You win!";
                message += "\nClick OK to continue...";
                messageBoxTimer.Start(); // Start the timer for message box delay

                // Commented out the following line to avoid showing the message box multiple times
                // MessageBox.Show(message, "Game Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // This message box is now only shown after the game ends
            MessageBox.Show(message, "Game Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
           */
        }

        private void MessageBoxTimer_Tick(object sender, EventArgs e)
        {
            messageBoxTimer.Stop();
            isMessageBoxDisplayed = false; // Allow the message box to be displayed again
        }


        private void DealerUpdateTimer_Tick(object sender, EventArgs e)
        {
            var gameState = game.GetGameState();
            if (dealerCardIndex < gameState.dealerCards.Count && dealerCardIndex < dealerPictureBoxes.Count)
            {
                UpdateDealerCardImage(dealerCardIndex, game.get_card_image(gameState.dealerCards[dealerCardIndex]));
                dealerCardIndex++;
            }
            else
            {
                dealerUpdateTimer.Stop(); // Stop when done updating
            }
        }

        private void UserUpdateTimer_Tick(object sender, EventArgs e)
        {
            var gameState = game.GetGameState();
            if (userCardIndex < gameState.playerCards.Count && userCardIndex < userPictureBoxes.Count)
            {
                UpdateUserCardImage(userCardIndex, game.get_card_image(gameState.playerCards[userCardIndex]));
                userCardIndex++;
            }
            else
            {
                userUpdateTimer.Stop(); // Stop when done updating
            }
        }

        private void UpdateDealerCardImage(int index, string imagePath)
        {
            // Define the relative path based on your project structure
            string relativePath = Path.Combine("inventory", "images", Path.GetFileName(imagePath));

            // Check both absolute and relative paths
            string[] pathsToCheck = { imagePath, relativePath };

            foreach (var path in pathsToCheck)
            {
                if (File.Exists(path))
                {
                    if (index < dealerPictureBoxes.Count)
                    {
                        dealerPictureBoxes[index].Image = Image.FromFile(path);
                        dealerPictureBoxes[index].Visible = true; // Make it visible after loading
                    }
                    return; // Exit the method after successfully loading an image
                }
            }

            // Optional: Show an error message if the image was not found
            ShowErrorMessage($"Image not found: {imagePath} and {relativePath}");
        }

        private void UpdateUserCardImage(int index, string imagePath)
        {
            // Define the relative path based on your project structure
            string relativePath = Path.Combine("inventory", "images", Path.GetFileName(imagePath));

            // Check both absolute and relative paths
            string[] pathsToCheck = { imagePath, relativePath };

            foreach (var path in pathsToCheck)
            {
                if (File.Exists(path))
                {
                    if (index < userPictureBoxes.Count)
                    {
                        userPictureBoxes[index].Image = Image.FromFile(path);
                        userPictureBoxes[index].Visible = true; // Make it visible after loading
                    }
                    return; // Exit the method after successfully loading an image
                }
            }

            // Optional: Show an error message if the image was not found
            ShowErrorMessage($"Image not found: {imagePath} and {relativePath}");
        }
        public void IncrementRoundNumber()
        {
            roundNumber++;
            UpdateRoundNumber(roundNumber); // Update the label to reflect the new round number
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
