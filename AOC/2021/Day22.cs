namespace AOC._2021;

public static class Day22
{
    public static string Part1()
    {
        return GetSolution(new Point3D(-50,-50,-50), new Point3D(50,50,50)).ToString();
    }

    public static string Part2()
    {
        return GetSolution(new Point3D(-200000, -200000, -200000), new Point3D(200000, 200000, 200000)).ToString();
    }

    private static long GetSolution(Point3D min, Point3D max)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day22.txt");

        var grid = new Grid3D(min, max);
        foreach (var line in lines)
            ChangeLights(line, grid);

        var cubes = new List<Cuboid>();

        return grid.LightsOnCount();
    }

    private static void ChangeLights(string line, Grid3D grid)
    {
        var split = line.Split(' ');
        var on = split[0] == "on";
        var ranges = split[1].Split(",");
        var xRange = GetRange(ranges[0][2..]);
        var yRange = GetRange(ranges[1][2..]);
        var zRange = GetRange(ranges[2][2..]);
        if (!InBounds(xRange, yRange, zRange, grid))
        {
            Console.WriteLine($"Skipping line: {line}");
            return;
        }
        foreach (var x in xRange)
            foreach (var y in yRange)
                foreach (var z in zRange)
                    grid.SetLight(new(x,y,z), on);
    }

    private static IEnumerable<int> GetRange(string input)
    {
        var spl = input.Split("..").Select(x => int.Parse(x)).ToArray();
        return Enumerable.Range(spl[0], spl[1] - spl[0] + 1);
    }

    private static bool InBounds(IEnumerable<int> x, IEnumerable<int> y, IEnumerable<int> z, Grid3D grid)
    {
        if (x.First() < grid.Min.X
            || x.Last() > grid.Max.X
            || y.First() < grid.Min.Y
            || y.Last() > grid.Max.Y
            || z.First() < grid.Min.Z
            || z.Last() > grid.Max.Z)
        {
            return false;
        }
        return true;
    }

    private class Grid3D
    {
        private readonly HashSet<Point3D> _onPoints;
        public readonly Point3D Min;
        public readonly Point3D Max;
        public Grid3D(Point3D min, Point3D max)
        {
            _onPoints = new HashSet<Point3D>();
            Min = min;
            Max = max;
        }

        public void SetLight(Point3D point, bool on)
        {
            if (on)
            {
                _onPoints.Add(point);
            }
            else
            {
                _onPoints.Remove(point);
            }
        }
        public long LightsOnCount() => _onPoints.Count;
    }

    private static void ChangeLightsv2(string line, List<Cuboid> cubes)
    {
        var split = line.Split(' ');
        var on = split[0] == "on";
        var ranges = split[1].Split(",");
        var (x0, x1) = GetMinMax(ranges[0][2..]);
        var (y0, y1) = GetMinMax(ranges[1][2..]);
        var (z0, z1) = GetMinMax(ranges[2][2..]);
        var cube = new Cuboid(new(x0, y0, y0), new(x1, y1, z1));
    }

    private static bool Intersects(Cuboid c0, Cuboid c1)
    {
        var (min0, max0) = c0;
        var (min1, max1) = c1;
        return true;
    }

    private static (int min, int max) GetMinMax(string input)
    {
        var spl = input.Split("..").Select(x => int.Parse(x)).ToArray();
        return (spl[0], spl[1]);
    }

    private record struct MinMax(int Min, int Max);
    private record struct Cuboid(Point3D Min, Point3D Max);
}
