namespace logparserkata;

public class PathPatterns
{
    private readonly IEnumerable<UserPathPartition> userPathPartitions;
    private readonly Dictionary<string, PathPattern> pathPatternOccurences;

    public PathPatterns(IEnumerable<UserPathPartition> userPathPartitions)
    {
        this.userPathPartitions = userPathPartitions ?? new List<UserPathPartition>();
        this.pathPatternOccurences = new Dictionary<string, PathPattern>();
    }

    public IOrderedEnumerable<PathPattern> OrderByOccurenceDescending()
    {
        pathPatternOccurences.Clear();

        foreach (var pathPartition in userPathPartitions)
        {
            var flattendPaths = pathPartition.FlattenedPaths;

            ExistingPathPattern(flattendPaths, pathPartition);
            NonExistingPathPattern(flattendPaths, pathPartition);
        }

        var orderedPatternOccurences = pathPatternOccurences.Values
            .OrderByDescending(p => p.OccurenceCount);

        return orderedPatternOccurences;
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

            patternOccurence.PathPatterns.Add(
                new UserPathPartition(userPathPartition.Paths)
            );

            var occurence = new PathPattern(++count, patternOccurence.PathPatterns);

            pathPatternOccurences.Remove(flattenedPaths);
            pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }
}