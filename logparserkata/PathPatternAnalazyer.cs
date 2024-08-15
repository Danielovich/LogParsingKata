namespace logparserkata;

public class PathPatternAnalazyer
{
    private readonly IEnumerable<UserPathPartition> userPathPartitions;

    public PathPatternAnalazyer(IEnumerable<UserPathPartition> userPathPartitions)
    {
        this.userPathPartitions = userPathPartitions ?? new List<UserPathPartition>();
    }

    private Dictionary<string, PathPattern> pathPatternOccurences = new();

    public IEnumerable<PathPattern> OccurenceOrderByDescending()
    {
        foreach (var pathPartition in userPathPartitions)
        {
            var flattendPaths = pathPartition.FlattenedPaths;

            ExistingPathPattern(flattendPaths, pathPartition);
            NonExistingPathPattern(flattendPaths, pathPartition);
        }

        var orderedMostCommonThreePagePattern = pathPatternOccurences
            .OrderByDescending(o => o.Value.OccurenceCount)
            .Select(o => o.Value);

        return orderedMostCommonThreePagePattern;
    }

    private void NonExistingPathPattern(string flattenedPaths, UserPathPartition userPathPartition)
    {
        if (!pathPatternOccurences.ContainsKey(flattenedPaths))
        {
            var occurence = new PathPattern(1,
                new List<UserPathPartition>() {
                    new UserPathPartition(userPathPartition.Paths)
                }
            );

            pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }

    private void ExistingPathPattern(string flattenedPaths, UserPathPartition userPathPartition)
    {
        if (pathPatternOccurences.ContainsKey(flattenedPaths))
        {
            var patternOccurence = pathPatternOccurences[flattenedPaths];
            var count = patternOccurence.OccurenceCount;

            var updatedUserPathPartitions = new List<UserPathPartition>(patternOccurence.PathPatterns)
            {
                new UserPathPartition(userPathPartition.Paths)
            };

            var occurence = new PathPattern(++count, updatedUserPathPartitions);

            pathPatternOccurences.Remove(flattenedPaths);
            pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }
}