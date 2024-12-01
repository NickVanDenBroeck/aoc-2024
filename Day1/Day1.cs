namespace aoc_2024.Day1
{
    public class Day1
    {
        public static void Problem1()
        {
            var (list1, list2) = ReadAndProcessData(".\\Day1\\Input.txt");
            var (sortedList1, sortedList2) = SortLists(list1, list2);
            int sumOfDistances = CalculateSumOfDistances(sortedList1, sortedList2);

            Console.WriteLine("Sum of Distances: " + sumOfDistances);
        }

        public static (List<int>, List<int>) ReadAndProcessData(string filePath)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            foreach (var line in File.ReadLines(filePath))
            {
                var values = line.Split("   ").Select(int.Parse).ToArray();
                if (values.Length == 2)
                {
                    list1.Add(values[0]);
                    list2.Add(values[1]);
                }
            }

            return (list1, list2);
        }

        public static (List<int>, List<int>) SortLists(List<int> list1, List<int> list2)
        {
            list1.Sort();
            list2.Sort();
            return (list1, list2);
        }

        public static int CalculateSumOfDistances(List<int> list1, List<int> list2)
        {
            int sumOfDistances = 0;

            int count = Math.Min(list1.Count, list2.Count);

            for (int i = 0; i < count; i++)
            {
                sumOfDistances += Math.Abs(list1[i] - list2[i]);
            }

            return sumOfDistances;
        }

        public static void Problem2()
        {
            var (list1, list2) = ReadAndProcessData(".\\Day1\\Input.txt");
            var (sortedList1, sortedList2) = SortLists(list1, list2);
            var occurrences = CountOccurrences(sortedList2);
            int similarityScore = CalculateSimilarityScore(sortedList1, occurrences);

            Console.WriteLine("Similarity Score: " + similarityScore);
        }

        public static Dictionary<int, int> CountOccurrences(List<int> list2)
        {
            Dictionary<int, int> occurrences = new Dictionary<int, int>();

            foreach (var num in list2)
            {
                if (occurrences.ContainsKey(num))
                {
                    occurrences[num]++;
                }
                else
                {
                    occurrences[num] = 1;
                }
            }

            return occurrences;
        }

        public static int CalculateSimilarityScore(List<int> list1, Dictionary<int, int> occurrences)
        {
            int similarityScore = 0;

            foreach (var num in list1)
            {
                if (occurrences.ContainsKey(num))
                {
                    similarityScore += num * occurrences[num];
                }
            }

            return similarityScore;
        }
    }
}
