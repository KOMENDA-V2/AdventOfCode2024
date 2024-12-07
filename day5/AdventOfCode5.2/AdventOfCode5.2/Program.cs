using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string line;
            List<string> Updates = new List<string>();
            int[] pages = new int[90];
            for (int i = 0; i < pages.Length; i++) { pages[i] = i + 10; }
            int sum = 0;

            TreeNode root = new TreeNode();

            using (StreamReader sr = new StreamReader("../../../../adventofcode5.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Updates.Add(FindUpdate(line));

                    string[] numbers = FindFirstRule(line).Split('/');
                    foreach (string number in numbers)
                    {
                        if (number != "")
                        {
                            var parts = number.Split('|');
                            if (parts.Length == 2)
                            {
                                int first = Int32.Parse(parts[0]);
                                int second = Int32.Parse(parts[1]);

                                root.AddRule(first, second);
                            }
                        }
                    }
                }
                sum = CheckUpdate(Updates,root);
            }

            Console.WriteLine($"Suma: {sum}");
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

    private static string FindFirstRule(string str)
    {
        string pattern = @"\d{1,2}\|\d{1,2}";
        var matchCollection = Regex.Matches(str, pattern);
        string result = "";

        foreach (Match match in matchCollection)
        {
            if (match.Success)
            {
                result += match.Value + "/";
            }
        }

        return result.TrimEnd('/');
    }

    private static int CheckUpdate(List<string> Updates, TreeNode root)
    {
        int sum = 0;

        foreach (string update in Updates)
        {
            if (!string.IsNullOrEmpty(update))
            {
                Console.WriteLine($"Przetwarzanie update: {update}");
                string[] updateArr = update.Split(",");

                bool wasBadUpdate = false; 
                bool isValid = false;  

                while (!isValid)
                {
                    isValid = true;  

                    for (int i = 0; i < updateArr.Length - 1; i++)
                    {
                        int current = Int32.Parse(updateArr[i]);
                        int next = Int32.Parse(updateArr[i + 1]);

                        if (!root.IsValidUpdate(current, next))
                        {
                            Console.WriteLine($"Błąd: Zła kolejność w update'cie! ({current} i {next})");
                            string temp = updateArr[i];
                            updateArr[i] = updateArr[i + 1];
                            updateArr[i + 1] = temp;
                            wasBadUpdate = true;  
                            isValid = false;  
                            break;  
                        }
                    }
                }
                if (wasBadUpdate)
                {
                    int middleIndex = updateArr.Length / 2;
                    int middlePage = Int32.Parse(updateArr[middleIndex]);
                    Console.WriteLine($"Środkowy wynik to {middlePage}");
                    sum += middlePage;  
                }
            }
        }

        return sum;
    }



    private static string FindUpdate(string str)
    {
        string pattern = @"^\d{1,2}(?:,\d{1,2})*$";
        var matchCollection = Regex.Matches(str, pattern);
        string result = "";

        foreach (Match match in matchCollection)
        {
            if (match.Success)
            {
                result += match.Value + "/";
            }
        }

        return result.TrimEnd('/');
    }
}

public class TreeNode
{
    private Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();

    public void AddRule(int first, int second)
    {
        if (!rules.ContainsKey(first))
        {
            rules[first] = new List<int>();
        }
        rules[first].Add(second);
    }

    public bool IsValidUpdate(int current, int next)
    {
        if (rules.ContainsKey(current) && rules[current].Contains(next))
        {
            return true;
        }

        return false;
    }
}
