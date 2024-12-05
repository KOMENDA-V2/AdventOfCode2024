using System;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../../../../../adventofcode4.txt");
            char[][] xmasArr = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                xmasArr[i] = lines[i].ToCharArray();

            int sCount = 0; 

            for (int j = 0; j < xmasArr.Length; j++)
            {
                for (int k = 0; k < xmasArr[j].Length; k++)
                {
                    if (xmasArr[j][k] == 'X')
                    {
                        sCount = FindNeighbours(xmasArr, j, k, 'X', "?", sCount);
                    }
                }
            }

            Console.WriteLine($"Total \"XMAS\" found: {sCount}");
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

    private static int FindNeighbours(char[][] xmasArr, int j, int k, char currentChar, string flag, int sCount)
    {
        if (j < 0 || k < 0 || j >= xmasArr.Length || k >= xmasArr[0].Length)
            return sCount;

        char nextChar = currentChar switch
        {
            'X' => 'M',
            'M' => 'A',
            'A' => 'S',
            _ => '?'
        };

        if (xmasArr[j][k] == 'S')
        {
            sCount++;
        }

        if (flag == "?")
        {
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, 0, "N", flag, sCount); // North
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, 1, "NE", flag, sCount); // North-East
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 0, 1, "E", flag, sCount); // East
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, 1, "SE", flag, sCount); // South-East
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, 0, "S", flag, sCount); // South
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, -1, "SW", flag, sCount); // South-West
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 0, -1, "W", flag, sCount); // West
            sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, -1, "NW", flag, sCount); // North-West
        }
        else
        {
            switch (flag)
            {
                case "N": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, 0, flag, flag, sCount); break;
                case "NE": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, 1, flag, flag, sCount); break;
                case "E": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 0, 1, flag, flag, sCount); break;
                case "SE": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, 1, flag, flag, sCount); break;
                case "S": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, 0, flag, flag, sCount); break;
                case "SW": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 1, -1, flag, flag, sCount); break;
                case "W": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, 0, -1, flag, flag, sCount); break;
                case "NW": sCount = CheckAndExplore(xmasArr, j, k, currentChar, nextChar, -1, -1, flag, flag, sCount); break;
            }
        }

        return sCount;
    }

    private static int CheckAndExplore(char[][] xmasArr, int j, int k, char currentChar, char nextChar, int dx, int dy, string direction, string flag, int sCount)
    {
        int newJ = j + dx;
        int newK = k + dy;
        if (newJ >= 0 && newJ < xmasArr.Length && newK >= 0 && newK < xmasArr[0].Length
            && xmasArr[newJ][newK] == nextChar)
        {
            Console.WriteLine($"Found connection at ({j},{k}) -> ({newJ},{newK}): '{currentChar}' -> '{nextChar}' in direction {direction}");
            sCount = FindNeighbours(xmasArr, newJ, newK, nextChar, direction, sCount); 
        }

        return sCount;
    }
}