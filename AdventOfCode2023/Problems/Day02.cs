using System.ComponentModel.Composition;

namespace AdventOfCode2023.Problems
{
    [Export(typeof(IProblem))]
    public class Day02 : IProblem
    {
        public void Solve(string[] input)
        {
            var games = new List<Game>();

            //process input
            foreach (var line in input)
            {
                var splitGameAndValues = line.Split(':');
                var splitValues = splitGameAndValues[1].Split(';');
                var gameToAdd = new Game { Id = int.Parse(new string(splitGameAndValues[0].Where(Char.IsDigit).ToArray()))};
                foreach (var value in splitValues)
                {
                    var colorValues = value.Split(',');
                    foreach (Colors color in Enum.GetValues(typeof(Colors)))
                    {
                        foreach (var colorValue in colorValues)
                        {
                            if(colorValue.Contains(color.ToString()!, StringComparison.OrdinalIgnoreCase)
                                && int.TryParse(new string(colorValue.Where(Char.IsDigit).ToArray()), out int currentColorValue) 
                                && currentColorValue > gameToAdd.MaxColors[color])
                                gameToAdd.MaxColors[color] = currentColorValue;
                        }
                    }
                }
                games.Add(gameToAdd);
            }

            Console.WriteLine($"Part 1: {games.Where(g => g.MaxColors[Colors.Red] <= 12 && g.MaxColors[Colors.Green] <= 13 && g.MaxColors[Colors.Blue] <= 14).Sum(g => g.Id)}\n");

            Console.WriteLine($"Part 2: {games.Sum(g => g.MaxColors.Values.Aggregate((x, y) => x * y))}\n");
        }
    }

    public class Game
    {
        public int Id { get; set; }
        public Dictionary<Colors, int> MaxColors { get; set; } = new Dictionary<Colors, int>() { { Colors.Red, 0}, { Colors.Green, 0 }, { Colors.Blue, 0} };
    }

    public enum Colors { Red, Green, Blue }
}
