using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day14 : BaseDay
{
    private readonly string _input;

    private readonly int maxX;
    private readonly int maxY;

    public Day14()
    {
        _input = File.ReadAllText(InputFilePath);
        maxX = 103 - 1;
        maxY = 101 - 1;
    }

    public Day14(string input)
    {
        _input = input;
        maxX = 7 - 1;
        maxY = 11 - 1;
    }

    public override ValueTask<string> Solve_1()
    {
        var robots = new List<Robot>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var match = Regex.Matches(line, "p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)").First();
            var robot = new Robot
            {
                Position = new Point(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[1].Value)),
                XMovement = int.Parse(match.Groups[3].Value),
                YMovement = int.Parse(match.Groups[4].Value)
            };
            robots.Add(robot);
        }

        for (int i = 1; i <= 100; i++)
        {
            foreach (var robot in robots)
            {
                var newX = robot.Position.X + robot.YMovement;
                var newY = robot.Position.Y + robot.XMovement;

                if (newX < 0)
                {
                    newX = maxX + 1 - Math.Abs(newX);
                }
                else if (newX > maxX)
                {
                    newX -= maxX + 1;
                }
                if (newY < 0)
                {
                    newY = maxY + 1 - Math.Abs(newY);
                }
                else if (newY > maxY)
                {
                    newY -= maxY + 1;
                }

                robot.Position = new Point(newX, newY);
            }
        }

        var q1 = robots.Count(robot => robot.Position.X < maxX / 2 && robot.Position.Y < maxY / 2);
        var q2 = robots.Count(robot => robot.Position.X < maxX / 2 && robot.Position.Y > maxY / 2);
        var q3 = robots.Count(robot => robot.Position.X > maxX / 2 && robot.Position.Y < maxY / 2);
        var q4 = robots.Count(robot => robot.Position.X > maxX / 2 && robot.Position.Y > maxY / 2);

        var safetyFactor = q1 * q2 * q3 * q4;

        return new ValueTask<string>(safetyFactor.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var robots = new List<Robot>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var match = Regex.Matches(line, "p=(\\d+),(\\d+) v=(-?\\d+),(-?\\d+)").First();
            var robot = new Robot
            {
                Position = new Point(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[1].Value)),
                XMovement = int.Parse(match.Groups[3].Value),
                YMovement = int.Parse(match.Groups[4].Value)
            };
            robots.Add(robot);
        }

        var seconds = 0;
        for (int i = 1; i < 10000; i++)
        {
            foreach (var robot in robots)
            {
                var newX = robot.Position.X + robot.YMovement;
                var newY = robot.Position.Y + robot.XMovement;

                if (newX < 0)
                {
                    newX = maxX + 1 - Math.Abs(newX);
                }
                else if (newX > maxX)
                {
                    newX -= maxX + 1;
                }
                if (newY < 0)
                {
                    newY = maxY + 1 - Math.Abs(newY);
                }
                else if (newY > maxY)
                {
                    newY -= maxY + 1;
                }

                robot.Position = new Point(newX, newY);
            }

            if (PrintMap(robots, i))
            {
                seconds = i;
                break;
            }
        }

        return new ValueTask<string>(seconds.ToString());
    }

    private bool PrintMap(List<Robot> robots, int counter)
    {
        Console.Clear();
        Console.WriteLine($"{counter} seconds");

        var sb = new StringBuilder();
        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                if (robots.Any(robot => robot.Position.X == x && robot.Position.Y == y))
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
            }
            sb.AppendLine();
        }

        var str = sb.ToString();
        if (str.Contains("#############################"))
        {
            Console.Write(str);
            return true;
        }
        return false;
    }

    class Robot
    {
        public Point Position { get; set; }

        public int XMovement { get; set; }

        public int YMovement { get; set; }
    }
}
