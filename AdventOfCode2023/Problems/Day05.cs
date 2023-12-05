namespace AdventOfCode2023.Problems
{
    internal class Day05 : IProblem
    {
        public void Solve(string[] input)
        {
            /*input = new string[] {"seeds: 79 14 55 13",
"",
"seed-to-soil map:",
"50 98 2",
"52 50 48",
"",
"soil-to-fertilizer map:",
"0 15 37",
"37 52 2",
"39 0 15",
"",
"fertilizer-to-water map:",
"49 53 8",
"0 11 42",
"42 0 7",
"57 7 4",
"",
"water-to-light map:",
"88 18 7",
"18 25 70",
"",
"light-to-temperature map:",
"45 77 23",
"81 45 19",
"68 64 13",
"",
"temperature-to-humidity map:",
"0 69 1",
"1 0 69",
"",
"humidity-to-location map:",
"60 56 37",
"56 93 4",
""};*/
            //Part 1
            var seeds = input[0].Replace("seeds: ", string.Empty).Split(' ').Select(n => long.Parse(new string(n.Where(Char.IsDigit).ToArray()))).ToList();
            var value = CalculateLocation(seeds, input);
            System.Console.WriteLine($"Part 1: {value}");

            //Part 2
            var seeds2 = new List<long>();
            for(int i = 0; i < seeds.Count - 1; i+=2)
                for(int j = 0; j < seeds[i+1]; j++)
                    seeds2.Add(seeds[i] + j);

            var value2 = CalculateLocation(seeds2, input);
            System.Console.WriteLine($"Part 2: {value2}");
        }

        private long CalculateLocation(List<long> seeds, string[] input)
        {
            var result = long.MaxValue;
            seeds.Distinct().ToList().ForEach(s => {
                var first = true;
                var value = 0L;
                for(int i = 1; i < input.Length; i++)
                {
                    if(string.IsNullOrWhiteSpace(input[i]) || input[i].Where(Char.IsDigit).Any())
                        continue;
                    else
                    {
                        while(!string.IsNullOrWhiteSpace(input[++i]))
                        {
                            var numbers = input[i].Split(' ').Select(n => long.Parse(new string(n.Where(Char.IsDigit).ToArray()))).ToList();

                            if(first)
                            {
                                if(s >= numbers[1] && s < numbers[1] + numbers[2])
                                {
                                    value = numbers[0] + (s - numbers[1]);
                                    break;
                                }
                            }
                            else
                            {
                                if(value >= numbers[1] && value < numbers[1] + numbers[2])
                                {
                                    value = numbers[0] + (value - numbers[1]);
                                    break;
                                }
                            }
                        }
                        if(first && value == 0) value = s;
                        first = false;
                    }
                }
                if(value < result) result = value;
            });
            return result;
        }
    }
}