namespace logparserkata;

public class PathPatternsAnalyzerTests
{
    [Theory, PathPatternsAutoData]
    public void What_Is_The_Single_Most_Common_Page_Pattern(IOrderedEnumerable<PathPattern> testData)
    {
        //Arrange
        var sut = new PathPatternsAnalyzer(testData);

        //Act
        var actual = sut.SingleMostCommonPathPattern();

        //Arrange
        Assert.True(actual.PathPatterns.All(a => a.FlattenedPaths == "/home/contact/settings"));
    }
}
