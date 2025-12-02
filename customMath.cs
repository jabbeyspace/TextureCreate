using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureCreate
{
    class customMath
    {
        public static Random randomizer = new Random();

        public static int randomInteger(int minimumValue, int maximumValue)
        {
            // Return a random integer in the range [minimumValue, maximumValue]
            return minimumValue + (int) Math.Floor((maximumValue - minimumValue + 1) * randomizer.NextDouble());
        }

        public static long randomLong(long minimumValue, long maximumValue)
        {
            // Return a random long in the range [minimumValue, maximumValue]
            return minimumValue + (long) Math.Floor((maximumValue - minimumValue + 1) * randomizer.NextDouble());
        }

        public static float randomFloat(float minimumValue, float maximumValue)
        {
            // Return a random float in the range [minimumValue, maximumValue)
            return minimumValue + (maximumValue - minimumValue) * (float)randomizer.NextDouble();
        }

        public double randomDouble(double minimumValue, double maximumValue)
        {
            // Return a random double in the range [minimumValue, maximumValue) 
            return minimumValue + (maximumValue - minimumValue) * randomizer.NextDouble();
        }
    }
}
