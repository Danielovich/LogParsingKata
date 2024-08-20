namespace logparserkata;

public class PathPatternsAnalyserLogEntryFileTests : IClassFixture<LogEntriesFixture>
{
    private readonly LogEntriesFixture _fixture;

    public PathPatternsAnalyserLogEntryFileTests(LogEntriesFixture fixture)
    {
        _fixture = fixture;
        _fixture.InitializeAsync().GetAwaiter().GetResult();
    }

    [Fact]
    public async Task What_Is_The_Single_Most_Common_Page_Pattern()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(_fixture.LogEntries, 3);

        //Act
        var actual = await Task.FromResult(sut.MostCommonPathPattern());

        //Arrange
        Assert.True(actual.PathPatterns.All(a => a.FlattenedPaths == "814.html368.html594.html"));
    }
}

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
