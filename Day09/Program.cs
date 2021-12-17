using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day09.txt");
        private static readonly List<(int, int)> directions = new List<(int, int)>() { (-1, 0), (0, -1), (1, 0), (0, 1) };
        
        static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var heightMap = CreateHeightMap(input);
            List<(int X, int Y)> lowPoints = GetLowPoints(heightMap);
            int sum = lowPoints.Select(p => heightMap[p.X, p.Y] + 1).Sum();
            Console.WriteLine($"Part 1: {sum}");
        }

        private static void Part2()
        {
            // Find all low points, these will be used as the start points for each basin
            var heightMap = CreateHeightMap(input);
            List<(int, int)> lowPoints = GetLowPoints(heightMap);
            
            List<int> basinSizes = new List<int>();

            // For each Low Point, use Breadth First Search (BFS) to find all points in the relevant basin
            foreach (var lowPoint in lowPoints)
            {
                Queue<(int X, int Y)> frontier = new Queue<(int, int)>();
                frontier.Enqueue(lowPoint);
                HashSet<(int X, int Y)> reached = new HashSet<(int, int)> { lowPoint };

                while (frontier.Count > 0)
                {
                    // Take the next position from the frontier queue
                    (int X, int Y) = frontier.Dequeue();

                    // Get neighbouring positions in the basin that are not 9's
                    var neighbours = new List<(int, int)>();
                    foreach ((int dX, int dY) in directions)
                    {
                        try { 
                            if (heightMap[X + dX, Y + dY] != 9)
                                neighbours.Add((X + dX, Y + dY)); 
                        } catch (IndexOutOfRangeException) { }
                    }

                    // Check each neighbour and add to frontier and reached collections if new
                    foreach (var n in neighbours.Except(reached))
                    {
                        if (!reached.Contains(n))
                        {
                            frontier.Enqueue(n);
                            reached.Add(n);
                        }
                    }
                }

                // Add the number of positions within the basin to the basin sizes list
                basinSizes.Add(reached.Count);
            }

            // Sort the basin sizes and find the product of the top 3
            var productTop3 = basinSizes.OrderByDescending(b => b).Take(3).Aggregate((total, next) => total * next);
            Console.WriteLine($"Part 2: {productTop3}");
        }

        private static int[,] CreateHeightMap(string[] input)
        {
            int rows = input.Length;
            int cols = input[0].Length;
            int[,] heightMap = new int[rows, cols];
            
            for (int i = 0; i < rows; i++)
                for(int j = 0; j < cols; j++)
                    heightMap[i, j] = int.Parse(input[i][j].ToString());

            return heightMap;
        }

        private static List<(int,int)> GetLowPoints(int[,] heightMap)
        {
            var lowPoints = new List<(int, int)>();
            int rows = heightMap.GetLength(0);
            int cols = heightMap.GetLength(1);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    int thisPoint = heightMap[x, y];
                    var neighbours = new List<int>();

                    foreach ((int dX, int dY) in directions) {
                        try { 
                            neighbours.Add(heightMap[x + dX, y + dY]); 
                        } catch (IndexOutOfRangeException) { }
                    }

                    if (thisPoint < neighbours.Min()) lowPoints.Add((x, y));
                }
            }

            return lowPoints;
        }
    }
}
