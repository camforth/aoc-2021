using System.Text;

namespace AOC._2021;

public static class Day20
{
    public static string Part1()
    {
        return GetSolution(2).ToString();
    }

    public static string Part2()
    {
        return GetSolution(50).ToString();
    }

    private static int GetSolution(int enhanceCount)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day20.txt");

        var algorithm = lines[0];

        var grid = AocHelpers.CreateStringGrid(lines[2..]);
        AocHelpers.WriteGridToConsole(grid);

        string[,] nextGrid = grid;
        string voidChar = ".";
        foreach (var i in Enumerable.Range(0, enhanceCount))
            (nextGrid, voidChar) = GetOutputImage(nextGrid, algorithm, voidChar);

        var litPixels = 0;
        foreach (var x in Enumerable.Range(0, nextGrid.GetLength(0)))
            foreach (var y in Enumerable.Range(0, nextGrid.GetLength(1)))
                litPixels += nextGrid[x, y] == "#" ? 1 : 0;

        return litPixels;
    }

    private static (string[,] output, string nextVoidChar) GetOutputImage(string[,] input, string algorithmn, string voidChar)
    {
        var output = new string[input.GetLength(0) + 2, input.GetLength(1) + 2];
        var voidValue = voidChar == "#" ? "1" : "0";

        foreach (var x in Enumerable.Range(0, output.GetLength(0)))
            foreach (var y in Enumerable.Range(0, output.GetLength(1)))
                output[x, y] = algorithmn[Convert.ToInt32(GetNeighbours(x - 1, y - 1, input, voidValue), 2)].ToString();

        AocHelpers.WriteGridToConsole(output);
        var nextVoidChar = algorithmn[Convert.ToInt32($"{voidValue}{voidValue}{voidValue}{voidValue}{voidValue}{voidValue}{voidValue}{voidValue}{voidValue}", 2)].ToString();
        return (output, nextVoidChar);
    }

    private static string GetNeighbours(int x, int y, string[,] grid, string voidValue)
    {
        var str = new StringBuilder();
        str.Append(GetPointValue(grid, x - 1, y - 1, voidValue));
        str.Append(GetPointValue(grid, x - 1, y, voidValue));
        str.Append(GetPointValue(grid, x - 1, y + 1, voidValue));
        str.Append(GetPointValue(grid, x, y - 1, voidValue));
        str.Append(GetPointValue(grid, x, y, voidValue));
        str.Append(GetPointValue(grid, x, y + 1, voidValue));
        str.Append(GetPointValue(grid, x + 1, y - 1, voidValue));
        str.Append(GetPointValue(grid, x + 1, y, voidValue));
        str.Append(GetPointValue(grid, x + 1, y + 1, voidValue));
        return str.ToString();
    }

    private static string GetPointValue(string[,] grid, int x, int y, string voidValue)
    {
        return AocHelpers.IsOutOfBounds(grid, x, y) || grid[x,y] is null ? voidValue : (grid[x, y] == "#" ? "1" : "0");
    }
}
