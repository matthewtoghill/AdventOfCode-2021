using System;
using System.IO;
using System.Text;

namespace Day16
{
    public class Program
    {
        private static readonly string input = File.ReadAllText(@"..\..\..\data\day16.txt");
        static void Main()
        {
            Packet packet = new Packet(HexToBinary(input));

            Console.WriteLine($"Part 1: {GetVersionSum(packet)}");
            Console.WriteLine($"Part 2: {packet.Value}");
        }

        private static int GetVersionSum(Packet packet)
        {
            int sum = packet.Version;
            foreach (Packet subpacket in packet.SubPackets)
                sum += GetVersionSum(subpacket);
            
            return sum;
        }


        #region Helpers
        private static string HexToBinary(string hex)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in hex)
                sb.Append(CharToBinary(c));
            
            return sb.ToString();
        }

        private static string CharToBinary(char c) => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
        #endregion
    }
}
