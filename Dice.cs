using System;
using System.Collections.Generic;
using System.Text;

namespace Knucklebones
{
    public class Dice
    {
        int _number;

        public int Number { get => _number; }

        // konstruktor
        public Dice()
        {
            Random rnd = new Random();
            _number = Convert.ToInt32(rnd.Next(1, 7));
        }
        public Dice(int nr)
        {
            _number = nr;
        }
    }
}
