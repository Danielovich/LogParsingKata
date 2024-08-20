namespace logparserkata;

public class PathPatternsAnalyzer
{
    private readonly IEnumerable<LogEntry> logEntries;
    private readonly int partitionSize;

    private IOrderedEnumerable<PathPattern> orderedPathPatterns = Enumerable.Empty<PathPattern>().OrderBy(p => 0);
    private IEnumerable<UserPathPartition> userPathPartitions = new List<UserPathPartition>();


    public PathPatternsAnalyzer(IEnumerable<LogEntry> logEntries, int partitionSize)
    {
        if (partitionSize == 0)
        {
            throw new ArgumentException($"{partitionSize} cannot be zero");
        }

        if(partitionSize > logEntries.Count())
        {
            throw new ArgumentException($"{partitionSize} is larger than the total " +
                $"entries in {logEntries} - {logEntries.Count()}");
        }

        this.logEntries = logEntries ?? new List<LogEntry>();
        this.partitionSize = partitionSize;

        PartitionUserPaths();
        OrderPathPatterns();
    }

    public PathPattern MostCommonPathPattern()
    {
        // not common paths, extremly slow!
        if (orderedPathPatterns.All(p => p.OccurenceCount == orderedPathPatterns.First().OccurenceCount))
        {
            return new PathPattern(0, new List<UserPathPartition>());
        }

        return orderedPathPatterns.First();
    }

    public IOrderedEnumerable<PathPattern> AllPathPatterns()
    {
        return orderedPathPatterns;
    }

    public UserPathPartition? FastestCommonPathPattern()
    {
        var mostCommon = MostCommonPathPattern();

        var fastest = mostCommon.PathPatterns.OrderBy(s => s.TotalLoadTime);

        return fastest.FirstOrDefault();
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
