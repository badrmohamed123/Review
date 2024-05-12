using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Dtos;
using Review.model;

namespace Review.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private new List<String> allowedExtenstion = new List<String>() { ".jpg", ".png" };
        private long _maxallowedpostersize = 1048576; // == 1mb


        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _context.Movies
                .OrderByDescending(x=> x.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetByIdAsync(byte  genereid)
        {
            var movies = await _context.Movies
               .OrderByDescending(x => x.Rate)
               .Include(m => m.Genre)
               .ToListAsync();
            return Ok(movies);
        }

        
        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _context.Movies
                .Where(m => m.GenreId == genreId)
               .OrderByDescending(x => x.Rate)
               .Include(m => m.Genre)
               .ToListAsync();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromForm] CreateMoviesDto dto)
        {
            if (dto.poster == null)
                return BadRequest("Poster is Required!");
            if (! allowedExtenstion.Contains(Path.GetExtension(dto.poster.FileName).ToLower()))
                return BadRequest("Only .png and .jog images are allowed");

            if (dto.poster.Length > _maxallowedpostersize)
                return BadRequest("Max allwed size for poster is 1MB");
            var isvalidGenere = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isvalidGenere)
                return BadRequest("Invalid genere Id"); 
            using var datastream = new MemoryStream();
            await dto.poster.CopyToAsync(datastream);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateMoviesDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound($"No Movies Was Found With Id{id}");

            var isvalidGenere = await _context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            if (!isvalidGenere)
                return BadRequest("Invalid genere Id");

            

            if(dto.poster != null)
            {
                if (!allowedExtenstion.Contains(Path.GetExtension(dto.poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jog images are allowed");

                if (dto.poster.Length > _maxallowedpostersize)
                    return BadRequest("Max allwed size for poster is 1MB");

                using var datastream = new MemoryStream();

                await dto.poster.CopyToAsync(datastream);

          //      movie.poster=datastream.ToArray();

            }
            movie.Title = dto.Title;
            movie.Storeline = dto.Storeline;
            movie.GenreId = dto.GenreId;
            movie.poster = dto.poster;
            movie.Rate = dto.Rate;
            movie.year = dto.year;

            _context.SaveChanges();
            return Ok(movie);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound($"No Movies Was Found With Id{id}");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

    }
}
