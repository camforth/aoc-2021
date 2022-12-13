using System.Text;

namespace AOC._2022;

public static class Day9
{
    public static string Part1() => GetSolution(1).ToString();

    public static string Part2() => GetSolution(9).ToString();

    private static int GetSolution(int tailCount)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day9.txt");
        var head = new Knot();
        var tails = Enumerable.Range(0, tailCount).Select(_ => new Knot()).ToList();
        foreach (var line in lines)
        {
            var spl = line.Split(' ');
            foreach (var i in Enumerable.Range(0, int.Parse(spl[1])))
            {
                head.Move(spl[0]);
                var following = head;
                foreach (var t in Enumerable.Range(0, tailCount))
                {
                    if (TailShouldMove(tails[t], following))
                    {
                        tails[t].MoveNearTo(following);
                    }

                    following = tails[t];
                }
                
            }
        }
        var last = tails.Last();

        last.PreviousPoints.Add(new Point(last.Current.X, last.Current.Y));
        
        // AocHelpers.WritePointsToConsole(last.PreviousPoints, 500);
        
        return last.PreviousPoints.Distinct().Count();
    }

    private static bool TailShouldMove(Knot tail, Knot head)
    {
        return Math.Abs(head.Current.X - tail.Current.X) >= 2
               || Math.Abs(head.Current.Y - tail.Current.Y) >= 2;
    }

    private record Knot
    {
        public Point Current = new(0, 0);
        public readonly List<Point> PreviousPoints = new();

        public void Move(string direction)
        {
            PreviousPoints.Add(new Point(Current.X, Current.Y));
            Current = direction switch
            {
                "L" => Current with {X = Current.X - 1},
                "R" => Current with {X = Current.X + 1},
                "U" => Current with {Y = Current.Y + 1},
                "D" => Current with {Y = Current.Y - 1},
                _ => throw new Exception("Wut!")
            };
        }
        
        public void MoveNearTo(Knot knot)
        {
            PreviousPoints.Add(new Point(Current.X, Current.Y));
            var point = knot.Current;

            var xChange = (point.X - Current.X) switch
            {
                2 => 1,
                -2 => -1,
                _ => point.X - Current.X
            };
            
            var yChange = (point.Y - Current.Y) switch
            {
                2 => 1,
                -2 => -1,
                _ => point.Y - Current.Y
            };
            
            Current = new Point(Current.X + xChange, Current.Y + yChange);
        }
    }
}