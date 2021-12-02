namespace AOC
{
    public static class AocHelpers
    {
        public static int[] ReadInputsAsInt(string filename)
        {
            var allLines = ReadInputsAsString(filename);
            return allLines.Select(line => int.Parse(line)).ToArray();
        }

        public static string[] ReadInputsAsString(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            return File.ReadAllLines(path);
        }

        public static string RunDay(string? day, Func<string> action)
        {
            Console.WriteLine($"Running day {day} solution");
            return action();
        }
    }
}
