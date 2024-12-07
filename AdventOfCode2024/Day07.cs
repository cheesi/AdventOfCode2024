namespace AdventOfCode2024;

public class Day07 : BaseDay
{
    private readonly string _input;

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day07(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var equations = new List<Equations>();
        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var parts = line.Split(' ');
            var result = long.Parse(parts[0][..^1]);
            var numbers = parts[1..].Select(long.Parse).ToArray();

            equations.Add(new Equations(result, numbers));
        }

        var calibrationValues = new List<long>();
        foreach (var equation in equations)
        {
            if (IsEquationPossible(equation))
            {
                calibrationValues.Add(equation.result);
            }
        }

        return new ValueTask<string>(calibrationValues.Sum().ToString());
    }

    private bool IsEquationPossible(Equations equation)
    {
        var possibleResults = CalculatePart(null, equation.numbers);
        return possibleResults.Contains(equation.result);
    }

    private long[] CalculatePart(long? existing, long[] numbers)
    {
        if (numbers.Length >= 2)
        {
            var sum = CalculatePart((existing ?? 0) + numbers[0], numbers[1..]);
            var product = CalculatePart((existing ?? 1) * numbers[0], numbers[1..]);
            return [.. sum, .. product];
        }
        else
        {
            var sum = (long)existing! + numbers[0];
            var product = (long)existing! * numbers[0];
            return [sum, product];
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var equations = new List<Equations>();
        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var parts = line.Split(' ');
            var result = long.Parse(parts[0][..^1]);
            var numbers = parts[1..].Select(long.Parse).ToArray();

            equations.Add(new Equations(result, numbers));
        }

        var calibrationValues = new List<long>();
        foreach (var equation in equations)
        {
            var possibleResults = CalculatePart2(null, equation.numbers);
            if (possibleResults.Contains(equation.result))
            {
                calibrationValues.Add(equation.result);
            }
        }

        return new ValueTask<string>(calibrationValues.Sum().ToString());
    }

    private long[] CalculatePart2(long? existing, long[] numbers)
    {
        if (numbers.Length >= 2)
        {
            var sum = CalculatePart2((existing ?? 0) + numbers[0], numbers[1..]);
            var product = CalculatePart2((existing ?? 1) * numbers[0], numbers[1..]);
            var concat = (existing?.ToString() ?? "") + numbers[0].ToString();
            var concats = CalculatePart2(long.Parse(concat), numbers[1..]);
            return [.. sum, .. product, .. concats];
        }
        else
        {
            var sum = (long)existing! + numbers[0];
            var product = (long)existing! * numbers[0];
            var contact = long.Parse(existing.ToString() + numbers[0].ToString());
            return [sum, product, contact];
        }
    }

    record Equations(long result, long[] numbers);
}
