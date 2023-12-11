

namespace AdventOfCode2023.Problems
{
    internal class Day11 : IProblem
    {
        public void Solve(string[] input)
        {
            Console.WriteLine($"Part 1: {CalculateGalaxyDistanceSum(input, 1)}");
            Console.WriteLine($"Part 2: {CalculateGalaxyDistanceSum(input, 999_999)}");
        }

        private long CalculateGalaxyDistanceSum(string[] input, int emptyRows)
        {
            var universe = input.Select(l => l.ToList()).ToList();
            var galaxies = new List<Galaxy>();

            var (emptyX, emptyY) = (new List<int>(), new List<int>());

            //find empty universe colums & rows
            for(int i = 0; i < universe[0].Count; i++)
                if(universe.Select(l => l[i]).ToList().TrueForAll(l => l == '.'))
                    emptyY.Add(i);
            for(int i = 0; i < universe.Count; i++)
                if(universe[i].ToList().TrueForAll(l => l == '.'))
                    emptyX.Add(i);
                    
            //find galaxies
            for(int i = 0; i < universe.Count; i++)
                for(int j = 0; j < universe[i].Count; j++)
                    if(universe[i][j] == '#') galaxies.Add(new Galaxy(i, j));

            var sum = 0L;
            for(int i = 0; i < galaxies.Count; i++)
                foreach(var galaxyToPair in galaxies.Skip(i+1))
                    sum += CalculateDistance(galaxies[i], galaxyToPair, emptyX, emptyY, emptyRows); 

            return sum;
        }

        private int CalculateDistance(Galaxy galaxy, Galaxy galaxyToPair, List<int> emptyX, List<int> emptyY, int replace)
        {
            var (X, Y) = (0, 0);
            if(galaxy.X > galaxyToPair.X)
                X = galaxy.X - galaxyToPair.X + (replace * emptyX.Where(val => val >= galaxyToPair.X && val <= galaxy.X).Count());
            else X = galaxyToPair.X - galaxy.X + (replace * emptyX.Where(val => val <= galaxyToPair.X && val >= galaxy.X).Count());
            if(galaxy.Y > galaxyToPair.Y)
                Y = galaxy.Y - galaxyToPair.Y + (replace * emptyY.Where(val => val >= galaxyToPair.Y && val <= galaxy.Y).Count());
            else Y = galaxyToPair.Y - galaxy.Y + (replace * emptyY.Where(val => val <= galaxyToPair.Y && val >= galaxy.Y).Count());
            return X + Y;
        }
    }

    public record struct Galaxy (int X, int Y);
}