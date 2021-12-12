using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day08.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            // Select the Length property of each string after the | character. Sum the total Counts where the Length is any of 2,3,4,7
            int total = input.ToList().Select(line => line.Split('|').Last().Split().Select(i => i.Length).Count(new[] { 2, 3, 4, 7 }.Contains)).Sum();
            Console.WriteLine($"Part 1: {total}");
        }

        private static void Part2()
        {
            int total = 0;
            foreach (var line in input)
            {
                var splits = line.Split('|');
                var signals = splits[0].Split();
                var output = splits[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Count the number of times each letter appears across the signals
                var letterCounts = new Dictionary<char, int>();
                foreach (var signal in signals)
                {
                    foreach (var c in signal)
                    {
                        letterCounts.IncrementAt(c);
                    }
                }

                // Create a new output string with each letter replaced with its appearance count
                string newOutput = "";
                foreach (var digit in output)
                {
                    int digitSum = 0;
                    foreach (var c in digit)
                    {
                        digitSum += letterCounts[c];
                    }

                    // Sum the values and convert the total to the digit as sum is unique
                    newOutput += ConvertScoreToDigit(digitSum);
                }

                total += int.Parse(newOutput);
            }

            Console.WriteLine($"Part 2: {total}");
        }

        private static int ConvertScoreToDigit(int score)
        {
            switch (score)
            {
                case 42: return 0;
                case 17: return 1;
                case 34: return 2;
                case 39: return 3;
                case 30: return 4;
                case 37: return 5;
                case 41: return 6;
                case 25: return 7;
                case 49: return 8;
                case 45: return 9;
                default: return -1;
            }
        }
    }

    public static class DictionaryExtensions
    {
        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T key)
        {
            dictionary.TryGetValue(key, out int value);
            dictionary[key] = ++value;
        }
    }
}
