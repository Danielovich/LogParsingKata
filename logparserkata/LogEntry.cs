namespace logparserkata;


public struct LogEntry
{
    public LogEntry(int userId, string path, int loadTime)
    {
        this.UserId = userId;
        this.LoadTime = loadTime;
        this.Path = path;
    }

    public int UserId { get; }
    public int LoadTime { get; }
    public string Path { get; }
}