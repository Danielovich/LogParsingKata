using System.Collections.Immutable;

namespace logparserkata;

public sealed class PathPatternsAnalyzer
{
    public IImmutableList<LogEntry> LogEntries { get; }

    private readonly int partitionSize;

    private IOrderedEnumerable<PathPattern> orderedPathPatterns = Enumerable.Empty<PathPattern>().OrderBy(p => 0);
    private IImmutableList<UserPathPartition> userPathPartitions = ImmutableList.Create<UserPathPartition>();

    public PathPatternsAnalyzer(IImmutableList<LogEntry> logEntries, int partitionSize)
    {
        this.partitionSize = partitionSize;
        this.LogEntries = logEntries ?? ImmutableList<LogEntry>.Empty;

        if (this.partitionSize == 0)
        {
            throw new ArgumentException($"{this.partitionSize} cannot be zero");
        }

        if (partitionSize > this.LogEntries.Count())
        {
            throw new ArgumentException($"{partitionSize} is larger than the total " +
                $"entries in {nameof(this.LogEntries)} - {this.LogEntries.Count()}");
        }

        PartitionUserPaths();
        OrderPathPatterns();
    }

    public PathPattern MostCommonPathPattern()
    {
        // if first and last entry in an ordered list has the same
        // occurence count, there can be no common path
        var first = this.orderedPathPatterns.First();
        var last = this.orderedPathPatterns.Last();

        if (first.OccurenceCount == last.OccurenceCount)
        {
            return new PathPattern(0, ImmutableList.Create<UserPathPartition>());
        }

        return this.orderedPathPatterns.First();
    }

    public IOrderedEnumerable<PathPattern> AllPathPatterns()
    {
        return orderedPathPatterns;
    }

    public UserPathPartition FastestCommonPathPattern()
    {
        var mostCommon = MostCommonPathPattern();

        var fastest = mostCommon.PathPatterns.OrderBy(s => s.TotalLoadTime);

        return fastest.FirstOrDefault() ?? new UserPathPartition(ImmutableList.Create<LogEntry>());        
    }

    public UserPathPartition SlowestPathPattern()
    {
        var slowest = this.orderedPathPatterns
            .SelectMany(p => p.PathPatterns)
            .OrderByDescending(o => o.TotalLoadTime)
            .First();

        return slowest;
    }

    private void PartitionUserPaths()
    {
        var pathPartitions = new UserPathPartitions(this.LogEntries);
        this.userPathPartitions = pathPartitions.PartitionedByUserId(this.partitionSize);
    }

    private void OrderPathPatterns()
    {
        var pathPatterns = new PathPatterns(this.userPathPartitions);
        this.orderedPathPatterns = pathPatterns.OrderByOccurenceDescending();
    }
}
