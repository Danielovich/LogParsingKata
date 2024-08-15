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

        var s = string.Join(string.Empty, this.Paths);
    }

    public string Flatten()
    {
        string s = "";
        foreach (var item in Paths)
        {
            s = s + item.Path;
        }

        return s;
    }
}