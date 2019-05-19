using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class ShowResults
    {
        public static void ShowBlackJackWinner(Player player)
        {
            Console.WriteLine($"{player.Name} won with 21 points!");
        }
        public static void ShowAceWinner(Player player)
        {
            Console.WriteLine($"{player.Name} won with two aces!");
        }
        public static void ShowWinner(Player player, ref WinStatistic wins)
        {
            if (player.Name == "Player")
                wins.PlayerWins++;
            else
                wins.ComputerWins++;
            Console.WriteLine($"{player.Name} won!");
        }
    }
}
