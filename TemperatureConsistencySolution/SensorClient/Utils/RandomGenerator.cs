using System;

namespace SensorClient.Utils
{
    // Pomoćna klasa za generisanje random vrednosti (temperatura, intervali...)
    public static class RandomGenerator
    {
        private static readonly Random _random = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
