using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace logparserkata;

public sealed class PathPatterns
{
    private readonly IImmutableList<UserPathPartition> userPathPartitions;
    private readonly Dictionary<string, PathPattern> pathPatternOccurences;

    public PathPatterns(IEnumerable<UserPathPartition> userPathPartitions)
    {
        this.userPathPartitions = userPathPartitions.ToImmutableList() ?? ImmutableList<UserPathPartition>.Empty;
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
                ImmutableList<UserPathPartition>.Empty.Add(
                    new UserPathPartition(userPathPartition.Paths)
                )
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

            var pathPatternsCopy = patternOccurence.PathPatterns.Add(
                new UserPathPartition(userPathPartition.Paths)
            );

            var occurence = new PathPattern(++count, pathPatternsCopy);

            pathPatternOccurences.Remove(flattenedPaths);
            pathPatternOccurences.Add(flattenedPaths, occurence);
        }
    }
}