using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public static class GameMath
    {
        public static Random Rand = new Random();

        public static int RandomRange(int start, int end)
        {
            return Rand.Next(start, ++end);
        }

        public static int Clamp(int value, int min, int max) =>
            value > max ? max : value < min ? min : value;
    }
}
