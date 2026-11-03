//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using ObligatoriskOpgave.Services;
using ObligatoriskOpgave.Models;
using Microsoft.AspNetCore.Identity;

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
