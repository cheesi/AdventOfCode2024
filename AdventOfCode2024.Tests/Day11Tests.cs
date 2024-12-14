namespace AdventOfCode2024.Tests;

public class Day11Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    125 17
                    """;
        var systemUnderTest = new Day11(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("55312");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    125 17
                    """;
        var systemUnderTest = new Day11(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("65601038650482");
    }
}
