namespace logparserkata
{
    public class PathPattern
    {
        public List<UserPathPartition> PathPatterns { get; } = new List<UserPathPartition>();
        public int OccurenceCount { get; }
        public PathPattern(int occurenceCount, List<UserPathPartition> pathPatterns)
        {         
            this.PathPatterns = pathPatterns;
            this.OccurenceCount = occurenceCount;
        }
    }
}