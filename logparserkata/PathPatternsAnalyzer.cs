namespace logparserkata;

public class PathPatternsAnalyzer
{
    private IOrderedEnumerable<PathPattern> orderedPathPatterns;


    public PathPatternsAnalyzer(IOrderedEnumerable<PathPattern> orderedPathPatterns)
    {
        if (!orderedPathPatterns.Any())
        {
            throw new ArgumentException($"{nameof(orderedPathPatterns)} cannot be empty.");
        }

        if (!IsDescendingOrder(orderedPathPatterns))
        {
            throw new ArgumentException($"{nameof(orderedPathPatterns)}" +
                $" is not in descending order by {nameof(PathPattern.OccurenceCount)}.");
        }

        this.orderedPathPatterns = orderedPathPatterns;
    }

    public PathPattern SingleMostCommonPathPattern()
    {
        return this.orderedPathPatterns.First();
    }

    public IEnumerable<PathPattern> MostCommonPathPatterns(int take)
    {
        return this.orderedPathPatterns.Take(take);
    }

    public int TotalCommonPathPatterns()
    {
        return this.orderedPathPatterns.Sum(s => s.PathPatterns.Count());
    }


    private bool IsDescendingOrder(IOrderedEnumerable<PathPattern> input)
    {
        var previous = input.FirstOrDefault();
        
        if (previous is null)  
            return true; 
        

        foreach (var current in input.Skip(1))
        {
            if (current.OccurenceCount > previous.OccurenceCount)
            {
                return false;
            }
            previous = current;
        }

        return true;
    }
}
