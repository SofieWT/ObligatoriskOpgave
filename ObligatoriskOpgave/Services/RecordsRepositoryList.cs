using ObligatoriskOpgave.Models;

namespace ObligatoriskOpgave.Services
{
    public class RecordsRepositoryList
    {
        private List<Record> _records = new List<Record>();
        public RecordsRepositoryList()
        {
            _records.Add(new Record { Title = "Song 1", Artist = "Artist 1", DurationInSecs = 180, PublicationYear = "2020" });
            _records.Add(new Record { Title = "Song 2", Artist = "Artist 2", DurationInSecs = 200, PublicationYear = "2021" });

        }



        public IEnumerable<Record> GetAll()
        {
            return _records.AsReadOnly();
        }


    }
}
