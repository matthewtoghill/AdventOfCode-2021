using System;
using System.IO;
using System.Collections.Generic;

namespace Day11
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day11.txt");
        private static readonly List<(int, int)> directions = new List<(int, int)>() { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
        static void Main()
        {
            int flashes = 0;        // Total count of flashes
            int step = 0;           // Current step value
            int part1Steps = 100;   // Steps to run for Part 1

            var grid = CreateGrid(input);

            while (true)
            {
                step++;

                // Increase the energy level of each octopus
                for (int x = 0; x < 10; x++)
                    for (int y = 0; y < 10; y++)
                        grid[x, y]++;

                // Check if any energy levels are > 9
                while (true)
                {
                    int countOver9 = 0;

                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            // If the octopus has an energy level over 9
                            if (grid[x, y] > 9)
                            {
                                // Check all adjacent, increase their energy levels (unless already flashed)
                                foreach ((int dX, int dY) in directions)
                                {
                                    try 
                                    {
                                        int pX = x + dX;
                                        int pY = y + dY;

                                        if (grid[pX, pY] > 0)
                                        {
                                            grid[pX, pY]++;
                                            if (grid[pX, pY] > 9) countOver9++;
                                        }
                                    } catch (IndexOutOfRangeException) { }
                                }

                                // Flash and reset energy level to 0
                                flashes++;
                                grid[x, y] = 0;
                            }

                        }
                    }

                    // Stop the loop if there are none left with an energy level over 9 to still flash
                    if (countOver9 == 0) break;
                }

                // Part 1
                if (step == part1Steps) Console.WriteLine($"Part 1: {flashes}");

                // Part 2
                if (AllFlashed(grid))
                {
                    Console.WriteLine($"Part 2: {step}");
                    break;
                }
            }
        }

        private static int[,] CreateGrid(string[] input)
        {
            int[,] grid = new int[10,10];
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    grid[x,y] = int.Parse(input[x][y].ToString());

            return grid;
        }

        private static bool AllFlashed(int[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0;y < grid.GetLength(1); y++)
                    if (grid[x, y] != 0) return false;

            return true;
        }
    }
}
