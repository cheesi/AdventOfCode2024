namespace AdventOfCode2024.Tests;

public class Day10Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    89010123
                    78121874
                    87430965
                    96549874
                    45678903
                    32019012
                    01329801
                    10456732
                    """;
        var systemUnderTest = new Day10(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("36");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    89010123
                    78121874
                    87430965
                    96549874
                    45678903
                    32019012
                    01329801
                    10456732
                    """;
        var systemUnderTest = new Day10(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("81");
    }
}
