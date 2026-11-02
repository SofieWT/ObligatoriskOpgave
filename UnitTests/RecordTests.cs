using ObligatoriskOpgave.Services;

namespace UnitTests
{
    public class RecordTests
    {
        private RecordsRepositoryList _repo;
        public RecordTests()
        {
            _repo = new RecordsRepositoryList();
        }


        [Fact]
        public void GetAll_ReturnsAllRecords()
        {
            // Arrange

            // Act
            var records = _repo.GetAll();

            // Assert
            Assert.NotNull(records);
            Assert.Equal(2, records.Count());
            Assert.NotEmpty(records);
            Assert.Equal("Song 1", records.First().Title);
            Assert.Equal("Artist 1", records.First().Artist);
            Assert.Equal(180, records.First().DurationInSecs);
            Assert.Equal("2020", records.First().PublicationYear);
            Assert.Equal("Song 2", records.Last().Title);
            Assert.Equal("Artist 2", records.Last().Artist);
            Assert.Equal(200, records.Last().DurationInSecs);
            Assert.Equal("2021", records.Last().PublicationYear);
        }
    }
}
