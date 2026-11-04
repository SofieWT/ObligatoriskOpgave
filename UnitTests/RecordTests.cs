//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using ObligatoriskOpgave.Services;
using ObligatoriskOpgave.Models;
using Microsoft.AspNetCore.Identity;
using Record = ObligatoriskOpgave.Models.Record;

namespace UnitTests
{
    public class RecordTests
    {
        private RecordsRepositoryList _repo;
        private AuthRepository _authRepo;
        private IConfiguration config;
        public RecordTests()
        {
            _repo = new RecordsRepositoryList();
            _authRepo = new AuthRepository(config);
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

        [Fact]
        public void GetAll_WithTitleFilter_ReturnsFilteredRecords()
        {
            // Arrange
            string titleFilter = "Song 1";
            // Act
            var records = _repo.GetAll(title: titleFilter);
            // Assert
            Assert.NotNull(records);
            Assert.Single(records);
            Assert.Equal(titleFilter, records.First().Title);
        }

        [Fact]
        public void GetAll_WithArtistFilter_ReturnsFilteredRecords()
        {
            // Arrange
            string artistFilter = "Artist 2";
            // Act
            var records = _repo.GetAll(artist: artistFilter);
            // Assert
            Assert.NotNull(records);
            Assert.Single(records);
            Assert.Equal(artistFilter, records.First().Artist);
        }

        [Fact]
        public void GetAll_WithDurationFilter_ReturnsFilteredRecords()
        {
            // Arrange
            int durationFilter = 200;
            // Act
            var records = _repo.GetAll(durationInSecs: durationFilter);
            // Assert
            Assert.NotNull(records);
            Assert.Single(records);
            Assert.Equal(durationFilter, records.First().DurationInSecs);
        }

        [Fact]
        public void GetAll_WithPublicationYearFilter_ReturnsFilteredRecords()
        {
            // Arrange
            string publicationYearFilter = "2020";
            // Act
            var records = _repo.GetAll(publicationYear: publicationYearFilter);
            // Assert
            Assert.NotNull(records);
            Assert.Single(records);
            Assert.Equal(publicationYearFilter, records.First().PublicationYear);
        }

        [Fact]
        public void AddRecord_AddsRecordSuccessfully()
        {
            // Arrange
            Record newRecord = new Record
            {
                Title = "New Song",
                Artist = "New Artist",
                DurationInSecs = 250,
                PublicationYear = "2023"
            };
            // Act
            var addedRecord = _repo.AddRecord(newRecord);

            // Assert
            Assert.NotNull(addedRecord);
            Assert.Equal(newRecord.Title, addedRecord.Title);
            Assert.Equal(newRecord.Artist, addedRecord.Artist);
            Assert.Equal(newRecord.DurationInSecs, addedRecord.DurationInSecs);
            Assert.Equal(newRecord.PublicationYear, addedRecord.PublicationYear);
            Assert.Equal(3, addedRecord.ID); // Since we already have 2 records in the repository
            Assert.Equal(3, _repo.GetAll().Count()); // Ensure the total count is now 3
        }





        #region AuthTests:

        [Fact]
        public void LoginTest()
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Username = "Sofie",
                Password = "1234"

            };

            //act
            var user = _authRepo.Login(loginRequest);

            //assert
            Assert.NotNull(user);
        }
        #endregion

    }
}
