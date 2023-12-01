namespace AdventOfCode2023
{
    public interface IProblem
    {
        int Index => int.Parse(new string(Name.Where(Char.IsDigit).ToArray()));
        string Name => GetType().Name;
        string[] Input => File.ReadAllLines($"Input/{Name}.txt");
        public void Solve(string[] input);
    }
}
