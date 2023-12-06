using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Problems
{
    internal class Day06 : IProblem
    {
        public void Solve(string[] input)
        {
            var times = new Regex("\\d+").Matches(input[0]).Cast<Match>().Select(m => m.Value.ToInt64()).ToArray();
            var distances = new Regex("\\d+").Matches(input[1]).Cast<Match>().Select(m => m.Value.ToInt64()).ToArray();
            var result = 1;

            //Part 1
            foreach(var (time, distance) in times.Zip(distances, (x,y) => (x,y)))
                result *= GetPossibilities(time, distance);
                
            System.Console.WriteLine($"Part 1: {result}");

            //Part 2 
            var totalTime = new Regex("\\d+").Matches(input[0].Replace(" ", string.Empty)).Cast<Match>().Select(m => m.Value.ToInt64()).ToArray().First();
            var totalDistance = new Regex("\\d+").Matches(input[1].Replace(" ", string.Empty)).Cast<Match>().Select(m => m.Value.ToInt64()).ToArray().First();

            System.Console.WriteLine($"Part 2: {GetPossibilities(totalTime, totalDistance)}");
        }

        public int GetPossibilities(long time, long distance)
        {
            var possibilities = 0;
            for(int i = 1; i < time; i++)
                if((time - i) * i > distance)
                    possibilities++;
            return possibilities;
        }
    }
}