using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../../../../../adventofcode6.txt");
            char[][] labFloor = new char[lines.Length][];
            char direction = 'N';
            int[] position = new int[2];
            int sum = 0;
            int infinityloops = 0;

            Queue<int[]> lastWalls = new Queue<int[]>(3);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains('^'))
                {
                    position[0] = i;
                    position[1] = lines[i].IndexOf('^');
                }
                labFloor[i] = lines[i].ToCharArray();
            }
            labFloor[position[0]][position[1]] = 'X';
            bool isAbleToWalk = true;

            while (isAbleToWalk)
            {
                Walk(labFloor, ref direction, position, ref isAbleToWalk, lastWalls);
                if (IsPassingWall(position[0], position[0], lastWalls, labFloor)) infinityloops++;
            }

            for (int j = 0; j < labFloor.Length; j++)
            {
                for (int k = 0; k < labFloor[j].Length; k++)
                {
                    Console.Write(labFloor[j][k]);
                }
                sum += labFloor[j].Count(c => c == 'X');
                Console.WriteLine();
            }

            Console.WriteLine("Total 'X' count: " + sum);

            Console.WriteLine("Last 3 walls (#) encountered:");
            foreach (var coord in lastWalls)
            {
                Console.WriteLine($"({coord[0]}, {coord[1]})");
            }
            Console.WriteLine(infinityloops);
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

    private static void Walk(char[][] labFloor, ref char direction, int[] position, ref bool isAbleToWalk, Queue<int[]> lastWalls)
    {
        switch (direction)
        {
            case 'N':
                if (position[0] - 1 >= 0 && labFloor[position[0] - 1][position[1]] != '#')
                {
                    position[0] = position[0] - 1;
                    labFloor[position[0]][position[1]] = 'X';
                }
                else if (position[0] - 1 >= 0 && labFloor[position[0] - 1][position[1]] == '#')
                {
                    if (lastWalls.Count == 3) lastWalls.Dequeue(); 
                    lastWalls.Enqueue(new int[] { position[0] - 1, position[1] });
                    direction = 'E';
                }

                if (position[0] - 1 < 0)
                {
                    isAbleToWalk = false;
                }
                break;

            case 'E':
                if (position[1] + 1 < labFloor[position[0]].Length && labFloor[position[0]][position[1] + 1] != '#')
                {
                    position[1] = position[1] + 1;
                    labFloor[position[0]][position[1]] = 'X';
                }
                else if (position[1] + 1 < labFloor[position[0]].Length && labFloor[position[0]][position[1] + 1] == '#')
                {
                    if (lastWalls.Count == 3) lastWalls.Dequeue(); 
                    lastWalls.Enqueue(new int[] { position[0], position[1] + 1 });
                    direction = 'S';
                }

                if (position[1] + 1 >= labFloor[position[0]].Length)
                {
                    isAbleToWalk = false;
                }
                break;

            case 'S':
                if (position[0] + 1 < labFloor.Length && labFloor[position[0] + 1][position[1]] != '#')
                {
                    position[0] = position[0] + 1;
                    labFloor[position[0]][position[1]] = 'X';
                }
                else if (position[0] + 1 < labFloor.Length && labFloor[position[0] + 1][position[1]] == '#')
                {
                    if (lastWalls.Count == 3) lastWalls.Dequeue(); 
                    lastWalls.Enqueue(new int[] { position[0] + 1, position[1] });
                    direction = 'W';
                }

                if (position[0] + 1 >= labFloor.Length)
                {
                    isAbleToWalk = false;
                }
                break;

            case 'W':
                if (position[1] - 1 >= 0 && labFloor[position[0]][position[1] - 1] != '#')
                {
                    position[1] = position[1] - 1;
                    labFloor[position[0]][position[1]] = 'X';
                }
                else if (position[1] - 1 >= 0 && labFloor[position[0]][position[1] - 1] == '#')
                {
                    if (lastWalls.Count == 3) lastWalls.Dequeue(); 
                    lastWalls.Enqueue(new int[] { position[0], position[1] - 1 });
                    direction = 'N';
                }

                if (position[1] - 1 < 0)
                {
                    isAbleToWalk = false;
                }
                break;
        }
    }
    private static bool IsPassingWall(int x, int y, Queue<int[]> lastWalls, char[][] labFloor)
    {
        int count = 0;
        foreach (var wall in lastWalls)
        {
            if (count == 2) 
            {
                if (wall[0] == x || wall[1] == y)
                {
                    if (wall[0] == x) 
                    {
                        if (!IsObstructed(x, y, wall[0], lastWalls, labFloor, false)) return true;
                    }
                    else 
                    {
                        if (!IsObstructed(x, y, wall[1], lastWalls, labFloor, true)) return true;
                    }
                }
            }
            count++;
        }
        return false;
    }

    private static bool IsObstructed(int x, int y, int length, Queue<int[]> lastWalls, char[][] labFloor, bool isHorizontal)
    {
        bool obstructed = false;
        if (isHorizontal) 
        {
            if (y < length - 1)
            {
                for (int i = y + 1; i < length; i++)
                {
                    if (labFloor[x][i] == '#')
                    {
                        obstructed = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = y - 1; i > length; i--)
                {
                    if (labFloor[x][i] == '#')
                    {
                        obstructed = true;
                        break;
                    }
                }
            }
        }
        else 
        {
            if (x < length - 1)
            {
                for (int i = x + 1; i < length; i++)
                {
                    if (labFloor[i][y] == '#')
                    {
                        obstructed = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = x - 1; i > length; i--)
                {
                    if (labFloor[i][y] == '#')
                    {
                        obstructed = true;
                        break;
                    }
                }
            }
        }

        return obstructed;
    }
}

