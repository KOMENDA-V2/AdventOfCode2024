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
                // If difference is out of range, try removing an element
                if (CanBeSafeByRemovingOne(numbers))
                {
                    return true; // It's safe if we remove one element
                }
                else
                {
                    return false; // It's unsafe and can't be fixed by removing one element
                }
            }

            // Check if the sequence is increasing or decreasing
            if (diff > 0)
            {
                if (isDecreasing)  // If it was already decreasing, it's unsafe
                {
                    if (CanBeSafeByRemovingOne(numbers))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                isIncreasing = true;
            }
            else if (diff < 0)
            {
                if (isIncreasing)  // If it was already increasing, it's unsafe
                {
                    if (CanBeSafeByRemovingOne(numbers))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                isDecreasing = true;
            }
            previousLevel = currentLevel;
        }

        // The report is safe only if it is strictly increasing or strictly decreasing
        return isSafe && (isIncreasing != isDecreasing);
    }

    // Function to check if removing one element makes the report safe
    static bool CanBeSafeByRemovingOne(string[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            string[] newNumbers = RemoveElement(numbers, i);
            if (IsSafe2(newNumbers))  // Check if the new sequence becomes safe
            {
                return true;
            }
        }
        return false;
    }

    static string[] RemoveElement(string[] numbers, int index)
    {
        string[] newNumbers = new string[numbers.Length - 1];
        for (int i = 0, j = 0; i < numbers.Length; i++)
        {
            if (i != index)
            {
                newNumbers[j] = numbers[i];
                j++;
            }
        }
        return newNumbers;
    }

    static bool IsSafe2(string[] numbers)
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
