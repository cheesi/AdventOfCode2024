namespace AdventOfCode2024;

public class Day11 : BaseDay
{
    private readonly string _input;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day11(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var arrangement = new List<long>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            arrangement = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
        }

        for (int i = 1; i <= 25; i++)
        {
            var newArrangement = new List<long>();
            foreach (var item in arrangement)
            {
                newArrangement.AddRange(Blink(item));
            }
            arrangement = newArrangement;
        }

        return new ValueTask<string>(arrangement.Count.ToString());
    }

    private static long[] Blink(long value)
    {
        if (value == 0)
        {
            return [1];
        }
        var stringified = value.ToString();
        if (stringified.Length % 2 == 0)
        {
            var firstNumber = long.Parse(stringified[..(stringified.Length / 2)]);
            var secondNumber = long.Parse(stringified[(stringified.Length / 2)..]);
            return [firstNumber, secondNumber];
        }

        return [value * 2024];
    }

    private readonly Dictionary<(long, long), long> _cache = new Dictionary<(long, long), long>();

    private long BlinkRecursive(long value, int steps = 1)
    {
        if (steps <= 75)
        {
            if (_cache.ContainsKey((value, steps)))
            {
                return _cache[(value, steps)];
            }

            if (value == 0)
            {
                var x = BlinkRecursive(1, steps + 1);
                _cache[(value, steps)] = x;
                return x;
            }
            var stringified = value.ToString();
            if (stringified.Length % 2 == 0)
            {
                var firstNumber = long.Parse(stringified[..(stringified.Length / 2)]);
                var secondNumber = long.Parse(stringified[(stringified.Length / 2)..]);

                var x = BlinkRecursive(firstNumber, steps + 1);
                var y = BlinkRecursive(secondNumber, steps + 1);
                _cache[(value, steps)] = x + y;

                return x + y;
            }

            var z = BlinkRecursive(value * 2024, steps + 1);
            _cache[(value, steps)] = z;
            return z;
        }
        else
        {
            return 1;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var initArrangement = new List<long>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            initArrangement = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
        }

        List<long> stones = new List<long>();

        foreach (var item in initArrangement)
        {
            var result = BlinkRecursive(item);
            stones.Add(result);
        }

        return new ValueTask<string>(stones.Sum().ToString());
    }
}
