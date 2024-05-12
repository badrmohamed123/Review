namespace Review.model
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; }
        public IFormFile poster { get; set; }//image
        public byte GenreId { get; set; }
        public Genre Genre { get; set;}
    }
}