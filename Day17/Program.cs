using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    public class Program
    {
        public static readonly (int minX, int maxX, int minY, int maxY) targetRange = (57, 116, -198, -148);
        static void Main()
        {
            List<Probe> probes = new List<Probe>();
            for (int x = 0; x <= targetRange.maxX; x++)
            {
                for (int y = Math.Abs(targetRange.minY); y >= targetRange.minY; y--)
                {
                    Probe p = new Probe((x, y), targetRange);
                    if (p.Success) probes.Add(p);
                }
            }

            int maxHeight = probes.Select(p => p.Steps.Max(s => s.Y)).Max();
            Console.WriteLine($"Part 1: {maxHeight}");
            Console.WriteLine($"Part 2: {probes.Count}");
        }
    }

    public static class Helpers
    {
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
        }
    }
}
