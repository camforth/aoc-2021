using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC
{
    public static class Day6
    {
        public static string Part1() => GetSolutionForDays(80).ToString();

        public static string Part2() => GetSolutionForDays(256).ToString();

        private static long GetSolutionForDays(int dayCount)
        {
            var lines = AocHelpers.ReadInputsAsString("input-day6.txt");

            var input = lines[0].Split(",").Select(x => int.Parse(x)).ToArray();

            var days = new long[9]
                .Select((x, i) => (long)input.Count(a => a == i)).ToArray();

            for (var i = 0; i < dayCount; i++)
            {
                var daysToReset = days[0];
                days = days.Skip(1).Append(daysToReset).ToArray();
                days[6] += daysToReset;
            }

            return days.Sum();
        }
    }
}
