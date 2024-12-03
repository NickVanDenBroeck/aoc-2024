using System.Text.RegularExpressions;

namespace aoc_2024.Day3;

internal class Day3
{
    public static void Problem1()
    {
        string input = File.ReadAllText(".\\Day3\\Input.txt");
        int totalSum = SumofMultiplications(input);

        Console.WriteLine($"Day 3 Part 1: {totalSum}");
    }

    public static void Problem2()
    {
        string input = File.ReadAllText(".\\Day3\\Input.txt");
        int totalSum = CalculateEnabledMultiplicationSum(input);

        Console.WriteLine($"Day 3 Part 2: {totalSum}");
    }


    public static int SumofMultiplications(string input)
    {
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        MatchCollection matches = Regex.Matches(input, pattern);

        int totalSum = 0;

        foreach (Match match in matches)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            totalSum += x * y;
        }

        return totalSum;
    }


    public static int CalculateEnabledMultiplicationSum(string input)
    {
        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        string doPattern = @"do\(\)";
        string dontPattern = @"don't\(\)";

        string combinedPattern = $"{mulPattern}|{doPattern}|{dontPattern}";

        MatchCollection matches = Regex.Matches(input, combinedPattern);

        bool isEnabled = true;
        int totalSum = 0;

        // Process each match
        foreach (Match match in matches)
        {
            if (match.Value.StartsWith("do()"))
            {
                isEnabled = true;
            }
            else if (match.Value.StartsWith("don't()"))
            {
                isEnabled = false;
            }
            else if (match.Value.StartsWith("mul"))
            {
                if (isEnabled)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    totalSum += x * y;
                }
            }
        }

        return totalSum;
    }
}

