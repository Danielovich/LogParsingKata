namespace logparserkata;

public class CommonPathDeterminatorTests
{
    [Fact]
    public void Find_Most_Common_Three_Page_Path_Pattern()
    {
        //Arrange
        var sut = new CommonPathDeterminator(FakeEntries_ThreeUsers());

        //Act
        var pattern = sut.ThreePagePattern(sut.CommonPathPartitionsByUserId());

        var commonPaths = pattern.Where(w => w.OccurenceCount == 3).SelectMany(s => s.CommonPaths);

        //Assert
        Assert.True(commonPaths.All(a => a.Paths.SequenceEqual(commonPaths.First().Paths)));
    }

    [Fact]
    public void Common_Path_Partitions_For_User_Is_Partitioned_Correct()
    {
        //Arrange
        var sut = new CommonPathDeterminator(FakeEntries_ThreeUsers());

        //Act
        var partitions = sut.CommonPathPartitionsByUserId();

        var user1PartitionSize = partitions.Where(p => p.UserId == 1).Select(x => x.Paths).Count();
        var user2PartitionSize = partitions.Where(p => p.UserId == 2).Select(x => x.Paths).Count();
        var user3PartitionSize = partitions.Where(p => p.UserId == 3).Select(x => x.Paths).Count();

        //Assert
        Assert.True(user1PartitionSize == 4);
        Assert.True(user2PartitionSize == 4);
        Assert.True(user3PartitionSize == 4);
    }


    [Fact]
    public void Given_No_Data_Return_Empty()
    {
        //Arrange
        var sut = new CommonPathDeterminator(FakeEntries_Empty());

        //Act
        var pattern = sut.ThreePagePattern(sut.CommonPathPartitionsByUserId());

        //Assert
        Assert.Empty(pattern);
    }

    [Fact]
    public void Given_One_Users_Data_Return_Occurences_Of_1()
    {
        //Arrange
        var sut = new CommonPathDeterminator(FakeEntries_OneUser());

        //Act
        var pattern = sut.ThreePagePattern(sut.CommonPathPartitionsByUserId());

        //Assert
        Assert.True(pattern.Count() == 10); //10 pairs of threes in a list of 13 
        Assert.True(pattern.All(a => a.OccurenceCount == 1));
    }

    [Fact]
    public void Find_Most_Common_Three_Page_Path_Pattern_Two_Users_Data()
    {
        //Arrange
        var sut = new CommonPathDeterminator(FakeEntries_TwoUsers());

        //Act
        var pattern = sut.ThreePagePattern(sut.CommonPathPartitionsByUserId());

        //Assert
        var commonPaths = pattern.Where(w => w.OccurenceCount == 2).SelectMany(s => s.CommonPaths);

        Assert.True(commonPaths.All(a => a.Paths.SequenceEqual(commonPaths.First().Paths)));
    }

    private IEnumerable<LogEntry> FakeEntries_Empty()
    {
        var entries = new List<LogEntry>();

        return entries;
    }

    private IEnumerable<LogEntry> FakeEntries_ThreeUsers()
    {
        var entries = new List<LogEntry>
        {
            new LogEntry(1, "1.html", 20),
            new LogEntry(1, "2.html", 10),
            new LogEntry(1, "3.html", 30),
            new LogEntry(1, "7.html", 20),
            new LogEntry(1, "5.html", 10),
            new LogEntry(1, "8.html", 30),
            new LogEntry(2, "1.html", 20),
            new LogEntry(2, "2.html", 10),
            new LogEntry(2, "3.html", 40),
            new LogEntry(2, "9.html", 20),
            new LogEntry(2, "12.html", 10),
            new LogEntry(2, "36.html", 30),
            new LogEntry(3, "6.html", 20),
            new LogEntry(3, "3.html", 10),
            new LogEntry(3, "4.html", 30),
            new LogEntry(3, "1.html", 20),
            new LogEntry(3, "2.html", 22),
            new LogEntry(3, "3.html", 30)
        };

        return entries;
    }

    private IEnumerable<LogEntry> FakeEntries_OneUser()
    {
        var entries = new List<LogEntry>
        {
            new LogEntry(1, "1.html", 20),
            new LogEntry(1, "2.html", 10),
            new LogEntry(1, "3.html", 30),
            new LogEntry(1, "7.html", 20),
            new LogEntry(1, "5.html", 10),
            new LogEntry(1, "8.html", 30),
            new LogEntry(1, "34.html", 20),
            new LogEntry(1, "11.html", 10),
            new LogEntry(1, "33.html", 30),
            new LogEntry(1, "71.html", 20),
            new LogEntry(1, "54.html", 10),
            new LogEntry(1, "81.html", 30)
        };

        return entries;
    }

    private IEnumerable<LogEntry> FakeEntries_TwoUsers()
    {
        var entries = new List<LogEntry>
        {
            new LogEntry(1, "1.html", 20),
            new LogEntry(1, "2.html", 10),
            new LogEntry(1, "3.html", 30),
            new LogEntry(1, "7.html", 20),
            new LogEntry(1, "5.html", 10),
            new LogEntry(1, "8.html", 30),
            new LogEntry(1, "34.html", 20),
            new LogEntry(1, "11.html", 10),
            new LogEntry(2, "7.html", 30),
            new LogEntry(2, "5.html", 20),
            new LogEntry(2, "8.html", 10),
            new LogEntry(2, "81.html", 30)
        };

        return entries;
    }
}