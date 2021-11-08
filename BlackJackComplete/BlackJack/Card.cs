using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Card
    {
        
        public string Name { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }

       
        public Card()
        {

        }

        public Card(string name, string suit, int value) // Card Constructor
        {
            this.Name = name;
            this.Suit = suit;
            this.Value = value;

        }

        public override string ToString() // To string to print card info// 
        {
            return string.Format($"{Name} of {Suit} ,value: {Value}");
        }
    }
}
