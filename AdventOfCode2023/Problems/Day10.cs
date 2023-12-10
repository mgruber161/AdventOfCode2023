namespace AdventOfCode2023.Problems
{
    internal class Day10 : IProblem
    {
        public void Solve(string[] input)
        {
            var rowIndex = Array.FindIndex(input, l => l.Contains('S'));
            var colIndex = input[rowIndex].IndexOf('S');
            var (prevRow, prevCol) = (rowIndex, colIndex);
            var steps = 1L;

            if (input[rowIndex - 1][colIndex] is 'F' or '7' or '|') rowIndex--;
            else if (input[rowIndex][colIndex + 1] is '-' or 'J' or '7') colIndex++;
            else if (input[rowIndex + 1][colIndex] is 'L' or 'J' or '|') rowIndex++;
            else if (input[rowIndex][colIndex - 1] is '-' or 'L' or 'F') colIndex--;

            var points = new List<Point> { new Point(prevRow, prevCol), new Point(rowIndex, colIndex) };

            while (input[rowIndex][colIndex] != 'S')
            {
                switch (input[rowIndex][colIndex])
                {
                    case 'F':
                        if(prevRow > rowIndex) (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex + 1);
                        else (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex + 1);
                        break;
                    case '7':
                        if(prevRow > rowIndex) (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex - 1);
                        else (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex + 1);
                        break;
                    case 'L':
                        if (prevRow < rowIndex) (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex + 1);
                        else (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex - 1);
                        break;
                    case 'J':
                        if (prevRow < rowIndex) (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex - 1);
                        else (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex - 1);
                        break;
                    case '-':
                        if (prevCol < colIndex) (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex + 1);
                        else (prevCol, prevRow, colIndex) = (colIndex, rowIndex, colIndex - 1);
                        break;
                    case '|':
                        if (prevRow < rowIndex) (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex + 1);
                        else (prevCol, prevRow, rowIndex) = (colIndex, rowIndex, rowIndex - 1);
                        break;
                    default: throw new ArgumentException($"Unexpected character '{input[rowIndex][colIndex]}'");
                }
                points.Add(new Point(rowIndex, colIndex));
                steps++;
            }

            Console.WriteLine($"Part 1: {steps/2}");

            //Picks theorem: A = i + b/2 - 1
            Console.WriteLine($"Part 2: {PolygonArea(points) + 1 - (points.Count/2)}");
        }

        public static double PolygonArea(IEnumerable<Point> polygon)
        {
            var e = polygon.GetEnumerator();
            if (!e.MoveNext()) return 0;
            Point first = e.Current, last = first;

            double area = 0;
            while (e.MoveNext())
            {
                Point next = e.Current;
                area += next.X * last.Y - last.X * next.Y;
                last = next;
            }
            area += first.X * last.Y - last.X * first.Y;
            return area / 2;
        }
    }

    public record Point(int X, int Y);
}
