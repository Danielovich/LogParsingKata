using AutoFixture;
using AutoFixture.Xunit2;

namespace logparserkata;

public class PathPatternsAutoDataAttribute : AutoDataAttribute
{
    public PathPatternsAutoDataAttribute() : base(() =>
    {
        var fixture = new Fixture();

        var pathPatterns = new PathPatterns(Partitions_For_4_Users());
        var testData = pathPatterns.OrderByOccurenceDescending();

        fixture.Inject(testData);

        return fixture;
    })
    { }

    private static IEnumerable<UserPathPartition> Partitions_For_4_Users()
    {
        var partitions = new List<UserPathPartition>
        {
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(1, "/home", 120),
                new LogEntry(1, "/profile", 200),
                new LogEntry(1, "/results", 300),

            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(2, "/home", 300),
                new LogEntry(2, "/profile", 300),
                new LogEntry(2, "/results", 300),
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(3, "/home", 100),
                new LogEntry(3, "/login", 250),
                new LogEntry(3, "/dashboard", 250),
                new LogEntry(3, "/settings", 400)
            }),
            new UserPathPartition(new List<LogEntry>
            {
                new LogEntry(4, "/login", 80),
                new LogEntry(4, "/home", 200),
                new LogEntry(4, "/dashboard", 200),
                new LogEntry(4, "/setting", 200),
                new LogEntry(4, "/search", 200),
                new LogEntry(4, "/people", 200),
            })
        };

        return partitions;
    }
}
