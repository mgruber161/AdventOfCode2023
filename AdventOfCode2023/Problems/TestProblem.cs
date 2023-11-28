using System.ComponentModel.Composition;

namespace AdventOfCode2023.Problems
{
    [Export(typeof(IProblem))]
    public class TestProblem : IProblem
    {
        public string Name => "TestProblem";

        public void Solve()
        {
            Console.WriteLine($"{1} + {1} = {1 + 1}");
        }
    }
}
