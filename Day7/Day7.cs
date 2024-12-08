using System.Diagnostics;
using System.Numerics;

namespace aoc_2024.Day7;

internal class Day7
{
    public static void Part1()
    {
        string filePath = ".\\Day7\\input.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Input file not found!");
            return;
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        BigInteger totalCalibrationResult = CalculateTotalCalibrationResult(filePath);

        stopwatch.Stop();

        Console.WriteLine($"Day 7 Part 1: Total Calibration Result: {totalCalibrationResult} => Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }

    static BigInteger CalculateTotalCalibrationResult(string filePath)
    {
        BigInteger totalResult = 0;

        foreach (string line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(": ");
            BigInteger targetValue = BigInteger.Parse(parts[0]);
            BigInteger[] numbers = parts[1].Split(' ').Select(BigInteger.Parse).ToArray();

            if (CanMatchTargetValue(numbers, targetValue, 0, numbers[0]))
            {
                totalResult += targetValue;
            }
        }

        return totalResult;
    }
    static bool CanMatchTargetValue(BigInteger[] numbers, BigInteger targetValue, int index, BigInteger current)
    {
        // Base case: if all numbers are used, check if the result matches the target
        if (index == numbers.Length - 1)
        {
            return current == targetValue;
        }

        // Try adding the next number
        if (CanMatchTargetValue(numbers, targetValue, index + 1, current + numbers[index + 1]))
        {
            return true;
        }

        // Try multiplying the next number
        if (CanMatchTargetValue(numbers, targetValue, index + 1, current * numbers[index + 1]))
        {
            return true;
        }

        // If neither operation works, return false
        return false;
    }
}