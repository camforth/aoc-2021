namespace AOC._2022;

public static class Day13
{
    public static string Part1() => GetSolution("p1").ToString();

    public static string Part2() => GetSolution("p2").ToString();

    private static int GetSolution(string part)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day13.txt");

        var pairs = new List<(int, int)>();
        for (var i = 0; i < lines.Length; i += 3)
        {
            var a = ParseLine(lines[i]);
            var b = ParseLine(lines[i + 1]);
        }
        

        return 1;
    }

    // private static Node ParseList(string list)
    // {
    //     var node = new Node(NodeType.List, new List<Node>());
    //     var body = list[1..^1];
    //     var spl = body.Split(',');
    //     if (spl[0].StartsWith("["))
    //     {
    //         
    //     }
    //     else
    //     {
    //         node.Nodes.
    //     }
    //
    //     return node;
    // }

    private static Node ParseLine(string line)
    {
        var position = 0;
        ReadOnlySpan<char> tokens = line;
        var node = ParseExpressions(tokens, ref position);
        Console.WriteLine($"{node}");
        return node;
    }
    
    private static Node ParseExpressions(ReadOnlySpan<char> tokens, ref int position)
    {
        var token = tokens[position];
        Console.WriteLine($"Token: {token}");
        
        var nodeType = token == '[' ? NodeType.List : NodeType.Int;
        var node = nodeType switch
        {
            NodeType.Int => new Node(NodeType.Int, int.Parse(token.ToString())),
            NodeType.List => new Node(NodeType.List, null, new List<Node>())
        };
        position++;
        
        if (tokens.Length == position + 1)
        {
            return node;
        }
        var ahead = tokens[position];
        Console.WriteLine($"Ahead token: {ahead}");
        if (ahead == ',')
        {
            position++;
            return node;
        }
            

        // if (!Expressions.ContainsKey(token))
        // {
        //     throw new Exception($"Unexpected token: {token}");
        // }

        while (tokens[position] != ']')
        {
            var child = ParseExpressions(tokens, ref position);
            node.Nodes.Add(child);
            token = tokens[position];
        }
        position++;
        return node;
    }

    private record Node(NodeType Type, int? Value = null, List<Node>? Nodes = null)
    {
        public override string ToString()
        {
            return Type switch
            {
                NodeType.Int => Value.ToString(),
                NodeType.List => $"[{string.Join(',', Nodes?.Select(x => x.ToString()).ToList() ?? Enumerable.Empty<string>())}]"
            };
        }
    }

    private enum NodeType
    {
        List,
        Int
    }
}