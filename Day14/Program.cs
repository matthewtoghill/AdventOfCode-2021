using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day14.txt");
        static void Main()
        {
            var mainSplits = input.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string template = mainSplits[0];
            var ruleInstructions = mainSplits[1].Split(new[] {"\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Create dictionary of element insertion rules
            var rules = new Dictionary<string, string>();
            foreach (var rule in ruleInstructions.Select(r => r.Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries)))
                rules.Add(rule[0], rule[1]);

            Console.WriteLine($"Part 1: {Solve(rules, template, 10)}");
            Console.WriteLine($"Part 2: {Solve(rules, template, 40)}");
        }

        private static long Solve(Dictionary<string,string> rules, string polymerTemplate, int steps)
        {
            var polymer = CreateNewPolymer(rules, polymerTemplate, steps);

            // Total the element counts using the first element of each pair
            var elementCounts = new Dictionary<char,long>();
            foreach (var pair in polymer)
                elementCounts.IncrementAt(pair.Key[0], pair.Value);

            // Increase the count of the last element from the polymer template by 1
            // as it was missed off once by only counting the first element of each pair
            elementCounts.IncrementAt(polymerTemplate.Last());

            // Return the difference between the most and least common elements
            return elementCounts.Values.Max() - elementCounts.Values.Min();
        }

        
        private static Dictionary<string, long> CreateNewPolymer(Dictionary<string,string> rules, string polymerTemplate, int steps)
        {
            var pairCounts = new Dictionary<string, long>();
            
            // Create initial pair counts collection using each element pair in the polymer template
            for (int i = 0; i < polymerTemplate.Length - 1; i++)
                pairCounts.IncrementAt(polymerTemplate.Substring(i, 2));

            // Run for the required number of steps
            for (int s = 0; s < steps; s++)
            {
                var updatedCounts = new Dictionary<string, long>();
                foreach (var pair in pairCounts)
                {
                    // Set new counts by applying element insertion rule to create 2 new pairs
                    // e.g. if pair 'NC' has rule 'NC -> B' then insert B element between N and C to become new pairs NB and BC
                    // if the number of NC pairs in the polymer was 10 then counts of both NB and BC pairs will be increased by 10
                    updatedCounts.IncrementAt($"{pair.Key[0]}{rules[pair.Key]}", pair.Value);
                    updatedCounts.IncrementAt($"{rules[pair.Key]}{pair.Key[1]}", pair.Value);
                }
                pairCounts = updatedCounts;
            }
            return pairCounts;
        }
    }

    public static class Helpers
    {
        public static void IncrementAt<T>(this Dictionary<T, long> dictionary, T index)
        {
            dictionary.TryGetValue(index, out long count);
            dictionary[index] = ++count;
        }

        public static void IncrementAt<T>(this Dictionary<T, long> dictionary, T index, long incrementAmount)
        {
            dictionary.TryGetValue(index, out long count);
            dictionary[index] = count + incrementAmount;
        }
    }
}
