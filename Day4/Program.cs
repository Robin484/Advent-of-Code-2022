using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 4: Camp Cleanup");

            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {FindOverlaps("test.txt")}");
            Console.WriteLine($"Answer: {FindOverlaps("input.txt")}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {FindOverlaps("test.txt", false)}");
            Console.WriteLine($"Answer: {FindOverlaps("input.txt", false)}");
        }

        static int FindOverlaps(string file, bool fully = true)
        {
            List<List<Section>> sections = new List<List<Section>>();

            foreach (string line in File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                List<Section> section = new List<Section>();
                foreach (string s in line.Split(","))
                {
                    section.Add(new Section(s));
                }

                if (section.Count != 2)
                    throw new Exception("Only two sections are expected per line!");

                section.Sort();

                if (fully)
                {
                    // 0 is within 1 if 0.Min >= 1.Min and 0.Max <= 1.Max
                    // 1 is within 0 if 1.Min >= 0.Min and 1.Max <= 0.Max
                    if (section[0].Min >= section[1].Min && section[0].Max <= section[1].Max)
                        sections.Add(section);
                    else if (section[1].Min >= section[0].Min && section[1].Max <= section[0].Max)
                        sections.Add(section);
                }
                else
                {
                    // 0 overlaps if 0.Min <= 1.Max and 0.Max >= 1.Min
                    // 1 overlaps if 1.Min <= 0.Max and 1.Max >= 0.Min
                    if (section[0].Min <= section[1].Max && section[0].Max >= section[1].Min)
                        sections.Add(section);
                    else if (section[1].Min <= section[0].Max && section[1].Max >= section[0].Min)
                        sections.Add(section);
                }


            }
            return sections.Count;
        }
    }

    class Section : IComparable<Section>
    {
        public Section(string section)
        {
            String[] parts = section.Split("-");
            if (parts.Length != 2)
                throw new Exception($"Unexpected section {section}");

            int a = (Int32.Parse(parts[0]));
            int b = (Int32.Parse(parts[1]));

            if (a < b)
            {
                Min = a;
                Max = b;
            }
            else
            {
                Min = b;
                Max = a;
            }
        }

        public int CompareTo([AllowNull] Section other)
        {
            return this.Min.CompareTo(other.Min);
        }

        public int Min { get; }
        public int Max { get; }
    }
}
