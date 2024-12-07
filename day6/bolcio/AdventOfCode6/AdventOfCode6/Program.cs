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
                Walk(labFloor, ref direction, position, ref isAbleToWalk);
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

    private static void Walk(char[][] labFloor, ref char direction, int[] position, ref bool isAbleToWalk)
    {
        switch (direction)
        {
            case 'N':
                if (position[0] - 1 >= 0 && labFloor[position[0] - 1][position[1]] != '#')  
                {
                    position[0] = position[0] - 1;
                    labFloor[position[0]][position[1]] = 'X';
                }
                else direction = 'E';
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
                else direction = 'S';
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
                else direction = 'W';
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
                else direction = 'N';
                if (position[1] - 1 < 0) 
                {
                    isAbleToWalk = false;
                }
                break;
        }
    }

}