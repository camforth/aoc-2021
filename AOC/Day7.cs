namespace AOC
{
    public static class Day7
    {
        public static string Part1() => CalculateLowestCost(true).ToString();

        public static string Part2() => CalculateLowestCost(false).ToString();

        public static int CalculateLowestCost(bool constantCost)
        {
            var lines = AocHelpers.ReadInputsAsString("input-day7.txt");

            var input = lines[0].Split(",").Select(x => int.Parse(x)).ToArray();

            var allPositions = new int[input.Max()];

            for (int i = 0; i < allPositions.Length; i++)
            {
                allPositions[i] = CalculateCostToGetToPosition(i, input, constantCost);
            }

            var leastCost = allPositions.Min();
            var position = Array.IndexOf(allPositions, leastCost);
            Console.WriteLine($"Position: {position}");

            return leastCost;
        }

        public static int CalculateCostToGetToPosition(int position, int[] crabs, bool constantCost)
        {
            int cost = 0;
            for(int i = 0;i < crabs.Length; i++)
            {
                var distance = Math.Abs(position - crabs[i]);
                if (constantCost)
                {
                    cost += distance;
                }
                else
                {
                    for (int j = 1; j <= distance; j++)
                    {
                        cost += j;
                    }
                    // Actual with triangle number n(n + 1) / 2
                    //cost += (distance * (distance + 1)) / 2;
                }
            }
            return cost;
        }
    }
}
