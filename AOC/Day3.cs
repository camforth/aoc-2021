namespace AOC
{
    public static class Day3
    {
        public static string Part1()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day3.txt");

            var lineCount = lines.Length;
            var bitLength = lines.First().Length;

            var sums = GetSums(lines.ToList(), bitLength);

            string gamma = string.Empty;
            string epsilon = string.Empty;
            foreach(var sum in sums)
            {
                if ((lineCount / sum) >= 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }

            }

            var gammaDecimal = Convert.ToInt32(gamma, 2);
            var epsilonDecimal = Convert.ToInt32(epsilon, 2);

            Console.WriteLine($"Gamma: {gammaDecimal}");
            Console.WriteLine($"Epsilon: {epsilonDecimal}");

            var result = gammaDecimal * epsilonDecimal;

            return result.ToString();
        }

        public static string Part2()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day3.txt");

            var bitLength = lines.First().Length;
            var currentBit = 0;

            var oxygen = lines.ToList();
            var co2 = lines.ToList();
            do
            {
                var oxygenSums = GetSums(oxygen, bitLength);
                var co2Sums = GetSums(co2, bitLength);

                if (oxygen.Count > 1)
                {
                    var mostCommon = oxygenSums[currentBit] >= (oxygen.Count - oxygenSums[currentBit]) ? '1' : '0';
                    oxygen = oxygen.Where(x => x.ToCharArray()[currentBit] == mostCommon).ToList();
                }

                if (co2.Count > 1)
                {
                    var leastCommon = co2Sums[currentBit] >= (co2.Count - co2Sums[currentBit]) ? '0' : '1';
                    co2 = co2.Where(x => x.ToCharArray()[currentBit] == leastCommon).ToList();
                }

                currentBit++;
            }
            while (oxygen.Count > 1 || co2.Count > 1);

            Console.WriteLine(oxygen.First());
            Console.WriteLine(co2.First());

            var oxygenDecimal = Convert.ToInt32(oxygen.First(), 2);
            var co2Decimal = Convert.ToInt32(co2.First(), 2);

            Console.WriteLine($"Oxygen: {oxygenDecimal}");
            Console.WriteLine($"CO2: {co2Decimal}");

            var result = oxygenDecimal * co2Decimal;

            return result.ToString();
        }

        private static int[] GetSums(List<string> list, int bitLength)
        {
            var sums = new int[bitLength];

            foreach (var item in list)
            {
                var binaryArray = item.ToCharArray();

                for (int x = 0; x < bitLength; x++)
                {
                    sums[x] += int.Parse(binaryArray[x].ToString());
                }
            }

            return sums;
        }
    }
}
