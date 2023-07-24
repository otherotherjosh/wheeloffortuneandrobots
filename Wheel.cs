using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wheeloffortuneandrobots
{
    public static class Wheel
    {
        public static int Spin()
        {
            Random rand = new Random();
            int[] segments = new int[] { 175, 250, 300, 500,
                                         750, 1000, 2000, 5000};
            int segmentID = rand.Next(segments.Length);
            return segments[segmentID];
        }
    }
}
