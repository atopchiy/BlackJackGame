using System;

namespace BlackJack
{
    public enum Suit
    {
        Hearts,
        Clubs,
        Spades,
        Diamonds
    }
    public enum Face
    {
       
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Lady,
        King,
        Ace
    }
    public struct Card
    {
        public Face Face;
        public Suit Suit;
        public int Value;
    }
    public struct Player
    {
        public string Name;
        public Card[] Hand;
    }
    public struct WinStatistic
    {
        public int PlayerWins;
        public int ComputerWins;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Black Jack Name!!");
            var playerComputer = new Player { Name = "Computer" };
            var playerHuman = new Player { Name = "Player" };
            var wins = new WinStatistic { PlayerWins = 0, ComputerWins = 0 };
            StartGame(playerHuman, playerComputer, ref wins);
            bool playAgain = true;
            while(playAgain)
            {
                Console.WriteLine("Do you want to start a new game? Yes/No");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "Yes":
                        StartGame(playerHuman, playerComputer, ref wins);
                        break;
                    case "No":
                        Console.WriteLine("Thank's for the game!");
                        Console.WriteLine($"Your won {wins.PlayerWins} times!");
                        Console.WriteLine($"Computer won {wins.ComputerWins} times!");
                        playAgain = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }

            }
            Console.ReadKey();
        }
        static void StartGame(Player playerHuman, Player playerComputer, ref WinStatistic wins)
        {
           
            Card[] cards = DeckActions.CreateDeck();
            cards = DeckActions.ShuffleDeck(cards);
            var order = 0;
            int cardsLength = cards.Length - 1;
            while(order == 0)
            {
                Console.WriteLine("Please enter 1 if you want to start the game ot enter 2 to let computer start: ");
                bool success = int.TryParse(Console.ReadLine(), out order);
                if (order != 1 && order != 2)
                    success = false;
                if (!success)
                    Console.WriteLine("You've entered invalid number");
            }
            if (order == 1)
            {
                var nextPlayerPlay = PlayUser(ref playerHuman, cards, ref cardsLength, order, ref wins);
                if (nextPlayerPlay)
                {
                    var calculatePoints = PlayComputer(ref playerComputer, cards, ref cardsLength, ref wins);
                    if (calculatePoints)
                        ShowResults.ShowWinner(PlayerActions.CalculatePoints(playerHuman, playerComputer), ref wins);

                }
            }
            if(order == 2 )
            {
                var nextPlayerPlay = PlayComputer(ref playerComputer, cards, ref cardsLength, ref wins);
                if (nextPlayerPlay)
                {
                    var calculatePoints = PlayUser(ref playerHuman, cards, ref cardsLength, order, ref wins);
                    if (calculatePoints)
                        ShowResults.ShowWinner(PlayerActions.CalculatePoints(playerHuman, playerComputer), ref wins);
                }

            }

        }
        
       
        static bool PlayUser(ref Player playerHuman, Card[] cards, ref int cardIndex, int order, ref WinStatistic wins)
        {
            playerHuman.Hand = new Card[2];
            int aceWinCount = 0;
            for (int i = 0, j = cardIndex; i < 2; i++, cardIndex--)
            {
                playerHuman.Hand[i] = cards[cardIndex];
                if (cards[j].Face == Face.Ace)
                    aceWinCount++;

            }
            if(aceWinCount == 2)
            {
                ShowResults.ShowAceWinner(playerHuman);
                wins.PlayerWins++;
                return false;
            }
            DeckActions.ShowPlayersHand(playerHuman.Hand);
            bool needMoreCards = true;
            while(needMoreCards)
            {
                Console.WriteLine("Do you want to take a card? Yes/No");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "Yes":
                        var newHand = new Card[playerHuman.Hand.Length + 1];
                        Array.Copy(playerHuman.Hand, newHand, playerHuman.Hand.Length);
                        newHand[playerHuman.Hand.Length] = cards[cardIndex];
                        playerHuman.Hand = newHand;
                        cardIndex--;
                        DeckActions.ShowPlayersHand(playerHuman.Hand);
                        break;
                    case "No":
                        needMoreCards = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }
                var value = DeckActions.CalculateHandValue(playerHuman.Hand);
                if(value == 21)
                {
                    DeckActions.ShowPlayersHand(playerHuman.Hand);
                    ShowResults.ShowBlackJackWinner(playerHuman);
                    wins.PlayerWins++;
                    needMoreCards = false;
                    return false;
                }
                if(value > 21)
                {
                    DeckActions.ShowPlayersHand(playerHuman.Hand);
                    Console.WriteLine("You can't take anymore cards cause you have more than 21 points...");
                    if(order != 2)
                    Console.WriteLine("Waiting for computer's turn...");
                    needMoreCards = false;
                    return true;
                }
            }
            DeckActions.ShowPlayersHand(playerHuman.Hand);
            return true;
        }
        static bool PlayComputer(ref Player playerComputer, Card[] cards, ref int cardIndex, ref WinStatistic wins)
        {
            playerComputer.Hand = new Card[2];
            int aceWinCount = 0;
            for (int i = 0, j = cardIndex; i < 2; i++, cardIndex--)
            {
                playerComputer.Hand[i] = cards[cardIndex];
                if (cards[j].Face == Face.Ace)
                    aceWinCount++;

            }
            if (aceWinCount == 2)
            {
                ShowResults.ShowAceWinner(playerComputer);
                wins.ComputerWins++;
                return false;
            }
            bool needMoreCards = true;
            while (needMoreCards)
            {
                var value = DeckActions.CalculateHandValue(playerComputer.Hand);
                var choice = value < 17 ? "Yes" : "No";
                switch (choice)
                {
                    case "Yes":
                        var newHand = new Card[playerComputer.Hand.Length + 1];
                        Array.Copy(playerComputer.Hand, newHand, playerComputer.Hand.Length);
                        newHand[playerComputer.Hand.Length] = cards[cardIndex];
                        playerComputer.Hand = newHand;
                        cardIndex--;
                        break;
                    case "No":
                        needMoreCards = false;
                        break;
                   
                }
                value = DeckActions.CalculateHandValue(playerComputer.Hand);
                if (value == 21)
                {
                    ShowResults.ShowBlackJackWinner(playerComputer);
                    needMoreCards = false;
                    wins.ComputerWins++;
                    return false;
                }
                if (value > 21)
                {
                    needMoreCards = false;
                    return true;
                }
              
            }
            return true;
        }
    }
}
