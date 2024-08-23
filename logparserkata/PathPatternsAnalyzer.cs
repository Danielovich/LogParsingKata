using System.Collections.Immutable;

namespace logparserkata;

public sealed class PathPatternsAnalyzer
{
    private readonly ImmutableList<LogEntry> logEntries;
    private readonly int partitionSize;

    private IOrderedEnumerable<PathPattern> orderedPathPatterns = Enumerable.Empty<PathPattern>().OrderBy(p => 0);
    private IEnumerable<UserPathPartition> userPathPartitions = new List<UserPathPartition>();

    public PathPatternsAnalyzer(IEnumerable<LogEntry> logEntries, int partitionSize)
    {
        this.partitionSize = partitionSize;
        this.logEntries = logEntries.ToImmutableList() ?? ImmutableList<LogEntry>.Empty;

        if (this.partitionSize == 0)
        {
            throw new ArgumentException($"{partitionSize} cannot be zero");
        }

        if (partitionSize > this.logEntries.Count())
        {
            throw new ArgumentException($"{partitionSize} is larger than the total " +
                $"entries in {logEntries} - {this.logEntries.Count()}");
        }

        PartitionUserPaths();
        OrderPathPatterns();
    }

    public PathPattern MostCommonPathPattern()
    {
        // if first and last entry in an ordered list has the same
        // occurence count, there can be no common path
        var first = orderedPathPatterns.First();
        var last = orderedPathPatterns.Last();

        if (first.OccurenceCount == last.OccurenceCount)
        {
            return new PathPattern(0, new List<UserPathPartition>());
        }

        return orderedPathPatterns.First();
    }

    public IOrderedEnumerable<PathPattern> AllPathPatterns()
    {
        return orderedPathPatterns;
    }

    public UserPathPartition FastestCommonPathPattern()
    {
        var mostCommon = MostCommonPathPattern();

        var fastest = mostCommon.PathPatterns.OrderBy(s => s.TotalLoadTime);

        return fastest.FirstOrDefault() ?? new UserPathPartition(Enumerable.Empty<LogEntry>());        
    }

    public UserPathPartition SlowestPathPattern()
    {
        var slowest = orderedPathPatterns
            .SelectMany(p => p.PathPatterns)
            .OrderByDescending(o => o.TotalLoadTime)
            .First();

        return slowest;
    }

    private void PartitionUserPaths()
    {
        var pathPartitions = new UserPathPartitions(this.logEntries);
        userPathPartitions = pathPartitions.PartitionedByUserId(this.partitionSize);
    }

    private void OrderPathPatterns()
    {
        var pathPatterns = new PathPatterns(this.userPathPartitions);
        orderedPathPatterns = pathPatterns.OrderByOccurenceDescending();
    }
}
