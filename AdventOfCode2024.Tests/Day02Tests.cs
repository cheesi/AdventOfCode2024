﻿namespace AdventOfCode2024.Tests;

public class Day02Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """

                    """;
        var systemUnderTest = new Day02(input);

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
        var systemUnderTest = new Day02(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("");
    }
}
