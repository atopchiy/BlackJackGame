using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class PlayerActions
    {
        public static Player CalculatePoints(Player human, Player computer)
        {
            var playerValue = DeckActions.CalculateHandValue(human.Hand);
            var computerValue = DeckActions.CalculateHandValue(computer.Hand);
            if (playerValue < 21 && computerValue < 21)
                return playerValue > computerValue ? human : computer;
            else if (playerValue < 21 && computerValue > 21)
                return human;
            else if (playerValue > 21 && computerValue < 21)
                return computer;
            else
                return playerValue < computerValue ? human : computer;
        }
    }
}
