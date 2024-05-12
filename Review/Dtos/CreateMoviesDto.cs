namespace Review.Dtos
{
    public class CreateMoviesDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; }
        public IFormFile ? poster { get; set; }//image
        public byte GenreId { get; set; }
    }
}
