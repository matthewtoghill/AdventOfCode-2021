using System;
using System.IO;

namespace Day02
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day02.txt");
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            // Position = Height, Depth
            (int h, int d) position = (0, 0);

            // Iterate through each line of the input
            foreach (var line in input)
            {
                // Split the line and set direction and distance values
                var command = line.Split();
                string direction = command[0];
                int distance = int.Parse(command[1]);

                // Update position based on the direction of the current command
                switch (direction)
                {
                    case "forward":
                        position.h += distance;
                        break;
                    case "down":
                        position.d += distance;
                        break;
                    case "up":
                        position.d -= distance;
                        break;
                    default:
                        break;
                }
            }

            // Result as Height x Depth
            Console.WriteLine($"Part 1: {position.h * position.d}");
        }

        private static void Part2()
        {
            // Position = Height, Depth, Aim 
            (int h, int d, int a) position = (0, 0, 0);

            // Iterate through each line of the input
            foreach (var line in input)
            {
                // Split the line and set direction and distance values
                var command = line.Split();
                string direction = command[0];
                int distance = int.Parse(command[1]);

                // Update position based on the direction of the current command
                switch (direction)
                {
                    case "forward":
                        position.h += distance;
                        position.d += (position.a * distance);
                        break;
                    case "down":
                        position.a += distance;
                        break;
                    case "up":
                        position.a -= distance;
                        break;
                    default:
                        break;
                }
            }

            // Result as Height x Depth
            Console.WriteLine($"Part 2: {position.h * position.d}");
        }
    }
}
