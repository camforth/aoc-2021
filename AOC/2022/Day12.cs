using System.Collections.Concurrent;

namespace AOC._2022;

public static class Day12
{
    private static readonly List<string> Letters = new()
    {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
    };
    
    public static string Part1() => GetSolution("p1").ToString();

    public static string Part2() => GetSolution("p2").ToString();

    private static int GetSolution(string part)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day12.txt");
        var grid = AocHelpers.CreateStringGrid(lines);
        var (start, end, aPoints) = GetStartAndEnd(grid);

        var pathDistances = new ConcurrentBag<int>();
        Parallel.ForEach(part == "p1" ? new List<Point> {start} : aPoints.ToList(), s =>
        {
            var previous = GetPathWithPriorityQueue(grid, s);
            var path = new Stack<Point>();
            Point? target = end;
            while (target is not null)
            {
                path.Push(target.Value);
                previous.TryGetValue(target.Value, out target);
            }

            pathDistances.Add(path.Count - 1);
        });

        return pathDistances.Where(x => x != 0).Min();
    }
    
    private static (Point start, Point end, HashSet<Point> aPoints) GetStartAndEnd(string[,] grid)
    {
        var start = new Point(0, 0);
        var end = new Point(0, 0);
        var aPoints = new HashSet<Point>();
        foreach(var x in Enumerable.Range(0, grid.GetLength(0)))
        foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
        {
            if (grid[x, y] == "S")
            {
                grid[x, y] = "a";
                start = new Point(x, y);
                aPoints.Add(start);
            }

            if (grid[x, y] == "E")
            {
                grid[x, y] = "z";
                end = new Point(x, y);
            }

            if (grid[x, y] == "a")
            {
                aPoints.Add(new Point(x, y));
            }
        }

        return (start, end, aPoints);
    }
    
    private static Dictionary<Point, Point?> GetPathWithPriorityQueue(string[,] grid, Point start)
    {
        var previous = new Dictionary<Point, Point?>();
        var distance = new Dictionary<Point, int>();
        var queue = new PriorityQueue<Point, int>();

        distance[start] = 0;
        foreach (var x in Enumerable.Range(0, grid.GetLength(0)))
        {
            foreach (var y in Enumerable.Range(0, grid.GetLength(1)))
            {
                var point = new Point(x, y);
                if (point != start)
                {
                    distance[point] = int.MaxValue;
                    previous[point] = null;
                }
            }
        }
        
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach(var point in GetNeighbours(current, grid))
            {
                var pointValue = Letters.IndexOf(grid[point.X, point.Y]);
                var currentValue = Letters.IndexOf(grid[current.X, current.Y]);
                if (pointValue - currentValue >= 2) continue;
                var currentElevation = distance[current] + 1;
                if (currentElevation < distance[point])
                {
                    distance[point] = currentElevation;
                    previous[point] = current;
                    queue.Enqueue(point, currentElevation);
                }
            }
        }

        return previous;
    }
    
    private static List<Point> GetNeighbours(Point point, string[,] grid)
    {
        var (x, y) = point;
        return new List<Point> { new(x - 1, y), new(x, y + 1), new(x + 1, y), new(x, y - 1) }
            .Where(p => !AocHelpers.IsOutOfBounds(grid, p)).ToList();
    }
}