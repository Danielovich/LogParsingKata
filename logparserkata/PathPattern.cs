namespace logparserkata;
public class PathPattern
{
    public List<UserPathPartition> PathPatterns { get; }
    public int OccurenceCount { get; }
    public PathPattern(int occurenceCount, List<UserPathPartition> pathPatterns)
    {         
        this.PathPatterns = pathPatterns ?? new List<UserPathPartition>();
        this.OccurenceCount = occurenceCount;
    }
}