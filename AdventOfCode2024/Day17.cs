namespace AdventOfCode2024;

public class Day17 : BaseDay
{
    private readonly string _input;

    public Day17()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day17(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var registerA = 0;
        var registerB = 0;
        var registerC = 0;
        var output = new List<int>();

        int[] program = [];

        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (line.StartsWith("Register A"))
            {
                var parts = line.Split(": ");
                registerA = int.Parse(parts[1]);
            }
            else if (line.StartsWith("Register B"))
            {
                var parts = line.Split(": ");
                registerB = int.Parse(parts[1]);
            }
            else if (line.StartsWith("Register C"))
            {
                var parts = line.Split(": ");
                registerC = int.Parse(parts[1]);
            }
            else if (line.StartsWith("Program"))
            {
                var parts = line.Split(": ");
                program = parts[1].Split(',').Select(int.Parse).ToArray();;
            }
        }

        var instructionPointer = 0;
        while (instructionPointer < program.Length)
        {
            var opcode = program[instructionPointer];
            var operand = program[instructionPointer + 1];

            if (opcode == 0) // adv (combo)
            {
                var denominator = (int)Math.Pow(2, GetComboValue(operand));
                var result = registerA / denominator;
                registerA = result;
                instructionPointer += 2;
            }
            else if (opcode == 1) // bxl (literal)
            {
                var result = registerB ^ operand;
                registerB = result;
                instructionPointer += 2;
            }
            else if (opcode == 2) // bst (combo)
            {
                var modulo8 = GetComboValue(operand) % 8;
                var modul08_3bit = modulo8 & 0b_111;
                registerB = modul08_3bit;
                instructionPointer += 2;
            }
            else if (opcode == 3) // jnz (literal)
            {
                if (registerA == 0)
                {
                    instructionPointer += 2;
                }
                else
                {
                    instructionPointer = operand;
                }
            }
            else if (opcode == 4) // bxc (read but ignore)
            {
                var result = registerB ^ registerC;
                registerB = result;
                instructionPointer += 2;
            }
            else if (opcode == 5) // out (combo)
            {
                var modulo8 = GetComboValue(operand) % 8;
                var modul08_3bit = modulo8 & 0b_111;

                output.Add(modul08_3bit);

                instructionPointer += 2;
            }
            else if (opcode == 6) // bdv (combo)
            {
                var denominator = (int)Math.Pow(2, GetComboValue(operand));
                var result = registerA / denominator;
                registerB = result;
                instructionPointer += 2;
            }
            else if (opcode == 7) // cdv (combo)
            {
                var denominator = (int)Math.Pow(2, GetComboValue(operand));
                var result = registerA / denominator;
                registerC = result;
                instructionPointer += 2;
            }
        }

        return new ValueTask<string>(string.Join(',', output));

        int GetComboValue(int operand)
        {
            return operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => registerA,
                5 => registerB,
                6 => registerC,
                7 => throw new NotSupportedException()
            };
        }
    }

    public override ValueTask<string> Solve_2()
    {
        // Needs performance optimization :(
        var registerBOriginal = 0l;
        var registerCOriginal = 0l;


        long[] program = [];

        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (line.StartsWith("Register B"))
            {
                var parts = line.Split(": ");
                registerBOriginal = long.Parse(parts[1]);
            }
            else if (line.StartsWith("Register C"))
            {
                var parts = line.Split(": ");
                registerCOriginal = long.Parse(parts[1]);
            }
            else if (line.StartsWith("Program"))
            {
                var parts = line.Split(": ");
                program = parts[1].Split(',').Select(long.Parse).ToArray();;
            }
        }

        var resultLock = new Lock();
        var lowestARegister = long.MaxValue;

        Parallel.For(int.MaxValue, long.MaxValue, (startRegisterA) =>
        //Parallel.For(0, 200_000, (startRegisterA) =>
        {
            long registerA = startRegisterA;
            long registerB = registerBOriginal;
            long registerC = registerCOriginal;
            var output = new List<long>();

            var instructionPointer = 0l;
            while (instructionPointer < program.Length)
            {
                if (!program[0..output.Count].SequenceEqual(output))
                {
                    return;
                }

                var opcode = program[instructionPointer];
                var operand = program[instructionPointer + 1];

                if (opcode == 0) // adv (combo)
                {
                    var denominator = (long)Math.Pow(2, GetComboValue(operand));
                    var result = registerA / denominator;
                    registerA = result;
                    instructionPointer += 2;
                }
                else if (opcode == 1) // bxl (literal)
                {
                    var result = registerB ^ operand;
                    registerB = result;
                    instructionPointer += 2;
                }
                else if (opcode == 2) // bst (combo)
                {
                    var modulo8 = GetComboValue(operand) % 8;
                    var modul08_3bit = modulo8 & 0b_111;
                    registerB = modul08_3bit;
                    instructionPointer += 2;
                }
                else if (opcode == 3) // jnz (literal)
                {
                    if (registerA == 0)
                    {
                        instructionPointer += 2;
                    }
                    else
                    {
                        instructionPointer = operand;
                    }
                }
                else if (opcode == 4) // bxc (read but ignore)
                {
                    var result = registerB ^ registerC;
                    registerB = result;
                    instructionPointer += 2;
                }
                else if (opcode == 5) // out (combo)
                {
                    var modulo8 = GetComboValue(operand) % 8;
                    var modul08_3bit = modulo8 & 0b_111;

                    output.Add(modul08_3bit);

                    instructionPointer += 2;
                }
                else if (opcode == 6) // bdv (combo)
                {
                    var denominator = (int)Math.Pow(2, GetComboValue(operand));
                    var result = registerA / denominator;
                    registerB = result;
                    instructionPointer += 2;
                }
                else if (opcode == 7) // cdv (combo)
                {
                    var denominator = (int)Math.Pow(2, GetComboValue(operand));
                    var result = registerA / denominator;
                    registerC = result;
                    instructionPointer += 2;
                }
            }

            if (output.SequenceEqual(program))
            {
                lock (resultLock)
                {
                    if (startRegisterA < lowestARegister)
                    {
                        lowestARegister = startRegisterA;
                    }
                }
            }


            long GetComboValue(long operand)
            {
                return operand switch
                {
                    0 => 0,
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => registerA,
                    5 => registerB,
                    6 => registerC,
                    7 => throw new NotSupportedException()
                };
            }
        });

        return new ValueTask<string>(lowestARegister.ToString());
    }
}
