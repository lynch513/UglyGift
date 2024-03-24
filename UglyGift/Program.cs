using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace UglyGift {
    public static class PrettyStringParser {
        public static IEnumerable<int> CountEqualChunks(IEnumerable<char> seq)
        {
            var counter = 0;
            var ch = '0';
            foreach (var i in seq.Select((value, index) => (value, index)))
            {
                if (i.index != 0 && i.value != ch)
                {
                    yield return counter;
                    counter = 0;
                }
                counter++;
                ch = i.value;
            }
            yield return counter;
        }

        public static bool IsPrettyLine(IList<int> chunks) => chunks.Distinct().Count() == 1;

        public static int HowChangeNeeded(IList<int> chunks)
        {
            if (IsPrettyLine(chunks))
                return 0;
            var max = chunks.Max();
            return chunks.Count(i => i == max);
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

