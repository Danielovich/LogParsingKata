namespace logparserkata;

public class CommonPathDeterminatorIntegrationTests
{
    [Fact]
    public async Task Find_Most_Common_Three_Page_Path_Pattern_From_Loaded_File()
    {
        //Arrange
        var logLoader = new LogFileLoader();
        var logEntries = await logLoader.LoadEntriesAsync();

        var partions = new PathPartition(logEntries);
        var up = partions.PathPartitionsByUserId(3);

        var sut = new PathPatterns(up);

        //Act
        sut.OrderByOccurenceDescending();
        
        //Assert
    }

}
