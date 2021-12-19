namespace AOC;

public static class Day18
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
        var lines = AocHelpers.ReadInputsAsString("input-day18.txt");

        var pairs = new PairSnailfish[lines.Length];
        foreach(var i in Enumerable.Range(0, lines.Length))
        {
            pairs[i] = ParseAndReduceLine(lines[i])!;
        }

        PairSnailfish currentPair = pairs[0];
        foreach (var i in Enumerable.Range(1, lines.Length - 1))
        {
            //Console.WriteLine($"Current pair: {currentPair}");
            var addedPair = new PairSnailfish(currentPair, pairs[i]);
            //Console.WriteLine($"Added pair: {addedPair}");
            currentPair = ParseAndReduceLine(addedPair.ToString())!;
        }

        var sums = new List<int>();
        foreach (var i in Enumerable.Range(0, lines.Length - 1))
        {
            foreach (var j in Enumerable.Range(0, lines.Length - 1))
            {
                if (i == j) continue;
                var addedPair = ParseAndReduceLine(new PairSnailfish(pairs[i], pairs[j]).ToString());
                sums.Add(addedPair!.Sum());
            }
        }

        var finalSum = currentPair.Sum();
        var largestSum = sums.Max();

        Console.WriteLine($"Final pair: {currentPair}");
        Console.WriteLine($"Final sum of magnitude: {finalSum}");
        Console.WriteLine($"Largest sum of magnitude: {largestSum}");

        var result = part2 ? largestSum : finalSum;

        return result;
    }

    private static PairSnailfish? ParseAndReduceLine(string line)
    {
        PairSnailfish? pair = null;
        var reducing = true;
        while (reducing)
        {
            pair = ParseLine(line);
            reducing = ReduceSnailfish(pair);
            if (reducing)
            {
                line = pair.ToString();
                //Console.WriteLine($"Reduced: {line}");
            }
        }
        return pair;
    }

    private static PairSnailfish ParseLine(string line)
    {
        var position = 1;
        var tokens = line.ToCharArray();
        return ParseExpressions(tokens, ref position);
    }

    private static PairSnailfish ParseExpressions(char[] tokens, ref int position)
    {
        var left = ParsePair(tokens, ref position);
        position++;
        var right = ParsePair(tokens, ref position);
        position++;
        var pair = new PairSnailfish(left, right);
        left.Parent = pair;
        right.Parent = pair;
        return pair;
    }

    private static Snailfish ParsePair(char[] tokens, ref int position)
    {
        var token = tokens[position];
        position++;
        if (token != '[' && int.TryParse(token.ToString(), out int value))
        {
            if (int.TryParse($"{token}{tokens[position]}", out int value2))
            {
                position++;
                return new RegularSnailfish(value2, position - 1);
            }
            return new RegularSnailfish(value, position - 1);
        }
        else
        {
            return ParseExpressions(tokens, ref position);
        }
    }

    private static bool ReduceSnailfish(PairSnailfish snailfish)
    {
        // Part 1: Explode
        var reduced = MaybeExplodeSnailfish(snailfish);
        if (reduced) return true;
        // Part 2: Split
        return MaybeSplitSnailfish(snailfish);
    }

    private static bool MaybeExplodeSnailfish(PairSnailfish snailfish)
    {
        if (snailfish.Left is PairSnailfish lPair)
        {
            var reduced = MaybeExplodeSnailfish(lPair);
            if (reduced) return true;
        }
        if (snailfish.Right is PairSnailfish rPair)
        {
            var reduced = MaybeExplodeSnailfish(rPair);
            if (reduced) return true;
        }
        if (snailfish.Left is RegularSnailfish left
            && snailfish.Right is RegularSnailfish right)
        {
            
            var parentCount = 0;
            snailfish.ParentCount(ref parentCount);
            if (parentCount >= 5)
            {
                // explode
                var foundLeft = new List<Snailfish>();
                var lessThanPosition = snailfish.GetRegularSnailfishBy(x => x.Position < left.Position, foundLeft).OrderBy(o => o.Position).LastOrDefault();
                if (lessThanPosition is not null)
                {
                    lessThanPosition.Value += left.Value;
                }

                var foundRight = new List<Snailfish>();
                var greaterThanPosition = snailfish.GetRegularSnailfishBy(x => x.Position > right.Position, foundRight).OrderBy(o => o.Position).FirstOrDefault();
                if (greaterThanPosition is not null)
                {
                    greaterThanPosition.Value += right.Value;
                }

                if (snailfish.Parent!.Left == snailfish)
                {
                    snailfish.Parent.Left = new RegularSnailfish(0, 0);
                }
                else if (snailfish.Parent.Right == snailfish)
                {
                    snailfish.Parent.Right = new RegularSnailfish(0, 0);
                }

                return true;
            }
        }
        return false;
    }

    private static bool MaybeSplitSnailfish(PairSnailfish snailfish)
    {
        if (snailfish.Left is PairSnailfish lPair)
        {
            var reduced = MaybeSplitSnailfish(lPair);
            if (reduced) return true;
        }
        if (snailfish.Left is RegularSnailfish l
            && l.Value >= 10)
        {
            snailfish.Left = SplitRegularNumber(l);
            return true;
        }
        if (snailfish.Right is RegularSnailfish r
            && r.Value >= 10)
        {
            snailfish.Right = SplitRegularNumber(r);
            return true;
        }
        if (snailfish.Right is PairSnailfish rPair)
        {
            var reduced = MaybeSplitSnailfish(rPair);
            if (reduced) return true;
        }
        return false;
    }

    private static PairSnailfish SplitRegularNumber(RegularSnailfish snailfish)
    {
        var left = new RegularSnailfish(Convert.ToInt32(Math.Floor((double)snailfish.Value / 2)), 0);
        var right = new RegularSnailfish(Convert.ToInt32(Math.Ceiling((double)snailfish.Value / 2)), 0);
        var parent = snailfish.Parent;
        var newPair = new PairSnailfish(left, right);
        newPair.Parent = parent;
        return newPair;
    }

    private abstract class Snailfish
    {
        public SnailfishType Type { get; init; }
        public PairSnailfish? Parent { get; set; }
        public Snailfish(SnailfishType type)
        {
            Type = type;
        }
        public void ParentCount(ref int count)
        {
            count++;
            if (Parent is not null)
            {
                Parent.ParentCount(ref count);
            }
        }

        public IEnumerable<RegularSnailfish> GetRegularSnailfishBy(Func<RegularSnailfish, bool> positionFunc, List<Snailfish> found)
        {
            if (found.Contains(this))
            {
                yield break;
            }
            found.Add(this);
            if (this is RegularSnailfish regular
                && positionFunc(regular))
            {
                yield return regular;
            }
            if (this is PairSnailfish pair)
            {
                foreach(var s in pair.Right.GetRegularSnailfishBy(positionFunc, found))
                {
                    yield return s;
                }
                foreach (var s in pair.Left.GetRegularSnailfishBy(positionFunc, found))
                {
                    yield return s;
                }
            }
            if (Parent is not null)
            {
                foreach (var s in Parent.GetRegularSnailfishBy(positionFunc, found))
                {
                    yield return s;
                }
            }
        }

        public int Sum(int? multiplier = null)
        {
            if (this is RegularSnailfish reg && multiplier.HasValue)
            {
                return reg.Value;
            }
            if (this is PairSnailfish pair)
            {
                var leftSum = pair.Left.Sum(3);
                var rightSum = pair.Right.Sum(2);
                return leftSum * 3 + rightSum * 2;
            }
            return 0; // not possible
        }
    }
    private class PairSnailfish : Snailfish
    {
        public PairSnailfish(Snailfish left, Snailfish right)
            : base(SnailfishType.Pair)
        {
            Left = left;
            Right = right;
        }
        public Snailfish Left { get; set; }
        public Snailfish Right { get; set; }
        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }
    }

    private class RegularSnailfish : Snailfish
    {
        public RegularSnailfish(int value, int position)
            : base(SnailfishType.Pair)
        {
            Value = value;
            Position = position;
        }
        public int Value { get; set; }
        public int Position { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    private enum SnailfishType
    {
        Regular,
        Pair
    }
}
