using System.Diagnostics;
using System.Drawing;
using System.Runtime.ExceptionServices;

namespace AOC;

public static class AocHelpers
{
    public static int[] ReadInputsAsInt(string filename)
    {
        var allLines = ReadInputsAsString(filename);
        return allLines.Select(line => int.Parse(line)).ToArray();
    }

    public static string[] ReadInputsAsString(string filename, string year = "2022")
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), year, filename);
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

    public static Func<string> GetDayRunFunc(string? day, string year)
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

        var type = Type.GetType($"AOC._{year}.Day{day[..^1]}");

        if (type is null) return () => "Invalid day";
        var partStr = part == "a" ? "1" : "2";
        var methodInfo = type.GetMethod($"Part{partStr}");
        return () =>
        {
            try
            {
                return methodInfo?.Invoke(null, null)?.ToString() ?? "There was an error";
            }
            catch (Exception ex)
            {
                // unwrap exception and keep stack trace intact
                ExceptionDispatchInfo.Capture(ex.InnerException ?? ex).Throw();
                return "There was an error";
            }
        };
    }

    public static void WriteGridToConsole<T>(T[,] grid, Func<T, string>? customOutput = null)
    {
        foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
        {
            string line = "";
            foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
            {
                line += customOutput is null ? grid[x, y] : customOutput(grid[x,y]);
            }
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }

    public static void OutputGridToBitmap<T>(T[,] grid, string filename, Func<int, int, T, Color> colorFunc)
    {
        if (!OperatingSystem.IsWindows())
        {
            return;
        }
        var bitmap = new Bitmap(grid.GetLength(0), grid.GetLength(1));
        foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
        {
            foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
            {
                bitmap.SetPixel(x, y, colorFunc(x, y, grid[x,y]));
            }
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
        bitmap.Save(path);
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

    public static string[,] CreateStringGrid(string[] lines)
    {
        var grid = new string[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                grid[i, j] = lines[i][j].ToString();
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