using System.Collections.Immutable;

namespace logparserkata;

public class UserPathPartitionsTests
{
    [Fact]
    public void Common_Path_Partitions_For_User_Is_Partitioned_Correct()
    {
        //Arrange
        var sut = new UserPathPartitions(Log_Entries_For_Three_Users());

        //Act
        var pathPartitions = sut.PartitionedByUserId(3);

        //Assert
        foreach (var pathPartitionForUser in pathPartitions
            .GroupBy(g => g.UserId).Select(g => g.AsEnumerable()))
        {
            Assert.Equal(4, pathPartitionForUser.Count());
        }
    }

    [Fact]
    public void Common_Path_Partitions_Has_Total_LoadTime_Calculated_Correct()
    {
        //Arrange
        var sut = new UserPathPartitions(Log_Entries_For_Three_Users());

        //Act
        var pathPartitions = sut.PartitionedByUserId(3);

        //Assert
        foreach (var pathPartitionForUser in pathPartitions)
        {
            var sum = pathPartitionForUser.Paths.Sum(s => s.LoadTime);

            Assert.Equal(pathPartitionForUser.TotalLoadTime, sum);
        }
    }

    [Fact]
    public void Common_Path_Partitions_UserId_Is_Not_0()
    {
        //Arrange
        var sut = new UserPathPartitions(Log_Entries_For_Three_Users());

        //Act
        var pathPartitions = sut.PartitionedByUserId(3);

        //Assert
        foreach (var pathPartitionForUser in pathPartitions)
        {
            Assert.True(pathPartitionForUser.UserId > 0);
        }
    }

    private IImmutableList<LogEntry> Log_Entries_For_Three_Users()
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

        return entries.ToImmutableList();
    }
}
