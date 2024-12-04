namespace AdventOfCode2024;

public class Day04 : BaseDay
{
    private readonly string _input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day04(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }

        var horizontal = FindHorizontalXmas(map);
        var vertical = FindVerticalXmas(map);
        var diagonalLeft = FindDiagonalLeftXmas(map);
        var diagonalRight = FindDiagonalRightXmas(map);

        var sum = horizontal + vertical + diagonalLeft + diagonalRight;

        return new ValueTask<string>(sum.ToString());
    }

    private long FindHorizontalXmas(char[,] map)
    {
        long counter = 0;
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1) - 3; j++)
            {
                var x = map[i, j];
                var m = map[i, j + 1];
                var a = map[i, j + 2];
                var s = map[i, j + 3];
                var xmas = $"{x}{m}{a}{s}";
                if (xmas is "XMAS" || xmas is "SAMX")
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    private long FindVerticalXmas(char[,] map)
    {
        long counter = 0;
        for (int i = 0; i < map.GetLength(1); i++)
        {
            for (int j = 0; j < map.GetLength(0) - 3; j++)
            {
                var x = map[j, i];
                var m = map[j + 1, i];
                var a = map[j + 2, i];
                var s = map[j + 3, i];
                var xmas = $"{x}{m}{a}{s}";
                if (xmas is "XMAS" || xmas is "SAMX")
                {
                    counter++;
                }
            }
        }

        return counter;
    }

    private long FindDiagonalLeftXmas(char[,] map)
    {
        long counter = 0;
        for (var i = 0; i < map.GetLength(0) - 3; i++)
        {
            for (var j = 0; j < map.GetLength(1) - 3; j++)
            {
                var x = map[i, j];
                var m = map[i + 1, j + 1];
                var a = map[i + 2, j + 2];
                var s = map[i + 3, j + 3];
                var xmas = $"{x}{m}{a}{s}";
                if (xmas is "XMAS" || xmas is "SAMX")
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    private long FindDiagonalRightXmas(char[,] map)
    {
        long counter = 0;
        for (var i = 0; i < map.GetLength(0) - 3; i++)
        {
            for (var j = map.GetLength(1) - 1; j >= 3; j--)
            {
                var x = map[i, j];
                var m = map[i + 1, j - 1];
                var a = map[i + 2, j - 2];
                var s = map[i + 3, j - 3];
                var xmas = $"{x}{m}{a}{s}";
                if (xmas is "XMAS" || xmas is "SAMX")
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }

        var counter = 0;

        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                var current = map[i, j];
                if (current == 'A')
                {
                    var topleft = map[i - 1, j - 1];
                    var topright = map[i - 1, j + 1];
                    var bottomleft = map[i + 1, j - 1];
                    var bottomright = map[i + 1, j + 1];
                    if (((topleft is 'M' && bottomright is 'S')
                        || (topleft is 'S' && bottomright is 'M'))
                        && ((topright is 'S' && bottomleft is 'M')
                        || (topright is 'M' && bottomleft is 'S')))
                    {
                        counter++;
                    }
                }
            }
        }

        return new ValueTask<string>(counter.ToString());
    }
}
