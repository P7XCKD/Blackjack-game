using System.Collections.Generic;

namespace blackjax
{
    public static class GameData
    {
        // Properties to hold game state data
        public static List<string> PlayerCards { get; set; } = new List<string>();
        public static List<string> DealerCards { get; set; } = new List<string>();
        public static int PlayerTotal { get; set; }
        public static int DealerTotal { get; set; }
        public static bool PlayerBust { get; set; }
        public static bool DealerBust { get; set; }
        public static bool PlayerBlackjack { get; set; }
        public static bool DealerBlackjack { get; set; }

        // Method to reset game data
        public static void Reset()
        {
            PlayerCards.Clear();
            DealerCards.Clear();
            PlayerTotal = 0;
            DealerTotal = 0;
            PlayerBust = false;
            DealerBust = false;
            PlayerBlackjack = false;
            DealerBlackjack = false;
        }
    }
}
