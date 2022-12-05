namespace AOC._2021;

public static class Day8
{
    public static string Part1()
    {
        return GetSolution1().ToString();
    }

    public static string Part2()
    {
        return GetSolution2().ToString();
    }

    private static int GetSolution1()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day8.txt");

        int uniqueNumberOfSegments = 0;
        foreach(var line in lines)
        {
            var split = line.Split(" | ");

            var input = split[0].Split(" ").ToArray();
            var output = split[1].Split(" ").ToArray();

            var uniqueInputsLengths = input.Where(x => new[] { 2, 4, 3, 7 }.Contains(x.Length)).Select(x => x.Length);

            var uniqueOutputLengths = output.Where(x => uniqueInputsLengths.Contains(x.Length)).ToArray();

            uniqueNumberOfSegments += uniqueOutputLengths.Count();
        }

        var result = uniqueNumberOfSegments;

        return result;
    }

    private static int GetSolution2()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day8.txt");

        var result = 0;
        foreach (var line in lines)
        {
            var map = new Dictionary<string, string>
            {
                { "a", "" },
                { "b", "" },
                { "c", "" },
                { "d", "" },
                { "e", "" },
                { "f", "" },
                { "g", "" },
            };
            var split = line.Split(" | ");

            var input = split[0].Split(" ").ToArray();
            var output = split[1].Split(" ").ToArray();

            var allInput = string.Join("", input);
            var grouped = allInput.GroupBy(x => x.ToString()).Select(x => new { seg = x.Key, count = x.Count() }).ToList();

            // segment count
            //a: 8
            //b: 6 <-
            //c: 8
            //d: 7
            //e: 4 <-
            //f: 9 <-
            //g: 7

            // Find 3 known segments (b, e, f)
            map["b"] = grouped.First(x => x.count == 6).seg;
            map["e"] = grouped.First(x => x.count == 4).seg;
            map["f"] = grouped.First(x => x.count == 9).seg;

            // Use the 1 and 7 to find a
            var one = input.First(x => x.Length == 2).ToCharArray();
            var seven = input.First(x => x.Length == 3).ToCharArray();
            map["a"] = seven.First(x => !one.Contains(x)).ToString();

            // Find c
            var eight = input.First(x => x.Length == 7).ToCharArray();
            var six = input.First(x => x.Length == 6 && !one.All(y => x.Contains(y))).ToCharArray();
            map["c"] = eight.Except(six).First().ToString();

            // find d
            var four = input.First(x => x.Length == 4).ToCharArray();
            map["d"] = four.Except(seven).Except(map["b"]).First().ToString();

            //find g
            map["g"] = map.Keys.Except(map.Where(x => x.Value != "").Select(x => x.Value)).First().ToString();

            Console.WriteLine(string.Join(",", map.Select(x => x.Value)));

            var newNumbers = new List<NumberSegment>();
            foreach(var number in GetNumberSegments())
            {
                var numberSegments = new string[7];
                for (var i = 0; i < number.Segments.Length; i++)
                {
                    numberSegments[i] = map[number.Segments[i]];
                }
                newNumbers.Add(new NumberSegment(number.Number, numberSegments));
            }

            var outputNumber = "";
            foreach(var item in output)
            {
                var number = newNumbers.First(x => x.Segments.Count(c => !string.IsNullOrEmpty(c)) == item.Length 
                                                   && item.All(a => x.Segments.Contains(a.ToString())));
                outputNumber += number.Number.ToString();
            }

            result += int.Parse(outputNumber);

            Console.WriteLine(outputNumber);
        }

        return result;
    }

    public record NumberSegment(int Number, string[] Segments);

    public static List<NumberSegment> GetNumberSegments() => new()
    {
        new (0, new[] {"a", "b", "c", "e", "f","g"}),
        new (1, new[] {"c", "f"}),
        new (2, new[] {"a", "c", "d", "e", "g"}),
        new (3, new[] {"a", "c", "d", "f", "g"}),
        new (4, new[] {"b", "c", "d", "f"}),
        new (5, new[] {"a", "b", "d", "f","g"}),
        new (6, new[] {"a", "b", "d", "e", "f","g"}),
        new (7, new[] {"a", "c", "f"}),
        new (8, new[] {"a", "b", "c", "d", "e", "f","g"}),
        new (9, new[] {"a", "b", "c", "d", "f","g"}),
    };
}