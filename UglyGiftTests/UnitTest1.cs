using System.Diagnostics;
using FluentAssertions;
using UglyGift;
namespace UglyGiftTests;

public class Tests
{
    [TestCaseSource(nameof(TestCasesForLineChunks))]
    public void IsPrettyLine_Should_Work(string input, IEnumerable<int> expectedArr, bool expectedStatus, int expectedChanges)
    {
        var chunks = PrettyStringParser.CountEqualChunks(input).ToList();
        chunks.Should().BeEquivalentTo(expectedArr);
        var isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
        isPrettyLine.Should().Be(expectedStatus);
        var howChangesNeeded = PrettyStringParser.HowChangeNeeded(chunks);
        howChangesNeeded.Should().Be(expectedChanges);
    }

    public static IEnumerable<TestCaseData> TestCasesForLineChunks()
    {
        // Pretty lines
        yield return new TestCaseData("010101", new [] {1,1,1,1,1,1}, true, 0);
        yield return new TestCaseData("000111", new [] {3, 3}, true, 0);
        yield return new TestCaseData("111000", new [] {3, 3}, true, 0);
        yield return new TestCaseData("1", new [] {1}, true, 0);
        yield return new TestCaseData("110011", new [] {2, 2, 2}, true, 0);
        yield return new TestCaseData("00000000", new [] {8}, true, 0);
        // Ugly lines
        yield return new TestCaseData("000101", new [] {3, 1, 1, 1}, false, 1);
        yield return new TestCaseData("00110100", new [] {2, 2, 1, 1, 2}, false, 3);
        yield return new TestCaseData("010111", new [] {1, 1, 1, 3}, false, 1);
        yield return new TestCaseData("110111", new [] {2, 1, 3}, false, 1);
        yield return new TestCaseData("110110", new [] {2, 1, 2, 1}, false, 2);
        yield return new TestCaseData("111000", new [] {3, 3}, true, 0);
    }
}