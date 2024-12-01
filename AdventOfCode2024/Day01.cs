
namespace AdventOfCode2024;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day01(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var left = new List<int>();
        var right = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var leftNumber = int.Parse(parts[0]);
            left.Add(leftNumber);
            var rightNumber = int.Parse(parts[1]);
            right.Add(rightNumber);
        }

        left = left.Order().ToList();
        right = right.Order().ToList();

        var distances = new List<int>();

        for (int i = 0; i < left.Count; i++)
        {
            var distance = Math.Abs(left[i] - right[i]);
            distances.Add(distance);
        }

        var totalDistance = distances.Sum();
        return new ValueTask<string>(totalDistance.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var left = new List<int>();
        var right = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var leftNumber = int.Parse(parts[0]);
            left.Add(leftNumber);
            var rightNumber = int.Parse(parts[1]);
            right.Add(rightNumber);
        }

        var similarity = new List<int>();
        foreach (var locatinId in left)
        {
            var countRight = right.Count(x => x == locatinId);
            var similarityScore = locatinId * countRight;
            similarity.Add(similarityScore);
        }

        var totalSimilaryScore = similarity.Sum();
        return new ValueTask<string>(totalSimilaryScore.ToString());
    }
}
