using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Directory
    {
        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
            Directories = new List<Directory>();
            Files = new List<File>();
        }

        public string Name { get; }
        public Directory Parent { get; }
        public List<Directory> Directories { get; set; }
        public List<File> Files { get; set; }

        public int Size {
            get
            {
                return Directories.Sum(d => d.Size) + Files.Sum(f => f.Size);
            }
        }
    }

    class File
    {
        public File(string filename, int size)
        {
            Filename = filename;
            Size = size;
        }

        public int Size { get; }
        public string Filename { get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7: No Space Left On Device");

            Console.WriteLine();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test: {FindDirectories(Process("test.txt"), 100000)}");
            Console.WriteLine($"Answer: {FindDirectories(Process("input.txt"), 100000)}");

            Console.WriteLine();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Test: {FreeUpSpace("test.txt", 70000000, 30000000)}");
            Console.WriteLine($"Answer: {FreeUpSpace("input.txt", 70000000, 30000000)}");
        }

        static Directory Process(string file)
        {
            Directory root = new Directory("/", null);
            Directory current = root;

            foreach (string line in System.IO.File.ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // Handle a command
                if(parts[0] == "$")
                {
                    if(parts[1] == "cd")
                    {
                        // Change directory
                        if (parts[2] == "/")
                            current = root;
                        else if(parts[2] == "..")
                        {
                            current = current.Parent ?? root;
                        }
                        else 
                        {
                            Directory next = current.Directories.SingleOrDefault(d => d.Name.Equals(parts[2]));
                            if (next == null)
                            {
                                next = new Directory(parts[2], current);
                                current.Directories.Add(next);
                                //throw new Exception($"Could not find directory {parts[2]}");
                            }
                            current = next;
                        }
                    }
                    else if (parts[1] == "ls")
                    {
                        // List contents
                    }
                }
                else
                {
                    if (parts[0] == "dir")
                    {
                        // Is a directory
                        if (!current.Directories.Any(d => d.Equals(parts[1])))
                            current.Directories.Add(new Directory(parts[1], current));
                    }
                    else
                    {
                        // Must be a file
                        if (!current.Files.Any(f => f.Filename.Equals(parts[1])))
                            current.Files.Add(new File(parts[1], Int32.Parse(parts[0])));
                    }
                }
            }

            return root;
        }

        static int FindDirectories(Directory directory, int sizeLimit)
        {
            int total = 0;
            foreach(Directory subDirectory in directory.Directories)
            {
                total += FindDirectories(subDirectory, sizeLimit);
            }

            int dirSize = directory.Size;
            if (dirSize <= sizeLimit)
                total += dirSize;

            return total;
        }

        static List<int> FindDirectoriesLargerThan(Directory directory, int sizeLimit)
        {
            List<int> sizes = new List<int>();

            int dirSize = directory.Size;
            if(dirSize >= sizeLimit)
            {
                sizes.Add(dirSize);

                foreach (Directory subDirectory in directory.Directories)
                    sizes.AddRange(FindDirectoriesLargerThan(subDirectory, sizeLimit));
            }

            return sizes;
        }

        static int FreeUpSpace(string file, int capacity, int required)
        {
            Directory root = Process(file);
            int toDelete = required - (capacity - root.Size);
            return FindDirectoriesLargerThan(root, toDelete).OrderBy(n => n).First();
        }
    }
}
