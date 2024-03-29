using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace UglyGift {
    public static class PrettyStringParser {
        public static IEnumerable<(int Count, char Char)> CountEqualChunks(IEnumerable<char> seq)
        {
            var counter = 0;
            var ch = '0';
            foreach (var i in seq.Select((value, index) => (value, index)))
            {
                if (i.index != 0 && i.value != ch)
                {
                    yield return (counter, ch);
                    counter = 0;
                }
                counter++;
                ch = i.value;
            }
            yield return (counter, ch);
        }

        public static bool IsPrettyLine(IList<(int Count, char Char)> chunks) => chunks.Select(i => i.Count).Distinct().Count() == 1;

        public static int HowChangeNeeded(IList<(int Count, char Char)> chunks)
        {
            if (IsPrettyLine(chunks))
                return 0;
            var maxChunk = chunks.MaxBy(i => i.Count);
            var countMax = chunks.Count(i => i == maxChunk);
            var chunksWithoutMax = chunks.Where(i => i != maxChunk).ToList();
            var countRemainingZeros = chunksWithoutMax.Where(i => i.Char == '0').Sum(i => i.Count);
            var countRemainingOnes = chunksWithoutMax.Where(i => i.Char == '1').Sum(i => i.Count);
            return countMax % 2 == 0 ? Math.Max(countRemainingOnes, countRemainingZeros) : Math.Min(countRemainingOnes, countRemainingZeros);
        }
    }

    public static class Program {
        public static void Main(string[] args)
        {
            var assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var stopWatch = new Stopwatch();

            var tenThousandsPrettyString = File.ReadAllText(Path.Combine(assemblyDirectory, "TestData", "tenthousandPrettyLine.txt"));
            stopWatch.Start();
            var chunks = PrettyStringParser.CountEqualChunks(tenThousandsPrettyString).ToList();
            var isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
            var howManyChanges = PrettyStringParser.HowChangeNeeded(chunks);
            stopWatch.Stop();
            Console.WriteLine($"Is pretty line: {isPrettyLine}");
            Console.WriteLine($"Changes needed: {howManyChanges}");
            Console.WriteLine($"Elapsed milliseconds: {stopWatch.ElapsedMilliseconds}ms");

            var tenThousandsUglyString = File.ReadAllText(Path.Combine(assemblyDirectory, "TestData", "tenthousandUglyLine.txt"));

            stopWatch.Restart();
            chunks = PrettyStringParser.CountEqualChunks(tenThousandsUglyString).ToList();
            isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
            howManyChanges = PrettyStringParser.HowChangeNeeded(chunks);
            stopWatch.Stop();
            Console.WriteLine($"Is pretty line: {isPrettyLine}");
            Console.WriteLine($"Changes needed: {howManyChanges}");
            Console.WriteLine($"Elapsed milliseconds: {stopWatch.ElapsedMilliseconds}ms");

            var bigPrettyString = File.ReadAllText(Path.Combine(assemblyDirectory, "TestData", "bigPrettyLine.txt"));

            stopWatch.Restart();
            chunks = PrettyStringParser.CountEqualChunks(bigPrettyString).ToList();
            isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
            howManyChanges = PrettyStringParser.HowChangeNeeded(chunks);
            stopWatch.Stop();
            Console.WriteLine($"Is pretty line: {isPrettyLine}");
            Console.WriteLine($"Changes needed: {howManyChanges}");
            Console.WriteLine($"Elapsed milliseconds: {stopWatch.ElapsedMilliseconds}ms");

            var bigUglyString = File.ReadAllText(Path.Combine(assemblyDirectory, "TestData", "bigUglyLine.txt"));

            stopWatch.Restart();
            chunks = PrettyStringParser.CountEqualChunks(bigUglyString).ToList();
            isPrettyLine = PrettyStringParser.IsPrettyLine(chunks);
            howManyChanges = PrettyStringParser.HowChangeNeeded(chunks);
            stopWatch.Stop();
            Console.WriteLine($"Is pretty line: {isPrettyLine}");
            Console.WriteLine($"Changes needed: {howManyChanges}");
            Console.WriteLine($"Elapsed milliseconds: {stopWatch.ElapsedMilliseconds}ms");
        }
    }
}

