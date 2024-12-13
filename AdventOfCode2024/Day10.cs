using System.Drawing;

namespace AdventOfCode2024;

public class Day10 : BaseDay
{
    private readonly string _input;

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day10(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var trailheads = new List<Point>();

        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new int[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = int.Parse(line[j].ToString());
                if (line[j] == '0')
                {
                    trailheads.Add(new Point(i, j));
                }
            }
        }

        var scores = new List<int>(trailheads.Count);
        foreach (var trailhead in trailheads)
        {
            var score = CalculateScore((int[,])map.Clone(), trailhead.X, trailhead.Y);
            scores.Add(score);
        }

        return new ValueTask<string>(scores.Sum().ToString());
    }

    private int CalculateScore(int[,] map, int x, int y, int? previousValue = null, bool onlyLongest = true)
    {
        if (previousValue is null || previousValue.Value + 1 == map[x, y])
        {
            var trails = 0;

            var currentValue = map[x, y];
            if (currentValue == 9)
            {
                if (onlyLongest)
                {
                    map[x, y] = -1;
                }

                return 1;
            }

            if (x - 1 >= 0)
            {
                trails += CalculateScore(map, x - 1, y, currentValue, onlyLongest);
            }
            if (y - 1 >= 0)
            {
                trails += CalculateScore(map, x, y - 1, currentValue, onlyLongest);
            }
            if (x + 1 < map.GetLength(0))
            {
                trails += CalculateScore(map, x + 1, y, currentValue, onlyLongest);
            }
            if (y + 1 < map.GetLength(1))
            {
                trails += CalculateScore(map, x, y + 1, currentValue, onlyLongest);
            }

            return trails;
        }

        return 0;
    }

    public override ValueTask<string> Solve_2()
    {
        var trailheads = new List<Point>();

        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new int[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = int.Parse(line[j].ToString());
                if (line[j] == '0')
                {
                    trailheads.Add(new Point(i, j));
                }
            }
        }

        var scores = new List<int>(trailheads.Count);
        foreach (var trailhead in trailheads)
        {
            var score = CalculateScore(map, trailhead.X, trailhead.Y, onlyLongest: false);
            scores.Add(score);
        }

        return new ValueTask<string>(scores.Sum().ToString());
    }
}
