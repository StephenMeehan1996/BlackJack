using System;
using static System.Console;
using System.Collections.Generic;

namespace BlackJack
{
    class Program
    {
      
        static void Main(string[] args)
        {
            PopulateList();
        }

        
        private static void PopulateList() 
        {
            Casino.PopulateDeck(); // creates deck
            WriteLine("-----------------------------------");
            WriteLine("Welcome to the BlackJack Table");
            WriteLine("-----------------------------------");
            WriteLine("       --------------");
            WriteLine("           -------\n\n");
            Write("How much money do you wish to play with:  ");
            Casino.money = int.Parse(ReadLine()); // stores money
            WriteLine("-----------------------------------");
            WriteLine("       --------------");
            
            GameLoop(); // Calls gameLoop //

        } // end PopList

        private static void GameLoop()
        {
            
           string input = "";
           while (input != "E")
              {

                List<Card> playingDeck = new List<Card>(Casino.deck); // copys deck from Casino class to be used every round
                playingDeck.Shuffle(); // shuffles deck

                WriteLine("\n\n  --------------");
                        Write("Enter P to play, E to exit: ");
                        input = ReadLine();
                        input = input.ToUpper();
                WriteLine(" \n  --------------");

                if (input != "E")
                 {
                        Write("How much would you like to bet: ");
                        Casino.bet = int.Parse(ReadLine());

                    while (Casino.bet > Casino.money) // only allows you continue if you have enough money for bet
                    {
                        if (Casino.bet > Casino.money)
                        {
                            WriteLine("\n--------------");
                            Write("Insufficent funds, Place Smaller Bet: ");
                            Casino.bet = int.Parse(ReadLine());
                            WriteLine(" --------------\n");

                        }

                    }
                    GameStart(playingDeck);
                 }
            }
        } // End GameLoop

        private static void GameStart(List<Card> deck)
        {
            string input = "";
            int aceCountP = 0, aceCountD = 0;
            List<Card> playerCards = new List<Card>(); // lists to hold player/dealer cards
            List<Card> dealerCards = new List<Card>();

            WriteLine("\n\n----------");
            WriteLine("Players Cards");
            WriteLine("----------");
            WriteLine("   -----");
            for (int i = 0; i < 2; i++)  // Deals player 2 cards, adds them too playerCard List
            {
                playerCards.Add(deck[0]);     //deals top card
                deck.RemoveAt(0);            // removes dealt card from deck

                if (playerCards[i].Name == "Ace") // Checks for ace//
                 {
                    aceCountP++; // counts aces
                    playerCards[i].Value = 11; // sets value of ace too 11, if two aces are dealt, if statement below will handle it
                }

                WriteLine(playerCards[i]); // Prints too string
            }
                     
                if (aceCountP == 2) // Incase of two aces
                {
                    playerCards[0].Value = 11;
                    playerCards[1].Value = 1;
                    WriteLine("**Ace Value is 11**");
                    WriteLine("**Ace Value is 1**");
                }

            WriteLine("Total value: {0}", GetTotal(playerCards));
            WriteLine("----------\n");
            WriteLine("Dealers first Card");
            WriteLine("----------");
            WriteLine("  --------");
            for (int i = 0; i < 2; i++)
            {
                
                dealerCards.Add(deck[0]);   
                deck.RemoveAt(0);           

                if (dealerCards[i].Name == "Ace") //same as above
                { 
                    aceCountD++;
                    dealerCards[i].Value = 11;

                }
            }
            if (aceCountD == 2) // Incase of two aces
            {
                dealerCards[0].Value = 11;
                dealerCards[1].Value = 1;
                WriteLine("**Ace Value is 11**");
                WriteLine("**Ace Value is 1**");
            }
            WriteLine(dealerCards[0]); // as per rules , player only sees first dealer card
            WriteLine("----------\n");

            if (GetTotal(playerCards) == 21) // check if you get 21 straight away
            {
                GetResults(Casino.Results.Win);
                GameLoop(); 
            }

            while (input != "S")
            {
                Write("Stick or twist S/T: ");
                input = ReadLine();
                input = input.ToUpper();

                if (input != "S")
                {
                    
                    playerCards.Add(deck[0]);
                    deck.RemoveAt(0); 
                   
                   
                    if (playerCards[playerCards.Count - 1].Name == "Ace")
                       {
                        getAceValue(playerCards); // calls to check ace value in method

                       }
                            WriteLine(playerCards[playerCards.Count - 1]); // prints new card
                            WriteLine("Total value: {0}", GetTotal(playerCards)); // gets total

                        if (GetTotal(playerCards) == 21)  // check for win
                        {

                                GetResults(Casino.Results.Win);
                                GameLoop(); // this may not work Test!
                        }

                            else if (GetTotal(playerCards) > 21) // checks for lose
                            {         
                                GetResults(Casino.Results.Bust);
                                GameLoop();// check this
                        
                            }
                  
                                     WriteLine("----------\n");
                }
            
            }

            if (input == "S") // triggers dealer play
            {
                DealerPlays(dealerCards, playerCards, deck);
            }

        } // End Game Start

        private static void DealerPlays(List<Card> dealerCards, List<Card> playerCards, List<Card> deck)
        {
            
            
            WriteLine("\n\n-----------------------------");
            WriteLine("      Dealers Cards");
            WriteLine("-----------------------------");
            WriteLine(dealerCards[0]); // prints dealers first to cards + total value
            WriteLine(dealerCards[1]);
            WriteLine("Total: {0}",GetTotal(dealerCards));
            WriteLine("-----------------------------\n");

            //As per rules the Dealer must draw if the total value of their cards is under 17, dealer must continue to draw till greater than 17//

            while (GetTotal(dealerCards) < 17)
            {
                if (GetTotal(dealerCards) < 17)
                {
                    dealerCards.Add(deck[0]);
                    deck.RemoveAt(0); //removes card from playing deck//

                    WriteLine("------------------");
                    WriteLine("   Dealer Draws");
                    WriteLine("------------------");

                    if (dealerCards[dealerCards.Count -1].Name == "Ace")// checks for Ace
                    {
                        getAceValue(dealerCards); 
                    }

                    WriteLine(dealerCards[dealerCards.Count - 1]);
                    WriteLine("Total value: {0}", GetTotal(dealerCards));
                }

                
            }
            CheckWinner(playerCards, dealerCards);
           
        } // end dealerPlays

        private static void CheckWinner(List<Card> playerCards, List<Card> dealerCards) 
        { // Checks winner if game runs to end and none of the win conditions are met above

            int playerTotal = GetTotal(playerCards);
            int dealerTotal = GetTotal(dealerCards);
            
                if (playerTotal > dealerTotal)
                {
                    GetResults(Casino.Results.Win);
                }
                else if (dealerTotal > playerTotal && dealerTotal <= 21)
                {
                    GetResults(Casino.Results.Lose);

                }
                else if (dealerTotal > 21)
                {
                    WriteLine("Dealer Bust !");
                    GetResults(Casino.Results.Win);
                }

                else
                {
                    GetResults(Casino.Results.Draw);
                }

            GameLoop();
        }
        private static void getAceValue( List<Card> cards)
        {
            /* If aces are dealt at the start 1 ace will be 11 and two aces will have a total of 12
             * This method is only called after the first 4 cards are dealt at the start and the game does not end, It will 
             * Check the total after setting the new ace to 0 and see what its value should be*/

                cards[cards.Count -1].Value = 0; // set card value to 0 so it can be tested 

                if (GetTotal(cards) + 11 <= 21)
                {
                    cards[cards.Count - 1].Value = 11;
                    WriteLine("**Ace Value is 11**");
                }
                    else
                    {
                        cards[cards.Count - 1].Value = 1;
                        WriteLine("**Ace Value is 1**");
                    }
        }
        // Small methods for GamePlay// 
        private static int GetTotal(List<Card> Cards) // returns total of passed cards
        {
            int total = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                total += Cards[i].Value;
            }
            return total;
        }


        private static void GetResults(Enum Results) // controls game messages
        {
            switch (Results)
            {
                case Casino.Results.Win:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Casino.money += Casino.bet;
                    WriteLine("\n-------------------------------");
                    WriteLine("-------------------------------");
                    WriteLine("        You Win, Congrats!");
                    WriteLine("        Money: " + Casino.money);
                    WriteLine("-------------------------------");
                    WriteLine("-------------------------------\n");
                    Console.ResetColor();
                    break;
                case Casino.Results.Lose:
                    Casino.money -= Casino.bet;
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLine("\n-------------------------------");
                    WriteLine("-------------------------------");
                    WriteLine("           Dealer Wins!");
                    WriteLine("           Money: " + Casino.money);
                    WriteLine("-------------------------------");
                    WriteLine("-------------------------------\n");
                    Console.ResetColor();
                    break;
                case Casino.Results.Draw:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteLine("\n-------------------------------");
                    WriteLine("-------------------------------");
                    WriteLine("         Draw Game");
                    WriteLine("         Money: " + Casino.money);
                    WriteLine("-------------------------------");
                    WriteLine("-------------------------------\n");
                    Console.ResetColor();
                    break;
                case Casino.Results.Bust:
                    Casino.money -= Casino.bet;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n-------------------------------");
                    WriteLine("-------------------------------");
                    WriteLine("    You are Bust, Dealer Wins!");
                    WriteLine("    Money: " + Casino.money);
                    WriteLine("-------------------------------");
                    WriteLine("-------------------------------\n");
                    Console.ResetColor();
                    break;
               

                default:
                    break;
            }

            if (Casino.money == 0)
            {
                EndGame();
            }
        } // end GetResults// 

        private static void EndGame() // ends game if money is at 0 
        {
            string input;

            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine(" \n-------------------------------");
            WriteLine("  -------------------------------");
            WriteLine("You are Broke, The House Always Wins!");
            WriteLine(" -------------------------------");
            WriteLine(" -------------------------------\n");
            Console.ResetColor();

            Write("Would you like to play again? Y/N: ");
            input = ReadLine();
            input = input.ToUpper();

            if (input == "Y")
            {
                PopulateList();
            }
            else
            {
                Environment.Exit(0); // closes program // 
            }
        }
    }
}
