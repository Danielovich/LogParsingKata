namespace logparserkata;

public class PathPatternsTests
{
    [Fact]
    public void Calling_Order_By_Occurence_Descending_Twice_Results_As_A_Single_Call()
    {
        //Arrange
        var sut = new PathPatterns(Partitions_For_20_Users());

        //Act
        var actual = sut.OrderByOccurenceDescending();
        var expected = actual.OrderByDescending(p => p.OccurenceCount).ToList();

        actual = sut.OrderByOccurenceDescending();

        //Assert
        Assert.Equivalent(expected, actual, true);
    }

    [Fact]
    public void Path_Pattern_Occurence_Is_Ordered_By_Descending()
    {
        //Arrange
        var sut = new PathPatterns(Partitions_For_20_Users());

        //Act
        var actual = sut.OrderByOccurenceDescending();
        var expected = actual.OrderByDescending(p => p.OccurenceCount);

        //Assert
        Assert.True(expected.SequenceEqual(actual));
    }

    [Fact]
    public void Path_Patterns_Has_Known_Number_Of_Occurences()
    {
        //Arrange
        var sut = new PathPatterns(Partitions_For_20_Users());

        //Act
        var actual = sut.OrderByOccurenceDescending();

        //Assert
        var first = actual.First();
        Assert.True(first.OccurenceCount == 3);
        Assert.True(first.PathPatterns.First().FlattenedPaths == "/home/contact");

        var second = actual.Skip(1).First();
        Assert.True(second.OccurenceCount == 2);
        Assert.True(second.PathPatterns.First().FlattenedPaths == "/home/profile");

    }


    private IEnumerable<UserPathPartition> Partitions_For_20_Users()
    {
        var partitions = new List<UserPathPartition>
        {
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(1, "/home", 120),
                new LogEntry(1, "/profile", 200)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(2, "/search", 150),
                new LogEntry(2, "/results", 300)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(3, "/home", 100),
                new LogEntry(3, "/dashboard", 250),
                new LogEntry(3, "/settings", 400)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(4, "/login", 80),
                new LogEntry(4, "/home", 200)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(5, "/register", 140),
                new LogEntry(5, "/confirm", 180)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(6, "/home", 130),
                new LogEntry(6, "/settings", 220)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(7, "/home", 110),
                new LogEntry(7, "/profile", 210),
                new LogEntry(7, "/logout", 50)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(8, "/search", 160),
                new LogEntry(8, "/details", 270)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(9, "/home", 120),
                new LogEntry(9, "/contact", 190)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(10, "/login", 90),
                new LogEntry(10, "/dashboard", 240),
                new LogEntry(10, "/settings", 310)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(11, "/home", 130),
                new LogEntry(11, "/profile", 200)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(12, "/search", 150),
                new LogEntry(12, "/results", 250)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(13, "/home", 110),
                new LogEntry(13, "/profile", 210),
                new LogEntry(13, "/logout", 70)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(14, "/login", 80),
                new LogEntry(14, "/dashboard", 230)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(15, "/home", 120),
                new LogEntry(15, "/contact", 190)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(16, "/home", 100),
                new LogEntry(16, "/settings", 210)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(17, "/search", 160),
                new LogEntry(17, "/details", 270)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(18, "/home", 120),
                new LogEntry(18, "/profile", 190),
                new LogEntry(18, "/settings", 300)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(19, "/register", 140),
                new LogEntry(19, "/confirm", 180)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(20, "/home", 80),
                new LogEntry(20, "/contact", 200)
            })
        };

        return partitions;
    }
}
