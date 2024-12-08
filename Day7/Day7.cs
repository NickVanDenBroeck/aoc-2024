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

        Stopwatch stopwatch1 = Stopwatch.StartNew();
        BigInteger part1Result = SolvePart1(filePath);
        stopwatch1.Stop();
        Console.WriteLine($"Day 7 Part 1: Total Calibration Result: {part1Result} => Execution Time: {stopwatch1.ElapsedMilliseconds} ms");


        Stopwatch stopwatch2 = Stopwatch.StartNew();
        BigInteger part2Result = SolvePart2(filePath);
        stopwatch2.Stop();
        Console.WriteLine($"Day 7 Part 1: Total Calibration Result: {part2Result} => Execution Time: {stopwatch2.ElapsedMilliseconds} ms");
    }

    static BigInteger SolvePart1(string filePath)
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

            if (CanMatchTargetValuePart1(numbers, targetValue, 0, numbers[0]))
            {
                totalResult += targetValue;
            }
        }

        return totalResult;
    }

    static BigInteger SolvePart2(string filePath)
    {
        BigInteger totalResult = 0;

        foreach (string line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(": ");
            BigInteger targetValue = BigInteger.Parse(parts[0]);
            BigInteger[] numbers = parts[1].Split(' ').Select(BigInteger.Parse).ToArray();

            if (CanMatchTargetValuePart2(numbers, targetValue, 0, numbers[0]))
            {
                totalResult += targetValue;
            }
        }

        return totalResult;
    }

    static bool CanMatchTargetValuePart1(BigInteger[] numbers, BigInteger targetValue, int index, BigInteger current)
    {
        if (index == numbers.Length - 1)
        {
            return current == targetValue;
        }

        if (CanMatchTargetValuePart1(numbers, targetValue, index + 1, current + numbers[index + 1]))
        {
            return true;
        }

        if (CanMatchTargetValuePart1(numbers, targetValue, index + 1, current * numbers[index + 1]))
        {
            return true;
        }

        return false;
    }

    static bool CanMatchTargetValuePart2(BigInteger[] numbers, BigInteger targetValue, int index, BigInteger current)
    {
        if (index == numbers.Length - 1)
        {
            return current == targetValue;
        }

        BigInteger nextNumber = numbers[index + 1];

        if (CanMatchTargetValuePart2(numbers, targetValue, index + 1, current + nextNumber))
        {
            return true;
        }

        if (CanMatchTargetValuePart2(numbers, targetValue, index + 1, current * nextNumber))
        {
            return true;
        }

        BigInteger concatenated = Concatenate(current, nextNumber);
        if (CanMatchTargetValuePart2(numbers, targetValue, index + 1, concatenated))
        {
            return true;
        }

        return false;
    }

    static BigInteger Concatenate(BigInteger left, BigInteger right)
    {
        int digitLength = right.ToString().Length;

        BigInteger powerOfTen = BigInteger.Pow(10, digitLength);
        return left * powerOfTen + right;
    }
}