using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class Packet
    {
        public int Version { get; set; }
        public int TypeID { get; set; }
        public long Value { get; set; }
        public List<Packet> SubPackets { get; set; } = new List<Packet>();
        public int BitsRead { get; set; }

        public Packet(string packetBinary)
        {
            Version = BinaryToInt(packetBinary.Substring(0, 3));
            TypeID = BinaryToInt(packetBinary.Substring(3, 3));
            BitsRead = 6;

            if (TypeID == 4)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = BitsRead; i < packetBinary.Length; i += 5)
                { 
                    sb.Append(packetBinary.Substring(i + 1, 4));
                    BitsRead += 5;
                    if (packetBinary[i] == '0') break;
                }

                Value = BinaryToLong(sb.ToString());
            }
            else
            {
                if (packetBinary.Substring(BitsRead++, 1) == "0")
                {
                    int subPacketsLength = BinaryToInt(packetBinary.Substring(BitsRead, 15));
                    BitsRead += 15;
                    int subBitsRead = 0;
                    while (subBitsRead < subPacketsLength)
                    {
                        Packet sub = new Packet(packetBinary.Substring(BitsRead, subPacketsLength - subBitsRead));
                        subBitsRead += sub.BitsRead;
                        BitsRead += sub.BitsRead;
                        SubPackets.Add(sub);
                    }
                }
                else
                {
                    int subPacketsCount = BinaryToInt(packetBinary.Substring(BitsRead, 11)); ;
                    BitsRead += 11;
                    while (SubPackets.Count < subPacketsCount)
                    {
                        Packet sub = new Packet(packetBinary.Substring(BitsRead));
                        BitsRead += sub.BitsRead;
                        SubPackets.Add(sub);
                    }
                }
            }
        }

        private static long BinaryToLong(string binary) => Convert.ToInt64(binary.ToString(), 2);
        private static int BinaryToInt(string binary) => Convert.ToInt32(binary.ToString(), 2);
    }
}
