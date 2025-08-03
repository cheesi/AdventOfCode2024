namespace AdventOfCode2024.Tests;

public class Day18Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    5,4
                    4,2
                    4,5
                    3,0
                    2,1
                    6,3
                    2,4
                    1,5
                    0,6
                    3,3
                    2,6
                    5,1
                    1,2
                    5,5
                    2,5
                    6,5
                    1,4
                    0,4
                    6,4
                    1,1
                    6,1
                    1,0
                    0,5
                    1,6
                    2,0
                    """;
        var systemUnderTest = new Day18(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("22");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    5,4
                    4,2
                    4,5
                    3,0
                    2,1
                    6,3
                    2,4
                    1,5
                    0,6
                    3,3
                    2,6
                    5,1
                    1,2
                    5,5
                    2,5
                    6,5
                    1,4
                    0,4
                    6,4
                    1,1
                    6,1
                    1,0
                    0,5
                    1,6
                    2,0
                    """;
        var systemUnderTest = new Day18(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("6,1");
    }
}
