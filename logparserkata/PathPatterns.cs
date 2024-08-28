using System.Collections.Immutable;

namespace logparserkata;

public sealed class PathPatterns
{
    private readonly IImmutableList<UserPathPartition> userPathPartitions;
    private readonly Dictionary<string, PathPattern> pathPatternOccurences;

    public PathPatterns(IImmutableList<UserPathPartition> userPathPartitions)
    {
        this.userPathPartitions = userPathPartitions ?? ImmutableList<UserPathPartition>.Empty;
        this.pathPatternOccurences = new Dictionary<string, PathPattern>();
    }

    public IOrderedEnumerable<PathPattern> OrderByOccurenceDescending()
    {
        this.pathPatternOccurences.Clear();

        foreach (var pathPartition in this.userPathPartitions)
        {
            var flattendPaths = pathPartition.FlattenedPaths;

            ExistingPathPattern(flattendPaths, pathPartition);
            NonExistingPathPattern(flattendPaths, pathPartition);
        }

        var orderedPatternOccurences = this.pathPatternOccurences.Values
            .OrderByDescending(p => p.OccurenceCount);

        return orderedPatternOccurences;
    }

    private void NonExistingPathPattern(string flattenedPaths, UserPathPartition userPathPartition)
    {
        if (!this.pathPatternOccurences.ContainsKey(flattenedPaths))
        {
            var occurence = new PathPattern(1,
                ImmutableList<UserPathPartition>.Empty.Add(
                    new UserPathPartition(userPathPartition.Paths)
                )
            );

            this.pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }

    private void ExistingPathPattern(string flattenedPaths, UserPathPartition userPathPartition)
    {
        if (this.pathPatternOccurences.ContainsKey(flattenedPaths))
        {
            var patternOccurence = this.pathPatternOccurences[flattenedPaths];
            var count = patternOccurence.OccurenceCount;

            var pathPatternsCopy = patternOccurence.PathPatterns.Add(
                new UserPathPartition(userPathPartition.Paths)
            );

            var occurence = new PathPattern(++count, pathPatternsCopy);

            this.pathPatternOccurences.Remove(flattenedPaths);
            this.pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }
}