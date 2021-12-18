using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day10.txt");
        private static readonly char[] openers = new char[] { '(', '[', '{', '<' };
        private static readonly char[] closers = new char[] { ')', ']', '}', '>' };
        static void Main()
        {
            var incompleteLines = Part1();
            Part2(incompleteLines);
        }

        private static List<string> Part1()
        {
            int totalScore = 0;

            List<string> incompleteLines = new List<string>();

            foreach (var line in input)
            {
                bool isCorrupt = false;
                List<char> cList = line.ToCharArray().ToList();
                for (int i = 1; i < cList.Count; i++)
                {
                    // if the char is a closing character
                    if (closers.Contains(cList[i])) 
                    {
                        // If the previous char is a matching opener
                        if (cList[i - 1] == openers[Array.IndexOf(closers, cList[i])])
                        {
                            // Remove the pair and step the iterator back 2 places
                            cList.RemoveRange(i - 1, 2);
                            i -= 2;
                        }
                        else // is an invalid closing char
                        {
                            // Get the error score for the invalid char
                            totalScore += GetSyntaxErrorScore(cList[i]);
                            isCorrupt = true;
                            break;
                        }
                    }
                }

                // Store incomplete lines
                if (!isCorrupt && cList.Count > 0)
                {
                    incompleteLines.Add(new string(cList.ToArray()));
                }
            }

            Console.WriteLine($"Part 1: {totalScore}");
            return incompleteLines;
        }

        private static void Part2(List<string> incompleteLines)
        {
            var scores = new List<long>();
            foreach (var line in incompleteLines)
            {
                // Calculate the score for completing the line
                long score = 0;
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    score = score * 5 + Array.IndexOf(openers, line[i]) + 1;
                }
                
                scores.Add(score);
            }

            // Sort the scores and output the middle value
            scores.Sort();
            Console.WriteLine($"Part 2: {scores[scores.Count / 2]}");
        }

        private static int GetSyntaxErrorScore(char c)
        {
            switch (c)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
                default: return 0;
            }
        }
    }
}
