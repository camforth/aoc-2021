namespace AOC._2022;

public static class Day2
{
    public static string Part1()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day2.txt");
        var total = 0;
        foreach (var line in lines)
        {
            var turns = line.Split(' ');

            var score = turns switch
            {
                ["A" or "X", "A" or "X"] => 1 + 3, // draw
                ["A" or "X", "B" or "Y"] => 2 + 6, // paper win
                ["A" or "X", "C" or "Z"] => 3 + 0, // loss
                ["B" or "Y", "A" or "X"] => 1 + 0, // loss
                ["B" or "Y", "B" or "Y"] => 2 + 3, // draw
                ["B" or "Y", "C" or "Z"] => 3 + 6, // win
                ["C" or "Z", "A" or "X"] => 1 + 6, // win
                ["C" or "Z", "B" or "Y"] => 2 + 0, // loss
                ["C" or "Z", "C" or "Z"] => 3 + 3, // draw
            };
            
            Console.WriteLine($"Score: {score}");

            total += score;
        }

        return total.ToString();
    }

    public static string Part2()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day2.txt");
        var total = 0;
        foreach (var line in lines)
        {
            var turns = line.Split(' ');

            // x = lose = 0
            // y = draw = 3
            // z = win = 6
            // a = rock = 1
            // b = paper = 2
            // c = scissors = 3
            var score = turns switch
            {
                ["A", "X"] => 3 + 0, // scissors + loss
                ["A", "Y"] => 1 + 3, // rock + draw
                ["A", "Z"] => 2 + 6, // paper win
                ["B", "X"] => 1 + 0, // loss rock
                ["B", "Y"] => 2 + 3, // draw paper
                ["B", "Z"] => 3 + 6, // win scissors
                ["C", "X"] => 2 + 0, // loss paper
                ["C", "Y"] => 3 + 3, // draw scissors
                ["C", "Z"] => 1 + 6, // win rock
            };
            
            Console.WriteLine($"Score: {score}");

            total += score;
        }

        return total.ToString();
    }
}