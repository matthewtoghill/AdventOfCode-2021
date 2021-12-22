using System;
using System.IO;
using System.Collections.Generic;
using Priority_Queue;

namespace Day15
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day15.txt");
        private static readonly List<(int, int)> directions = new List<(int, int)>() { (0, 1), (1, 0), (-1, 0), (0, -1) };
        static void Main()
        {
            // Part 1:
            var grid = CreateGrid(input);
            (int X, int Y) endPos = (grid.GetLength(0) - 1, grid.GetLength(1) - 1);
            Console.WriteLine($"Part 1: {FindCostToGoal(grid, (0, 0), endPos)}");

            // Part 2: Scale up the grid and then find cost to goal
            var scaledGrid = ScaleGrid(grid, 5);
            endPos = (scaledGrid.GetLength(0) - 1, scaledGrid.GetLength(1) - 1);
            Console.WriteLine($"Part 2: {FindCostToGoal(scaledGrid, (0, 0), endPos)}");
        }

        private static int FindCostToGoal(int[,] grid, (int X, int Y) start, (int X, int Y) goal)
        {
            var frontier = new SimplePriorityQueue<(int X, int Y)>();
            frontier.Enqueue(start, 0);
            var costSoFar = new Dictionary<(int X, int Y), int> { [start] = 0 };

            while (frontier.Count > 0)
            {
                (int cX, int cY) = frontier.Dequeue();

                // Exit early if the goal location has been reached
                if ((cX, cY) == goal) break;

                // Get neighbour positions
                var neighbours = new List<(int X, int Y)>();
                foreach ((int dX, int dY) in directions)
                    neighbours.Add((cX + dX, cY + dY));

                // Check each neighbour 
                foreach ((int nX, int nY) in neighbours)
                {
                    try 
                    {   // Get the cost to reach the neighbour position
                        int cost = costSoFar[(cX, cY)] + grid[nX, nY];

                        // If the position hasn't been reached yet, or cost is less than others recorded for position
                        if (!costSoFar.ContainsKey((nX, nY)) || cost < costSoFar[(nX, nY)])
                        {
                            // Update the value for reaching the position
                            costSoFar[(nX, nY)] = cost;

                            // Add to Priority Queue with priority score and distance from goal heuristic
                            frontier.Enqueue((nX, nY), cost + (Math.Abs(goal.X - cX) + Math.Abs(goal.Y - cY)));
                        }
                    } catch (IndexOutOfRangeException) { }
                }
            }

            return costSoFar[goal];
        }

        private static int[,] CreateGrid(string[] input)
        {
            int rows = input.Length;
            int cols = input[0].Length;
            int[,] grid = new int[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i, j] = int.Parse(input[i][j].ToString());

            return grid;
        }

        private static int[,] ScaleGrid(int[,] grid, int scale = 1)
        {
            int gridRows = grid.GetLength(0);
            int gridCols = grid.GetLength(1);
            int rows = gridRows * scale;
            int cols = gridCols * scale;
            int[,] newGrid = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    int row = y % gridRows;
                    int col = x % gridCols;
                    int distance = (y / gridRows) + (x / gridCols);
                    int riskVal = (grid[row,col] + distance - 1) % 9 + 1;
                    newGrid[y, x] = riskVal;
                }
            }
            return newGrid;
        }
    }
}
