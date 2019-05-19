using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class DeckActions
    {
        public static Card[] CreateDeck()
        {
           int index = 0;
           Card[] deck = new Card[36];
           for(var i = 0; i < 4; i++)
           {
                for(var j = 0; j < 9; j++)
                {
                    var cardFace = GetCardFace(j);
                    Card card = new Card { Face = cardFace, Suit = (Suit)i, Value = GetCardValue(cardFace) };
                    deck[index] = card;
                    index++;
                }
           }
            return deck;
        }
        public static Card[] ShuffleDeck(Card[] deck)
        {
            Random rng = new Random();
            int n = deck.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = deck[k];
                deck[k] = deck[n];
                deck[n] = card;
            }
            return deck;
        }
        public static Card GetCard(Card[] deck) 
        {
            return deck[deck.Length];
        }
        public static int GetCardValue(Face face)
        {
            int value = 0;
            switch(face)
            {
                case Face.Ace:
                    value = 11;
                    break;
                case Face.King:
                    value = 4;
                    break;
                case Face.Lady:
                    value = 3;
                    break;
                case Face.Jack:
                    value = 2;
                    break;
                default:
                    value = (int)face;
                    break;

            }
            return value;
        }
        public static Face GetCardFace(int index)
        {
            var face = new Face();
            switch(index)
            {
                case 0:
                    face = Face.Six;
                    break;
                case 1:
                    face = Face.Seven;
                    break;
                case 2:
                    face = Face.Eight;
                    break;
                case 3:
                    face = Face.Nine;
                    break;
                case 4:
                    face = Face.Ten;
                    break;
                case 5:
                    face = Face.Jack;
                    break;
                case 6:
                    face = Face.Lady;
                    break;
                case 7:
                    face = Face.King;
                    break;
                case 8:
                    face = Face.Ace;
                    break;
            }
            return face;
        }
        public static void ShowPlayersHand(Card[] cards)
       
        {
            Console.Write("Your cards are: ");
            int value = 0;
            foreach (var card in cards)
            {
                Console.Write($"{card.Face} of {card.Suit} ");
                value += card.Value;

            }
            Console.WriteLine();
            Console.WriteLine("The value of your hand is: " + value);
        }
        public static int CalculateHandValue(Card[] cards)
        {
            int value = 0;
            foreach (var card in cards)
                value += card.Value;
            return value;
            
        }
      
    }
}
