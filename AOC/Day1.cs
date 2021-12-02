namespace AOC
{
    public static class Day1
    {
        public static string Part1()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "input-day1.txt");

            var reader = new StreamReader(path);

            int counter = 0;
            int? previousNumber = null;
            var line = reader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                if (!int.TryParse(line, out var currentNumber))
                {
                    line = reader.ReadLine();
                    continue;
                }

                if (currentNumber > previousNumber)
                {
                    counter++;
                }

                previousNumber = currentNumber;
                line = reader.ReadLine();
            }

            return counter.ToString();
        }

        public static string Part2()
        {
            var lines = AocHelpers.ReadInputsAsInt("input-day1.txt");

            int counter = 0;
            int? previous = null;
            for (int i = 0; i < lines.Length - 2; i++)
            {
                var depth = lines[i] + lines[i + 1] + lines[i + 2];
                if (depth > previous)
                {
                    counter++;
                }
                previous = depth;
            }

            return counter.ToString();
        }
    }
}
