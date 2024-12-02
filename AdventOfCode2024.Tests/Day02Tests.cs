namespace AdventOfCode2024.Tests;

public class Day02Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    7 6 4 2 1
                    1 2 7 8 9
                    9 7 6 2 1
                    1 3 2 4 5
                    8 6 4 4 1
                    1 3 6 7 9
                    """;
        var systemUnderTest = new Day02(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("2");
    }


    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    7 6 4 2 1
                    1 2 7 8 9
                    9 7 6 2 1
                    1 3 2 4 5
                    8 6 4 4 1
                    1 3 6 7 9
                    """;
        var systemUnderTest = new Day02(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("4");
    }
}
