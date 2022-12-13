using System.Text;

namespace AOC._2022;

public static class Day10
{
    public static string Part1() => GetSolution().ToString();

    public static string Part2() => GetSolution().ToString();

    private static int GetSolution()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day10.txt");

        var cycle = 0;
        var register = 1;
        var signalStrengths = new List<int>();
        var crt = "";

        foreach (var line in lines)
        {
            var spl = line.Split(' ');
            if (spl[0] == "noop")
            {
                RunCycle(signalStrengths, ref crt, register, ref cycle);
            }
            else if (spl[0] == "addx")
            {
                foreach (var _ in ..1)
                {
                    RunCycle(signalStrengths, ref crt, register, ref cycle);
                }

                register += int.Parse(spl[1]);
            }
        }
        
        foreach(var r in ..6)
            Console.WriteLine(string.Join("", crt.Skip(r * 40).Take(40)));

        return signalStrengths.Sum();
    }

    private static void RunCycle(List<int> signalStrengths, ref string crt, int register, ref int cycle)
    {
        cycle++;
        if (Part1Cycles.Contains(cycle))
        {
            signalStrengths.Add(cycle * register);
        }

        var adjustedCycle = cycle % 40 - 1;
        if (adjustedCycle == register - 1
            || adjustedCycle == register
            || adjustedCycle == register + 1)
        {
            crt += "#";
        }
        else
        {
            crt += ".";
        }
    }

    private static readonly int[] Part1Cycles = {
        20, 60, 100, 140, 180, 220
    };
}