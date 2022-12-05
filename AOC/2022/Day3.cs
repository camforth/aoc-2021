namespace AOC._2022;

public static class Day3
{
    private static readonly string[] Letters = {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };
    public static string Part1()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day3.txt");
        var total = 0;
        foreach (var line in lines)
        {
            var half = line.Length / 2;
            var pack1 = line[..half].ToCharArray();
            var pack2 = line[half..].ToCharArray();
            
            var error = pack1.First(x => pack2.Contains(x)).ToString();
            var errorValue = Letters.ToList().IndexOf(error) + 1;

            total += errorValue;
        }

        return total.ToString();
    }

    public static string Part2()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day3.txt");
        var total = 0;
        for (var i = 0; i < lines.Length; i += 3)
        {
            var pack1 = lines[i].ToCharArray();
            var pack2 = lines[i + 1].ToCharArray();
            var pack3 = lines[i + 2].ToCharArray();
            
            var common = pack1.Where(x => pack2.Contains(x)).First(x => pack3.Contains(x)).ToString();
            var commonValue = Letters.ToList().IndexOf(common) + 1;

            total += commonValue;
        }

        return total.ToString();
    }
}