namespace logparserkata
{
    public class CommonPathOccurence
    {
        public List<CommonPath> CommonPaths { get; } = new List<CommonPath>();
        public int OccurenceCount { get; }
        public CommonPathOccurence(int occurenceCount, List<CommonPath> commonPaths)
        {         
            this.CommonPaths = commonPaths;
            this.OccurenceCount = occurenceCount;
        }
    }
}