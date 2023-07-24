using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wheeloffortuneandrobots
{
    public static class Wheel
    {
        public static int Spin(Player currentPlayer)
        {
            // would be cool if you could choose how hard to spin the wheel
            Random rand = new Random();
            int[] segments = new int[] { 175, 250, 300, 500,
                                         750, 1000, 2000, 5000};
            int segmentID = rand.Next(segments.Length);
            int segment = segments[segmentID];

            Console.Clear();
            currentPlayer.ShowTurn();

            Decor.Highlight("^g^(temporary)^w^ Wheel spun!" +
                $"\nAmount = ${segment}");
            Decor.StandBy();

            return segment;
        }
    }
}
