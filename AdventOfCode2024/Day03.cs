using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day03(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var products = new List<long>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var matches = Regex.Matches(line, "mul\\((\\d+),(\\d+)\\)");
            foreach (Match match in matches)
            {
                var firstNr = long.Parse(match.Groups[1].Value);
                var secondNr = long.Parse(match.Groups[2].Value);
                var product = firstNr * secondNr;
                products.Add(product);
            }
        }

        return new ValueTask<string>(products.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var products = new List<long>();
        var enabled = true;

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var matches = Regex.Matches(line, "(?:(?<func>mul)\\((\\d+),(\\d+)\\)|(?<func>do)\\(\\)|(?<func>don't)\\(\\))");
            foreach (Match match in matches)
            {
                var func = match.Groups["func"].Value;
                if (func is "do")
                {
                    enabled = true;
                }
                else if (func is "don't")
                {
                    enabled = false;
                }
                else if (func is "mul")
                {
                    if (enabled)
                    {
                        var firstNr = long.Parse(match.Groups[1].Value);
                        var secondNr = long.Parse(match.Groups[2].Value);
                        var product = firstNr * secondNr;
                        products.Add(product);
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }

            }
        }

        return new ValueTask<string>(products.Sum().ToString());
    }
}
