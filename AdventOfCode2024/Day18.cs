using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode2024;

public class Day18 : BaseDay
{
    private readonly string _input;

    private readonly int _size;
    private readonly int _readSize;

    public Day18()
    {
        _input = File.ReadAllText(InputFilePath);
        _size = 71;
        _readSize = 1024;
    }

    public Day18(string input)
    {
        _input = input;
        _size = 7;
        _readSize = 12;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        Node startingNode = null!;
        Node destinationNode = null!;

        var trunkatedLines = lines.Take(_readSize).ToList();

        var map = new Node[_size, _size];
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                var node = new Node
                {
                    Name = trunkatedLines.Contains($"{j},{i}") ? '#' : '.',
                    Point = new Point(i, j)
                };
                map[i, j] = node;
                if (node.Point is { X: 0, Y: 0 })
                {
                    startingNode = node;
                }
                else if (node.Point.X == _size - 1 && node.Point.Y == _size - 1)
                {
                    destinationNode = node;
                }
            }
        }

        MapConnections(lines, map);

        var startToEndCost = Dijkstra(startingNode, destinationNode);

        return new ValueTask<string>(startToEndCost.ToString());
    }

    private void MapConnections(string[] lines, Node[,] map)
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                var node = map[i, j];
                if (!node.IsWalkable)
                {
                    continue;
                }

                if (j - 1 >= 0)
                {
                    var leftNode = map[i, j - 1];
                    if (leftNode.IsWalkable)
                    {
                        node.Connections.Add(leftNode);
                    }
                }
                if (j + 1 < _size)
                {
                    var rightNode = map[i, j + 1];
                    if (rightNode.IsWalkable)
                    {
                        node.Connections.Add(rightNode);
                    }
                }
                if (i - 1 >= 0)
                {
                    var upNode = map[i - 1, j];
                    if (upNode.IsWalkable)
                    {
                        node.Connections.Add(upNode);
                    }
                }
                if (i + 1 < _size)
                {
                    var downNode = map[i + 1, j];
                    if (downNode.IsWalkable)
                    {
                        node.Connections.Add(downNode);
                    }
                }
            }
        }
    }

    private static int Dijkstra(Node startingNode, Node destinationNode)
    {
        var queue = new List<Node>
        {
            startingNode
        };
        var startToEndCost = int.MaxValue;
        startingNode.MinCostToStart = 0;

        while (queue.Count > 0)
        {
            queue = queue.OrderBy(x => x.MinCostToStart).ToList();
            var node = queue[0];
            queue.Remove(node);
            foreach (var connectedNode in node.Connections)
            {
                if (connectedNode.Visited)
                {
                    continue;
                }

                const int cost = 1;
                if (connectedNode.MinCostToStart is null
                    || node.MinCostToStart + cost < connectedNode.MinCostToStart && node.MinCostToStart + cost < startToEndCost)
                {
                    connectedNode.MinCostToStart = node.MinCostToStart + cost;
                    connectedNode.NearestToStart = node;
                    if (!queue.Contains(connectedNode))
                    {
                        queue.Add(connectedNode);
                    }
                }

            }
            node.Visited = true;
            if (node == destinationNode)
            {
                startToEndCost = node.MinCostToStart!.Value;
            }
        }

        return startToEndCost;
    }

    [DebuggerDisplay("{Point}")]
    class Node
    {
        public char Name { get; set; }

        public Point Point { get; set; }

        public bool IsWalkable => Name != '#';

        public List<Node> Connections { get; set; } = [];

        public int? MinCostToStart { get; set; }

        public Node? NearestToStart { get; set; }

        public bool Visited { get; set; }
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        string blockedCoordinate = "";

        for (int take = _readSize; take < lines.Length; take++)
        {
            Node startingNode = null!;
            Node destinationNode = null!;

            var trunkatedLines = lines.Take(take).ToList();

            var map = new Node[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    var node = new Node
                    {
                        Name = trunkatedLines.Contains($"{j},{i}") ? '#' : '.',
                        Point = new Point(i, j)
                    };
                    map[i, j] = node;
                    if (node.Point is { X: 0, Y: 0 })
                    {
                        startingNode = node;
                    }
                    else if (node.Point.X == _size - 1 && node.Point.Y == _size - 1)
                    {
                        destinationNode = node;
                    }
                }
            }

            MapConnections(lines, map);

            var startToEndCost = Dijkstra(startingNode, destinationNode);
            if (startToEndCost == int.MaxValue)
            {
                blockedCoordinate = trunkatedLines.Last();
                break;
            }
        }

        return new ValueTask<string>(blockedCoordinate);



        /*var path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        while (currentNode != startingNode)
        {
            currentNode = currentNode.NearestToStart!;
            path.Add(currentNode);
        }

        path.Reverse();

        var blockedCoordinate = "";

        foreach (var line in lines)
        {
            var parts = line.Split(',').Select(int.Parse).ToArray();;
            var hasBlockade = path.Any(node => node.Point.X == parts[1] && node.Point.Y == parts[0]);
            if (hasBlockade)
            {
                blockedCoordinate = $"{parts[1]},{parts[0]}";
                break;
            }
        }*/

        //return new ValueTask<string>(blockedCoordinate);
    }
}
