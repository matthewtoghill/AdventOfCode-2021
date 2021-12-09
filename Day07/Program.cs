using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day07
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day07.txt");
        static void Main()
        {
            var crabs = input.Split(',').Select(int.Parse).ToArray();

            Part1(crabs);
            Part2(crabs);
        }

        private static void Part1(int[] crabs)
        {
            List<int> fuelCosts = new List<int>();

            // Check each position, get the total fuel cost for each crab to move to that position
            for (int i = crabs.Min(); i <= crabs.Max(); i++)
            {
                // Fuel cost for each crab is the absolute difference between start and end positions
                fuelCosts.Add(crabs.Sum(c => Math.Abs(c - i)));
            }

            Console.WriteLine($"Part 1: {fuelCosts.Min()}");
        }

        private static void Part2(int[] crabs)
        {
            List<int> fuelCosts = new List<int>();

            for (int i = crabs.Min(); i <= crabs.Max(); i++)
            {
                // Fuel cost for each crab is the sum of the numbers from 1 to the absolute difference
                // i.e. the triangle number sequence for the difference calculated as n(n+1)/2
                fuelCosts.Add(crabs.Select(c => Math.Abs(c - i)).Sum(n => n * (n + 1) / 2));
            }

            Console.WriteLine($"Part 2: {fuelCosts.Min()}");
        }
    }
}
