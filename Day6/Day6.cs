using System.Net.NetworkInformation;

namespace aoc_2024.Day6;

internal class Day6
{
    public static void Part1()
    {
        // Read the input from a file
        string[] lines = File.ReadAllLines(".\\Day6\\input.txt");
        int rows = lines.Length;
        int cols = lines[0].Length;

        char[,] map = new char[rows, cols];
        int guardX = 0, guardY = 0;
        string direction = "up";

        // Populate the map and locate the guard's initial position
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = lines[i][j];
                if ("^v<>".Contains(lines[i][j]))
                {
                    guardX = i;
                    guardY = j;
                    direction = lines[i][j] switch
                    {
                        '^' => "up",
                        'v' => "down",
                        '<' => "left",
                        '>' => "right",
                        _ => direction
                    };
                    map[i, j] = '.'; // Clear the starting position
                }
            }
        }

        // Step 2: Simulate the patrol
        HashSet<(int, int)> visited = new HashSet<(int, int)> { (guardX, guardY) };
        HashSet<(int x, int y, string dir)> states = new HashSet<(int, int, string)>(); // Detect cycles

        int[,] deltas = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } }; // Up, Down, Left, Right
        string[] directions = { "up", "down", "left", "right" };

        int steps = 0;
        while (true)
        {
            steps++;

            // Track state to detect cycles
            var currentState = (guardX, guardY, direction);
            if (states.Contains(currentState))
            {
                Console.WriteLine($"Cycle detected after {steps} steps. Exiting.");
                break;
            }
            states.Add(currentState);

            int dx = 0, dy = 0;
            switch (direction)
            {
                case "up": (dx, dy) = (-1, 0); break;
                case "down": (dx, dy) = (1, 0); break;
                case "left": (dx, dy) = (0, -1); break;
                case "right": (dx, dy) = (0, 1); break;
            }

            // Check the cell in front of the guard
            int nextX = guardX + dx, nextY = guardY + dy;
            if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols || map[nextX, nextY] == '#')
            {
                // Turn right
                direction = directions[(Array.IndexOf(directions, direction) + 1) % 4];
            }
            else
            {
                // Move forward
                guardX = nextX;
                guardY = nextY;
                visited.Add((guardX, guardY));
            }

            // Stop if the guard leaves the map
            if (guardX < 0 || guardX >= rows || guardY < 0 || guardY >= cols)
            {
                Console.WriteLine($"Guard exited the map after {steps} steps.");
                break;
            }

            // Log progress every 100 steps
            if (steps % 100 == 0)
            {
                Console.WriteLine($"Progress: Steps = {steps}, Visited Positions = {visited.Count}");
            }

            // Safety mechanism: stop if too many steps (infinite loop prevention)
            if (steps > 100000)
            {
                Console.WriteLine("Exceeded 100,000 steps. Exiting for safety.");
                break;
            }
        }

        // Output the number of distinct positions visited
        Console.WriteLine($"Day 6 Part 1: Distinct positions visited is {visited.Count}");
    }
}