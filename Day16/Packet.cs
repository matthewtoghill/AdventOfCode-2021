using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day16
{
    public enum PacketTypeID
    {
        Sum = 0,
        Product = 1,
        Min = 2,
        Max = 3,
        Literal = 4,
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7
    }

    public class Packet
    {
        public int Version { get; private set; }
        public long Value { get; private set; }
        public List<Packet> SubPackets = new List<Packet>();
        
        private PacketTypeID _typeID;
        private int _bitsRead;

        public Packet(string packetBinary)
        {
            Version = BinaryToInt(packetBinary.Substring(0, 3));
            _typeID = (PacketTypeID)BinaryToInt(packetBinary.Substring(3, 3));
            _bitsRead = 6;

            if (_typeID == PacketTypeID.Literal)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = _bitsRead; i < packetBinary.Length; i += 5)
                {
                    sb.Append(packetBinary.Substring(i + 1, 4));
                    _bitsRead += 5;
                    if (packetBinary[i] == '0') break;
                }

                Value = BinaryToLong(sb.ToString());
            }
            else
            {
                if (packetBinary.Substring(_bitsRead++, 1) == "0")
                {
                    int subPacketsLength = BinaryToInt(packetBinary.Substring(_bitsRead, 15));
                    _bitsRead += 15;
                    int subBitsRead = 0;
                    while (subBitsRead < subPacketsLength)
                    {
                        Packet sub = new Packet(packetBinary.Substring(_bitsRead, subPacketsLength - subBitsRead));
                        subBitsRead += sub._bitsRead;
                        _bitsRead += sub._bitsRead;
                        SubPackets.Add(sub);
                    }
                }
                else
                {
                    int subPacketsCount = BinaryToInt(packetBinary.Substring(_bitsRead, 11)); ;
                    _bitsRead += 11;
                    while (SubPackets.Count < subPacketsCount)
                    {
                        Packet sub = new Packet(packetBinary.Substring(_bitsRead));
                        _bitsRead += sub._bitsRead;
                        SubPackets.Add(sub);
                    }
                }

                var vals = SubPackets.Select(s => s.Value).ToList();
                switch (_typeID)
                {
                    case PacketTypeID.Sum:          Value = vals.Sum();                                     break;
                    case PacketTypeID.Product:      Value = vals.Aggregate((total, n) => total * n);        break;
                    case PacketTypeID.Min:          Value = vals.Min();                                     break;
                    case PacketTypeID.Max:          Value = vals.Max();                                     break;
                    case PacketTypeID.GreaterThan:  Value = vals[0] > vals[1] ? 1 : 0;                      break;
                    case PacketTypeID.LessThan:     Value = vals[0] < vals[1] ? 1 : 0;                      break;    
                    case PacketTypeID.EqualTo:      Value = vals[0] == vals[1] ? 1 : 0;                     break;
                    default:
                        break;
                }
            }
        }

        private static long BinaryToLong(string binary) => Convert.ToInt64(binary.ToString(), 2);
        private static int BinaryToInt(string binary) => Convert.ToInt32(binary.ToString(), 2);
    }
}
