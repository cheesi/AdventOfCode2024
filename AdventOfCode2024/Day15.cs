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
        var movements = new StringBuilder();
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var lastLine = Array.FindLastIndex(lines, x => x.StartsWith("#######"));
        var map = new char[lastLine + 1, lines[lastLine].Length * 2];
        var positionRobot = new Point(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i > lastLine)
            {
                movements.Append(lines[i]);
            }
            else if (i <= lastLine)
            {
                // Build map
                for (int j = 0; j < lines[i].Length * 2; j = j + 2)
                {
                    var tile = lines[i][j / 2];
                    map[i, j] = tile;
                    if (tile == 'O')
                    {
                        map[i, j] = '[';
                    }
                    map[i, j + 1] = tile switch
                    {
                        '#' => '#',
                        'O' => ']',
                        '.' => '.',
                        '@' => '.'
                    };
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
            positionRobot = Move2(map, positionRobot, movement);
            //Print(map, movement);
        }

        //Print(map, 'I');

        var gpsCoordinates = new List<long>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == '[')
                {
                    gpsCoordinates.Add(100 * i + j);
                }
            }
        }

        return new ValueTask<string>(gpsCoordinates.Sum().ToString());
    }

    private bool CheckMapForInvalidTiles(char[,] map)
    {
        try
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '[')
                    {
                        if (map[i, j + 1] != ']')
                        {
                            return false;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return true;
    }

    private Point Move2(char[,] map, Point position, char movement)
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
            else if (nextTile is '[' or ']' && movement is '<' or '>')
            {
                Move2(map, new Point(newX, newY), movement);
                nextTile = map[newX, newY];
                if (nextTile == '.')
                {
                    map[newX, newY] = currentTile;
                    map[position.X, position.Y] = '.';
                    return new Point(newX, newY);
                }
                return position;
            }
            else if (nextTile is '[' or ']' && movement is '^' or 'v')
            {
                var nextPosition = new Point(newX, newY);
                var canMove = CanPushBox(map, nextPosition, (movement is '^') ? x => x - 1 : x => x + 1);
                if (canMove)
                {
                    PushBox(map, nextPosition, movement);
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

    public bool CanPushBox(char[,] map, Point position, Func<int, int> rowModifier)
    {
        var currentTile = map[position.X, position.Y];
        if (currentTile == '.')
        {
            return true;
        }
        else if (currentTile == '#')
        {
            return false;
        }
        else
        {
            var currentTIly2Y = (currentTile == '[') ? position.Y + 1 : position.Y - 1;
            return CanPushBox(map, position with { X = rowModifier(position.X) }, rowModifier)
                && CanPushBox(map, new Point(rowModifier(position.X), currentTIly2Y), rowModifier);;
        }
    }

    public void PushBox(char[,] map, Point position, char movement, Point? previousPosition = null)
    {
        var newX = position.X;

        switch (movement)
        {
            case '^':
                newX--;
                break;
            case 'v':
                newX++;
                break;
        }

        var currentTile = map[position.X, position.Y];
        if (currentTile == '.')
        {
            var previousTile = map[previousPosition.Value.X, previousPosition.Value.Y];
            map[position.X, position.Y] = previousTile;
            map[previousPosition.Value.X, previousPosition.Value.Y] = '.';
            return;
        }
        else if (currentTile == '#')
        {
            return;
        }
        else
        {
            var currentTile2Y = (currentTile == '[') ? position.Y + 1 : position.Y - 1;
            PushBox(map, position with { X = newX }, movement, position);
            PushBox(map, new Point(newX, currentTile2Y), movement, position with { Y = currentTile2Y });;

            map[newX, position.Y] = currentTile;
            map[newX, currentTile2Y] = (currentTile == '[') ? ']' : '[';

            map[position.X, position.Y] = '.';
            map[position.X, currentTile2Y] = '.';
        }
    }
}
