using System.Collections.Concurrent;
using System.Drawing;

namespace AdventOfCode2024;

public class Day06 : BaseDay
{
    private readonly string _input;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day06(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        Point currentPosition = new();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
                if (line[j] == '^')
                {
                    currentPosition = new Point(i, j);
                }
            }
        }

        map[currentPosition.X, currentPosition.Y] = 'X';

        var maxX = map.GetLength(0);
        var maxY = map.GetLength(1);

        var currentDirection = Direction.Up;

        try
        {
            while (currentPosition.X <= maxX && currentPosition.Y <= maxY)
            {
                var xModifier = 0;
                var yModifier = 0;
                if (currentDirection == Direction.Up)
                {
                    xModifier = -1;
                }
                else if (currentDirection == Direction.Right)
                {
                    yModifier = 1;
                }
                else if (currentDirection == Direction.Down)
                {
                    xModifier = 1;
                }
                else if (currentDirection == Direction.Left)
                {
                    yModifier = -1;
                }

                var nextX = currentPosition.X + xModifier;
                var nextY = currentPosition.Y + yModifier;
                var nextPosition = map[nextX, nextY];
                if (nextPosition == '#')
                {
                    currentDirection = ChangeDirection90Degrees(currentDirection);
                }
                else
                {
                    map[nextX, nextY] = 'X';
                    currentPosition.X = nextX;
                    currentPosition.Y = nextY;
                }
            }
        }
        catch (Exception)
        {
            // we are out of the map
        }

        var counter = 0;
        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                if (map[i, j] == 'X')
                {
                    counter++;
                }
            }
        }

        return new ValueTask<string>(counter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        Point startingPosition = new();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
                if (line[j] == '^')
                {
                    startingPosition = new Point(i, j);
                }
            }
        }

        var currentPosition = new Point(startingPosition.X, startingPosition.Y);
        //map[currentPosition.X, currentPosition.Y] = 'X';

        var maxX = map.GetLength(0);
        var maxY = map.GetLength(1);

        var currentDirection = Direction.Up;

        var pointsInPath = new HashSet<Point>();

        try
        {
            while (currentPosition.X <= maxX && currentPosition.Y <= maxY)
            {
                var xModifier = 0;
                var yModifier = 0;
                if (currentDirection == Direction.Up)
                {
                    xModifier = -1;
                }
                else if (currentDirection == Direction.Right)
                {
                    yModifier = 1;
                }
                else if (currentDirection == Direction.Down)
                {
                    xModifier = 1;
                }
                else if (currentDirection == Direction.Left)
                {
                    yModifier = -1;
                }

                var nextX = currentPosition.X + xModifier;
                var nextY = currentPosition.Y + yModifier;
                var nextPosition = map[nextX, nextY];
                if (nextPosition == '#')
                {
                    currentDirection = ChangeDirection90Degrees(currentDirection);
                }
                else
                {
                    currentPosition.X = nextX;
                    currentPosition.Y = nextY;

                    if (!pointsInPath.Contains(currentPosition))
                    {
                        pointsInPath.Add(currentPosition);
                    }
                }
            }
        }
        catch (Exception)
        {
            // we are out of the map
        }

        var loopObstacles = new ConcurrentBag<Point>();
        //foreach (var point in pointsInPath)
        Parallel.ForEach(pointsInPath, point =>
        {
            var currentMap = new char[lines.Length, lines[0].Length];
            Array.Copy(map, currentMap, map.Length);
            currentMap[point.X, point.Y] = '#';

            var newCurrentPosition = new Point(startingPosition.X, startingPosition.Y);
            var newCurrentDirection = Direction.Up;

            var knownPath = new HashSet<(Point point, Direction direction)>();

            try
            {
                while (true)
                {
                    var xModifier = 0;
                    var yModifier = 0;
                    if (newCurrentDirection == Direction.Up)
                    {
                        xModifier = -1;
                    }
                    else if (newCurrentDirection == Direction.Right)
                    {
                        yModifier = 1;
                    }
                    else if (newCurrentDirection == Direction.Down)
                    {
                        xModifier = 1;
                    }
                    else if (newCurrentDirection == Direction.Left)
                    {
                        yModifier = -1;
                    }

                    var nextX = newCurrentPosition.X + xModifier;
                    var nextY = newCurrentPosition.Y + yModifier;
                    var nextPosition = currentMap[nextX, nextY];
                    if (nextPosition == '#')
                    {
                        newCurrentDirection = ChangeDirection90Degrees(newCurrentDirection);
                    }
                    else
                    {
                        newCurrentPosition.X = nextX;
                        newCurrentPosition.Y = nextY;
                        currentMap[nextX, nextY] = 'X';

                        if (knownPath.Contains((newCurrentPosition, newCurrentDirection)) && nextPosition == 'X')
                        {
                            loopObstacles.Add(point);
                            break;
                        }
                        else if (nextPosition == 'X')
                        {
                            knownPath.Add((newCurrentPosition, newCurrentDirection));
                        }
                    }
                }
            }
            catch (Exception)
            {
                // we are out of the map
                // no loop
            }
        });

        return new ValueTask<string>(loopObstacles.Count.ToString());
    }

    private static Direction ChangeDirection90Degrees(Direction direction)
    {
        if (direction == Direction.Left)
        {
            return Direction.Up;
        }
        return direction + 1;
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
