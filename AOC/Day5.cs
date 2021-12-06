using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public static class Day5
    {
        public static string Part1()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day5.txt");

            var grid = new int[1000, 1000];
            var intersections = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = GetLine(lines[i]);
                var newIntersections = AddLineToGrid(grid, line, true);
                intersections += newIntersections;
            }

            Console.WriteLine($"Intersections: {intersections}");

            OutputGridToFile(grid);

            var result = intersections;

            return result.ToString();
        }

        public static string Part2()
        {
            var lines = AocHelpers.ReadInputsAsString("input-day5.txt");

            var grid = new int[1000, 1000];
            var intersections = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = GetLine(lines[i]);
                var newIntersections = AddLineToGrid(grid, line, false);
                intersections += newIntersections;
            }

            Console.WriteLine($"Intersections: {intersections}");

            OutputGridToFile(grid);

            var result = intersections;

            return result.ToString();
        }

        public static int AddLineToGrid(int[,] grid, Line line, bool horizontalOrVerticalOnly)
        {
            int newIntersections = 0;

            var (p1, p2) = line;

            if (horizontalOrVerticalOnly && !(p1.X == p2.X || p1.Y == p2.Y ))
            {
                return newIntersections;
            }

            var (x1, y1) = p1;
            var (x2, y2) = p2;

            // Bresenham algorithm
            //int deltaX = Math.Abs(x2 - x1);
            //int signX = x1 < x2 ? 1 : -1;
            //int deltaY = Math.Abs(y2 - y1);
            //int signY = y1 < y2 ? 1 : -1;
            //int err = (deltaX > deltaY ? deltaX : -deltaY) / 2;
            //int e2;
            //for (; ; )
            //{
            //    grid[x1, y1]++;
            //    if (grid[x1, y1] == 2)
            //    {
            //        newIntersections++;
            //    }
            //    if (x1 == x2 && y1 == y2)
            //    {
            //        break;
            //    }
            //    e2 = err;
            //    if (e2 > -deltaX)
            //    {
            //        err -= deltaY;
            //        x1 += signX;
            //    }
            //    if (e2 < deltaY)
            //    {
            //        err += deltaX;
            //        y1 += signY;
            //    }
            //}

            // My solution that only works for horizontal, vertical and 45 degree diagonals
            var yStep = y1 == y2 ? 0 : (y1 < y2 ? 1 : -1);
            var xStep = x1 == x2 ? 0 : (x1 < x2 ? 1 : -1);

            for (; ; )
            {
                grid[x1, y1]++;
                if (grid[x1, y1] == 2)
                {
                    newIntersections++;
                }
                if (x1 == x2 && y1 == y2)
                {
                    break;
                }
                x1 += xStep;
                y1 += yStep;
            }

            return newIntersections;
        }

        public static Line GetLine(string line)
        {
            var points = line.Split(" -> ");
            var p1 = points[0].Split(",");
            var p2 = points[1].Split(",");
            return new Line(new Point(int.Parse(p1[0]), int.Parse(p1[1])),
                new Point(int.Parse(p2[0]), int.Parse(p2[1])));
        }

        public static void OutputGridToFile(int[,] grid)
        {
            var str = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    str.Append(grid[i, j]);
                }
                str.Append("\n");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "output-day5.txt");
            File.WriteAllText(path, str.ToString());
        }

        //public static void OutputGridToBitmap(int[,] grid)
        //{
        //    var bitmap = new Bitmap(1000, 1000);
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        for (int j = 0; j < 1000; j++)
        //        {
        //            var color = grid[i, j] > 0 ? Color.Black : Color.White;
        //            bitmap.SetPixel(i, j, color);
        //        }
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "output-day5.bmp");
        //    bitmap.Save(path);
        //}
    }

    public record struct Point(int X, int Y);
    public record struct Line(Point A, Point B);
}
