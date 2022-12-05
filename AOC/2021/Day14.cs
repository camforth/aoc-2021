namespace AOC._2021;

public static class Day14
{
    public static string Part1()
    {
        return GetSolution(10).ToString();
    }

    public static string Part2()
    {
        return GetSolution(40).ToString();
    }

    private static long GetSolution(int steps)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day14.txt");

        var template = lines[0];

        var rules = lines.Skip(2).ToDictionary(x => x[..2], x => x[6..]);

        var pairCounts = new Dictionary<string, long>();
        var arr = template.ToCharArray();
        foreach(var i in Enumerable.Range(0, arr.Length - 1))
        {
            IncementCounts(pairCounts, $"{arr[i]}{arr[i + 1]}");
        }

        foreach (var step in Enumerable.Range(0, steps))
        {
            var newCounts = new Dictionary<string, long>();
            foreach (var pair in pairCounts)
            {
                if (pair.Value == 0) continue;

                var next = rules[pair.Key];
                IncementCounts(newCounts, $"{pair.Key[0]}{next}", pair.Value);
                IncementCounts(newCounts, $"{next}{pair.Key[1]}", pair.Value);
            }
            pairCounts = newCounts;
        }

        Console.WriteLine("-Final pair counts-");
        PrintPairCounts(pairCounts);

        var trimmedCounts = new Dictionary<string, long>();
        foreach(var pair in pairCounts)
        {
            IncementCounts(trimmedCounts, pair.Key[0].ToString(), pair.Value);
        }

        // Add last character
        IncementCounts(trimmedCounts, template[^1].ToString());

        var sum = trimmedCounts.Sum(x => x.Value);

        var ordered = trimmedCounts.OrderBy(x => x.Value).ToList();

        var result = ordered.Last().Value - ordered.First().Value;

        return result;
    }

    private static void PrintPairCounts(Dictionary<string, long> counts)
    {
        foreach(var pair in counts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        var trimmedCounts = new Dictionary<string, long>();
        foreach (var pair in counts)
        {
            IncementCounts(trimmedCounts, pair.Key[0].ToString(), pair.Value);
        }

        foreach (var pair in trimmedCounts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }

    private static void IncementCounts(Dictionary<string, long> counts, string key, long increment = 1)
    {
        if (!counts.ContainsKey(key))
            counts[key] = increment;
        else
            counts[key] += increment;
    }
}
