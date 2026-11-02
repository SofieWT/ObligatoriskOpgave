namespace ObligatoriskOpgave.Models
{
    public class Record
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int DurationInSecs { get; set; }
        public string PublicationYear { get; set; }


        public override string ToString()
        {
                return $"Title: {Title}, Artist: {Artist}, Duration: {DurationInSecs} seconds, Publication Year: {PublicationYear}";
        }

    }
}
