using System.Text;

namespace AOC._2022;

public static class Day5
{
    public static string Part1() => GetSolution("1");

    public static string Part2() => GetSolution("2");

    private static string GetSolution(string part)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day5.txt");

        var secondPart = false;
        var stacks = new List<List<string>>();
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (!secondPart && !line.Contains('['))
            {
                secondPart = string.IsNullOrEmpty(line);
                continue;
            }

            if (!secondPart)
            {
                for (var j = 0; j < line.Length; j += 4)
                {
                    // init stacks only on first line
                    var stack = (int)Math.Floor((decimal) (j / 4));
                    if (i == 0)
                    {
                        stacks.Add(new List<string>());
                    }
                    var item = line[j..(j + 3)];
                    if (item.StartsWith('['))
                    {
                        var itemValue = item[1].ToString();
                        stacks[stack].Add(itemValue);
                    }
                }

                continue;
            }
            
            DoInstruction(stacks, line, part);
        }
        
        var output = new StringBuilder();
        foreach(var stack in stacks)
            output.Append(stack.First());

        return output.ToString();
    }

    private static void DoInstruction(List<List<string>> stacks, string instruction, string part)
    {
        var words = instruction.Split(' ');
        var count = int.Parse(words[1]);
        var from = int.Parse(words[3]) - 1;
        var to = int.Parse(words[5]) - 1;

        if (part == "1")
        {
            foreach (var _ in Enumerable.Range(0, count))
            {
                var item = stacks[from].First();
                stacks[from].Remove(item);
                stacks[to].Insert(0, item);
            }

            return;
        }

        var items = stacks[from].Take(count).ToList();
        stacks[from].RemoveRange(0, count);
        stacks[to].InsertRange(0, items);
    }
}