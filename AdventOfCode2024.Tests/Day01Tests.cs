namespace AdventOfCode2024.Tests;

public class Day01Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    3   4
                    4   3
                    2   5
                    1   3
                    3   9
                    3   3
                    """;
        var systemUnderTest = new Day01(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("11");
    }


    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    3   4
                    4   3
                    2   5
                    1   3
                    3   9
                    3   3
                    """;
        var systemUnderTest = new Day01(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("31");
    }
}
