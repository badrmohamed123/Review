namespace Review.model
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public string year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; }
        public byte[] poster { get; set; }//image
        public byte GenreId { get; set; }
        public Genre Genre { get; set;}
    }
}