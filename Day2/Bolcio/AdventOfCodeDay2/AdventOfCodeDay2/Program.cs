using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string line;
            int safeCount = 0;

            // Open the input file
            using (StreamReader sr = new StreamReader("../../../../../adventofcode2.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    if (IsSafe(numbers)) safeCount++;
                }
            }

            // Output the result
            Console.WriteLine($"Total safe reports: {safeCount}");
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

    static bool IsSafe(string[] numbers)
    {
        bool isSafe = true;
        bool isIncreasing = false;
        bool isDecreasing = false;

        int previousLevel = Int32.Parse(numbers[0]);

        // Iterate through the levels and check the conditions
        for (int i = 1; i < numbers.Length; i++)
        {
            int currentLevel = Int32.Parse(numbers[i]);
            int diff = currentLevel - previousLevel;

            // Check if the difference is within the allowed range (1 to 3)
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                isSafe = false;
                break;  // If any difference is outside the range, the report is unsafe
            }

            // Check if the sequence is increasing or decreasing
            if (diff > 0)
            {
                if (isDecreasing)  // If it was already decreasing, it's unsafe
                {
                    isSafe = false;
                    break;
                }
                isIncreasing = true;
            }
            else if (diff < 0)
            {
                if (isIncreasing)  // If it was already increasing, it's unsafe
                {
                    isSafe = false;
                    break;
                }
                isDecreasing = true;
            }

            previousLevel = currentLevel;
        }

        // The report is safe only if it is strictly increasing or strictly decreasing
        return isSafe && (isIncreasing != isDecreasing);
    }
}
