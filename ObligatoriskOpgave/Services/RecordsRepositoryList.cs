using ObligatoriskOpgave.Models;

namespace ObligatoriskOpgave.Services
{
    public class RecordsRepositoryList
    {
        private List<Record> _records = new List<Record>();
        private int _nextId = 1;
        public RecordsRepositoryList()
        {
            _records.Add(new Record {ID = _nextId++, Title = "Song 1", Artist = "Artist 1", DurationInSecs = 180, PublicationYear = "2020" });
            _records.Add(new Record { ID = _nextId++, Title = "Song 2", Artist = "Artist 2", DurationInSecs = 200, PublicationYear = "2021" });

        }



        public IEnumerable<Record> GetAll(string? title = null, string? artist = null, int? durationInSecs = null, string? publicationYear = null)
        {
            var query = _records.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(r => r.Title != null && r.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(artist))
            {
                query = query.Where(r => r.Artist != null && r.Artist.Contains(artist));
            }

            if (durationInSecs.HasValue)
            {
                query = query.Where(r => r.DurationInSecs == durationInSecs.Value);
            }

            if (!string.IsNullOrEmpty(publicationYear))
            {
                query = query.Where(r => r.PublicationYear != null && r.PublicationYear == publicationYear);
            }

            return query.ToList();
        }

        public Record AddRecord(Record record)
        {
            record.ID = _nextId++;
            _records.Add(record);
            return record;
        }


    }
}
