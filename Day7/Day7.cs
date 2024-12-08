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

            if (CanMatchTargetValue(numbers, targetValue))
            {
                totalResult += targetValue;
            }
        }

        return totalResult;
    }

    static bool CanMatchTargetValue(BigInteger[] numbers, BigInteger targetValue)
    {
        int operatorCount = numbers.Length - 1;
        int combinations = 1 << operatorCount;

        for (int i = 0; i < combinations; i++)
        {
            BigInteger result = numbers[0];
            int config = i;

            for (int j = 1; j < numbers.Length; j++)
            {
                int op = config & 1;
                config >>= 1;

                result = op == 0 ? result + numbers[j] : result * numbers[j];
            }

            if (result == targetValue)
            {
                return true;
            }
        }

        return false;
    }

}