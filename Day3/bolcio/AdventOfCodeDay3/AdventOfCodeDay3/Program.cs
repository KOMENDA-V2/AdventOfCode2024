using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string line;
            int safeCount = 0;
            string returningString = ""; 
            // Open the input file
            using (StreamReader sr = new StreamReader("../../../../../adventofcode3.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                   returningString = FindString(line);
                   returningString = returningString.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                }
            }
            // Output the result
            Console.WriteLine($"Total String: " + returningString);
            string[] multiplies = returningString.Trim().Split('|');
            foreach (string multiplier in multiplies)
            {
/*                multiplier.Remove(0, 3).Remove(multiplier.Length - 1);*/
                Console.WriteLine(multiplier);
            }
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

    static string FindString(string str)
    {
        string pattern = @"^mul\(\d{1,3},\d{1,3}\)$";
        var indexStart = str.IndexOf("mul(");
        Console.WriteLine(indexStart);
        var indexEnd = 0; 

        if (indexStart != -1)
        {
            indexEnd = str.IndexOf(")", indexStart);
        }

        string foundString = "";
        string returnString = "";

        if (indexStart != -1)
        {
            for (int i = indexStart; i <= indexEnd; i++)
            {
                foundString = foundString + str[i];
            }
        }

        string newString = "";

        if (indexStart != -1)
        {
            for (int i = indexStart + 3; i < str.Length; i++)
            {
                newString = newString + str[i];
            }
        }
/*        Console.WriteLine(newString);*/

        var match = Regex.Match(foundString, pattern);
        if (match.Success)
        {
            Console.WriteLine("Match found: " + match);
        }
        else
        {
            Console.WriteLine("No match found");
        }

        if (indexStart != -1) 
        {
            returnString = match.ToString() + "|" + FindString(newString);        
        };
        return returnString;
    }
}