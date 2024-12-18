using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    private static readonly int[] dx = { 0, 1, 0, -1 };
    private static readonly int[] dy = { 1, 0, -1, 0 };

    private static void Main(string[] args)
    {
        try
        {
            string[] lines = File.ReadAllLines("../../../../../adventofcode18.txt");

            var coordinates = lines.Select(line => line.Split(',').Select(int.Parse).ToArray()).ToList();

            Console.Write("Enter the number of coordinates to use: ");
            int biteCount = int.Parse(Console.ReadLine() ?? "0");
            biteCount = Math.Min(biteCount, coordinates.Count);

            for (int i = biteCount; i <= coordinates.Count; i++)
            {
                var selectedCoordinates = coordinates.Take(i).ToList();

                int maxRow = selectedCoordinates.Max(coord => coord[1]);
                int maxCol = selectedCoordinates.Max(coord => coord[0]);

                char[][] xmasArr = new char[maxRow + 1][];
                for (int j = 0; j <= maxRow; j++)
                {
                    xmasArr[j] = Enumerable.Repeat('.', maxCol + 1).ToArray();
                }

                foreach (var coord in selectedCoordinates)
                {
                    int row = coord[1];
                    int col = coord[0];
                    xmasArr[row][col] = '#';
                }

                var start = (x: 0, y: 0);
                var end = (x: maxRow, y: maxCol);

                if (xmasArr[start.x][start.y] == '#' || xmasArr[end.x][end.y] == '#')
                {
                    Console.WriteLine("Start or end point is blocked.");
                    return;
                }

                var path = FindShortestPath(xmasArr, start, end);
                if (path != null && i == biteCount)
                {
                    foreach (var (x, y) in path)
                    {
                        if (xmasArr[x][y] != '#')
                        {
                            xmasArr[x][y] = 'O';
                        }
                    }

                    int pathLength = xmasArr.Sum(row => row.Count(cell => cell == 'O'));

                    Console.WriteLine($"Total 'O' (path length): {pathLength - 1}");
                }
                if (path == null)
                {
                    var lastBite = coordinates[i - 1]; 
                    Console.WriteLine($"There is no path if {i} bites fall.");
                    Console.WriteLine($"Coordinates of {i}th bite: {lastBite[0]},{lastBite[1]}");
                    break;
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

    static List<(int x, int y)> FindShortestPath(char[][] grid, (int x, int y) start, (int x, int y) end)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        var pq = new SortedSet<(int cost, int x, int y, int dir, List<(int, int)> path)>(
            Comparer<(int cost, int x, int y, int dir, List<(int, int)> path)>.Create((a, b) =>
                a.cost == b.cost
                    ? a.x == b.x
                        ? a.y == b.y ? a.dir.CompareTo(b.dir) : a.y.CompareTo(b.y)
                        : a.x.CompareTo(b.x)
                    : a.cost.CompareTo(b.cost))
        );

        var visited = new HashSet<(int x, int y, int dir)>();

        for (int dir = 0; dir < 4; dir++)
        {
            pq.Add((0, start.x, start.y, dir, new List<(int, int)> { start }));
        }

        while (pq.Count > 0)
        {
            var (cost, x, y, dir, path) = pq.Min;
            pq.Remove(pq.Min);

            if ((x, y) == end)
                return path;

            if (visited.Contains((x, y, dir)))
                continue;

            visited.Add((x, y, dir));

            for (int newDir = 0; newDir < 4; newDir++)
            {
                int newX = x + dx[newDir];
                int newY = y + dy[newDir];
                bool isTurn = newDir != dir;

                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && grid[newX][newY] != '#')
                {
                    int newCost = cost + 1 + (isTurn ? 1 : 0);
                    var newPath = new List<(int, int)>(path) { (newX, newY) };
                    pq.Add((newCost, newX, newY, newDir, newPath));
                }
            }
        }

        return null;
    }
}
