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
        this.UserId = this.Paths.FirstOrDefault().UserId;
    }

    private string Flatten()
    {        
        string concat = string.Empty;
        for (var i = 0; i < this.Paths.Count; i++)
        {
            concat += string.Concat(this.Paths[i].Path);
        }

        return concat;
    }
}