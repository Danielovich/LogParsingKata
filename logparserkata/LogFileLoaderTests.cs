namespace logparserkata
{
    public class LogFileLoaderTests
    {
        [Fact]
        public async Task Log_File_Entries_Loads()
        {
            //Arrange
            var sut = new LogFileLoader();

            //Act
            var entries = await sut.LoadEntriesAsync();

            //Assert
            Assert.True(entries.Count() > 0);
        }

        [Fact]
        public async Task All_LogEntry_Properties_Are_Present()
        {
            //Arrange
            var sut = new LogFileLoader();

            //Act
            var entries = await sut.LoadEntriesAsync("logfile2.csv");

            //Assert
            Assert.True(entries.AllWithNonEmptyCheck(a => a.LoadTime > 0));
            Assert.True(entries.AllWithNonEmptyCheck(a => a.UserId > 0));
            Assert.True(entries.AllWithNonEmptyCheck(a => !string.IsNullOrEmpty(a.Path)));
        }
    }

    internal static class IEnumerableExtensions
    {
        public static bool AllWithNonEmptyCheck<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Any() && source.All(predicate);
        }
    }
}