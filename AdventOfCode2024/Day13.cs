namespace AdventOfCode2024;

public class Day13 : BaseDay
{
    private readonly string _input;

    public Day13()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day13(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        const int COST_BUTTON_A = 3;
        const int COST_BUTTON_B = 1;

        var machines = new List<Machine>();
        var parseMachine = new Machine();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.StartsWith("Button A"))
            {
                parseMachine.ButtonAStr = line;
            }
            else if (line.StartsWith("Button B"))
            {
                parseMachine.ButtonBStr = line;
            }
            else if (line.StartsWith("Prize"))
            {
                parseMachine.PriceStr = line;
                machines.Add(parseMachine);
                parseMachine = new Machine();
            }
        }

        var tokens = new List<long>();
        foreach (var machine in machines)
        {
            (var countA, var countB) = CalculateButtonsForPrice(machine);
            if (countA >= 0 || countB >= 0)
            {
                countA = (countA == -1) ? 0 : countA;
                countB = (countB == -1) ? 0 : countB;
                var cost = countA * COST_BUTTON_A + countB * COST_BUTTON_B;
                tokens.Add(cost);
            }
        }

        return new ValueTask<string>(tokens.Sum().ToString());
    }

    private (long countA, long countB) CalculateButtonsForPrice(Machine machine)
    {
        for (var countB = 100; countB > 0; countB--)
        {
            for (var countA = 1; countA <= 100; countA++)
            {
                var calculatedX = countA * machine.A.AddX + countB * machine.B.AddX;
                var calculatedY = countA * machine.A.AddY + countB * machine.B.AddY;
                if (calculatedX == machine.PriceX
                    && calculatedY == machine.PriceY)
                {
                    return (countA, countB);
                }
            }
        }

        return (-1, -1);
    }

    public override ValueTask<string> Solve_2()
    {
        const long COST_BUTTON_A = 3;
        const long COST_BUTTON_B = 1;

        var machines = new List<Machine>();
        var parseMachine = new Machine();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.StartsWith("Button A"))
            {
                parseMachine.ButtonAStr = line;
            }
            else if (line.StartsWith("Button B"))
            {
                parseMachine.ButtonBStr = line;
            }
            else if (line.StartsWith("Prize"))
            {
                parseMachine.PriceModifier = 10_000_000_000_000;
                parseMachine.PriceStr = line;
                machines.Add(parseMachine);
                parseMachine = new Machine();
            }
        }

        var tokens = new List<long>();
        foreach (var machine in machines)
        {
            (var countA, var countB) = CalculateButtonsForPrice2(machine);
            if (countA >= 0 || countB >= 0)
            {
                countA = (countA == -1) ? 0 : countA;
                countB = (countB == -1) ? 0 : countB;
                var cost = countA * COST_BUTTON_A + countB * COST_BUTTON_B;
                tokens.Add(cost);
            }
        }

        return new ValueTask<string>(tokens.Sum().ToString());
    }

    private (long countA, long countB) CalculateButtonsForPrice2(Machine machine)
    {
        // Craemer's rule
        var factor = (machine.A.AddX * machine.B.AddY - machine.A.AddY * machine.B.AddX);

        var countA = (machine.PriceX * machine.B.AddY - machine.PriceY * machine.B.AddX) / factor;
        var countB = (machine.A.AddX * machine.PriceY - machine.A.AddY * machine.PriceX) / factor;

        var calculatedX = countA * machine.A.AddX + countB * machine.B.AddX;
        var calculatedY = countA * machine.A.AddY + countB * machine.B.AddY;
        if (calculatedX == machine.PriceX
            && calculatedY == machine.PriceY)
        {
            return (countA, countB);
        }

        return (-1, -1);
    }


    class Machine
    {
        public Button A { get; set; } = new Button();
        public string ButtonAStr
        {
            set
            {
                var parts = value.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);

                A.AddX = int.Parse(parts[2].Split('+')[1]);
                A.AddY = int.Parse(parts[3].Split('+')[1]);
            }
        }

        public Button B { get; set; } = new Button();
        public string ButtonBStr
        {
            set
            {
                var parts = value.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);

                B.AddX = int.Parse(parts[2].Split('+')[1]);
                B.AddY = int.Parse(parts[3].Split('+')[1]);
            }
        }

        public long PriceModifier { get; set; }

        public long PriceX { get; set; }
        public long PriceY { get; set; }
        public string PriceStr
        {
            set
            {
                var parts = value.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);

                PriceX = long.Parse(parts[1].Split('=')[1]) + PriceModifier;
                PriceY = long.Parse(parts[2].Split('=')[1]) + PriceModifier;
            }
        }

        internal class Button
        {
            public long AddX { get; set; }

            public long AddY { get; set; }
        }
    }
}
