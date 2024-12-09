using System;
using System.IO;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../../../../../adventofcode8.txt");
            char[][] xmasArr = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
                xmasArr[i] = lines[i].ToCharArray();

            int pairCount = 0;

            Dictionary<char, List<(int, int)>> charPositions = new Dictionary<char, List<(int, int)>>();

            for (int i = 0; i < xmasArr.Length; i++)
            {
                for (int j = 0; j < xmasArr[i].Length; j++)
                {
                    char currentChar = xmasArr[i][j];

                    if (currentChar != '.')
                    {
                        if (!charPositions.ContainsKey(currentChar))
                        {
                            charPositions[currentChar] = new List<(int, int)>();
                        }
                        charPositions[currentChar].Add((i, j)); 
                    }
                }
            }
            char[][] hashArr = xmasArr;
            char[][] hashArrInf = xmasArr;

            foreach (var entry in charPositions)
            {
                char character = entry.Key;
                var positions = entry.Value;

                if (positions.Count > 1)
                {
                    for (int i = 0; i < positions.Count; i++)
                    {
                        for (int j = i + 1; j < positions.Count; j++)
                        {
                            var pos1 = positions[i];
                            var pos2 = positions[j];

                            int rowDiff = pos1.Item1 - pos2.Item1;
                            int colDiff = pos1.Item2 - pos2.Item2;

                            putHash(xmasArr, hashArr, pos1.Item1, pos1.Item2, rowDiff, colDiff);
                            putHashInf(xmasArr, hashArrInf, pos1.Item1, pos1.Item2, rowDiff, colDiff);
                            pairCount++;
                        }
                    }
                }
            }

            int hashCount = 0;
            foreach (var row in hashArr)
            {
                foreach (var cell in row)
                {
                    if (cell == '#')
                        hashCount++;
                }
            }
            int hashCountInf = 0;
            foreach (var row in hashArrInf)
            {
                foreach (var cell in row)
                {
                    if (cell == '#')
                        hashCountInf++;
                }
            }

            Console.WriteLine($"Liczba znalezionych par: {pairCount}");
            Console.WriteLine($"Liczba znaków '#': {hashCount}");
            Console.WriteLine($"Liczba znaków '#' part 2 : {hashCountInf}");

        }
        catch (Exception e)
        {
            Console.WriteLine($"Wystąpił wyjątek: {e.Message}");
        }
        finally
        {
            Console.WriteLine("Wykonanie programu zakończone.");
        }
    }

    private static void putHash(char[][] array, char[][] hashArr, int pos1, int pos2, int rowDiff, int colDiff)
    {
        if (pos1 - 2 * rowDiff >= 0 && pos1 - 2 * rowDiff < array.Length &&
            pos2 - 2 * colDiff >= 0 && pos2 - 2 * colDiff < array[0].Length)
        {
            hashArr[pos1 - 2 * rowDiff][pos2 - 2 * colDiff] = '#';
        }

        if (pos1 + rowDiff >= 0 && pos1 + rowDiff < array.Length &&
            pos2 + colDiff >= 0 && pos2 + colDiff < array[0].Length)
        {
            hashArr[pos1 + rowDiff][pos2 + colDiff] = '#';
        }
    }

    private static void putHashInf(char[][] array, char[][] hashArr, int pos1, int pos2, int rowDiff, int colDiff)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (pos1 -  (i+1) * rowDiff >= 0 && pos1 - (i + 1) * rowDiff < array.Length &&
                pos2 - (i + 1) * colDiff >= 0 && pos2 - (i + 1) * colDiff < array[0].Length)
            {
                hashArr[pos1 - 2 *  rowDiff][pos2 - 2 * colDiff] = '#';
            }
        }
        for (int i = 1; i < array[1].Length; i++)
        {
            if (pos1 + i * rowDiff >= 0 && pos1 + i* rowDiff < array.Length &&
            pos2 + i * colDiff >= 0 && pos2 + i * colDiff < array[0].Length)
            {
                hashArr[pos1 + rowDiff][pos2 + colDiff] = '#';
            }
        }
    }
}