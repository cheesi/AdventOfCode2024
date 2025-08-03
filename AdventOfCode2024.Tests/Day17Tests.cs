namespace AdventOfCode2024.Tests;

public class Day17Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    Register A: 729
                    Register B: 0
                    Register C: 0
                    
                    Program: 0,1,5,4,3,0
                    """;
        var systemUnderTest = new Day17(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("4,6,3,5,6,3,5,2,1,0");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    Register A: 2024
                    Register B: 0
                    Register C: 0
                    
                    Program: 0,3,5,4,3,0
                    """;
        var systemUnderTest = new Day17(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("117440");
    }
}
