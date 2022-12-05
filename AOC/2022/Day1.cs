namespace AOC._2022;

public static class Day1
{
    public static string Part1()
    {
        var elves = GetCounts();

        var maxValue = elves.Max();

        return maxValue.ToString();
    }

    public static string Part2()
    {
        var elves = GetCounts();
        
        var sum = elves.OrderByDescending(o => o).Take(3).Sum();

        return sum.ToString();
    }

    private static int[] GetCounts()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day1.txt");

        var elves = new int[lines.Count(string.IsNullOrEmpty) + 1];
        var currentIndex = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                currentIndex++;
                continue;
            }

            elves[currentIndex] += int.Parse(line);
        }

        return elves;
    }
}