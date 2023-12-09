using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problems
{
    internal class Day09 : IProblem
    {
        public void Solve(string[] input)
        {
            //Part 1
            var sum = 0L;
            foreach (var line in input)
            {
                var originalLine = new Regex(@"([-]?\d+)").Matches(line).Cast<Match>().Select(m => long.Parse(m.Value)).ToList();
                sum += PredictNextValue(originalLine);
            }
            Console.WriteLine($"Part 1: {sum}");

            //Part 2
            sum = 0L;
            foreach (var line in input)
            {
                var originalLine = new Regex(@"([-]?\d+)").Matches(line).Cast<Match>().Select(m => long.Parse(m.Value)).Reverse().ToList();
                sum += PredictNextValue(originalLine);
            }
            Console.WriteLine($"Part 2: {sum}");
        }

        private long PredictNextValue(List<long> values)
        {
            if (values.TrueForAll(x => x == 0L)) return 0L;
            return PredictNextValue(values.Zip(values.Skip(1), (a, b) => b - a).ToList()) + values[values.Count-1];
        }
    }
}
