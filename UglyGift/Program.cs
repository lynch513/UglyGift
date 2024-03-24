
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

        public static bool IsPrettyLine(IEnumerable<char> seq) => CountEqualChunks(seq).Distinct().Count() == 1;
    }

    public static class Program {
        public static void Main(string[] args)
        {
            Console.WriteLine(string.Join("; ", PrettyStringParser.IsPrettyLine("110011")));
        }
    }
}

