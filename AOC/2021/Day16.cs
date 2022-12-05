namespace AOC._2021;

public static class Day16
{
    public static string Part1()
    {
        return GetSolution(false).ToString();
    }

    public static string Part2()
    {
        return GetSolution(true).ToString();
    }

    private static long GetSolution(bool part2)
    {
        var lines = AocHelpers.ReadInputsAsString("input-day16.txt");

        var binary = HexStringToBinaryString(lines[0]);
        var packet = ReadPacket(binary);

        var result1 = (long)CountVersions(packet);
        var result2 = packet.Value;

        return part2 ? result2 : result1;
    }

    private static Packet ReadPacket(string binary)
    {
        var packet = new Packet
        {
            Version = Convert.ToInt32(binary[..3], 2),
            Type = Enum.Parse<PacketType>(Convert.ToInt32(binary[3..6], 2).ToString()),
            Length = 6
        };
        // literal
        if (packet.Type == PacketType.Literal)
        {
            var (group, length) = ReadGroup(binary[6..]);
            packet.Literal = Convert.ToInt64(group, 2);
            packet.Length += length;
        }
        else
        {
            var lengthTypeId = binary[6];
            packet.Length++;
            if (lengthTypeId == '0')
            {
                packet.Length += 15;
                var subPacketLength = Convert.ToInt32(binary[7..22], 2);
                packet.Length += subPacketLength;
                var subPackets = binary[22..];
                var index = 0;
                while (index < subPacketLength)
                {
                    var currentPacket = ReadPacket(subPackets[index..]);
                    packet.SubPackets.Add(currentPacket);
                    index += currentPacket.Length;
                }
            }
            else
            {
                packet.Length += 11;
                var subPacketCount = Convert.ToInt32(binary[7..18], 2);
                var subPackets = binary[18..];
                var index = 0;
                foreach (var x in Enumerable.Range(0, subPacketCount))
                {
                    var currentPacket = ReadPacket(subPackets[index..]);
                    packet.SubPackets.Add(currentPacket);
                    index += currentPacket.Length;
                }
                packet.Length += index;
            }
        }
        
        return packet;
    }

    private static (string group, int length) ReadGroup(string binary)
    {
        var result = "";
        var length = 5;
        var next = binary[..5];
        while (next != null)
        {
            var prefix = next[0];
            result += next[1..];
            if (prefix == '1')
            {
                binary = binary[5..];
                next = binary[..5];
                length += 5;
            }
            else
            {
                next = null;
            }
        }
        return (result, length);
    }

    private static int CountVersions(Packet packet)
    {
        var count = packet.Version;
        foreach (var sub in packet.SubPackets)
            count += CountVersions(sub);
        return count;
    }

    private static string HexStringToBinaryString(string hex)
    {
        return string.Join("", hex.ToCharArray().Select(x => _hexToBinary[x]));
    }

    private static readonly Dictionary<char, string> _hexToBinary = new()
    {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },
        { 'A', "1010" },
        { 'B', "1011" },
        { 'C', "1100" },
        { 'D', "1101" },
        { 'E', "1110" },
        { 'F', "1111" }
    };

    private class Packet
    {
        public int Version { get; set; }
        public PacketType Type { get; set; }
        public int Length { get; set; }
        public long? Literal { get; set; }
        public long Value => Type switch
        {
            PacketType.Sum => SubPackets.Sum(x => x.Value),
            PacketType.Product => SubPackets.Count == 1 ? SubPackets.First().Value : SubPackets.Select(x => x.Value).Aggregate((x, y) => x * y),
            PacketType.Minimum => SubPackets.MinBy(x => x.Value)!.Value,
            PacketType.Literal => Literal!.Value,
            PacketType.Maximum => SubPackets.MaxBy(x => x.Value)!.Value,
            PacketType.GreaterThan => SubPackets.First().Value > SubPackets.Last().Value ? 1 : 0,
            PacketType.LessThan => SubPackets.First().Value < SubPackets.Last().Value ? 1 : 0,
            PacketType.EqualTo => SubPackets.First().Value == SubPackets.Last().Value ? 1 : 0,
            _ => 0
        };
        public List<Packet> SubPackets { get; set; } = new();
    }

    private enum PacketType
    {
        Sum = 0,
        Product = 1,
        Minimum = 2,
        Maximum = 3,
        Literal = 4,
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7
    }
}
