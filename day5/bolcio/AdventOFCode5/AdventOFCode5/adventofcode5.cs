using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string line;
            List<(int, int)> FirstRule = new List<(int, int)>(); // Lista par (tuple) z liczbami
            int[] pages = new int[90];
            for (int i = 0; i < pages.Length; i++) { pages[i] = i + 10; }

            using (StreamReader sr = new StreamReader("../../../../adventofcode5.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    // Wydobycie liczb z każdej linii
                    string[] numbers = FindFirstRule(line).Split('|');
                    foreach (string number in numbers)
                    {
                        if (number != "")
                        {
                            var parts = number.Split(','); // Rozdziel liczby
                            if (parts.Length == 2)
                            {
                                int first = Int32.Parse(parts[0]);
                                int second = Int32.Parse(parts[1]);
                                FirstRule.Add((first, second)); // Dodanie pary do listy
                            }
                        }
                    }
                }

                // Sprawdzanie każdej pary liczb
                foreach (var pair in FirstRule)
                {
                    int first = pair.Item1;
                    int second = pair.Item2;

                    // Tu dodajesz logikę do analizy każdej pary
                    Console.WriteLine($"Pierwsza liczba: {first}, Druga liczba: {second}");
                    // Możesz sprawdzać, która liczba jest większa, mniejsza itd.
                    if (first < second)
                    {
                        Console.WriteLine("Pierwsza liczba jest mniejsza.");
                    }
                    else if (first > second)
                    {
                        Console.WriteLine("Pierwsza liczba jest większa.");
                    }
                    else
                    {
                        Console.WriteLine("Obie liczby są równe.");
                    }
                }
            }
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

    static string FindFirstRule(string str)
    {
        string pattern = @"\d{1,2},\d{1,2}";
        var matchCollection = Regex.Matches(str, pattern);
        string result = "";

        foreach (Match match in matchCollection)
        {
            if (match.Success)
            {
                result += match.Value + "|"; // Łączenie liczb z każdej pary
            }
        }

        return result.TrimEnd('|'); // Usuwanie ostatniego "pipe"
    }

    static int CheckIndex(int[] arr, int number)
    {
        int val = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == number)
            {
                val = i;
            }
        }
        return val;
    }
}
