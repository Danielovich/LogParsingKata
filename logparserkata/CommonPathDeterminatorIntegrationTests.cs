namespace logparserkata;

public class CommonPathDeterminatorIntegrationTests
{
    [Fact]
    public async Task Find_Most_Common_Three_Page_Path_Pattern_From_Loaded_File()
    {
        //Arrange
        var logLoader = new LogFileLoader();
        var logEntries = await logLoader.LoadEntriesAsync();

        var sut = new CommonPathDeterminator(logEntries);

        //Act
        var pattern = sut.ThreePagePattern(sut.CommonPathPartitionsByUserId());
        var oo = pattern.OrderByDescending(o => o.OccurenceCount).ToList();
        //Assert

    }

}
