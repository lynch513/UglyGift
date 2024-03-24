using System.Diagnostics;
using FluentAssertions;
using UglyGift;
namespace UglyGiftTests;

public class Tests
{
    private string bigPrettyString = default!;

    [OneTimeSetUp]
    public void Init()
    {
        var assemblyDirectory = TestContext.CurrentContext.TestDirectory;
        bigPrettyString = File.ReadAllText(Path.Combine(assemblyDirectory, "TestData", "tenthousand.txt"));
        Trace.Listeners.Add(new ConsoleTraceListener());
    }

    [OneTimeTearDown]
    public void EndTests()
    {
        Trace.Flush();
    }

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
        yield return new TestCaseData("000101", new [] {3, 1, 1, 1}, false, 1); // 1
        yield return new TestCaseData("00110100", new [] {2, 2, 1, 1, 2}, false, 3); // 3
        yield return new TestCaseData("010111", new [] {1, 1, 1, 3}, false, 1); // 1
        yield return new TestCaseData("110111", new [] {2, 1, 3}, false, 1); // 1
        yield return new TestCaseData("110110", new [] {2, 1, 2, 1}, false, 2); // 2
    }

    [Test]
    public void IsPrettyLine_Load_Test()
    {
        var stopWatch = Stopwatch.StartNew();
        var chunks = PrettyStringParser.CountEqualChunks(bigPrettyString).ToList();
        var isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
        stopWatch.Stop();
        TestContext.WriteLine($"Elapsed time: {stopWatch.ElapsedMilliseconds}");
        isPrettyLine.Should().BeTrue();
    }
}