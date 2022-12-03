using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 1: Calorie Counting!");
            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test data most calories: {CountCalories("test").Max()}");
            Console.WriteLine($"Most calories: {CountCalories("input").Max()}");

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test answer: {CountCalories("test", 3)}");
            Console.WriteLine($"Answer: {CountCalories("input", 3)}");
        }

        /// <summary>
        /// Take an input file of elves carrying food and create a list of the total calories carried by each elf
        /// </summary>
        /// <param name="file">Input file, each line is an item of food, each empty line indicates a new elf</param>
        /// <returns>List of calories carried by each elf</returns>
        static List<int> CountCalories(string file)
        {
            List<int> elves = new List<int>();

            int calories = 0;
            foreach (string line in File.ReadAllLines(file))
            {
                // Each empty line is a new elf
                if (string.IsNullOrEmpty(line))
                {
                    elves.Add(calories);
                    calories = 0;
                }
                else if (int.TryParse(line, out int food))
                {
                    // Increase the calories 
                    calories += food;
                }
            }

            if (calories > 0)
            {
                elves.Add(calories);
                calories = 0;
            }

            return elves;
        }

        /// <summary>
        /// Count the number of calories carried by the top N elves
        /// </summary>
        /// <param name="file">Input file</param>
        /// <param name="numberElves">Number of elves</param>
        /// <returns>Total calories carried</returns>
        static int CountCalories(string file, int numberElves)
        {
            List<int> elves = CountCalories(file);
            elves.Sort();
            return elves.TakeLast(numberElves).Sum();
        }
    }
}
