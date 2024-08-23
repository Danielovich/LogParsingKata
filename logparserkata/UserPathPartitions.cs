using System.Collections.Immutable;

namespace logparserkata;

public sealed class UserPathPartitions
{
    private readonly IImmutableList<LogEntry> logEntries;

    public UserPathPartitions(IEnumerable<LogEntry> logEntries)
    {
        this.logEntries = logEntries.ToImmutableList() ?? ImmutableList<LogEntry>.Empty;
    }

    public ImmutableList<UserPathPartition> PartitionedByUserId(int partitionSize)
    {
        var groupedLogEntriesByUser = logEntries
            .GroupBy(x => x.UserId)
            .Select(s => s.AsEnumerable());

        List<UserPathPartition> userPathPartitions = new();

        foreach (var userLogEntries in groupedLogEntriesByUser)
        {
            logEntryPartitions.Clear();
            var sizedLogPartitions = CreateUserPathPartitions(partitionSize, userLogEntries);

            foreach (var logPartition in sizedLogPartitions)
            {
                userPathPartitions.Add(
                    new UserPathPartition(logPartition)
                );
            }
        }

        return userPathPartitions.ToImmutableList();
    }

    private List<IEnumerable<LogEntry>> logEntryPartitions = new List<IEnumerable<LogEntry>>();

    private List<IEnumerable<LogEntry>> CreateUserPathPartitions(
        int partitionSize, IEnumerable<LogEntry> logEntries, int skip = 0)
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

            return CreateUserPathPartitions(partitionSize, logEntries, ++skip);
        }

        return logEntryPartitions;
    }
}
