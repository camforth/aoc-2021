namespace AOC._2021;

public static class Day2
{
    public static string Part1()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day2.txt");

        int hPos = 0;
        int depth = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var direction = parts[0];
            var x = int.Parse(parts[1]);
            if (direction == "forward")
            {
                hPos += x;
            }
            else if (direction == "down")
            {
                depth += x;
            }
            else if (direction == "up")
            {
                depth -= x;
            }
        }

        var result = hPos * depth;

        return result.ToString();
    }

    public static string Part2()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day2.txt");

        int aim = 0;
        int hPos = 0;
        int depth = 0;

        foreach(var line in lines)
        {
            var parts = line.Split(' ');
            var direction = parts[0];
            var x = int.Parse(parts[1]);
            if (direction == "down")
            {
                aim += x;
            }
            else if (direction == "up")
            {
                aim -= x;
            }
            else if (direction == "forward")
            {
                hPos += x;
                depth += aim * x;
            }
        }

        var result = hPos * depth;

        return result.ToString();
    }
}