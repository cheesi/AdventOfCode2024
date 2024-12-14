namespace AdventOfCode2024.Tests;

public class Day12Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE
                    """;
        var systemUnderTest = new Day12(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("1930");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE
                    """;
        var systemUnderTest = new Day12(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("1206");
    }
}
