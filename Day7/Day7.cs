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

        string[] inputLines = File.ReadAllLines(filePath);

        BigInteger totalCalibrationResult = 0;

        foreach (var line in inputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(": ");
            BigInteger targetValue = BigInteger.Parse(parts[0]);
            var numbers = parts[1].Split(' ').Select(BigInteger.Parse).ToArray();
                      
            if (CanMatchTargetValue(numbers, targetValue))
            {
                totalCalibrationResult += targetValue;
            }
        }

        Console.WriteLine($"Total Calibration Result: {totalCalibrationResult}");
    }

    static bool CanMatchTargetValue(BigInteger[] numbers, BigInteger targetValue)
    {        
        int operatorCount = numbers.Length - 1;
        int combinations = 1 << (2 * operatorCount);

        for (int i = 0; i < combinations; i++)
        {
            BigInteger result = numbers[0];
            int index = i;

            for (int j = 1; j < numbers.Length; j++)
            {
                int op = index % 2;
                index /= 2;

                if (op == 0)
                    result += numbers[j];
                else
                    result *= numbers[j];
            }

            if (result == targetValue)
            {
                return true;
            }
        }

        return false;
    }

}