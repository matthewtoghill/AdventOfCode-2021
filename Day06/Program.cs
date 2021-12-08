using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day06.txt");
        static void Main()
        {
            // Group fish by their timers, store in array where index = the total fish with that timer value
            long[] fish = new long[9];
            input.Split(',').Select(int.Parse).GroupBy(f => f).ToList().ForEach(i => fish[i.Key] = i.Count());

            // Part 1:
            var partOneFish = SimulateDays(fish, 80);
            Console.WriteLine($"Part 1: {partOneFish.Sum()}");

            // Part 2:
            var partTwoFish = SimulateDays(fish, 256);
            Console.WriteLine($"Part 1: {partTwoFish.Sum()}");
        }

        private static long[] SimulateDays(long[] fishCounts, int days)
        {
            for (int day = 0; day < days; day++)
            {
                long[] newFish = new long[9];
                for (int i = 0; i < 8; i++)
                {
                    newFish[i] = fishCounts[i + 1]; // Reduce all fish timers by 1 day
                }

                newFish[8] = fishCounts[0];  // Spawn new fish with 8 day timers
                newFish[6] += fishCounts[0]; // Move fish with 0 day timers to 6 day timer group

                fishCounts = newFish.ToArray();
            }

            return fishCounts;
        }
    }
}
