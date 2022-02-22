namespace Extentions
{
    public static class MathExtentions
    {
        public static int MutlipleTimes(this int Multiplier, int Times)
        {
            int result = 1;
            for (int i = 0; i < Times; i++)
                result *= Multiplier;
            return result;
        }
        public static float MutlipleTimes(this float Multiplier, int Times)
        {
            float result = 1;
            for (int i = 0; i < Times; i++)
                result *= Multiplier;
            return result;
        }
        public static string MinifyLong(this long value)
        {
            if (value >= 100000000000)
                return (value / 1000000000).ToString("#,0") + " B";
            if (value >= 10000000000)
                return (value / 1000000000D).ToString("0.#") + " B";
            if (value >= 100000000)
                return (value / 1000000).ToString("#,0") + " M";
            if (value >= 10000000)
                return (value / 1000000D).ToString("0.#") + " M";
            if (value >= 100000)
                return (value / 1000).ToString("#,0") + " K";
            if (value >= 10000)
                return (value / 1000D).ToString("0.#") + " K";
            return value.ToString("#,0");
        }
        public static string MinifyLong(this int value)
        {
            if (value >= 100000000)
                return (value / 1000000).ToString("#,0") + " M";
            if (value >= 10000000)
                return (value / 1000000D).ToString("0.#") + " M";
            if (value >= 100000)
                return (value / 1000).ToString("#,0") + " K";
            if (value >= 10000)
                return (value / 1000D).ToString("0.#") + " K";
            return value.ToString("#,0");
        }
    }
}


