using System.Collections.Immutable;

namespace logparserkata;
public sealed class PathPattern
{
    public IImmutableList<UserPathPartition> PathPatterns { get; }

    public int OccurenceCount { get; }
    public PathPattern(int occurenceCount, IImmutableList<UserPathPartition> pathPatterns)
    {         
        this.PathPatterns = pathPatterns?.ToImmutableList() ?? ImmutableList<UserPathPartition>.Empty;
        this.OccurenceCount = occurenceCount;
    }
}