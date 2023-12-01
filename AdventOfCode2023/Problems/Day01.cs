using AdventOfCode2023;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

[Export(typeof(IProblem))]
public class Day01 : IProblem
{
    public void Solve(string[] input)
    {
        var result = new List<int>();
        foreach (var line in input){
            var calibrationValueToAdd = string.Empty;
            for (int i = 0; i < line.Length; i++) {
                if(Char.IsNumber(line[i]))
                    calibrationValueToAdd += line[i].ToString();
                else
                    foreach(var digit in Enum.GetValues(typeof(Digits))){
                        if(digit.ToString().Length + i <= line.Length){
                            if(line.Substring(i, digit.ToString().Length).Contains(digit.ToString())){
                                calibrationValueToAdd += (int)digit;
                                continue;
                            }    
                        }
                    }

            }
            if(calibrationValueToAdd.Length > 0)
                result.Add(Convert.ToInt32(calibrationValueToAdd[0].ToString() + calibrationValueToAdd[calibrationValueToAdd.Length-1].ToString()));
        }
        System.Console.WriteLine(result.Sum());
    }
}

public enum Digits{ Undefined, one, two, three, four, five, six, seven, eight, nine }