namespace AOC;

public static class Day15
{
    public static string Part1()
    {
        return GetSolution(false).ToString();
    }

    public static string Part2()
    {
        return GetSolution(true).ToString();
    }

    private static int GetSolution(bool part2)
    {
        var grid = GetGrid(part2);

        //AocHelpers.WriteGridToConsole(grid);

        var (risk, previous) = GetPathWithPriorityQueue(grid, new(0, 0));

        var path = new Stack<Point>();
        Point? target = new Point(grid.GetLength(0) - 1, grid.GetLength(1) - 1);
        while (target is not null)
        {
            path.Push(target.Value);
            previous.TryGetValue(target.Value, out target);
        }

        var result = path.Sum(x => grid[x.X, x.Y]);

        return result;
    }

    // Dijkstra's algorithm
    private static (Dictionary<Point, int> risk, Dictionary<Point, Point?> previous) GetPathWithPriorityQueue(int[,] grid, Point start)
    {
        var previous = new Dictionary<Point, Point?>();
        var risk = new Dictionary<Point, int>();
        var queue = new PriorityQueue<Point, int>();

        risk[start] = 0;

        foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
        {
            foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
            {
                var point = new Point(x, y);
                if (point != start)
                {
                    risk[point] = int.MaxValue;
                    previous[point] = null;
                }
                queue.Enqueue(new(x, y), risk[point]);
            }
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach(var point in GetNeighbours(current, null, grid))
            {
                var pointRisk = risk[current] + grid[point.X, point.Y];
                if (pointRisk < risk[point])
                {
                    risk[point] = pointRisk;
                    previous[point] = current;
                    queue.Enqueue(point, pointRisk);
                }
            }
        }

        return (risk , previous);
    }

    private static int[,] GetGrid(bool part2)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day15.txt");

        var grid = AocHelpers.CreateGrid(lines);
        if (!part2)
        {
            return grid;
        }

        var gridWidth = grid.GetLength(0);
        var gridHeight = grid.GetLength(1);
        var bigGrid = new int[gridWidth * 5, gridHeight * 5];
        foreach(var gridX in Enumerable.Range(0, 5))
        {
            foreach (var gridY in Enumerable.Range(0, 5))
            {
                foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
                {
                    foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
                    {
                        var risk = grid[x, y] + gridX + gridY;
                        if (risk > 9)
                        {
                            risk -= 9;
                        }
                        bigGrid[x + (gridX * gridWidth), y + (gridY * gridHeight)] = risk;
                    }
                }
            }
        }
        

        return bigGrid;
    }

    private static List<Point> GetNeighbours(Point point, Point? previous, int[,] grid)
    {
        var points = new List<Point>();

        var (x, y) = point;

        // left
        var left = new Point(x - 1, y);
        if (!AocHelpers.IsOutOfBounds(grid, left)
            && left != previous)
        {
            points.Add(left);
        }

        // up
        var up = new Point(x, y + 1);
        if (!AocHelpers.IsOutOfBounds(grid, up)
            && up != previous)
        {
            points.Add(up);
        }

        // right
        var right = new Point(x + 1, y);
        if (!AocHelpers.IsOutOfBounds(grid, right)
            && right != previous)
        {
            points.Add(right);
        }

        // down
        var down = new Point(x, y - 1);
        if (!AocHelpers.IsOutOfBounds(grid, down)
            && down != previous)
        {
            points.Add(down);
        }
        return points;
    }
}


