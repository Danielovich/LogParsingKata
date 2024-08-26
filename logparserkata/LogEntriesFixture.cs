namespace logparserkata;

public class LogEntriesFixture
{
    public List<LogEntry> LogEntries { get; set; } = new List<LogEntry>();

    public async Task InitializeAsync()
    {
        var logLoader = new LogFileLoader();
        LogEntries = (await logLoader.LoadEntriesAsync()).ToList();
    }

    public LogEntriesFixture()
    {
    }
}
