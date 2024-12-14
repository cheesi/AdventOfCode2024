namespace AdventOfCode2024.Tests;

public class Day14Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    p=0,4 v=3,-3
                    p=6,3 v=-1,-3
                    p=10,3 v=-1,2
                    p=2,0 v=2,-1
                    p=0,0 v=1,3
                    p=3,0 v=-2,-2
                    p=7,6 v=-1,-3
                    p=3,0 v=-1,-2
                    p=9,3 v=2,3
                    p=7,3 v=-1,2
                    p=2,4 v=2,-3
                    p=9,5 v=-3,-3
                    """;
        var systemUnderTest = new Day14(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("12");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """

                    """;
        var systemUnderTest = new Day14(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("");
    }
}
