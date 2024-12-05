using System;
using System.IO;

class Program
{
    static void Main(string[] args)
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
                    if (xmasArr[j][k] == 'A')
                    {
                        sCount = FindNeighbours(xmasArr, j, k, 'A', "?", sCount);
                    }
                }
            }

            Console.WriteLine($"Total \"MAS\" found: {sCount}");
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

    static int FindNeighbours(char[][] xmasArr, int j, int k, char currentChar, string flag, int sCount)
    {
        if (j < 0 || k < 0 || j >= xmasArr.Length || k >= xmasArr[0].Length)
            return sCount;

        if (xmasArr[j][k] == 'S')
        {
            sCount++;
        }

        if (flag == "?")
        {
            if (CheckAndExplore(xmasArr, j, k, currentChar, 'M', -1, 1, "NE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', 1, -1, "SW", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', 1, 1, "SE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', -1, -1, "NW", sCount))
            {
                sCount++;
            }else
            if (CheckAndExplore(xmasArr, j, k, currentChar, 'S', -1, 1, "NE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', 1, -1, "SW", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', 1, 1, "SE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', -1, -1, "NW", sCount))
            {
                sCount++;
            }else
            if (CheckAndExplore(xmasArr, j, k, currentChar, 'M', -1, 1, "NE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', 1, -1, "SW", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', 1, 1, "SE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', -1, -1, "NW", sCount))
            {
                sCount++;
            }else
            if (CheckAndExplore(xmasArr, j, k, currentChar, 'S', -1, 1, "NE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', 1, -1, "SW", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'M', 1, 1, "SE", sCount) &&
                CheckAndExplore(xmasArr, j, k, currentChar, 'S', -1, -1, "NW", sCount))
            {
                sCount++;
            }

        }


        return sCount;
    }

    static bool CheckAndExplore(char[][] xmasArr, int j, int k, char currentChar, char nextChar, int dx, int dy, string direction, int sCount)
    {
        int newJ = j + dx;
        int newK = k + dy;
        bool flag2 = false;

        if (newJ >= 0 && newJ < xmasArr.Length && newK >= 0 && newK < xmasArr[0].Length
            && xmasArr[newJ][newK] == nextChar)
        {
            flag2 = true;
        }

        return flag2;
    }
}
