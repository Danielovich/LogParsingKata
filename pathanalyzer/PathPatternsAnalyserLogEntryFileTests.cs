namespace logparserkata;

public class PathPatternsAnalyserLogEntryFileTests : IClassFixture<LogEntriesFixture>
{
    private readonly LogEntriesFixture _fixture;

    public PathPatternsAnalyserLogEntryFileTests(LogEntriesFixture fixture)
    {
        this._fixture = fixture;
        this._fixture.InitializeAsync().GetAwaiter().GetResult();
    }

    [Fact]
    public async Task What_Is_The_Single_Most_Common_Page_Pattern()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(this._fixture.LogEntries, 3);

        //Act
        var actual = await Task.FromResult(sut.MostCommonPathPattern());

        //Arrange
        Assert.True(actual.PathPatterns.All(a => a.FlattenedPaths == "814.html368.html594.html"));
    }
}
