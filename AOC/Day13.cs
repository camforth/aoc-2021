namespace AOC;

public static class Day13
{
    public static string Part1()
    {
        return GetSolution(1).ToString();
    }

    public static string Part2()
    {
        return GetSolution().ToString();
    }

    private static int GetSolution(int? foldCount = null)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day13.txt");

        var points = new List<MutablePoint>();
        var folds = new List<string>();
        var readingPoints = true;
        foreach(var line in lines)
        {
            if (line == "")
            {
                readingPoints = false;
                continue;
            }
            if (readingPoints)
            {
                var p = line.Split(",").Select(x => int.Parse(x)).ToArray();
                points.Add(new(p[0], p[1]));
            }
            else
            {
                folds.Add(line.Split(" ")[2]);
            }
        }

        foreach(var x in Enumerable.Range(0, foldCount ?? folds.Count))
        {
            Fold(points, folds[x]);
        }

        var grid = new int[points.MaxBy(x => x.X)!.X + 1, points.MaxBy(x => x.Y)!.Y + 1];
        AddPointsToGrid(grid, points);
        WriteGridToConsole(grid);

        var result = points.Distinct().Count();

        return result;
    }

    private static void Fold(List<MutablePoint> points, string instruction)
    {
        var spl = instruction.Split("=").ToArray();
        var axis = spl[0];
        var fold = int.Parse(spl[1]);

        foreach (var point in points)
        {
            if (axis == "x" && point.X > fold)
            {
                point.X = fold - (point.X - fold);
            }
            if (axis == "y" && point.Y > fold)
            {
                point.Y = fold - (point.Y - fold);
            }
        }
    }

    public static void WriteGridToConsole(int[,] grid)
    {
        foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
        {
            string line = "";
            foreach (var y in Enumerable.Range(0, grid.GetLength(1)).Reverse())
            {
                line += grid[x, y] == 0 ? "." : "#";
            }
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }

    private static void AddPointsToGrid(int[,] grid, List<MutablePoint> points)
    {
        foreach (var point in points)
        {
            grid[point.X, point.Y] = 1;
        }
    }

    private record MutablePoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MutablePoint(int x, int y)
        {
            X = x; Y = y;
        }
    }
}


