namespace AdventOfCode2023
{
    public interface IProblem
    {
        string Name { get => GetType().Name; }
        public void Solve();
    }
}
