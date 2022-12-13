using System.Text;

namespace AOC._2022;

public static class Day6
{
    public static string Part1() => GetSolution(4);

    public static string Part2() => GetSolution(14);

    private static string GetSolution(int count)
    {
        var chars = AocHelpers.ReadInputsAsString("input-day6.txt")[0].ToCharArray();
        var marker = 0;
        for (var i = 0; i <= chars.Length - count; i++)
        {
            if (chars[i..(i + count)].ToHashSet().Count == count)
            {
                marker = i + count;
                break;
            }
        }

        return marker.ToString();
    }
}