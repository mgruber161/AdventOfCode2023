namespace AdventOfCode2023
{
    internal static class Program
    {
        private static readonly List<IProblem> Problems = new();

        public static void Main(string[] args)
        {
            LoadProblems();
            while (true)
            {
                Console.WriteLine("Select a problem! (By index or by name, x to Exit)\n");
                Console.WriteLine("\tIndex\t-\tName");
                Problems.Select(p => (p.Name, p.Index)).ToList().ForEach(n => Console.WriteLine($"\t{n.Index}\t-\t{n.Name}"));

                var selectedProblem = (Console.ReadLine() ?? string.Empty).Trim();
                if (selectedProblem.ToLower() == "x") break;

                Problems.Where(p => p.Name.Equals(selectedProblem, StringComparison.OrdinalIgnoreCase) || 
                    (int.TryParse(selectedProblem, out int problemIndex) && p.Index == problemIndex))
                    .ToList().ForEach(p => { Console.WriteLine($"\nSolution of Problem '{p.Name}':"); p.Solve(p.Input); });

                Console.WriteLine();
            }
        }

        private static void LoadProblems()
        {
            var baseType = typeof(IProblem);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && !p.IsAbstract);
            foreach (var type in types) Problems.Add((IProblem)Activator.CreateInstance(type)!);
        }
    }
}
