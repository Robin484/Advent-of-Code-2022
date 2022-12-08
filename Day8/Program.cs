using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 8: Treetop Tree House");

            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {GetVisibleTrees("test.txt")}");
            Console.WriteLine($"Answer:  {GetVisibleTrees("input.txt")}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {GetMostSceinic("test.txt")}");
            Console.WriteLine($"Answer: {GetMostSceinic("input.txt")}");
        }

        /// <summary>
        /// Load the grid from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static List<List<int>> LoadGrid(string file)
        {
            List<List<int>> grid = new List<List<int>>();

            // Load the grid
            foreach (string line in File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                grid.Add(line.ToList<char>().ConvertAll<int>(c => Int32.Parse(c.ToString())));
            }

            return grid;
        }

        /// <summary>
        /// Load the trees then work out how many trees are visible
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static int GetVisibleTrees(string file)
        {
            int trees = 0;
            List<List<int>> grid = LoadGrid(file);

            // Trees on the edge are visible
            int edges = 2 * (grid.Count + grid[0].Count - 2);

            for (int row = 1; row + 1 < grid.Count; row++)
            {
                for (int col = 1; col + 1 < grid[row].Count; col++)
                {
                    // Tree is visible if there are no trees between it and the
                    // edge that are the same height or taller
                    int currentTree = grid[row][col];
                    bool visible = false;

                    var theRow = GetRow(grid, row);
                    var theCol = GetCol(grid, col);

                    // Left
                    if (!theRow.Take(col).Any(i => i >= currentTree))
                        visible = true;

                    // Right
                    else if (!theRow.Skip(col + 1).Any(i => i >= currentTree))
                        visible = true;

                    // Top
                    if (!theCol.Take(row).Any(i => i >= currentTree))
                        visible = true;

                    // bottom
                    else if (!theCol.Skip(row + 1).Any(i => i >= currentTree))
                        visible = true;

                    if (visible)
                        trees++;
                }
            }

            return edges + trees;
        }

        /// <summary>
        /// Get the most sceinic score
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>score</returns>
        static int GetMostSceinic(string file)
        {
            int score = 0;
            List<List<int>> grid = LoadGrid(file);

            // Ignore trees on the edges as they will have a score of 0

            for (int row = 1; row + 1 < grid.Count; row++)
            {
                for (int col = 1; col + 1 < grid[row].Count; col++)
                {
                    var theRow = GetRow(grid, row);
                    var theCol = GetCol(grid, col);

                    // In each direction workout how many trees can be seen
                    int up = GetTreesInView(theCol, false, row);
                    int left = GetTreesInView(theRow, false, col);
                    int right = GetTreesInView(theRow, true, col);
                    int down = GetTreesInView(theCol, true, row);

                    // Work out the score and find the most sceinic
                    score = Math.Max(score, (up * left * right * down));
                }
            }

            return score;
        }

        /// <summary>
        /// Get the number of trees in view
        /// </summary>
        /// <param name="trees">The trees on the row/column</param>
        /// <param name="forwards">If we are looking forwards or backwards</param>
        /// <param name="currentTree">The index of the current tree</param>
        /// <returns>Number of trees visible</returns>
        static int GetTreesInView(List<int> trees, bool forwards, int currentTree)
        {
            int currentHeight = trees[currentTree];

            int treesInView = 0;
            if(forwards)
            {
                for (int n = currentTree + 1; n < trees.Count; n++)
                {
                    treesInView++;
                    // Stop if we can't see past the tree
                    if (trees[n] >= currentHeight)
                        break;
                }
            }
            else
            {
                for (int n = currentTree - 1; n >= 0; n--)
                {
                    treesInView++;
                    // Stop if we can't see past the tree
                    if (trees[n] >= currentHeight)
                        break;
                }
            }

            return treesInView;
        }

        /// <summary>
        /// Helper method to get a row
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="row">the index of the row to select</param>
        /// <returns>row</returns>
        static List<int> GetRow(List<List<int>> grid, int row)
        {
            return grid[row];
        }

        /// <summary>
        /// Helper method to get a col
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="col">the index of the col to select</param>
        /// <returns>column</returns>
        static List<int> GetCol(List<List<int>> grid, int col)
        {
            List<int> cols = new List<int>();
            for(int i=0; i < grid.Count; i++)
            {
                cols.Add(grid[i][col]);
            }
            return cols;
        }
    }
}
