namespace AdventOfCode2023
{
    public static class Extensions
    {
        public static long ToInt64(this string input)
        {
            var num = 0L;
            for (var i = 0; i < input.Length; i++)
            {
                num = num * 10 + (input[i] - '0');
            }
            return num;
        }

        public static long[] ToInt64(this string[] values) => [.. values.Select(value => value.ToInt64())];

        public static long LCM(this long[] numbers) => numbers.Aggregate((S, val) => S * val / GCD(S, val));

        static long GCD(long n1, long n2)
        {
            if (n2 == 0) return n1;
            else return GCD(n2, n1 % n2);
        }
    }
}