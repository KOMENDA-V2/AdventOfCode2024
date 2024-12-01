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
            int index = 0;
            int similarities = 0;

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
            
            for (int i = 0; i < Math.Min(allStringsFromPart1.Count, allStringsFromPart2.Count); i++)
            {
                var newList = allStringsFromPart2.FindAll(s => s.Equals(allStringsFromPart1[i]));
                int occurance = newList.Count;
                similarities += occurance * Int32.Parse(allStringsFromPart1[i]);
            }

            Console.WriteLine($"Final Total of similarietes: {similarities}");

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
