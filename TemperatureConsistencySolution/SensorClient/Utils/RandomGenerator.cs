using System;

namespace SensorClient.Utils
{
    public static class RandomGenerator
    {
        private static readonly Random _random = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
