using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        // Scores
        // Rock = 1, Paper = 2, Scissors = 3
        enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        // Loss = 0, Draw = 3, Win = 6
        enum Result
        {
            Loss = 0,
            Draw = 3,
            Win = 6
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Day 2: Rock Paper Scissors");

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {GetPart1Results("test.txt").Sum()} ({string.Join(" + ", GetPart1Results("test.txt"))})");
            Console.WriteLine($"Answer: {GetPart1Results("input.txt").Sum()}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {GetPart2Score("test.txt")}");
            Console.WriteLine($"Answer: {GetPart2Score("input.txt")}");
        }

        /// <summary>
        /// Convert a character into a Shape (Rock/Paper/Scissors)
        /// </summary>
        /// <param name="shape">Character representing a shape</param>
        /// <returns>Shape</returns>
        static Shape GetShape(string shape)
        {
            switch(shape)
            {
                case "A":
                case "X":
                    return Shape.Rock;
                case "B":
                case "Y":
                    return Shape.Paper;
                case "C":
                case "Z":
                    return Shape.Scissors;
                default:
                    throw new Exception($"Unexpected option '{shape}'");
            }
        }

        /// <summary>
        /// Convert a character into a Result (Loss/Draw/Win)
        /// </summary>
        /// <param name="result">Character representing the result</param>
        /// <returns>Result</returns>
        static Result GetResult(string result)
        {
            switch (result)
            {
                case "X":
                    return Result.Loss;
                case "Y":
                    return Result.Draw;
                case "Z":
                    return Result.Win;
                default:
                    throw new Exception($"Unexpected result '{result}'");
            }
        }

        /// <summary>
        /// Get the results for Part1
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Result of each game</returns>
        static List<int> GetPart1Results(string file)
        {
            List<int> results = new List<int>();

            foreach (string line in File.ReadAllLines(file))
            {
                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                // Elf: A = Rock, B = Paper, C = Scissors
                // You: X = Rock, Y = Paper, C = Scissors
                Shape elf = GetShape(parts[0]);
                Shape you = GetShape(parts[1]);

                int score = (int)you;

                switch (you)
                {
                    case Shape.Rock:
                        // Rock beats Scissors
                        if (elf == Shape.Scissors)
                        {
                            score += (int)Result.Win;
                        }
                        // Rock loses to Paper
                        else if (elf == Shape.Paper)
                        {
                            score += (int)Result.Loss;
                        }
                        else
                        {
                            score += (int)Result.Draw;
                        }
                        break;

                    case Shape.Paper:
                        // Paper beats Rock
                        if (elf == Shape.Rock)
                        {
                            score += (int)Result.Win;
                        }
                        // Paper loses to Scissors
                        else if (elf == Shape.Scissors)
                        {
                            score += (int)Result.Loss;
                        }
                        else
                        {
                            score += (int)Result.Draw;
                        }
                        break;

                    case Shape.Scissors:
                        // Scissors beats Paper
                        if (elf == Shape.Paper)
                        {
                            score += (int)Result.Win;
                        }
                        // Scissors loses to Rock
                        else if (elf == Shape.Rock)
                        {
                            score += (int)Result.Loss;
                        }
                        else
                        {
                            score += (int)Result.Draw;
                        }
                        break;
                }
                results.Add(score);
            }

            return results;
        }

        /// <summary>
        /// Get the results for Part 2
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Get the total score</returns>
        static int GetPart2Score(string file)
        {
            int finalScore = 0;

            foreach (string line in File.ReadAllLines(file))
            {
                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                // Get the elfs shape and result of the round
                Shape elf = GetShape(parts[0]);
                Result result = GetResult(parts[1]);
                Shape you = Shape.Rock;

                switch (result)
                {
                    case Result.Win:
                        // Rock loses to Paper
                        if (elf == Shape.Rock)
                            you = Shape.Paper;
                        // Paper loses to Scissors
                        else if (elf == Shape.Paper)
                            you = Shape.Scissors;
                        // Scissors loses to Rock
                        else
                            you = Shape.Rock;
                        break;
                    case Result.Loss:
                        // Rock beats Scissors
                        if (elf == Shape.Rock)
                            you = Shape.Scissors;
                        // Paper beats Rock
                        else if (elf == Shape.Paper)
                            you = Shape.Rock;
                        // Scissors beats Paper
                        else
                            you = Shape.Paper;
                        break;
                    default:
                        you = elf;
                        break;
                }
                finalScore += (int)you + (int)result;
            }

            return finalScore;
        }
    }
}
