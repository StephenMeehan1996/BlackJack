using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public static class ExtentionMethod
    {
        /*This is a extention method, I used the Fisher-Yates shuffle to randomise
         the playing deck after it is copied in the main program*/

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
