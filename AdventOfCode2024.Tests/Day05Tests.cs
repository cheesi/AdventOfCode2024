namespace AdventOfCode2024.Tests;

public class Day05Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    47|53
                    97|13
                    97|61
                    97|47
                    75|29
                    61|13
                    75|53
                    29|13
                    97|29
                    53|29
                    61|53
                    97|53
                    61|29
                    47|13
                    75|47
                    97|75
                    47|61
                    75|61
                    47|29
                    75|13
                    53|13
                    
                    75,47,61,53,29
                    97,61,53,29,13
                    75,29,13
                    75,97,47,61,53
                    61,13,29
                    97,13,75,29,47
                    """;
        var systemUnderTest = new Day05(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("143");
    }


    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """

                    """;
        var systemUnderTest = new Day05(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("");
    }
}
