namespace aoc_2024.Day2
{
    public class Day2
    {
        public static void Problem1()
        {
            string[] inputReports = File.ReadAllLines(".\\Day2\\Input.txt");

            int safeCountPart1 = inputReports.Count(IsSafe);
            Console.WriteLine($"Day 2 Part 1: {safeCountPart1} safe reports");

        }

        public static void Problem2()
        {
            string[] inputReports = File.ReadAllLines(".\\Day2\\Input.txt");

            int safeCountPart2 = inputReports.Count(report =>
            {
                return IsSafe(report) || CanBeMadeSafeWithDampener(report);
            });

            Console.WriteLine($"Day 2 Part 2: {safeCountPart2} safe reports (with dampener)");
        }

        static bool IsSafe(string report)
        {
            var levels = ParseLevels(report);

            if (!AllDifferencesAreValid(levels))
            {
                return false;
            }

            return IsMonotonic(levels);
        }

        static bool CanBeMadeSafeWithDampener(string report)
        {
            var levels = ParseLevels(report);

            for (int i = 0; i < levels.Count; i++)
            {
                var modifiedLevels = new List<int>(levels);
                modifiedLevels.RemoveAt(i);

                if (AllDifferencesAreValid(modifiedLevels) && IsMonotonic(modifiedLevels))
                {
                    return true;
                }
            }

            return false;
        }

        static List<int> ParseLevels(string report)
        {
            return report.Split(' ').Select(int.Parse).ToList();
        }

        static bool IsMonotonic(List<int> levels)
        {
            bool increasing = true, decreasing = true;

            for (int i = 1; i < levels.Count; i++)
            {
                if (levels[i] > levels[i - 1])
                {
                    decreasing = false;
                }
                else if (levels[i] < levels[i - 1])
                {
                    increasing = false;
                }

                if (!increasing && !decreasing)
                {
                    return false;
                }
            }

            return increasing || decreasing;
        }

        static bool AllDifferencesAreValid(List<int> levels)
        {
            for (int i = 1; i < levels.Count; i++)
            {
                int difference = Math.Abs(levels[i] - levels[i - 1]);
                if (difference < 1 || difference > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
