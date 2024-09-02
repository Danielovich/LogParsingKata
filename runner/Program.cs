using logparserkata;
using System.Collections.Immutable;

namespace Runner;

internal class Program
{
    static async Task Main(string[] args)
    {
        var logReader = new LogFileLoader();
        var logEntries = await logReader.LoadEntriesAsync();

        var pathAnalyzer = new PathPatternsAnalyzer(logEntries.ToImmutableList(), 3);

        var mostCommon = pathAnalyzer.MostCommonPathPattern();

        Console.WriteLine("What is the most common three-page pattern for all users?");
        foreach (var path in mostCommon.PathPatterns.First().Paths) {
            Console.WriteLine(path.Path);
        }

        Console.WriteLine();
        Console.WriteLine("What is the frequency (how many times) of the most common pattern for all users?");
        Console.WriteLine(mostCommon.OccurenceCount);

        Console.WriteLine();
        Console.WriteLine("How many different three-page patterns are there in total?");
        var differentPaths = pathAnalyzer.AllPathPatterns();
        Console.WriteLine(differentPaths.Count());
        Console.WriteLine();
        var fastestPath = pathAnalyzer.FastestCommonPathPattern();
        Console.WriteLine("(Bonus) What is the fastest most common three-page pattern for all users?");
        Console.WriteLine(string.Concat(fastestPath.FlattenedPaths, " - ", fastestPath.TotalLoadTime));
        Console.WriteLine();
        var slowestPath = pathAnalyzer.SlowestPathPattern();
        Console.WriteLine("(Bonus) What is the slowest three-page pattern for all users?");
        Console.WriteLine(string.Concat(slowestPath.FlattenedPaths, " - ", slowestPath.TotalLoadTime));

        Console.ReadKey();
    }
}
