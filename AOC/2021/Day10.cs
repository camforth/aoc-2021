namespace AOC._2021;

public static class Day10
{
    public static string Part1()
    {
        var (syntaxErrors, _) = GetSolution();

        var errors = 0;
        foreach (var error in syntaxErrors)
        {
            errors += Part1CharacterPoints[error];
        }

        return errors.ToString();
    }

    public static string Part2()
    {
        var (_, completedLines) = GetSolution();

        Console.WriteLine($"Completed line count: {completedLines.Count}");

        var scores = new List<long>();
        foreach(var line in completedLines)
        {
            long score = 0;
            foreach(var c in line)
            {
                score *= 5;
                score += Part2CharacterPoints[c];
            }
            scores.Add(score);
        }

        var half = (int)Math.Floor((decimal)scores.Count / 2);

        return scores.OrderBy(o => o).Skip(half).First().ToString()!;
    }

    private static (List<string> syntaxErrors, List<string[]> completedLines) GetSolution()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day10.txt");

        var syntaxErrors = new List<string>();
        var completedLines = new List<string[]>();
        foreach (var line in lines)
        {
            try
            {
                var position = 0;
                var tokens = line.ToCharArray().Select(x => x.ToString()).ToList();
                var originalLength = tokens.Count;
                //Console.WriteLine($"Original line: {string.Join(",", tokens)}");
                while(position < tokens.Count)
                {
                    var node = ParseExpressions(tokens, ref position);
                }
                //Console.WriteLine($"Final line: {string.Join(",", tokens)}");
                var addedTokens = tokens.Skip(originalLength).ToArray();
                if (addedTokens.Any())
                {
                    completedLines.Add(tokens.Skip(originalLength).ToArray());
                    //Console.WriteLine($"Added lines: {string.Join(",", tokens.Skip(originalLength).ToArray())}");
                }
                    
            }
            catch (Exception ex) when (ex.Message.StartsWith("Unexpected token"))
            {
                //Console.WriteLine(ex.Message);
                syntaxErrors.Add(ex.Message.Split(" ")[2]);
            }
        }
        Console.WriteLine($"Syntax errors: {string.Join(",", syntaxErrors)}");

        return (syntaxErrors, completedLines);
    }

    private static Node ParseExpressions(List<string> tokens, ref int position)
    {
        var token = tokens[position];
        var node = new Node(token, new List<Node>());
        position++;
        if (tokens.Count == position)
        {
            tokens.Add(Expressions[node.Type]);
            position++;
            return node;
        }

        if (!Expressions.ContainsKey(token))
        {
            throw new Exception($"Unexpected token: {token}");
        }

        while (tokens[position] != Expressions[node.Type])
        {
            var child = ParseExpressions(tokens, ref position);
            node.Nodes.Add(child);
            // complete expressions
            if (tokens.Count == position)
            {
                tokens.Add(Expressions[node.Type]);

                break;
            }
            token = tokens[position];
        }
        position++;
        return node;
    }

    private static Dictionary<string, string> Expressions = new Dictionary<string, string>()
    {
        { "(", ")" },
        { "[", "]" },
        { "{", "}" },
        { "<", ">" },
    };

    private static Dictionary<string, int> Part1CharacterPoints = new Dictionary<string, int>()
    {
        { ")", 3 },
        { "]", 57 },
        { "}", 1197 },
        { ">", 25137 },
    };

    private static Dictionary<string, int> Part2CharacterPoints = new()
    {
        { ")", 1 },
        { "]", 2 },
        { "}", 3 },
        { ">", 4 },
    };
}

public record Node(string Type, List<Node> Nodes);
