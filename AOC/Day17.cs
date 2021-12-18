namespace AOC;

public static class Day17
{
    public static string Part1()
    {
        return GetSolution().ToString();
    }

    public static string Part2()
    {
        return GetSolution().ToString();
    }

    private static int GetSolution()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day17.txt");

        var targetArea = GetTargetAreaPoints(lines[0]);
        var validTrajectories = new Dictionary<Point, Point[]>();

        // brute force it
        foreach (var x in Enumerable.Range(0, 500))
        {
            foreach (var y in Enumerable.Range(0, 500))
            {
                var t0 = GetPointsFromVelocity(x, y);
                var t1 = GetPointsFromVelocity(-x, -y);
                var t2 = GetPointsFromVelocity(x, -y);
                var t3 = GetPointsFromVelocity(-x, y);
                CheckPointsAgainstTarget(x, y, t0, targetArea, validTrajectories);
                CheckPointsAgainstTarget(-x, -y, t1, targetArea, validTrajectories);
                CheckPointsAgainstTarget(x, -y, t2, targetArea, validTrajectories);
                CheckPointsAgainstTarget(-x, y, t3, targetArea, validTrajectories);
            }
        }

        Console.WriteLine($"Valid trajectories: {validTrajectories.Count}");

        var result = validTrajectories.MaxBy(x => x.Value.First().Y);

        return result.Value.First().Y;
    }

    private static void CheckPointsAgainstTarget(int x0, int y0, List<Point> trajectory, List<Point> target, Dictionary<Point, Point[]> validTrajectories)
    {
        var velocities = new Point(x0, y0);
        if (trajectory.Any(x => target.Contains(x)))
        {
            validTrajectories[velocities] = trajectory.OrderByDescending(o => o.Y).ToArray();
        }
    }

    private static List<Point> GetPointsFromVelocity(int x0, int y0)
    {
        var points = new List<Point>();

        var current = new Point(0, 0);
        while(current.X < 2000 && current.Y < 10000 && current.X > -1 && current.Y > -100)
        {
            points.Add(current);
            var nextX = current.X + x0;
            x0 += x0 > 0 ? -1 : (x0 < 0 ? 1 : 0);
            var nextY = current.Y + y0;
            y0 += -1;
            current = new Point(nextX, nextY);
        }

        return points;
    }

    private static List<Point> GetTargetAreaPoints(string target)
    {
        var xRange = target[15..target.IndexOf(",")].Split("..");
        var x1 = int.Parse(xRange[0]);
        var x2 = int.Parse(xRange[1]);
        var yRange = target[(target.IndexOf("y=") + 2)..].Split("..");
        var y1 = int.Parse(yRange[0]);
        var y2 = int.Parse(yRange[1]);
        var points = new List<Point>();

        foreach(var x in Enumerable.Range(x1, Math.Abs(x1 - x2) + 1))
        {
            foreach(var y in Enumerable.Range(y1, Math.Abs(y2 - y1) + 1))
            {
                points.Add(new Point(x, y));
            }
        }
        return points;
    }
}
