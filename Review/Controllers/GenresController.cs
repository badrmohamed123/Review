using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review.Dtos;
using Review.model;

namespace Review.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("[action]")]//name = GetAllAsync
        public async Task<IActionResult> GetAllAsync()
        {
            var geners = await _context.Genres.ToListAsync();
            return Ok(geners);

        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UdateAsync(int id, GenreDto dto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g=>g.Id ==id);

            if (genre == null)
                return NotFound($"No Genre Was Found With Id : {id}");

            genre.Name = dto.Name;

            _context.SaveChanges();

            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"No Genre Was Found With Id : {id}");
            _context.Genres.Remove(genre);

            _context.SaveChanges();

            return Ok(genre);
        }
    }
}
