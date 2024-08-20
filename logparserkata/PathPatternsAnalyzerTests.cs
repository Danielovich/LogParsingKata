namespace logparserkata;

public class PathPatternsAnalyzerTests
{

    [Fact]
    public void What_Is_The_Single_Most_Common_Page_Pattern()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 3);

        //Act
        var actual = sut.MostCommonPathPattern();

        //Arrange
        Assert.True(actual.PathPatterns.All(a => a.FlattenedPaths == "/home/profile/results"));
    }

    [Fact]
    public void What_Is_The_Frequency_Of_The_Most_Common_Pattern_For_All_Users()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 3);

        //Act
        var actual = sut.MostCommonPathPattern();

        //Arrange
        Assert.True(actual.OccurenceCount == 2);
    }

    [Fact]
    public void How_Many_Different_Patterns_Exists()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 3);

        //Act
        var actual = sut.AllPathPatterns();

        //Arrange
        Assert.True(actual.Count() == 8);
    }

    [Fact]
    public void Fastest_Most_Common_Path_Pattern()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 3);

        //Act
        var actual = sut.FastestCommonPathPattern();

        //Arrange
        Assert.True(actual!.TotalLoadTime == 620);
    }

    [Fact]
    public void Slowest_Of_All_Path_Patterns()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 3);

        //Act
        var actual = sut.SlowestPathPattern();

        //Arrange
        Assert.True(actual.TotalLoadTime == 900);
    }

    [Fact]
    public void Constructor_Throws_Due_To_Partition_Size_Is_Larger_Than_Collection_Count()
    {
        //Arrange Act Assert
        Assert.Throws<ArgumentException>(() => new PathPatternsAnalyzer(Empty_Log_Entries(), 3));
    }

    [Fact]
    public void Constructor_Throws_Due_To_Partition_Size_Is_0()
    {
        //Arrange Act Assert
        Assert.Throws<ArgumentException>(() => new PathPatternsAnalyzer(Log_Entries_For_4_Users(), 0));
    }

    [Fact]
    public void What_Is_The_Single_Most_Common_Page_Pattern_No_Common_Paths_Exists()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_No_Common_Paths(), 3);

        //Act
        var actual = sut.MostCommonPathPattern();

        //Arrange
        Assert.Equal(0, actual.OccurenceCount);
        Assert.Empty(actual.PathPatterns);
    }

    [Fact]
    public void Fastest_Most_Common_Path_Pattern_No_Common_Paths_Exists()
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(Log_Entries_No_Common_Paths(), 3);

        //Act
        var actual = sut.FastestCommonPathPattern();

        //Arrange
        Assert.Null(actual);
    }

    private IEnumerable<LogEntry> Log_Entries_No_Common_Paths()
    {
        var partitions = new List<LogEntry>
        {
            new LogEntry(1, "/login", 300),
            new LogEntry(1, "/home", 120),
            new LogEntry(1, "/profile", 200),
            new LogEntry(1, "/results", 300),
            new LogEntry(2, "/people", 300),
            new LogEntry(2, "/hello", 120),
            new LogEntry(2, "/contact", 200),
            new LogEntry(2, "/jobs", 300),
        };

        return partitions;
    }

    private IEnumerable<LogEntry> Log_Entries_For_4_Users()
    {
        var partitions = new List<LogEntry>
        {
            new LogEntry(1, "/login", 300),
            new LogEntry(1, "/home", 120),
            new LogEntry(1, "/profile", 200),
            new LogEntry(1, "/results", 300),
            new LogEntry(2, "/home", 300),
            new LogEntry(2, "/profile", 300),
            new LogEntry(2, "/results", 300),
            new LogEntry(3, "/home", 100),
            new LogEntry(3, "/login", 250),
            new LogEntry(3, "/dashboard", 250),
            new LogEntry(3, "/settings", 400),
            new LogEntry(4, "/login", 80),
            new LogEntry(4, "/home", 200),
            new LogEntry(4, "/dashboard", 200),
            new LogEntry(4, "/setting", 200),
            new LogEntry(4, "/search", 200),
            new LogEntry(4, "/people", 200)
        };

        return partitions;
    }

    private IEnumerable<LogEntry> Empty_Log_Entries()
    {
        var partitions = new List<LogEntry>();
        return partitions;
    }
}
