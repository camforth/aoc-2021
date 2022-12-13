using System.Text;

namespace AOC._2022;

public static class Day8
{
    public static string Part1() => GetSolution().visibleTrees.ToString();

    public static string Part2() => GetSolution().topScenicScore.ToString();

    private static (int visibleTrees, int topScenicScore) GetSolution()
    {
        var grid = AocHelpers.CreateGrid(AocHelpers.ReadInputsAsString("input-day8.txt"));
        var visible = new List<int>();
        var scenicScores = new List<int>();
        var maxY = grid.GetLength(0) - 1;
        var maxX = grid.GetLength(1) - 1;
        foreach (var x in ..maxY)
            foreach (var y in ..maxX)
            {
                var val = grid[x, y];
                if (x == 0 || x == maxX || y == 0 || y == maxY)
                {
                    visible.Add(val);
                    continue;
                }
                
                var left = (..(y - 1)).Select(s => grid[x, s]).ToList();
                var right = ((y + 1)..maxY).Select(s => grid[x, s]).ToList();
                var up = (..(x - 1)).Select(s => grid[s, y]).ToList();
                var down = ((x + 1)..maxX).Select(s => grid[s, y]).ToList();

                if (left.All(a => a < val) || right.All(a => a < val)
                    || up.All(a => a < val) || down.All(a => a < val))
                {
                    visible.Add(val);
                }

                left.Reverse();
                up.Reverse();
                var score = left.CountWhile(c => c < val)
                            * right.CountWhile(c => c < val)
                            * up.CountWhile(c => c < val)
                            * down.CountWhile(c => c < val);
                scenicScores.Add(score);
            }

        return (visible.Count, scenicScores.Max());
    }
}