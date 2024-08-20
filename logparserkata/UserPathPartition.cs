namespace logparserkata;

public class UserPathPartition
{
    public int UserId { get; }
    public int TotalLoadTime { get => this.Paths.Sum(x => x.LoadTime); }
    public IEnumerable<LogEntry> Paths { get; }
    public string FlattenedPaths { get => Flatten(); }

    public UserPathPartition(IEnumerable<LogEntry> paths)
    {
        this.Paths = paths ?? new List<LogEntry>();
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