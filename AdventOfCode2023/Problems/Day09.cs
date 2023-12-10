namespace AdventOfCode2023.Problems
{
    internal class Day09 : IProblem
    {
        public void Solve(string[] input)
        {
            Console.WriteLine($"Part 1: {input.Sum(x => PredictNextValue(x.Split(' ').Select(long.Parse).ToList()))}");
            Console.WriteLine($"Part 2: {input.Sum(x => PredictNextValue(x.Split(' ').Select(long.Parse).Reverse().ToList()))}");
        }

        private long PredictNextValue(List<long> values)
        {
            if (values.TrueForAll(x => x == 0L)) return 0L;
            return PredictNextValue(values.Zip(values.Skip(1), (a, b) => b - a).ToList()) + values[values.Count-1];
        }
    }
}
