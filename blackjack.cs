using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Probz_Blackjack;
using System.IO;


namespace blackjax
{
    internal class blackjack
    {
        public int roundNumber { get; private set; } = 1; // Public property for tracking the round number

        private List<string> deck;
        private Random random;
        private List<string> player_cards;
        private List<string> dealer_cards;
        private int player_total;
        private int dealer_total;
        private bool player_blackjack;
        private bool dealer_blackjack;
        private bool player_bust;
        private bool dealer_bust;
        private bool round_ended; // Flag to track if the round has ended
        private Label playerTotalLabel; // Reference to the player's total label

        public blackjack(Label playerTotalLabel) // Constructor now accepts a label reference
        {
            this.playerTotalLabel = playerTotalLabel; // Initialize the label reference
            random = new Random();
            player_cards = new List<string>();
            dealer_cards = new List<string>();
            initialize_deck();
            round_ended = false;
        }

        private void initialize_deck()
        {
            deck = new List<string>();
            string[] suits = { "H", "D", "C", "S" };
            string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add($"{rank}{suit}");
                }
            }
        }

        public void ResetDeck()
        {
            initialize_deck();
        }

        private string generate_card()
        {
            if (deck.Count == 0)
            {
                ResetDeck();
                if (deck.Count == 0)
                {
                    throw new InvalidOperationException("Deck is empty and could not be reset.");
                }
            }

            int index = random.Next(deck.Count);
            string card = deck[index];
            deck.RemoveAt(index);
            return card;
        }

        public string get_card_image(string card)
        {
            // Define the relative path
            string relativePath = $"images/{card}.jpg";
            // Define the absolute path
            string absolutePath = $@"C:\Users\DEV\source\repos\blackjax\bin\Debug\inventory\images\{card}.jpg";

            // Check if the relative path file exists
            if (System.IO.File.Exists(relativePath))
            {
                return relativePath; // Return relative path if it exists
            }
            else
            {
                return absolutePath; // Otherwise, return absolute path
            }
        }

        public void StartNewRound()
        {
            roundNumber++; // Increment the round number
            player_cards.Clear();
            dealer_cards.Clear();
            player_total = 0;
            dealer_total = 0;
            player_blackjack = false;
            dealer_blackjack = false;
            player_bust = false;
            dealer_bust = false;
            round_ended = false; // Reset round_ended flag

            deal_initial_cards();
            check_blackjack();
        }

        private void deal_initial_cards()
        {
            for (int i = 0; i < 2; i++)
            {
                player_cards.Add(generate_card());
                dealer_cards.Add(generate_card());
            }

            update_totals();
            UpdatePlayerTotalLabel(); // Update player's total label after dealing cards
        }

        public void PlayerHit()
        {
            if (!player_bust && !player_blackjack && !round_ended)
            {
                player_cards.Add(generate_card());
                update_totals();
                UpdatePlayerTotalLabel(); // Update label after player's card is added

                if (player_total > 21)
                {
                    player_bust = true;
                    EndRound("Player busts!");
                }
                else if (player_total == 21)
                {
                    player_blackjack = true;
                    EndRound("Player has blackjack!");
                }
            }
        }

        public void PlayerStand()
        {
            if (!player_bust && !player_blackjack && !round_ended)
            {
                dealer_turn();
                EndRound();
            }
        }

        private void dealer_turn()
        {
            while (dealer_total < 17)
            {
                dealer_cards.Add(generate_card());
                update_totals();
            }

            if (dealer_total > 21)
            {
                dealer_bust = true;
            }
            else if (dealer_total == 21)
            {
                dealer_blackjack = true;
            }
        }

        private void update_totals()
        {
            player_total = calculate_total(player_cards);
            dealer_total = calculate_total(dealer_cards);
        }


        private int calculate_total(List<string> cards)
        {
            int total = 0;
            int ace_count = 0;

            foreach (var card in cards)
            {
                string rank = card.Substring(0, card.Length - 1);
                int value;

                switch (rank)
                {
                    case "J":
                    case "Q":
                    case "K":
                        value = 10;
                        break;
                    case "A":
                        value = 11; // Initially treat Ace as 11
                        ace_count++; // Count the Ace for later adjustment
                        break;
                    default:
                        value = int.Parse(rank);
                        break;
                }

                total += value;
            }

            // Adjust for Aces: if total exceeds 21, convert Aces from 11 to 1
            while (total > 21 && ace_count > 0)
            {
                total -= 10; // Convert one Ace from 11 to 1
                ace_count--;
            }

            return total;
        }

        private void check_blackjack()
        {
            player_blackjack = player_total == 21;
            dealer_blackjack = dealer_total == 21;

            if (player_blackjack && dealer_blackjack)
            {
                EndRound("Both player and dealer have blackjack! It's a tie.");
            }
            else if (player_blackjack)
            {
                EndRound("Player has blackjack!");
            }
            else if (dealer_blackjack)
            {
                EndRound("Dealer has blackjack!");
            }
        }

        public void EndRound()
        {
            EndRound(null);
        }

        private void EndRound(string message)
        {
            if (round_ended) return; // Avoid multiple endings

            round_ended = true;

            if (!player_bust && !player_blackjack && !dealer_blackjack)
            {
                if (!dealer_bust)
                {
                    if (player_total > dealer_total)
                    {
                        message = "Player wins!";
                    }
                    else if (player_total < dealer_total)
                    {
                        message = "Dealer wins!";
                    }
                    else
                    {
                        message = "It's a tie!";
                    }
                }
                else
                {
                    message = "Dealer busts! Player wins!";
                }
            }

            if (message != null)
            {
                ShowEndRoundMessage(message); // Call to show the message with options
            }
        }
        private void ShowEndRoundMessage(string message)
        {
            string playerCards = string.Join(", ", player_cards);
            string dealerCards = string.Join(", ", dealer_cards);
            string roundMessageDisplay = $"{message}\n\nPlayer's cards: {playerCards} (Total: {player_total})\nDealer's cards: {dealerCards} (Total: {dealer_total})";

            // Show a message box with Yes and No options
            var result = MessageBox.Show(
                roundMessageDisplay + "\n\nWould you like to play again?",
                "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            // Set up the database connection with the relative and absolute paths
            string relativeDbPath = @"..\Database1.mdf"; // Adjust the relative path as needed
            string absoluteDbPath = @"C:\Users\DEV\source\repos\blackjax\Database1.mdf"; // Your absolute path

            // Use the relative path if it exists, otherwise fall back to the absolute path
            string dbFilePath = File.Exists(relativeDbPath) ? relativeDbPath : absoluteDbPath;

            // Debugging: Check which path is being used
            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show($"Database file does not exist at: {dbFilePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the file doesn't exist
            }

            // Connection string using the selected database file path
            string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbFilePath};Integrated Security=True;Connect Timeout=30";

            // Update the database with round details
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Determine the next round number
                int nextRoundNumber = GetNextRoundNumber(con); // Get the next round number

                // Insert round message and number into the database
                string insertQuery = "INSERT INTO [dbo].[Table] (RoundNumber, RoundMessage) VALUES (@RoundNumber, @RoundMessage)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@RoundNumber", nextRoundNumber);
                    cmd.Parameters.AddWithValue("@RoundMessage", roundMessageDisplay);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (result == DialogResult.Yes)
            {
                StartNewRound(); // Start a new round if the user chooses to play again
            }
            else if (result == DialogResult.No)
            {
                // Teleport to Form2
                TeleportToForm2();
            }
        }

        // Method to get the next round number
        private int GetNextRoundNumber(SqlConnection con)
        {
            int currentRound = 0;

            string selectQuery = "SELECT MAX(RoundNumber) FROM [dbo].[Table]";

            try
            {
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    con.Open(); // Open the connection
                    object result = cmd.ExecuteScalar(); // Execute the query

                    if (result != DBNull.Value)
                    {
                        currentRound = Convert.ToInt32(result); // Get the current round number
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Error: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed if it was opened
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return currentRound + 1; // Increment the round number
        }



        private void TeleportToForm2()
        {
            // Assuming you have a reference to the main application form or access to the Application context
            // You can use this method to create and show Form2
            Form2 form2 = new Form2();
            form2.Show(); // Show Form2
                          // Optionally, you may want to close the current form or perform other actions here
                          // For example: Application.Exit(); // Uncomment this if you want to close the application
        }



        private void UpdatePlayerTotalLabel() // Method to update the player's total label
        {
            if (playerTotalLabel != null)
            {
                playerTotalLabel.Text = $"{player_total}"; // Update label text
            }
        }

        public string GetRoundMessage()
        {
            return round_ended ? "Game Over" : string.Empty;
        }

        public int GetPlayerTotal()
        {
            return player_total;
        }

        public int GetDealerTotal()
        {
            return dealer_total;
        }

        public List<string> GetDealerCards()
        {
            return new List<string>(dealer_cards);
        }

        public List<string> GetUserCards()
        {
            return new List<string>(player_cards);
        }

        public (List<string> playerCards, List<string> dealerCards, int playerTotal, int dealerTotal, bool playerBust, bool dealerBust, bool playerBlackjack, bool dealerBlackjack) GetGameState()
        {
            return (new List<string>(player_cards), new List<string>(dealer_cards), player_total, dealer_total, player_bust, dealer_bust, player_blackjack, dealer_blackjack);
        }
    }
}
