using System;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program
    {
        private static readonly int[] input = File.ReadAllLines(@"..\..\..\data\day01.txt").Select(int.Parse).ToArray();
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            int increaseCount = 0;

            for (int i = 1; i < input.Length; i++)
                if (input[i] > input[i - 1])
                    increaseCount++;

            Console.WriteLine($"Part 1: {increaseCount}");
        }

        private static void Part2()
        {
            int increaseCount = 0;

            for (int i = 3; i < input.Length; i++)
            {
                int groupA = input[i - 3] + input[i - 2] + input[i - 1];
                int groupB = input[i - 2] + input[i - 1] + input[i];

                if (groupB > groupA)
                    increaseCount++;
            }

            Console.WriteLine($"Part 2: {increaseCount}");
        }
    }
}
