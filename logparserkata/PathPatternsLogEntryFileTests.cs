using System.Diagnostics;

namespace logparserkata;

public class PathPatternsLogEntryFileTests : IClassFixture<LogEntriesFixture>
{
    private readonly LogEntriesFixture _fixture;
    private const long maxElapsedExecutionMilliseconds = 2000;

    public PathPatternsLogEntryFileTests(LogEntriesFixture fixture)
    {
        this._fixture = fixture;
        _fixture.InitializeAsync().GetAwaiter().GetResult();
    }

    [Fact]
    public void OrderByOccurenceDescending_Performance()
    {
        //Arrange
        var partitions = new UserPathPartitions(_fixture.LogEntries);
        
        var sut = new PathPatterns(partitions.PartitionedByUserId(3));

        //Act
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var actual = sut.OrderByOccurenceDescending();
        stopwatch.Stop();

        //Act
        Assert.True(stopwatch.ElapsedMilliseconds < maxElapsedExecutionMilliseconds);
        Console.WriteLine(stopwatch.Elapsed.ToString());
    }
}
