using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the number of iterations:");
            int numIterations;
            if (!int.TryParse(Console.ReadLine(), out numIterations) || numIterations <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer.");
                return;
            }

            List<BigInteger> updatedRocksList = new List<BigInteger>();

            using (StreamReader sr = new StreamReader("../../../../../adventofcode11.txt"))
            {
                string line = sr.ReadLine();
                if (line != null)
                {
                    foreach (string rock in line.Split(' '))
                    {
                        updatedRocksList.Add(BigInteger.Parse(rock));
                    }
                }

                Dictionary<BigInteger, List<BigInteger>> cache = new Dictionary<BigInteger, List<BigInteger>>();

                for (BigInteger i = 0; i < numIterations; i++)
                {
                    List<BigInteger> newRocksList = new List<BigInteger>();

                    foreach (BigInteger rock in updatedRocksList)
                    {
                        List<BigInteger> transformedRocks;

                        if (cache.ContainsKey(rock))
                        {
                            transformedRocks = cache[rock];
                        }
                        else
                        {
                            if (rock == 0)
                            {
                                transformedRocks = new List<BigInteger> { 1 };
                            }
                            else if (rock % 2 == 0) 
                            {
                                BigInteger firstHalf = rock / 2;
                                BigInteger secondHalf = rock - firstHalf;
                                transformedRocks = new List<BigInteger> { firstHalf, secondHalf };
                            }
                            else
                            {
                                transformedRocks = new List<BigInteger> { rock * 2024 };
                            }

                            cache[rock] = transformedRocks;
                        }

                        newRocksList.AddRange(transformedRocks);
                    }

                    updatedRocksList = newRocksList;
                    Console.WriteLine($"Number of rocks after {i + 1} iterations is {updatedRocksList.Count}");
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
}
