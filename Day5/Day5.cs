namespace aoc_2024.Day5;

internal class Day5
{
    public static void Problem1()
    {
        string[] input = File.ReadAllLines(".\\Day5\\input.txt");

        var rules = new List<(int, int)>();
        var updates = new List<List<int>>();
        bool isRuleSection = true;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRuleSection = false;
                continue;
            }

            if (isRuleSection)
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                rules.Add((parts[0], parts[1]));
            }
            else
            {
                var update = line.Split(',').Select(int.Parse).ToList();
                updates.Add(update);
            }
        }

        var ruleLookup = rules.ToLookup(r => r.Item1, r => r.Item2);
        var validMiddlePages = new List<int>();

        foreach (var update in updates)
        {
            if (IsValidUpdate(update, ruleLookup))
            {
                validMiddlePages.Add(update[update.Count / 2]);
            }
        }

        int result = validMiddlePages.Sum();

        Console.WriteLine("Day 5 Part 1: The sum of the middle page numbers is " + result);
    }

    static bool IsValidUpdate(List<int> update, ILookup<int, int> ruleLookup)
    {
        var pagePositions = update
            .Select((page, index) => new { Page = page, Position = index })
            .ToDictionary(x => x.Page, x => x.Position);

        foreach (var rule in ruleLookup)
        {
            int x = rule.Key;
            foreach (int y in rule)
            {
                if (pagePositions.ContainsKey(x) && pagePositions.ContainsKey(y))
                {
                    if (pagePositions[x] >= pagePositions[y])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }



    public static void Problem2()
    {
        string[] input = File.ReadAllLines(".\\Day5\\input.txt");

        var rules = new List<(int, int)>();
        var updates = new List<List<int>>();
        bool isRuleSection = true;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRuleSection = false;
                continue;
            }

            if (isRuleSection)
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                rules.Add((parts[0], parts[1]));
            }
            else
            {
                var update = line.Split(',').Select(int.Parse).ToList();
                updates.Add(update);
            }
        }

        var ruleLookup = rules.ToLookup(r => r.Item1, r => r.Item2);
        var reorderedMiddlePages = new List<int>();

        foreach (var update in updates)
        {
            if (!IsValidUpdate(update, ruleLookup))
            {
                var reordered = ReorderUpdate(update, ruleLookup);
                reorderedMiddlePages.Add(reordered[reordered.Count / 2]);
            }
        }

        int result = reorderedMiddlePages.Sum();
        Console.WriteLine($"Day 5 Part 2: Sum of reordered middle pages is {result}");
    }

    static List<int> ReorderUpdate(List<int> update, ILookup<int, int> ruleLookup)
    {
        var graph = new Dictionary<int, List<int>>();
        var inDegree = new Dictionary<int, int>();

        foreach (int page in update)
        {
            graph[page] = new List<int>();
            inDegree[page] = 0;
        }

        foreach (var rule in ruleLookup)
        {
            int x = rule.Key;
            foreach (int y in rule)
            {
                if (update.Contains(x) && update.Contains(y))
                {
                    graph[x].Add(y);
                    inDegree[y]++;
                }
            }
        }

        var sorted = new List<int>();
        var queue = new Queue<int>(inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            sorted.Add(current);

            foreach (var neighbor in graph[current])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sorted;
    }
}

