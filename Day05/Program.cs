using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day05
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day05.txt");
        static void Main()
        {
            // Setup
            var points = new List<(int x1, int y1, int x2, int y2)>();
            foreach (var line in input)
            {
                var splits = line.Split(new[] { "->", " ", "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                points.Add((splits[0], splits[1], splits[2], splits[3]));
            }

            // Part 1
            Dictionary<(int x, int y), int> pointsCrossed = GetPointsCrossedByLines(points, false);
            Console.WriteLine($"Part 1: {pointsCrossed.Count(p => p.Value > 1)}");

            // Part 2
            pointsCrossed = GetPointsCrossedByLines(points, true);
            Console.WriteLine($"Part 2: {pointsCrossed.Count(p => p.Value > 1)}");
        }

        private static Dictionary<(int x, int y), int> GetPointsCrossedByLines(List<(int x1, int y1, int x2, int y2)> lineCoords, bool includeDiagonals)
        {
            var pointsCrossed = new Dictionary<(int x, int y), int>();

            foreach (var (x1, y1, x2, y2) in lineCoords)
            {
                // Diagonal Line
                if (includeDiagonals && Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
                {
                    int stepX = x1 < x2 ? 1 : -1;
                    int stepY = y1 < y2 ? 1 : -1;

                    for (int i = 0; i <= Math.Abs(x1 - x2); i++)
                    {
                        pointsCrossed.IncrementAt((x1 + i * stepX, y1 + i * stepY));
                    }

                    continue;
                }

                // Vertical Line
                if (x1 == x2)
                {
                    int startY = Math.Min(y1, y2);
                    int endY = Math.Max(y1, y2);

                    for (int y = startY; y <= endY; y++)
                    {
                        pointsCrossed.IncrementAt((x1, y));
                    }
                }
                // Horizontal Line
                else if (y1 == y2)
                {
                    int startX = Math.Min(x1, x2);
                    int endX = Math.Max(x1, x2);

                    for (int x = startX; x <= endX; x++)
                    {
                        pointsCrossed.IncrementAt((x, y1));
                    }
                }
            }
            return pointsCrossed;
        }
    }

    public static class Utilities
    {
        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T key)
        {
            dictionary.TryGetValue(key, out int value);
            dictionary[key] = ++value;
        }
    }
}
