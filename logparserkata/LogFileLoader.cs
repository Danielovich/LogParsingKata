namespace logparserkata;

public class LogFileLoader
{
    private List<LogEntry> LogEntries { get; } = new List<LogEntry>();

    private string logFilePath = Path.Combine(Environment.CurrentDirectory, "logfile.csv");

    public async Task<IEnumerable<LogEntry>> LoadEntriesAsync(string logFilePath)
    {
        this.logFilePath = Path.Combine(Environment.CurrentDirectory, logFilePath);

        return await LoadEntriesAsync();
    }

    public async Task<IEnumerable<LogEntry>> LoadEntriesAsync()
    {
        using StreamReader sr = new StreamReader(logFilePath);

        string? line;

        bool firstLine = true;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            // skip comlumn identifiers
            if (firstLine)
            {
                firstLine = false;
                continue;
            }
                
            // string represented as : user, path, loadtime
            var split = line.Split(',');

            // if any identifier (user, path, loadtime) is missing we will not use the remainding
            if (split.Length != 3)
                continue;

            LogEntries.Add(
                new LogEntry(
                    ParseUserId(split[0]),
                    ParsePath(split[1]),
                    ParseLoadTime(split[2])
                )
            );
        }

        return LogEntries;
    }

    private string ParsePath(string candidate)
    {
        return candidate;
    }

    private int ParseUserId(string candidate)
    {
        return int.Parse(candidate);
    }

    private int ParseLoadTime(string candidate)
    {
        return int.Parse(candidate);
    }
}
