using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day04.txt");
        static void Main()
        {
            var splits = input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Get list of bingo ball numbers to be called
            List<int> bingoBalls = splits[0].Split(',').Select(int.Parse).ToList();

            // Set up Bingo Boards
            List<BingoBoard> bingoBoards = splits.Skip(1).Select(b => new BingoBoard(b)).ToList();
            List<BingoBoard> completedBoards = new List<BingoBoard>();

            // Call each bingo ball in turn
            foreach (var ball in bingoBalls)
            {
                // Attempt to mark the number in each bingo board
                for (int i = 0; i < bingoBoards.Count; i++)
                {
                    int sumUnmarked = bingoBoards[i].MarkNumber(ball);
                    if (sumUnmarked > -1)
                    {
                        bingoBoards[i].Score = sumUnmarked * ball;
                        completedBoards.Add(bingoBoards[i]);
                    }
                }

                // Clear any completed boards from the still playing boards list
                bingoBoards = bingoBoards.Except(completedBoards).ToList();
            }

            Console.WriteLine($"Part 1: {completedBoards.First().Score}");
            Console.WriteLine($"Part 2: {completedBoards.Last().Score}");
        }
    }
}
