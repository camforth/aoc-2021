using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public static class Day4
    {
        public static string Part1()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day4.txt");

            var input = lines[0].Split(",").Select(x => int.Parse(x)).ToArray();

            var boards = GetBoards(lines.Skip(2).ToArray());

            int counter = 0;
            var result = 0;

            do
            {
                var currentNumber = input[counter];
                foreach(var board in boards)
                {
                    
                    for (var i = 0; i < 5; i++)
                    {
                        var xCompleted = 0;
                        var yCompleted = 0;
                        for (var j = 0; j < 5; j++)
                        {
                            if (board[i, j] == null)
                            {
                                xCompleted++;
                            }
                            else if (board[i,j] == currentNumber)
                            {
                                board[i, j] = null;
                                xCompleted++;
                            }

                            if (board[j, i] == null)
                            {
                                yCompleted++;
                            }
                            else if (board[j, i] == currentNumber)
                            {
                                board[j, i] = null;
                                yCompleted++;
                            }
                        }
                        if (xCompleted == 5 || yCompleted == 5)
                        {
                            // Bingo
                            Console.WriteLine("Bingo!");
                            Console.WriteLine(string.Join(" ", board.Cast<int?>()));
                            // Calculate result
                            var sum = board.Cast<int?>().Sum(k => k ?? 0);
                            result = sum * currentNumber;
                            break;
                        }
                    }

                    
                }
                counter++;
            }
            while (result == 0);

            return result.ToString();
        }

        public static string Part2()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day4.txt");

            var input = lines[0].Split(",").Select(x => int.Parse(x)).ToArray();

            var boards = GetBoards(lines.Skip(2).ToArray());

            int counter = 0;
            var result = 0;
            var winners = new List<int?[,]>();
            var currentNumber = input[counter];

            do
            {
                currentNumber = input[counter];
                foreach (var board in boards)
                {

                    for (var i = 0; i < 5; i++)
                    {
                        var xCompleted = 0;
                        var yCompleted = 0;
                        for (var j = 0; j < 5; j++)
                        {
                            if (board[i, j] == null)
                            {
                                xCompleted++;
                            }
                            else if (board[i, j] == currentNumber)
                            {
                                board[i, j] = null;
                                xCompleted++;
                            }

                            if (board[j, i] == null)
                            {
                                yCompleted++;
                            }
                            else if (board[j, i] == currentNumber)
                            {
                                board[j, i] = null;
                                yCompleted++;
                            }
                        }
                        if (xCompleted == 5 || yCompleted == 5)
                        {
                            // Bingo
                            Console.WriteLine("Bingo!");
                            Console.WriteLine(string.Join(" ", board.Cast<int?>()));
                            winners.Add(board);
                        }
                    }

                    boards = boards.Where(x => !winners.Contains(x)).ToList();
                }
                counter++;
            }
            while (counter < input.Length && boards.Count > 0);

            var sum = winners.Last().Cast<int?>().Sum(k => k ?? 0);
            result = sum * currentNumber;

            return result.ToString();
        }

        private static List<int?[,]> GetBoards(string[] lines)
        {
            var boards = new List<int?[,]>();

            var counter = 0;
            do
            {
                var board = new int?[5,5];
                var rows = lines[counter..(counter + 5)];
                for (var i = 0; i < 5; i++)
                {
                    var row = rows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                    for (var j = 0; j < 5; j++)
                    {
                        board[i, j] = row[j];
                    }
                }
                boards.Add(board);
                counter += 6;
            }
            while (counter < lines.Length);

            return boards;
        }
    }
}
