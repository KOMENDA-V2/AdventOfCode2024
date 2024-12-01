using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string line;
            int totalSum = 0;
            int index = 0;

            List<string> allStringsFromPart1 = new List<string>();
            List<string> allStringsFromPart2 = new List<string>();

            using (StreamReader sr = new StreamReader("D:\\Advent\\advent1\\adventofcode1.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Replace("   ", ",").Split(',');

                    allStringsFromPart1.Add(parts[0]);
                    allStringsFromPart2.Add(parts[1]);
                }
            }

            allStringsFromPart1.Sort();
            allStringsFromPart2.Sort();

            Console.WriteLine("Sorted strings from part 1:");
            foreach (var str in allStringsFromPart1)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine("Sorted strings from part 2:");
            foreach (var str in allStringsFromPart2)
            {
                Console.WriteLine(str);
            }

            for (int i = 0; i < Math.Min(allStringsFromPart1.Count, allStringsFromPart2.Count); i++)
            {
                int num1 = int.Parse(allStringsFromPart1[i]);
                int num2 = int.Parse(allStringsFromPart2[i]);

                int absDifference = Math.Abs(num1 - num2);

                totalSum += absDifference;
                Console.WriteLine($"Abs difference between {num1} and {num2}: {absDifference}");
            }

            Console.WriteLine($"Final Total Sum of absolute differences: {totalSum}");

        }
        catch (Exception e)
        {
            // Handle any exceptions that may occur
            Console.WriteLine($"Exception: {e.Message}");
        }
        finally
        {
            Console.WriteLine("Execution complete.");
        }
    }
}
