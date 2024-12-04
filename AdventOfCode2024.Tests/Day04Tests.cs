namespace AdventOfCode2024.Tests;

public class Day04Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    MMMSXXMASM
                    MSAMXMSMSA
                    AMXSXMAAMM
                    MSAMASMSMX
                    XMASAMXAMM
                    XXAMMXXAMA
                    SMSMSASXSS
                    SAXAMASAAA
                    MAMMMXMMMM
                    MXMXAXMASX
                    """;
        var systemUnderTest = new Day04(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("18");
    }


    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    MMMSXXMASM
                    MSAMXMSMSA
                    AMXSXMAAMM
                    MSAMASMSMX
                    XMASAMXAMM
                    XXAMMXXAMA
                    SMSMSASXSS
                    SAXAMASAAA
                    MAMMMXMMMM
                    MXMXAXMASX
                    """;
        var systemUnderTest = new Day04(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("9");
    }
}
