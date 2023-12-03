using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Problems
{
    internal class Day03 : IProblem
    {
        public void Solve(string[] input)
        {
            var numbers = new List<int>();
            var gearRatioSum = 0;

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (!Char.IsDigit(input[i][j]) && input[i][j] != '.')
                    {
                        var addedNumbers = 0;
                        if (i > 0)
                        {
                            for (var k = j - 1; k <= j + 1; k++)
                            {
                                if (k >= j && Char.IsDigit(input[i - 1][k - 1]))
                                    continue;

                                var number = string.Empty;
                                if (Char.IsDigit(input[i - 1][k]))
                                {
                                    number += input[i - 1][k];
                                    for (int l = k - 1; l >= 0 && Char.IsDigit(input[i - 1][l]); l--)
                                        number = input[i - 1][l] + number;
                                    for (int m = k + 1; m < input[i - 1].Length && Char.IsDigit(input[i - 1][m]); m++)
                                        number += input[i - 1][m];
                                }
                                if (number != string.Empty) { numbers.Add(int.Parse(number)); addedNumbers++; };
                            }
                        }

                        if (j > 0)
                        {
                            var number = string.Empty;
                            for (int n = j - 1; n >= 0 && Char.IsDigit(input[i][n]); n--)
                                number = input[i][n] + number;
                            if (number != string.Empty) { numbers.Add(int.Parse(number)); addedNumbers++; };
                        }

                        if (j < input[i].Length - 1)
                        {
                            var number = string.Empty;
                            for (int o = j + 1; o < input[i].Length && Char.IsDigit(input[i][o]); o++)
                                number += input[i][o];
                            if (number != string.Empty) { numbers.Add(int.Parse(number)); addedNumbers++; };
                        }

                        if (i < input.Length - 1)
                        {
                            for (var p = j - 1; p <= j + 1; p++)
                            {
                                if (p >= j && Char.IsDigit(input[i + 1][p - 1]))
                                    continue;

                                var number = string.Empty;
                                if (Char.IsDigit(input[i + 1][p]))
                                {
                                    number += input[i + 1][p];
                                    for (int l = p - 1; l >= 0 && Char.IsDigit(input[i + 1][l]); l--)
                                        number = input[i + 1][l] + number;
                                    for (int m = p + 1; m < input[i + 1].Length && Char.IsDigit(input[i + 1][m]); m++)
                                        number += input[i + 1][m];
                                }
                                if (number != string.Empty) { numbers.Add(int.Parse(number)); addedNumbers++; };
                            }
                        }

                        if (addedNumbers == 2 && input[i][j] == '*')
                            gearRatioSum += (numbers[numbers.Count-1] * numbers[numbers.Count - 2]);
                    }

                }
            }

            Console.WriteLine($"Part 1: {numbers.Sum()}");
            Console.WriteLine($"Part 2: {gearRatioSum}");
        }
    }
}
