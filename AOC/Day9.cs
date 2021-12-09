﻿namespace AOC
{
    public static class Day9
    {
        public static string Part1()
        {
            return GetSolution1().ToString();
        }

        public static string Part2()
        {
            return GetSolution2().ToString();
        }

        private static int GetSolution1()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day9.txt");

            var grid = CreateGrid(lines);

            var lowPoints = GetLowPoints(grid);

            var result = 0;
            foreach (var point in lowPoints)
            {
                Console.WriteLine($"Low point: {point.X}, {point.Y}");
                result += grid[point.X, point.Y] + 1;
            }

            return result;
        }

        private static int GetSolution2()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day9.txt");

            var grid = CreateGrid(lines);

            var lowPoints = GetLowPoints(grid);

            var sizes = new List<int>();
            foreach(var point in lowPoints)
            {
                var points = new List<Point>();
                sizes.Add(GetNeighbours(grid, point, points));
            }

            Console.WriteLine($"{string.Join(",", sizes)}");

            var top3 = sizes.OrderByDescending(o => o).Take(3);
            var result = 1;
            foreach(var item in top3)
            {
                result *= item;
            }

            return result;
        }

        private static int[,] CreateGrid(string[] lines)
        {
            var grid = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            return grid;
        }

        private static List<Point> GetLowPoints(int[,] grid)
        {
            var lowPoints = new List<Point>();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    var gridValue = grid[x,y];

                    var surroundingNumbers = new List<int>();

                    // left
                    if (!IsOutOfBound(x - 1, y, grid))
                    {
                        surroundingNumbers.Add(grid[x - 1,y]);
                    }

                    // up
                    if (!IsOutOfBound(x, y + 1, grid))
                    {
                        surroundingNumbers.Add(grid[x, y + 1]);
                    }

                    // right
                    if (!IsOutOfBound(x + 1, y, grid))
                    {
                        surroundingNumbers.Add(grid[x + 1, y]);
                    }

                    // down
                    if (!IsOutOfBound(x, y - 1, grid))
                    {
                        surroundingNumbers.Add(grid[x, y - 1]);
                    }

                    if (surroundingNumbers.All(x => x > gridValue))
                    {
                        lowPoints.Add(new(x,y));
                    }
                }
            }
            return lowPoints;
        }

        private static int GetNeighbours(int[,] grid, Point p0, List<Point> previous)
        {
            if (previous.Contains(p0)
                || IsOutOfBound(p0.X, p0.Y, grid)
                || grid[p0.X, p0.Y] > 8)
            {
                return 0;
            }

            var count = 1;

            var (x, y) = p0;

            previous.Add(p0);

            count += GetNeighbours(grid, new Point(x - 1, y), previous);
            count += GetNeighbours(grid, new Point(x, y + 1), previous);
            count += GetNeighbours(grid, new Point(x + 1, y), previous);
            count += GetNeighbours(grid, new Point(x, y - 1), previous);
            return count;
        }

        private static bool IsOutOfBound(int x, int y, int[,] grid)
        {
            try
            {
                _ = grid[x, y];
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
