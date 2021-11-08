using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Casino
    {  /*This is a Game Controller class used to hold money, bet and the created deck, so
        its only created once at the start, then is copied every round*/

        // extra feature, Money and setting bet // 

        public static int money = 0;
        public static int bet = 0;
        public static  List<Card> deck = new List<Card>();

        /* Below is a Method to create object list from data in Arrays. 
           BlackJack is usually played with multiple decks of cards at once, 
           This could be easily motified to do that*/
        public static void PopulateDeck()
        {
            string[] names = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            string[] suits = { "Clubs", "Spades", "Diamonds", "Hearts" };
            int[] values = { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < names.Length; j++)
                {
                    deck.Add(new Card($"{names[j]}", $"{suits[i]}", values[j]));
                }
            }
        }

      
        // This enum is used to control win,lose,draw messages in the main program
        public enum Results
        {
            Win,
            Lose,
            Draw,
            Bust
        }

       

    }
}
