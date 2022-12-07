using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 6: Tuning Trouble");

            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {string.Join(",", DoWork("test.txt"))}");
            Console.WriteLine($"Answer:  {string.Join(",", DoWork("input.txt"))}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {string.Join(",", DoWork("test.txt", 14))}");
            Console.WriteLine($"Answer:  {string.Join(",", DoWork("input.txt", 14))}");
        }


        static List<int> DoWork(string file, int packetSize = 4)
        {
            List<int> packets = new List<int>();

            foreach (string line in File.ReadAllLines(file))
            {
                packets.Add(FindPacket(line, packetSize));
            }

            return packets;
        }

        static int FindPacket(string line, int packetSize)
        {
            for(int i=0; i < line.Length; i++)
            {
                // If we have got to the end of the string before finding the packet stop
                if (i + packetSize > line.Length)
                    break;

                char[] packet = line.Substring(i, packetSize).ToCharArray();

                // If all chars are distinct, this must be the packet marker
                if (packet.Distinct().Count() == packet.Length)
                    return i + packetSize;

            }
            return -1;
        }
    }
}
