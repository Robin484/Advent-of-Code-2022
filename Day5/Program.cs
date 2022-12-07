using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 5: Supply Stacks");

            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {string.Join(",", DoWork("test.txt"))}");
            Console.WriteLine($"Answer:  {string.Join(",", DoWork("input.txt"))}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {string.Join(",", DoWork("test.txt", true))}");
            Console.WriteLine($"Answer:  {string.Join(",", DoWork("input.txt", true))}");
        }

        static List<string> DoWork(string file, bool preserveOrder = false)
        {
            List<Stack<string>> stacks = new List<Stack<string>>();

            bool loading = true;
            foreach (string line in File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                {
                    loading = false;
                    continue;
                }

                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // If we are still loading
                if (loading)
                {
                    if (parts.Length != 2)
                        throw new Exception($"Unexpected loading {line}");

                    // build up a stack of all the crates
                    Stack<string> stack = new Stack<string>();
                    foreach(string s in parts[1].Split(","))
                    {
                        stack.Push(s);
                    }
                    stacks.Add(stack);

                    // Don't carry on
                    continue;
                }

                // Move N from A to B
                
                if (parts.Length != 6)
                    throw new Exception($"Unexpected line {line}");

                int n = Int32.Parse(parts[1]);
                Stack<string> a = null;
                Stack<string> b = null;

                // If A and B are the same, nothing moves
                if (parts[3].Equals(parts[5]))
                    continue;

                // Work out A
                int src = Int32.Parse(parts[3]) - 1;
                int dst = Int32.Parse(parts[5]) - 1;

                if (src < 0 || src >= stacks.Count || dst < 0 || dst >= stacks.Count)
                    throw new Exception("Unepected Stack!");

                a = stacks[src];
                b = stacks[dst];

                Stack<string> tmp = new Stack<string>();

                // N times
                while(n > 0)
                {
                    if (!preserveOrder)
                    {
                        // Move from A to B (if we can)
                        if (a.Count > 0)
                        {
                            string crate = a.Pop();
                            b.Push(crate);
                        }
                    }
                    else
                    {
                        if (a.Count > 0)
                            tmp.Push(a.Pop());
                    }
                    n--;
                }

                if (preserveOrder)
                {
                    while(tmp.Count > 0)
                    {
                        string crate = tmp.Pop();
                        b.Push(crate);
                    }
                }
            }

            List<string> tops = new List<string>();

            foreach (Stack<string> stack in stacks)
            {
                tops.Add(stack.Peek() ?? "");
            }

            return tops;
        }
    }
}
