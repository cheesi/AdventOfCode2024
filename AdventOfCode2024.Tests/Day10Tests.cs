﻿namespace AdventOfCode2024.Tests;

public class Da10Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """

                    """;
        var systemUnderTest = new Day10(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """

                    """;
        var systemUnderTest = new Day10(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("");
    }
}
