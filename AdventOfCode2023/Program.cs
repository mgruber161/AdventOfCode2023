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
            var p = new Program();
            while (true)
            {
                Console.WriteLine("Select a problem! (x to Exit)\n");
                var problemNames = p.Problems.Select(p => p.Value.Name).ToList();
                problemNames.ForEach(n => Console.WriteLine($"\t- {n}"));
                var selectedProblem = (Console.ReadLine() ?? string.Empty).Trim();

                if (selectedProblem.ToLower() == "x") break;

                p.Problems.Where(p => p.Value.Name.Contains(selectedProblem, StringComparison.OrdinalIgnoreCase)).ToList()
                    .ForEach(p => { Console.WriteLine($"Solution of Problem '{selectedProblem}':"); p.Value.Solve(); });

                Console.WriteLine();
            }
        }
    }
}
