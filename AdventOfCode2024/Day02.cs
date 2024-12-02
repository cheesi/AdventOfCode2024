namespace AdventOfCode2024;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day02(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var safeReports = 0;

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            if (ValidateReport(numbers))
            {
                safeReports++;
            }
        }

        return new ValueTask<string>(safeReports.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var safeReports = 0;

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            if (ValidateReport(numbers))
            {
                safeReports++;
            }
            else
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    var newNumbers = numbers.ToList();
                    newNumbers.RemoveAt(i);
                    if (ValidateReport(newNumbers))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        return new ValueTask<string>(safeReports.ToString());
    }

    private static bool ValidateReport(IEnumerable<long> parts)
    {
        long? previous = null;
        Mode? mode = null;
        var safe = true;

        foreach (var part in parts)
        {
            if (previous is not null)
            {
                if (previous > part)
                {
                    if (mode is not null && mode == Mode.Increasing)
                    {
                        safe = false;
                    }
                    if (previous - part > 3)
                    {
                        safe = false;
                    }
                    mode = Mode.Decreasing;
                }
                else if (part > previous)
                {
                    if (mode is not null && mode == Mode.Decreasing)
                    {
                        safe = false;
                    }
                    if (part - previous > 3)
                    {
                        safe = false;
                    }
                    mode = Mode.Increasing;
                }
                else
                {
                    safe = false;
                }
            }
            if (!safe)
            {
                break;
            }
            previous = part;
        }

        return safe;
    }

    enum Mode
    {
        Increasing,
        Decreasing
    }
}
