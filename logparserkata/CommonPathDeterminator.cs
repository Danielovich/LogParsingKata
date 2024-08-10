namespace logparserkata;

public class CommonPathDeterminator
{
    private readonly IEnumerable<LogEntry> logEntries = new List<LogEntry>();

    public CommonPathDeterminator(IEnumerable<LogEntry> logEntries)
    {
        this.logEntries = logEntries;
    }

    // todo: try using a IEnumerable as key
    private Dictionary<string, CommonPathOccurence> commonPathOccurences = new();

    public IEnumerable<CommonPathOccurence> ThreePagePattern(IEnumerable<CommonPath> commonPaths)
    {
        foreach (var commonPath in commonPaths)
        {
            var flatPaths = string.Join(string.Empty, commonPath.Paths);

            ExistingCommonPathOccurence(flatPaths, commonPath);
            NonExistingCommonPathOccurence(flatPaths, commonPath);
        }

        var orderedMostCommonThreePagePattern = commonPathOccurences
            .OrderBy(o => o.Key)
            .Select(o => o.Value)
            .ToList();

        return orderedMostCommonThreePagePattern;
    }

    private void NonExistingCommonPathOccurence(string compositeFlatPathKey, CommonPath commonPath)
    {
        if (!commonPathOccurences.ContainsKey(compositeFlatPathKey))
        {
            var occurence = new CommonPathOccurence(1,
                new List<CommonPath>() {
                    new CommonPath(commonPath.LoadTimeSum, 0, commonPath.Paths)
                }
            );

            commonPathOccurences.Add(compositeFlatPathKey, occurence);
        }
    }

    private void ExistingCommonPathOccurence(string compositeFlatPathKey, CommonPath commonPath)
    {
        if (commonPathOccurences.ContainsKey(compositeFlatPathKey))
        {
            var existing = commonPathOccurences[compositeFlatPathKey];
            var newCount = existing.OccurenceCount;

            var newCommonPaths = new List<CommonPath>(existing.CommonPaths)
            {
                new CommonPath(commonPath.LoadTimeSum, 0, commonPath.Paths)
            };

            var occurence = new CommonPathOccurence(++newCount, newCommonPaths);

            commonPathOccurences.Remove(compositeFlatPathKey);
            commonPathOccurences.Add(compositeFlatPathKey, occurence);
        }
    }

    public IEnumerable<CommonPath> CommonPathPartitionsByUserId()
    {
        var groupByUser = logEntries
            .GroupBy(x => x.UserId)
            .Select(s => s.ToList())
            .ToList();

        List<CommonPath> commonPaths = new();

        foreach (var entry in groupByUser)
        {
            logPartitions.Clear();
            var partitioned = CreateLogPartitions(3, entry);

            foreach (var partition in partitioned)
            {
                var paths = partition.Select(p => p.Path);
                var loadTimeSum = partition.Sum(s => s.LoadTime);
                var userId = partition.Select(u => u.UserId).FirstOrDefault();

                commonPaths.Add(
                    new CommonPath(loadTimeSum, userId, paths)
                );
            }
        }

        return commonPaths;
    }

    private List<IEnumerable<LogEntry>> logPartitions = new List<IEnumerable<LogEntry>>();

    private List<IEnumerable<LogEntry>> CreateLogPartitions(int partitionSize, IEnumerable<LogEntry> logEntries, int skip = 0)
    {
        // no reason to continue if our log entries holds too little data
        if (logEntries.Count() < partitionSize)
        {
            return logPartitions;
        }

        // skip and take because we are in recursion and must diminish the log entry size
        var skippedLogEntries = logEntries.Skip(skip);
        var logEntriesPartition = skippedLogEntries.Take(partitionSize);

        // partion entry must match wished size of partition
        if (logEntriesPartition.Count() == partitionSize)
        {
            logPartitions.Add(logEntriesPartition);

            return CreateLogPartitions(partitionSize, logEntries, ++skip);
        }

        return logPartitions;
    }
}