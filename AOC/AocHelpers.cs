using System.Diagnostics;

namespace AOC
{
    public static class AocHelpers
    {
        public static int[] ReadInputsAsInt(string filename)
        {
            var allLines = ReadInputsAsString(filename);
            return allLines.Select(line => int.Parse(line)).ToArray();
        }

        public static string[] ReadInputsAsString(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            return File.ReadAllLines(path);
        }

        public static string RunDay(string? day, Func<string> action)
        {
            Console.WriteLine($"Running day {day} solution");
            var sw = Stopwatch.StartNew();
            sw.Start();
            var result = action();
            sw.Stop();
            Console.WriteLine($"Soluton took {sw.ElapsedMilliseconds}ms to run");
            return result;
        }

        public static Func<string> GetDayRunFunc(string? day)
        {
            if (day is null)
            {
                return () => "Invalid day";
            }
            var part = day[^1].ToString();

            if (!new[] { "a", "b" }.Contains(part))
            {
                return () => "Invalid part";
            }

            var type = Type.GetType($"AOC.Day{day[..^1]}");

            if (type is null) return () => "Invalid day";
            var partStr = part == "a" ? "1" : "2";
            var methodInfo = type.GetMethod($"Part{partStr}");
            return () => methodInfo?.Invoke(null, null)?.ToString() ?? "There was an error";
        }

        public static void WriteGridToConsole(int[,] grid)
        {
            foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
            {
                string line = "";
                foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
                {
                    line += grid[x, y];
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public static int[,] CreateGrid(string[] lines)
        {
            var grid = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            return grid;
        }

        public static bool IsOutOfBounds<T>(T[,] grid, Point p)
            => IsOutOfBounds(grid, p.X, p.Y);

        public static bool IsOutOfBounds<T>(T[,] grid, int x, int y)
        {
            try
            {
                _ = grid[x, y];
                return false;
            }
            catch
            {
                return true;
            }
        }
    }

    public record struct Point(int X, int Y);
    public record struct Line(Point A, Point B);
}
