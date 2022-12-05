namespace AOC._2021;

public static class Day12
{
    public static string Part1()
    {
        return GetSolution(false).ToString();
    }

    public static string Part2()
    {
        return GetSolution(true).ToString();
    }

    private static int GetSolution(bool part2)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day12.txt");

        var map = new List<Node>();

        foreach (var line in lines)
        {
            var nodes = line.Split("-");
            AddNodes(map, nodes[0], nodes[1]);
        }

        var start = map.First(x => x.Name == "start");

        var completedPaths = new HashSet<string>();
        foreach (var node in start.Nodes)
        {
            var visitedNodes = new List<Node>();
            completedPaths.UnionWith(NavigateAllPaths(node, ref visitedNodes, part2));
        }

        //foreach (var path in completedPaths)
        //    Console.WriteLine($"Completed path: {path}");

        var result = completedPaths.Count;

        return result;
    }

    private static HashSet<string> NavigateAllPaths(Node node, ref List<Node> visitedNodes, bool part2 = false)
    {
        visitedNodes.Add(node);

        if (node.Name == "end")
        {
            return new() { string.Join(",", visitedNodes.Select(x => x.Name)) };
        }

        var completedPaths = new HashSet<string>();
        foreach (var n in node.Nodes)
        {
            var smallCaveAlreadyVisitedTwice = visitedNodes.Where(x => !x.IsLarge).GroupBy(x => x.Name).Select((x, y) => x.Count()).Any(x => x > 1);
            if (n.Name != "start"
                && (!visitedNodes.Any(x => !x.IsLarge && x.Name == n.Name)
                    || (part2 && !smallCaveAlreadyVisitedTwice)))
            {
                var newVisitedNodes = new List<Node>();
                newVisitedNodes.AddRange(visitedNodes);
                completedPaths.UnionWith(NavigateAllPaths(n, ref newVisitedNodes, part2));
            }
        }
        return completedPaths;
    }

    private static void AddNodes(List<Node> nodes, string nodeA, string nodeB)
    {
        var existingNodeA = nodes.FirstOrDefault(x => string.Equals(x.Name, nodeA));
        var existingNodeB = nodes.FirstOrDefault(x => string.Equals(x.Name, nodeB));
        if (existingNodeA is null)
        {
            existingNodeA = new(nodeA, new());
            nodes.Add(existingNodeA);
        }
        if (existingNodeB is null)
        {
            existingNodeB = new(nodeB, new());
            nodes.Add(existingNodeB);
        }
        if (!existingNodeA.Nodes.Any(x => x.Name == nodeB))
        {
            existingNodeA.Nodes.Add(existingNodeB);
        }
        if (!existingNodeB.Nodes.Any(x => x.Name == nodeA))
        {
            existingNodeB.Nodes.Add(existingNodeA);
        }
    }

    private record Node(string Name, List<Node> Nodes)
    {
        public bool IsLarge => Char.IsUpper(Name[0]);
    }
}


