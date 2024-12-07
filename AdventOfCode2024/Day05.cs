namespace AdventOfCode2024;

public class Day05 : BaseDay
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day05(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var middlePageNumbers = new List<int>();
        var rulesBefore = new Dictionary<int, List<int>>();
        var rulesAfter = new Dictionary<int, List<int>>();
        var manuals = new List<List<int>>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.Contains('|'))
            {
                var parts = line.Split("|").Select(int.Parse).ToArray();
                if (!rulesBefore.ContainsKey(parts[0]))
                {
                    rulesBefore[parts[0]] = new List<int>();
                }
                rulesBefore[parts[0]].Add(parts[1]);

                if (!rulesAfter.ContainsKey(parts[1]))
                {
                    rulesAfter[parts[1]] = new List<int>();
                }
                rulesAfter[parts[1]].Add(parts[0]);
            }
            else if (line.Contains(','))
            {
                var manual = line.Split(",").Select(int.Parse).ToList();
                manuals.Add(manual);
            }
        }

        foreach (var manual in manuals)
        {
            var inOrder = true;
            foreach (var page in manual)
            {
                var indexOfPage = manual.IndexOf(page);

                if (rulesBefore.TryGetValue(page, out var applicableBeforeRules))
                {
                    foreach (var rule in applicableBeforeRules)
                    {
                        var indexOf = manual.IndexOf(rule);
                        if (indexOf != -1 && page != rule && indexOf < indexOfPage)
                        {
                            inOrder = false;
                        }
                    }
                }

                if (rulesAfter.TryGetValue(page, out var applicableAfterRules))
                {
                    foreach (var rule in applicableAfterRules)
                    {
                        var indexOf = manual.IndexOf(rule);
                        if (indexOf != -1 && page != rule && indexOf > indexOfPage)
                        {
                            inOrder = false;
                        }
                    }
                }
            }

            if (inOrder)
            {
                var middlePage = manual.Skip((manual.Count - 1) / 2).First();
                middlePageNumbers.Add(middlePage);
            }
        }

        return new ValueTask<string>(middlePageNumbers.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var middlePageNumbers = new List<int>();
        var rulesBefore = new Dictionary<int, List<int>>();
        var rulesAfter = new Dictionary<int, List<int>>();
        var manuals = new List<List<int>>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.Contains('|'))
            {
                var parts = line.Split("|").Select(int.Parse).ToArray();
                if (!rulesBefore.ContainsKey(parts[0]))
                {
                    rulesBefore[parts[0]] = new List<int>();
                }
                rulesBefore[parts[0]].Add(parts[1]);

                if (!rulesAfter.ContainsKey(parts[1]))
                {
                    rulesAfter[parts[1]] = new List<int>();
                }
                rulesAfter[parts[1]].Add(parts[0]);
            }
            else if (line.Contains(','))
            {
                var manual = line.Split(",").Select(int.Parse).ToList();
                manuals.Add(manual);
            }
        }

        foreach (var manual in manuals)
        {
            var inOrder = true;
            foreach (var page in manual)
            {
                var indexOfPage = manual.IndexOf(page);

                if (rulesBefore.TryGetValue(page, out var applicableBeforeRules))
                {
                    foreach (var rule in applicableBeforeRules)
                    {
                        var indexOf = manual.IndexOf(rule);
                        if (indexOf != -1 && page != rule && indexOf < indexOfPage)
                        {
                            inOrder = false;
                        }
                    }
                }

                if (rulesAfter.TryGetValue(page, out var applicableAfterRules))
                {
                    foreach (var rule in applicableAfterRules)
                    {
                        var indexOf = manual.IndexOf(rule);
                        if (indexOf != -1 && page != rule && indexOf > indexOfPage)
                        {
                            inOrder = false;
                        }
                    }
                }
            }

            if (!inOrder)
            {
                manual.Sort(new PageSorter(rulesBefore, rulesAfter));
                var middlePage = manual.Skip((manual.Count - 1) / 2).First();
                middlePageNumbers.Add(middlePage);
            }
        }

        return new ValueTask<string>(middlePageNumbers.Sum().ToString());
    }

    public class PageSorter : IComparer<int>
    {
        private readonly Dictionary<int, List<int>> _rulesBefore;
        private readonly Dictionary<int, List<int>> _rulesAfter;

        public PageSorter(Dictionary<int, List<int>> rulesBefore, Dictionary<int, List<int>> rulesAfter)
        {
            _rulesBefore = rulesBefore;
            _rulesAfter = rulesAfter;
        }

        public int Compare(int x, int y)
        {
            if (_rulesBefore.TryGetValue(x, out var rulesBeforeX)
                && rulesBeforeX.Contains(y))
            {
                return -1;
            }
            else if (_rulesBefore.TryGetValue(y, out var rulesBeforeY)
                && rulesBeforeY.Contains(x))
            {
                return 1;
            }
            else if (_rulesAfter.TryGetValue(x, out var rulesAfterX)
                && rulesAfterX.Contains(y))
            {
                return -1;
            }
            else if (_rulesAfter.TryGetValue(y, out var rulesAfterY)
                && rulesAfterY.Contains(x))
            {
                return 1;
            }

            return 0;
        }
    }
}
