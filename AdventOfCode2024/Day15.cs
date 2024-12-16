using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace AdventOfCode2024;

public class Day15 : BaseDay
{
    private readonly string _input;

    public Day15()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day15(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var movements = new StringBuilder();
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var lastLine = Array.FindLastIndex(lines, x => x.StartsWith("########"));
        var map = new char[lines.Length, lines[lastLine].Length];
        var positionRobot = new Point(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i > lastLine)
            {
                movements.Append(lines[i]);
            }
            else if (i <= lastLine)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = lines[i][j];
                    if (map[i, j] == '@')
                    {
                        positionRobot = new Point(i, j);
                    }
                }
            }
        }
        var movementInstruction = movements.ToString().ToCharArray();

        //Print(map, 'I');

        foreach (var movement in movementInstruction)
        {
            positionRobot = Move(map, positionRobot, movement);
            //Print(map, movement);
        }

        var gpsCoordinates = new List<long>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 'O')
                {
                    gpsCoordinates.Add(100 * i + j);
                }
            }
        }

        return new ValueTask<string>(gpsCoordinates.Sum().ToString());
    }

    private void Print(char[,] map, char movement)
    {
        Debug.WriteLine(string.Empty);
        Debug.WriteLine($"Move {movement}:");
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Debug.Write(map[i, j]);
            }
            Debug.WriteLine(string.Empty);
        }
    }

    private Point Move(char[,] map, Point position, char movement)
    {
        var newX = position.X;
        var newY = position.Y;

        switch (movement)
        {
            case '^':
                newX--;
                break;
            case '>':
                newY++;
                break;
            case 'v':
                newX++;
                break;
            case '<':
                newY--;
                break;
        }

        if (newX >= 0
            && newX < map.GetLength(0)
            && newY >= 0
            && newY < map.GetLength(1))
        {
            var currentTile = map[position.X, position.Y];
            var nextTile = map[newX, newY];
            if (nextTile == '.')
            {
                map[newX, newY] = currentTile;
                map[position.X, position.Y] = '.';
                return new Point(newX, newY);
            }
            else if (nextTile == 'O')
            {
                Move(map, new Point(newX, newY), movement);
                nextTile = map[newX, newY];
                if (nextTile == '.')
                {
                    map[newX, newY] = currentTile;
                    map[position.X, position.Y] = '.';
                    return new Point(newX, newY);
                }
            }
            else if (nextTile == '#')
            {
                // path blocked
                return position;
            }
        }

        return position;
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
