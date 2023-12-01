using AdventOfCode2023.Extensions;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace AdventOfCode2023
{
    internal class Program
    {
        [ImportMany]
        public IEnumerable<Lazy<IProblem>> Problems { get; set; } = new List<Lazy<IProblem>>();

        public Program()
        {
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            compositionContainer.SatisfyImportsOnce(this);
        }

        public static void Main(string[] args)
        {
            var program = new Program();
            while (true)
            {
                Console.WriteLine("Select a problem! (By index or by name, x to Exit)\n");
                Console.WriteLine("\tIndex\t-\tName");
                program.Problems.Select(p => (p.Value.Name, p.Value.Index)).ToList().ForEach(n => Console.WriteLine($"\t{n.Index}\t-\t{n.Name}"));

                var selectedProblem = (Console.ReadLine() ?? string.Empty).Trim();
                if (selectedProblem.ToLower() == "x") break;

                program.Problems.Where(p => p.Value.Name.Equals(selectedProblem, StringComparison.OrdinalIgnoreCase) || 
                    (int.TryParse(selectedProblem, out int problemIndex) && p.Value.Index == problemIndex))
                    .ToList().ForEach(p => { Console.WriteLine($"Solution of Problem '{p.Value.Name}':"); p.Value.Solve(p.Value.Input); });

                Console.WriteLine();
            }
        }
    }
}
