using System.Data;
using System.Text;

namespace AOC._2022;

public static class Day11
{
    public static string Part1() => GetSolution("p1").ToString();

    public static string Part2() => GetSolution("p2").ToString();

    private static long GetSolution(string part)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day11.txt");

        var chunks = lines.Chunk(7).Select(x => x.Skip(1).Take(5).ToList()).ToList();
        var monkeys = new List<Monkey>();
        foreach (var chunk in chunks)
        {
            var monkey = new Monkey();
            foreach (var line in chunk)
            {
                var instruction = line.Trim().Split(':', StringSplitOptions.TrimEntries);
                if (instruction[0].StartsWith("Starting items"))
                {
                    monkey.RawItems = instruction[1].Split(',', StringSplitOptions.TrimEntries)
                        .Select(int.Parse).ToList();
                }
                else if (instruction[0].StartsWith("Operation"))
                {
                    monkey.Operation = instruction[1];
                    var expSpl = instruction[1].Split('=', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.TrimEntries);
                    monkey.OperationFunc = (x) => ExecuteExpression(x, expSpl[0], expSpl[1], expSpl[2]);
                }
                else if (instruction[0].StartsWith("Test"))
                {
                    monkey.Divider = int.Parse(instruction[1].Split(' ').Last());
                }
                else if (instruction[0].StartsWith("If true"))
                {
                    monkey.IfTrue = int.Parse(instruction[1].Split(' ').Last());
                }
                else if (instruction[0].StartsWith("If false"))
                {
                    monkey.IfFalse = int.Parse(instruction[1].Split(' ').Last());
                }
            }
            monkeys.Add(monkey);
        }

        var mod = monkeys.Aggregate(1, (x, y) => x * y.Divider);
        
        foreach(var monkey in monkeys)
            monkey.InitItems(mod);
        
        foreach(var i in Enumerable.Range(0, monkeys.Count))
            Console.WriteLine($"Monkey {i} items: {string.Join(",", monkeys[i].Items.Select(x => x.ToString()).ToArray())}. Inspections: {monkeys[i].Counter}");

        foreach (var i in Enumerable.Range(0, part == "p1" ? 20: 10000))
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryDequeue(out var current))
                {
                    monkey.Counter++;
                    var itemWorry = monkey.OperationFunc(current);
                    // ehh part 1 is broken
                    if (part == "p1")
                        itemWorry /= new IntMod(3, mod);
                    if (itemWorry % new IntMod(monkey.Divider, mod) == 0)
                    {
                        monkeys[monkey.IfTrue].Items.Enqueue(itemWorry);
                    }
                    else
                    {
                        monkeys[monkey.IfFalse].Items.Enqueue(itemWorry);
                    }
                }
                
            }
        }
        
        foreach(var i in Enumerable.Range(0, monkeys.Count))
            Console.WriteLine($"Monkey {i} items: {string.Join(",", monkeys[i].Items.Select(x => x.ToString()).ToArray())}. Inspections: {monkeys[i].Counter}");

        var mostActive = monkeys.OrderByDescending(o => o.Counter).Take(2).ToList();
        
        return mostActive[0].Counter * mostActive[1].Counter;
    }

    private static IntMod ExecuteExpression(IntMod old, string left, string op, string right)
    {
        var leftNum = left == "old" ? old : IntMod.Parse(left, old.Modulus);
        var rightNum = right == "old" ? old : IntMod.Parse(right, old.Modulus);
        return op switch
        {
            "*" => leftNum * rightNum,
            "+" => leftNum + rightNum,
            "-" => leftNum - rightNum,
            "/" => leftNum / rightNum,
            _ => throw new Exception("oh")
        };
    }

    private class Monkey
    {
        public List<int> RawItems { get; set; } = new();
        public Queue<IntMod> Items { get; set; } = new();
        public long Counter = 0;
        public string Operation { get; set; }
        public Func<IntMod, IntMod> OperationFunc { get; set; }
        public int Divider { get; set; }
        public int IfTrue { get; set; }
        public int IfFalse { get; set; }

        public void InitItems(int modulus)
        {
            foreach(var item in RawItems)
                Items.Enqueue(new IntMod(item, modulus));
        }
    }
    
    private record IntMod
    {
        public long N { get; }
        public int Modulus { get; }

        public IntMod(long n, int modulus)
        {
            N = n % modulus;
            Modulus = modulus;
        }
        
        public static IntMod Parse(string s, int modulus)
        {
            var num = long.Parse(s);
            return new(num, modulus);
        }
        
        public IntMod Inverse()
        {
            var (x, y, d) = ExtendedEuclideanAlgorithm(N, Modulus);
            return new(x, Modulus);
        }

        public static IntMod operator *(IntMod a, IntMod b) => new(a.N * b.N, a.Modulus);
        public static IntMod operator +(IntMod a, IntMod b) => new(a.N + b.N, a.Modulus);
        public static IntMod operator -(IntMod a, IntMod b) => new(a.N - b.N, a.Modulus);
        public static IntMod operator /(IntMod a, IntMod b) => a * b.Inverse();
        public static IntMod operator /(IntMod a, int b) => new IntMod(a.N / b, a.Modulus);

        public static long operator %(IntMod a, IntMod b)
        {
            var r = a.N % b.N;
            return r;
        }
        public override string ToString() => $"{N} (mod {Modulus})";
    }
    
    public static (long x, long y, long d) ExtendedEuclideanAlgorithm(long a, long b)
    {
        if (Math.Abs(b) > Math.Abs(a))
        {
            var (x, y, d) = ExtendedEuclideanAlgorithm(b, a);
            return (y, x, d);
        }

        if (Math.Abs(b) == 0)
        {
            return (1, 0, a);
        }

        long x1 = 0;
        long x2 = 1;
        long y1 = 1;
        long y2 = 0;
        while (Math.Abs(b) > 0)
        {
            var q = a / b;
            var r = a % b;
            var x = x2 - q * x1;
            var y = y2 - q * y1;
            a = b;
            b = r;
            x2 = x1;
            x1 = x;
            y2 = y1;
            y1 = y;
        }

        return (x2, y2, a);
    }
}