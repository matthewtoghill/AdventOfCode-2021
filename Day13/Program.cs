using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day13.txt");
        static void Main()
        {
            var mainSplits = input.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var dotInstructions = mainSplits[0].Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Split(','));
            var foldInstructions = mainSplits[1].Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var dots = new HashSet<(int X, int Y)>();

            foreach (var dot in dotInstructions)
                dots.Add((int.Parse(dot[0]), int.Parse(dot[1])));

            // Complete the first fold and count the number of dots
            dots = FoldPaper(dots, foldInstructions[0]);
            Console.WriteLine($"Part 1: {dots.Count}");

            // Complete the rest of the folds
            for (int i = 1; i < foldInstructions.Length; i++)
            {
                dots = FoldPaper(dots, foldInstructions[i]);
            }

            // Create the grid from the set of dot co-ords
            int xMax = dots.Max(d => d.X) + 1;
            int yMax = dots.Max(d => d.Y) + 1;
            var grid = new bool[xMax, yMax];

            foreach ((int X, int Y) in dots)
            {
                grid[X, Y] = true;
            }

            // Print the grid
            Console.WriteLine($"Part 2:");
            for (int y = 0; y < yMax; y++)
            {
                string line = "";
                for (int x = 0; x < xMax; x++)
                {
                    line += grid[x, y] ? "#" : " ";
                }
                Console.WriteLine(line);
            }
        }

        private static HashSet<(int X,int Y)> FoldPaper(HashSet<(int X, int Y)> dots, string instruction)
        {
            var newDots = new HashSet<(int X, int Y)>();
            var instructionSplit = instruction.Split(new[] { "fold along ", "=" }, StringSplitOptions.RemoveEmptyEntries);
            string foldOn = instructionSplit[0];
            int line = int.Parse(instructionSplit[1]);

            foreach ((int X, int Y) in dots)
            {
                // Set new X and Y values where position is within folded section
                // New position calculated as the fold line less the gap between the position and the fold line
                int newX = foldOn == "x" && X > line ? (line - (X - line)) : X;
                int newY = foldOn == "y" && Y > line ? (line - (Y - line)) : Y;
                newDots.Add((newX, newY));
            }

            return newDots;
        }
    }
}
