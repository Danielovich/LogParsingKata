using System.Collections.Immutable;

namespace logparserkata;

public sealed class UserPathPartition
{
    public int UserId { get; }
    public int TotalLoadTime { get => this.Paths.Sum(x => x.LoadTime); }
    public IImmutableList<LogEntry> Paths { get; }
    public string FlattenedPaths { get => Flatten(); }

    public UserPathPartition(IEnumerable<LogEntry> paths)
    {
        this.Paths = paths?.ToImmutableList() ?? ImmutableList<LogEntry>.Empty;
        this.UserId = this.Paths.Select(u => u.UserId).FirstOrDefault();
    }

    public string Flatten()
    {
        string flattenedPaths = string.Empty;
        foreach (var logEntry in Paths)
        {
            flattenedPaths = flattenedPaths + logEntry.Path;
        }

        return flattenedPaths;
    }
}