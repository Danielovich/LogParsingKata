using System.Collections.Immutable;

namespace logparserkata;

public class LogEntriesFixture
{
    public IImmutableList<LogEntry> LogEntries { get; set; } = ImmutableList.Create<LogEntry>();

    public async Task InitializeAsync()
    {
        var logLoader = new LogFileLoader();
        this.LogEntries = (await logLoader.LoadEntriesAsync()).ToImmutableList();
    }

    public LogEntriesFixture()
    {
    }
}
