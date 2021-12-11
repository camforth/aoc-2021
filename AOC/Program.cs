global using System.Linq;
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
    var dayFunc = AocHelpers.GetDayRunFunc(day);
    var result = AocHelpers.RunDay(day, dayFunc);
    Console.WriteLine($"Result: {result}");
}
