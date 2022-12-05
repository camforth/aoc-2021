namespace AOC._2022;

public static class Day4
{
    public static string Part1()
    {
        var total = GetSolution(
            (elf1, elf2) => (elf2.start >= elf1.start && elf2.end <= elf1.end) ||
                             (elf1.start >= elf2.start && elf1.end <= elf2.end));

        return total.ToString();
    }

    public static string Part2()
    {
        var total = GetSolution(
            (elf1, elf2) => (elf1.start <= elf2.end && elf2.start <= elf1.end) ||
                            (elf2.start <= elf1.end && elf1.start <= elf2.end));

        return total.ToString();
    }

    private static int GetSolution(Func<(int start, int end), (int start, int end), bool> func)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day4.txt");
        var total = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(",");
            var elf1Range = GetElfRange(pairs[0]);
            var elf2Range = GetElfRange(pairs[1]);
            if (func(elf1Range, elf2Range))
            {
                total++;
            }
        }

        return total;
    }

    private static (int start, int end) GetElfRange(string elf)
    {
        var split = elf.Split("-");
        return (int.Parse(split[0]), int.Parse(split[1]));
    }
}