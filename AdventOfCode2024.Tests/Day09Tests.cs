namespace AdventOfCode2024.Tests;

public class Day09Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    2333133121414131402
                    """;
        var systemUnderTest = new Day09(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("1928");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    2333133121414131402
                    """;
        var systemUnderTest = new Day09(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("2858");
    }
}
