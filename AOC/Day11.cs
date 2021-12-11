namespace AOC;

public static class Day11
{
    public static string Part1()
    {
        return GetSolution(100).ToString();
    }

    public static string Part2()
    {
        return GetSolution(null).ToString();
    }

    private static int GetSolution(int? days)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day11.txt");

        var grid = AocHelpers.CreateGrid(lines);

        if (days.HasValue)
        {
            var flashes = 0;
            foreach (var x in Enumerable.Range(0, 100))
            {
                flashes += RunStep(grid);
                //AocHelpers.WriteGridToConsole(grid);
            }
            return flashes;
        }
        else
        {
            var step = 0;
            var flashes = 0;
            while (flashes != 100)
            {
                flashes = RunStep(grid);
                //AocHelpers.WriteGridToConsole(grid);
                step++;
            }
            return step;
        }
    }

    private static int RunStep(int[,] grid)
    {
        var pointsToFlash = new List<Point>();

        // Step all
        foreach(var x in Enumerable.Range(0, 10)) {
            foreach(var y in Enumerable.Range(0, 10))
            {
                grid[x, y]++;
                if (grid[x,y] > 9)
                {
                    pointsToFlash.Add(new(x, y));
                }
            }
        }

        // Step flashed points neighbours
        foreach (var x in Enumerable.Range(0, pointsToFlash.Count))
        {
            StepPointAndNeighbours(grid, pointsToFlash[x], pointsToFlash, true);
        }

        // Reset flashed points
        foreach (var x in Enumerable.Range(0, pointsToFlash.Count))
        {
            var point = pointsToFlash[x];
            grid[point.X, point.Y] = 0;
        }

        return pointsToFlash.Count;
    }

    private static void StepPointAndNeighbours(int[,] grid, Point point, List<Point> flashes, bool alreadyFlashed = false)
    {
        var (x, y) = point;

        if (AocHelpers.IsOutOfBounds(grid, point)
            || !alreadyFlashed && flashes.Contains(point))
        {
            return;
        }

        grid[x, y]++;

        if (grid[x, y] > 9)
        {
            if (!alreadyFlashed)
            {
                flashes.Add(point);
            }
            StepPointAndNeighbours(grid, new(x - 1, y), flashes);
            StepPointAndNeighbours(grid, new(x, y + 1), flashes);
            StepPointAndNeighbours(grid, new(x + 1, y), flashes);
            StepPointAndNeighbours(grid, new(x, y - 1), flashes);

            StepPointAndNeighbours(grid, new(x - 1, y - 1), flashes);
            StepPointAndNeighbours(grid, new(x - 1, y + 1), flashes);
            StepPointAndNeighbours(grid, new(x + 1, y + 1), flashes);
            StepPointAndNeighbours(grid, new(x + 1, y - 1), flashes);
        }
    }
}
