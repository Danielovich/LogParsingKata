namespace logparserkata;

public class PathPartition
{
    private readonly IEnumerable<LogEntry> logEntries;

    public PathPartition(IEnumerable<LogEntry> logEntries)
    {
        this.logEntries = logEntries ?? new List<LogEntry>();
    }

    //public IEnumerable<CommonPath> CommonPathPartitionsByUserId()
    public IEnumerable<UserPathPartition> PathPartitionsByUserId(int partitionSize)
    {
        var groupedLogEntriesByUser = logEntries
            .GroupBy(x => x.UserId)
            .Select(s => s.ToList());

        List<UserPathPartition> userPathPartitions = new();

        foreach (var userLogEntries in groupedLogEntriesByUser)
        {
            logEntryPartitions.Clear();
            var sizedLogPartitions = CreatePathPartitions(partitionSize, userLogEntries);

            foreach (var logPartition in sizedLogPartitions)
            {
                userPathPartitions.Add(
                    new UserPathPartition(logPartition)
                );
            }
        }

        return userPathPartitions;
    }


    private List<IEnumerable<LogEntry>> logEntryPartitions = new List<IEnumerable<LogEntry>>();

    private List<IEnumerable<LogEntry>> CreatePathPartitions(int partitionSize, IEnumerable<LogEntry> logEntries, int skip = 0)
    {
        // no reason to continue if our log entries holds too little data
        if (logEntries.Count() < partitionSize)
        {
            return logEntryPartitions;
        }

        // skip and take because we are in recursion and must diminish the log entry size
        var skippedLogEntries = logEntries.Skip(skip);
        var logEntriesPartition = skippedLogEntries.Take(partitionSize);

        // partion entry must match wished size of partition
        if (logEntriesPartition.Count() == partitionSize)
        {
            logEntryPartitions.Add(logEntriesPartition);

            return CreatePathPartitions(partitionSize, logEntries, ++skip);
        }

        return logEntryPartitions;
    }
}
