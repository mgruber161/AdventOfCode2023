using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace AdventOfCode2023.Problems
{
    internal class Day08 : IProblem
    {
        public void Solve(string[] input)
        {
            var directionsDictionary = new Dictionary<string, Direction>();
            var leftRightInstructions = input[0];

            //parse
            for (int i = 2; i < input.Length; i++)
            {
                var letters = new Regex("[A-Z]+").Matches(input[i]).Cast<Match>().Select(l => l.Value).ToArray();
                directionsDictionary.Add(letters[0], new Direction(letters[1], letters[2]));
            }

            //Part 1
            var steps = 0L;
            var currentLocation = "AAA";
            while (currentLocation != "ZZZ")
            {
                foreach (var instruction in leftRightInstructions)
                {
                    currentLocation = instruction == 'L' ? directionsDictionary[currentLocation].Left : directionsDictionary[currentLocation].Right;
                    steps++;
                    if (currentLocation == "ZZZ") break;
                }
            }

            Console.WriteLine($"Part 1: {steps}");

            //Part 2
            var currentLocations = directionsDictionary.Keys.Where(x => x.EndsWith('A')).ToList();
            var zDict = new Dictionary<int, List<long>> { { 0, new()}, { 1, new() }, { 2, new() }, { 3, new() }, { 4, new() }, { 5, new() } };

            for (int i = 0; i < currentLocations.Count; i++)
            {
                steps = 0L;
                var zCount = 0;
                while (zCount < 2)
                {
                    foreach (var instruction in leftRightInstructions)
                    {
                        currentLocations[i] = instruction == 'L' ? directionsDictionary[currentLocations[i]].Left : directionsDictionary[currentLocations[i]].Right;
                        steps++;
                        if (currentLocations[i].EndsWith('Z'))
                        {
                            zDict[i].Add(steps);
                            zCount++;
                        }
                    }
                }
            }

            Console.WriteLine($"Part 2: {zDict.Values.Select(x => x[1] - x[0]).ToArray().LCM()}");
        }

        public record Direction(string Left, string Right);
    }
}
