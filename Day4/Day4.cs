namespace aoc_2024.Day4;

internal class Day4
{

    public static void Problem1()
    {
        string[] inputReports = File.ReadAllLines(".\\Day4\\Input.txt");

        string word = "XMAS";
        int count = CountWordOccurrences(inputReports, word);

        Console.WriteLine("Day 4 Part 1: " + count);
    }

    static int CountWordOccurrences(string[] grid, string word)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int wordLength = word.Length;
        int count = 0;

        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
        int[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                for (int dir = 0; dir < 8; dir++)
                {
                    int nx = x, ny = y;
                    int matched = 0;

                    for (int i = 0; i < wordLength; i++)
                    {
                        // Boundary check
                        if (nx < 0 || nx >= rows || ny < 0 || ny >= cols)
                            break;

                        if (grid[nx][ny] != word[i])
                            break;

                        nx += dx[dir];
                        ny += dy[dir];
                        matched++;
                    }

                    if (matched == wordLength)
                        count++;
                }
            }
        }

        return count;
    }
}

