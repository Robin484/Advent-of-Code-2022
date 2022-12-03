using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 3: Rucksack Reorganization");
            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {string.Join(", ", ProcessRucksacks("test"))} = {ProcessRucksacks("test").Sum()}");
            Console.WriteLine($"Answer: {ProcessRucksacks("input").Sum()}");
            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {CalculateBadgePriority("test")}");
            Console.WriteLine($"Answer: {CalculateBadgePriority("input")}");
        }

        static List<int> ProcessRucksacks(string file)
        {
            List<int> results = new List<int>();

            foreach (string line in File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                // Split into the two compartments
                char[] comp1 = line.Take(line.Length / 2).ToArray();
                char[] comp2 = line.TakeLast(line.Length / 2).ToArray();

                // Find the items that appear in both compartments
                var appearInBoth = comp1.Join(comp2, one => one, two => two, (one, two) => one).Distinct();

                // Calculate the priority of the items
                results.Add(appearInBoth.Sum(c => GetPriority(c)));
            }

            return results;
        }

        static int GetPriority(char c)
        {
            if(Char.IsUpper(c))
            {
                return ((int)c) - 38;
            }
            else if (Char.IsLower(c))
            {
                return ((int)c) - 96;
            }
            return 0;
        }

        static List<char[]> FindBadges(string file)
        {
            List<char[]> result = new List<char[]>();
            List<char[]> elves = new List<char[]>();
            foreach (string line in File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                elves.Add(line.Distinct().ToArray());

                // Process the group
                if(elves.Count >= 3)
                {
                    // Work out what items are common between the evles
                    var common = elves[0];
                    for(int i=1; i<elves.Count; i++)
                    {
                        common = common.Join(elves[i], one => one, two => two, (one, two) => one).Distinct().ToArray();
                    }

                    result.Add(common);

                    // Start a new group
                    elves = new List<char[]>();
                }
            }

            return result;
        }

        static int CalculateBadgePriority(string file)
        {
            List<char[]> badges = FindBadges(file);

            int priority = 0;
            foreach(char[] badge in badges)
            {
                priority += badge.Sum(b => GetPriority(b));
            }

            return priority;
        }
    }
}
