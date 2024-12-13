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

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
