using AOC;

string? day;

do
{
    Console.WriteLine("Enter the day for the solution you would like to run and press enter:");
    day = Console.ReadLine();

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
        _ => AocHelpers.RunDay(day, () => "Invalid day")
    };

    Console.WriteLine($"Result: {result}");
} while (day != "quit");