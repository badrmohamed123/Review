using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Review.Dtos;
using Review.model;

namespace Review.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]

        public async Task<IActionResult> CreateAsync(CreateMoviesDto dto)
        {
            using var datastream = new MemoryStream();
         //   await dto.poster.CopyToAsync(datastream);


            var Movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                poster = dto.poster,
                Rate = dto.Rate,
                Storeline = dto.Storeline,
                year = dto.year
            };
            await _context.AddAsync(Movie);
            _context.SaveChanges();
            return Ok(Movie);
        }
    }
}
