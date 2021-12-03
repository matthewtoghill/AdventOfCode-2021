using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day03.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            // Create a collection of dictionaries to hold the count of each character found
            // Each dictionary represents a different column (index) from the input data
            var columnCharacterFrequencies = new List<Dictionary<char, int>>();
            for (int i = 0; i < input[0].Length; i++)
                columnCharacterFrequencies.Add(new Dictionary<char, int>());

            foreach (var line in input)
            {
                // Check each character from the line
                for (int i = 0; i < line.Length; i++)
                {
                    // Update the dictionary for column i by incrementing the value for the character key
                    columnCharacterFrequencies[i].IncrementAt(line[i]);
                }
            }

            string maxValString = "";
            string minValString = "";

            // Check each column dictionary, get the Keys based on the Max and Min Values
            foreach (var item in columnCharacterFrequencies)
            {
                // Create a string of the Max Value per column dictionary
                maxValString += item.OrderByDescending(d => d.Value).First().Key;

                // Create a string of the Min Value per column dictionary
                minValString += item.OrderBy(d => d.Value).First().Key;
            }

            // Convert from Binary to Decimal to get Gamma and Epsilon values
            int gamma = Convert.ToInt32(maxValString, 2);
            int epsilon = Convert.ToInt32(minValString, 2);

            // Power Consumption calculated as Gamma * Epsilon rates
            Console.WriteLine($"Part 1: {gamma * epsilon}");
        }

        private static void Part2()
        {
            // Get the Oxygen and Scrubber Rating binary strings then Convert from Binary to Decimal
            int oxygen = Convert.ToInt32(GetOxygenRating(), 2);
            int scrubber = Convert.ToInt32(GetScrubberRating(), 2);

            // Life Support Rating calculated as Oxygen * CO2 Scrubber ratings
            Console.WriteLine($"Part 2: {oxygen * scrubber}");
        }

        private static string GetOxygenRating()
        {
            // Create a new list from the input array
            var possibleOxygenRatings = input.ToList();

            // Iterate through each index ('column') of the list
            for (int i = 0; i < input[0].Length; i++)
            {
                // Create and populate a dictionary of character count frequencies at the current index of each line in the list
                Dictionary<char, int> frequencies = new Dictionary<char, int>();
                possibleOxygenRatings.ForEach(line => frequencies.IncrementAt(line[i]));

                // Check the dictionary contains a count for both keys 0 and 1
                frequencies.TryGetValue('0', out int count0);
                frequencies.TryGetValue('1', out int count1);

                // Set the bit value to keep as the most frequently counted value
                // If there is a tie, then the bit to keep is 1
                char keepBit = count1 >= count0 ? '1' : '0';

                // Create a new list of possible ratings where the char at the current column matches the bit to keep
                possibleOxygenRatings = possibleOxygenRatings.Where(b => b[i] == keepBit).ToList();
            }

            return possibleOxygenRatings[0];
        }

        private static string GetScrubberRating()
        {
            // Create a new list from the input array
            var possibleScrubberRatings = input.ToList();

            // Iterate through each index ('column') of the list
            for (int i = 0; i < input[0].Length; i++)
            {
                // Create and populate a dictionary of character count frequencies at the current index of each line in the list
                Dictionary<char, int> frequencies = new Dictionary<char, int>();
                possibleScrubberRatings.ForEach(line => frequencies.IncrementAt(line[i]));

                // Check the dictionary contains a count for both keys 0 and 1
                // If not found then set the count for that key to the max int value
                frequencies.TryGetValue('0', out int count0);
                frequencies.TryGetValue('1', out int count1);
                if (count0 == 0) count0 = int.MaxValue;
                if (count1 == 0) count1 = int.MaxValue;

                // Set the bit value to keep as the least frequently counted value
                // If there is a tie, then the bit to keep is 0
                char keepBit = count0 <= count1 ? '0' : '1';

                // Create a new list of possible ratings where the char at the current column matches the bit to keep
                possibleScrubberRatings = possibleScrubberRatings.Where(b => b[i] == keepBit).ToList();
            }

            return possibleScrubberRatings[0];
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
