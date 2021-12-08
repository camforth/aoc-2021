using AOC;

if (args.Any() && !string.IsNullOrEmpty(args[0])) RunDay(args[0]);

string? day;
do
{
    Console.WriteLine("\nEnter the day for the solution you would like to run and press enter:");
    day = Console.ReadLine();
    RunDay(day);
} while (day != "quit");

static void RunDay(string? day)
{
    var result = day switch
    {
        "1a" => AocHelpers.RunDay(day, () => Day1.Part1()),
        "1b" => AocHelpers.RunDay(day, () => Day1.Part2()),
        "2a" => AocHelpers.RunDay(day, () => Day2.Part1()),
        "2b" => AocHelpers.RunDay(day, () => Day2.Part2()),
        "3a" => AocHelpers.RunDay(day, () => Day3.Part1()),
        "3b" => AocHelpers.RunDay(day, () => Day3.Part2()),
        "4a" => AocHelpers.RunDay(day, () => Day4.Part1()),
        "4b" => AocHelpers.RunDay(day, () => Day4.Part2()),
        "5a" => AocHelpers.RunDay(day, () => Day5.Part1()),
        "5b" => AocHelpers.RunDay(day, () => Day5.Part2()),
        "6a" => AocHelpers.RunDay(day, () => Day6.Part1()),
        "6b" => AocHelpers.RunDay(day, () => Day6.Part2()),
        "7a" => AocHelpers.RunDay(day, () => Day7.Part1()),
        "7b" => AocHelpers.RunDay(day, () => Day7.Part2()),
        "8a" => AocHelpers.RunDay(day, () => Day8.Part1()),
        "8b" => AocHelpers.RunDay(day, () => Day8.Part2()),
        "9a" => AocHelpers.RunDay(day, () => Day9.Part1()),
        "9b" => AocHelpers.RunDay(day, () => Day9.Part2()),
        _ => AocHelpers.RunDay(day, () => "Invalid day")
    };

    Console.WriteLine($"Result: {result}");
}