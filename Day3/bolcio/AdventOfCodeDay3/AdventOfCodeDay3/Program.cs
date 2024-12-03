using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int sum = 0;
            string line;
            string returningString = "";
            using (StreamReader sr = new StreamReader("adventofcode3.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    returningString += FindString(line);
                }
            }
            Console.WriteLine($"Total String: " + returningString);
            string[] multiplies = returningString.Trim().Split('|');

            foreach (string multiplier in multiplies)
            {
                if (multiplier.Length > 0)
                {
                    string[] numbers = FindNumbers(multiplier).Split(',');
                    sum += Int32.Parse(numbers[0]) * Int32.Parse(numbers[1]);
                    Console.WriteLine(Int32.Parse(numbers[0]) * Int32.Parse(numbers[1]));
                    Console.WriteLine(sum);
                }
            }
            Console.WriteLine(sum);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }
        finally
        {
            Console.WriteLine("Execution complete.");
        }
    }

    static string FindString(string str)
    {
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var matchCollection = Regex.Matches(str, pattern);
        string result = "";
        
        foreach (Match match in matchCollection)
        {
            if (match.Success)
            {
                result += match.Value + "|";
            }
        }

        return result;
    }

    static string FindNumbers(string str)
    {
        string pattern = @"\d{1,3},\d{1,3}";
        var match = Regex.Match(str, pattern);
        return match.Success ? match.Value : "";
    }
}
