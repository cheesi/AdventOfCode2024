using System.Drawing;

namespace AdventOfCode2024;

public class Day12 : BaseDay
{
    private readonly string _input;

    public Day12()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day12(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }

        var visited = new List<Point>();
        var regions = new List<Region>();

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var currentCoordinate = new Point(x, y);
                if (visited.Contains(currentCoordinate))
                {
                    continue;
                }

                var region = FindRegion(map, currentCoordinate);
                regions.Add(region);

                visited.AddRange(region.plots);
            }
        }

        var prices = new List<long>();
        foreach (var region in regions)
        {
            var perimeter = CalculatePerimeterOfRegion(map, region);
            var area = region.plots.Count;
            var price = perimeter * area;
            prices.Add(price);
        }

        return new ValueTask<string>(prices.Sum().ToString());
    }

    private static Region FindRegion(char[,] map, Point currentCoordinate)
    {
        var regionName = map[currentCoordinate.X, currentCoordinate.Y];
        var region = new Region(regionName, []);
        region.plots.Add(currentCoordinate);
        FindPlotsForRegion(map, region, currentCoordinate);
        region.plots.Sort(new CoordinateSorter());

        return region;
    }

    private static void FindPlotsForRegion(
        char[,] map,
        Region region,
        Point currentCoordinate,
        List<Point>? visited = null)
    {
        if (visited is null)
        {
            visited = [currentCoordinate];
        }

        var xMinus = currentCoordinate.X - 1;
        if (xMinus >= 0)
        {
            var currentPlot = new Point(xMinus, currentCoordinate.Y);
            if (IsSameRegion(map, region, currentPlot, visited))
            {
                FindPlotsForRegion(map, region, currentPlot, visited);
            }
        }
        var xPlus = currentCoordinate.X + 1;
        if (xPlus < map.GetLength(0))
        {
            var currentPlot = new Point(xPlus, currentCoordinate.Y);
            if (IsSameRegion(map, region, currentPlot, visited))
            {
                FindPlotsForRegion(map, region, currentPlot, visited);
            }
        }

        var yMinus = currentCoordinate.Y - 1;
        if (yMinus >= 0)
        {
            var currentPlot = new Point(currentCoordinate.X, yMinus);
            if (IsSameRegion(map, region, currentPlot, visited))
            {
                FindPlotsForRegion(map, region, currentPlot, visited);
            }
        }

        var yPlus = currentCoordinate.Y + 1;
        if (yPlus < map.GetLength(1))
        {
            var currentPlot = new Point(currentCoordinate.X, yPlus);
            if (IsSameRegion(map, region, currentPlot, visited))
            {
                FindPlotsForRegion(map, region, currentPlot, visited);
            }
        }
    }

    private static bool IsSameRegion(char[,] map, Region region, Point currentPlot, List<Point> visited)
    {
        if (visited.Contains(currentPlot))
        {
            return false;
        }
        visited.Add(currentPlot);

        var currentPlotRegionName = map[currentPlot.X, currentPlot.Y];
        if (currentPlotRegionName == region.regionName)
        {
            region.plots.Add(currentPlot);
            return true;
        }

        return false;
    }

    private static int CalculatePerimeterOfRegion(char[,] map, Region region)
    {
        var perimeter = 0;
        foreach (var plot in region.plots)
        {
            var xMinus = plot.X - 1;
            if (xMinus >= 0)
            {
                var nextPlot = new Point(xMinus, plot.Y);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                }
            }
            else
            {
                perimeter++;
            }
            var xPlus = plot.X + 1;
            if (xPlus < map.GetLength(0))
            {
                var nextPlot = new Point(xPlus, plot.Y);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                }
            }
            else
            {
                perimeter++;
            }

            var yMinus = plot.Y - 1;
            if (yMinus >= 0)
            {
                var nextPlot = new Point(plot.X, yMinus);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                }
            }
            else
            {
                perimeter++;
            }

            var yPlus = plot.Y + 1;
            if (yPlus < map.GetLength(1))
            {
                var nextPlot = new Point(plot.X, yPlus);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                }
            }
            else
            {
                perimeter++;
            }
        }
        return perimeter;
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }

        var visited = new List<Point>();
        var regions = new List<Region>();

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var currentCoordinate = new Point(x, y);
                if (visited.Contains(currentCoordinate))
                {
                    continue;
                }

                var region = FindRegion(map, currentCoordinate);
                regions.Add(region);

                visited.AddRange(region.plots);
            }
        }

        var prices = new List<long>();
        foreach (var region in regions)
        {
            var perimeter = CalculatePerimeterSidesOfRegion(map, region);
            var area = region.plots.Count;
            var price = perimeter * area;
            prices.Add(price);
        }

        return new ValueTask<string>(prices.Sum().ToString());
    }

    private static int CalculatePerimeterSidesOfRegion(char[,] map, Region region)
    {
        var upFence = new List<Point>();
        var rightFence = new List<Point>();
        var downFence = new List<Point>();
        var leftFence = new List<Point>();

        var perimeter = 0;
        foreach (var plot in region.plots)
        {
            var xMinus = plot.X - 1;
            if (xMinus >= 0)
            {
                var nextPlot = new Point(xMinus, plot.Y);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                    upFence.Add(plot);
                }
            }
            else
            {
                perimeter++;
                upFence.Add(plot);
            }
            var xPlus = plot.X + 1;
            if (xPlus < map.GetLength(0))
            {
                var nextPlot = new Point(xPlus, plot.Y);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                    downFence.Add(plot);
                }
            }
            else
            {
                perimeter++;
                downFence.Add(plot);
            }

            var yMinus = plot.Y - 1;
            if (yMinus >= 0)
            {
                var nextPlot = new Point(plot.X, yMinus);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                    leftFence.Add(plot);
                }
            }
            else
            {
                perimeter++;
                leftFence.Add(plot);
            }

            var yPlus = plot.Y + 1;
            if (yPlus < map.GetLength(1))
            {
                var nextPlot = new Point(plot.X, yPlus);
                if (!region.plots.Contains(nextPlot))
                {
                    perimeter++;
                    rightFence.Add(plot);
                }
            }
            else
            {
                perimeter++;
                rightFence.Add(plot);
            }
        }

        upFence = upFence.OrderBy(plot => plot.X).ThenBy(plot => plot.Y).ToList();
        rightFence = rightFence.OrderBy(plot => plot.Y).ThenBy(plot => plot.X).ToList();
        downFence = downFence.OrderByDescending(plot => plot.X).ThenByDescending(plot => plot.Y).ToList();
        leftFence = leftFence.OrderByDescending(plot => plot.Y).ThenByDescending(plot => plot.X).ToList();

        var previous = upFence[0];
        var counter = 0;
        foreach (var plot in upFence.Skip(1))
        {
            if (previous.Y + 1 == plot.Y && previous.X == plot.X)
            {
                counter++;
            }
            else if (counter > 0)
            {
                perimeter -= counter;
                counter = 0;
            }
            previous = plot;
        }
        if (counter > 0)
        {
            perimeter -= counter;
            counter = 0;
        }

        previous = rightFence[0];
        foreach (var plot in rightFence.Skip(1))
        {
            if (previous.X + 1 == plot.X && previous.Y == plot.Y)
            {
                counter++;
            }
            else if (counter > 0)
            {
                perimeter -= counter;
                counter = 0;
            }
            previous = plot;
        }
        if (counter > 0)
        {
            perimeter -= counter;
            counter = 0;
        }

        previous = downFence[0];
        foreach (var plot in downFence.Skip(1))
        {
            if (previous.Y - 1 == plot.Y && previous.X == plot.X)
            {
                counter++;
            }
            else if (counter > 0)
            {
                perimeter -= counter;
                counter = 0;
            }
            previous = plot;
        }
        if (counter > 0)
        {
            perimeter -= counter;
            counter = 0;
        }

        previous = leftFence[0];
        foreach (var plot in leftFence.Skip(1))
        {
            if (previous.X - 1 == plot.X && previous.Y == plot.Y)
            {
                counter++;
            }
            else if (counter > 0)
            {
                perimeter -= counter;
                counter = 0;
            }
            previous = plot;
        }
        if (counter > 0)
        {
            perimeter -= counter;
        }

        return perimeter;
    }

    record Region(char regionName, List<Point> plots);

    internal class CoordinateSorter : IComparer<Point>
    {
        public int Compare(Point a, Point b)
        {
            if (a.X < b.X)
            {
                return -1;
            }
            else if (a.X > b.X)
            {
                return 1;
            }
            else
            {
                if (a.Y < b.Y)
                {
                    return -1;
                }
                else if (a.Y > b.Y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
