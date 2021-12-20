using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day12.txt");
        private static readonly Dictionary<string, List<string>> rooms = new Dictionary<string, List<string>>();
        static void Main()
        {
            // Build the list of rooms (keys) and connections (values)
            foreach (var splitLine in input.Select(line => line.Split('-')))
            {
                if (!rooms.ContainsKey(splitLine[0])) rooms.Add(splitLine[0], new List<string>());
                rooms[splitLine[0]].Add(splitLine[1]);

                if (!rooms.ContainsKey(splitLine[1])) rooms.Add(splitLine[1], new List<string>());
                rooms[splitLine[1]].Add(splitLine[0]);
            }

            Console.WriteLine($"Part 1: {CountPaths()}");
            Console.WriteLine($"Part 2: {CountPaths(allowTwice: true)}");
        }

        private static int CountPaths(string current = "start", string visited = "", bool allowTwice = false)
        {
            int numPaths = 0;
            // Add any small rooms to visited - don't need to track big rooms
            if (current.All(char.IsLower)) visited += $",{current}";

            // Iterate through the list of rooms that current is connected to
            foreach (var room in rooms[current])
                if (room == "end") // If the end is reached then count a completed path
                    numPaths++;
                else if (room != "start")
                    if (!visited.Contains(room)) // If the room is not visited (big or unseen) then explore further
                        numPaths += CountPaths(room, visited, allowTwice);
                    else if (allowTwice) // else small and seen before but allow twice flag is true, explore further
                        numPaths += CountPaths(room, visited, false);

            return numPaths;
        }
    }
}
