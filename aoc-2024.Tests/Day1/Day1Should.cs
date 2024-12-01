
using FluentAssertions;
using Day1Solution = aoc_2024.Day1.Day1;

namespace aoc_2024.Day1.Tests;

[TestFixture]
public class Day1Should
{
    // Test for CountOccurrences method
    [Test]
    public void CountOccurrences_ValidList_ReturnsCorrectCount()
    {
        // Arrange
        List<int> list2 = new List<int> { 3, 3, 3, 4, 5, 9 };

        // Act
        Dictionary<int, int> result = Day1Solution.CountOccurrences(list2);

        // Assert
        result[3].Should().Be(3);
        result[4].Should().Be(1);
        result[5].Should().Be(1);
        result[9].Should().Be(1);
    }

    // Test for CalculateSimilarityScore method
    [Test]
    public void CalculateSimilarityScore_ValidLists_ReturnsCorrectScore()
    {
        // Arrange
        List<int> list1 = new List<int> { 1, 2, 3, 3, 3, 4 };
        Dictionary<int, int> occurrences = new Dictionary<int, int>
        {
            { 3, 3 },
            { 4, 1 },
            { 5, 1 },
            { 9, 1 }
        };

        // Act
        int result = Day1Solution.CalculateSimilarityScore(list1, occurrences);

        // Assert
        result.Should().Be(31);
    }

    // Test for ReadAndProcessData method
    [Test]
    public void ReadAndProcessData_ValidFile_ReturnsCorrectLists()
    {
        // Arrange
        string filePath = ".\\Day1\\Input.txt";

        // Act
        (List<int> list1, List<int> list2) = Day1Solution.ReadAndProcessData(filePath);

        // Assert
        list1.Should().BeEquivalentTo(new List<int> { 3, 4, 2, 1, 3, 3 }); // Expected values based on the test file
        list2.Should().BeEquivalentTo(new List<int> { 4, 3, 5, 3, 9, 3 }); // Expected values based on the test file
    }

    // Test for SortLists method
    [Test]
    public void SortLists_ValidLists_ReturnsSortedLists()
    {
        // Arrange
        List<int> list1 = new List<int> { 3, 4, 2, 1, 3, 3 };
        List<int> list2 = new List<int> { 4, 3, 5, 3, 9, 3 };

        // Act
        (List<int> sortedList1, List<int> sortedList2) = Day1Solution.SortLists(list1, list2);

        // Assert
        sortedList1.Should().BeEquivalentTo(new List<int> { 1, 2, 3, 3, 3, 4 });
        sortedList2.Should().BeEquivalentTo(new List<int> { 3, 3, 3, 4, 5, 9 });
    }
}

