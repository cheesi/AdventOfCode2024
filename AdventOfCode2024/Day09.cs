namespace AdventOfCode2024;

public class Day09 : BaseDay
{
    private readonly string _input;

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day09(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var disk = ParseDisk();

        for (var i = disk.Count - 1; i >= 0; i--)
        {
            var fileId = disk.GetAt(i);
            if (fileId.Value >= 0)
            {
                var target = disk.First(x => x.Value == -1);
                if (target.Key < fileId.Key)
                {
                    disk[target.Key] = fileId.Value;
                    disk[i] = -1;
                }
                else
                {
                    break;
                }
            }
        }

        var checkSum = CalculateChecksum(disk);

        return new ValueTask<string>(checkSum.ToString());
    }

    private static long CalculateChecksum(OrderedDictionary<int, int> disk)
    {
        var checkSum = 0L;
        foreach (var position in disk)
        {
            if (position.Value >= 0)
            {
                checkSum += position.Key * position.Value;
            }
        }

        return checkSum;
    }

    private OrderedDictionary<int, int> ParseDisk()
    {
        var disk = new OrderedDictionary<int, int>();
        var fileMode = true;
        var diskIndex = 0;
        var fileIdCounter = 0;
        var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var diskMap = line.ToCharArray().Select(x => x.ToString()).Select(int.Parse).ToArray();
            foreach (var filesAndFreeSpace in diskMap)
            {
                for (var i = 1; i <= filesAndFreeSpace; i++)
                {
                    if (fileMode)
                    {
                        disk.Add(diskIndex, fileIdCounter);
                    }
                    else
                    {
                        disk.Add(diskIndex, -1);
                    }
                    diskIndex++;
                }

                if (fileMode)
                {
                    fileIdCounter++;
                }

                fileMode = !fileMode;
            }
        }
        return disk;
    }

    public override ValueTask<string> Solve_2()
    {
        var disk = ParseDisk();

        for (var i = disk.Count - 1; i >= 0; i--)
        {
            var fileIdEnd = disk.GetAt(i);

            if (fileIdEnd.Value >= 0)
            {
                var counter = 1;
                while ((i - counter >= 0) && disk.GetAt(i - counter).Value == fileIdEnd.Value)
                {
                    counter++;
                }
                var fileIdStart = disk.GetAt(i - counter + 1);
                var length = fileIdEnd.Key - fileIdStart.Key + 1;

                var startTarget = -1;
                var consecutiveFree = 0;
                for (var j = 0; consecutiveFree != length && j < fileIdStart.Key; j++)
                {
                    var space = disk.GetAt(j);
                    if (space.Value == -1)
                    {
                        consecutiveFree++;
                        if (startTarget == -1)
                        {
                            startTarget = j;
                        }
                    }
                    else
                    {
                        consecutiveFree = 0;
                        startTarget = -1;
                    }
                }

                if (startTarget != -1 && consecutiveFree == length)
                {
                    for (var j = 0; j < length; j++)
                    {
                        disk[startTarget + j] = disk[fileIdStart.Key + j];
                        disk[fileIdStart.Key + j] = -1;
                    }
                }

                i = fileIdStart.Key;
            }
        }

        var checkSum = CalculateChecksum(disk);

        return new ValueTask<string>(checkSum.ToString());
    }
}
