namespace AdventOfCode2024.Tests;

public class Day03Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("161");
    }


    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("48");
    }
}
