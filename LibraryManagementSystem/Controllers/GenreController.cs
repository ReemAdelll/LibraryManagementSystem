using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Shared;
using NPOI.SS.Formula.Functions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private Genre MapToGenre(GenreDTO genreDTO)
        {
            return new Genre
            {
                Id = genreDTO.Id, GenreName = genreDTO.GenreName,
            };
        }
        private GenreDTO MapToGenreDTO(Genre genre)
        {
            return new GenreDTO { Id = genre.Id, GenreName = genre.GenreName };
        }

        //working
        //old get (without filter)
        //[HttpGet]
        //public IQueryable<GenreDTO> GetAll()
        //{
        //    return _unitOfWork.Genres.GetAll();
        //}

        //new get (with filter)
        [HttpGet]
        public async Task <IActionResult> GetAll([FromQuery] string? name, [FromQuery] string? sortOrder, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var genreQuery = _unitOfWork.Genres.GetAll();
            if(!string.IsNullOrEmpty(name))
            {
                genreQuery = genreQuery.Where(a => a.GenreName.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                genreQuery = sortOrder.ToLower() == "desc"?genreQuery.OrderByDescending(a => a.GenreName):genreQuery.OrderBy(a => a.GenreName);
            }
            var genres = await genreQuery.ToListAsync();
            var totalCount = await genreQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var gen = await genreQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Genres = genres
            };
            return Ok(response);
        }


        //working
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre =await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null) return NotFound();
            return Ok(genre);
           
        }

        //working
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreEditDTO genreEditDTO) 
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            var existingGenre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (existingGenre == null) return NotFound();

            //implicit conversion
            Genre genre = genreEditDTO;

            genre.Id = id;
            await _unitOfWork.Genres.UpdateAsync(genre);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        //working
        [HttpPost]
        public async Task<IActionResult>CreateGenre([FromBody] GenreCreateDTO genreCreateDTO)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            // implicit conversion
            Genre genre = genreCreateDTO;
            var AddedGenre = await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetGenreById), new { id = AddedGenre.Id }, (GenreDTO)AddedGenre);
        }

        //working
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var success = await _unitOfWork.Genres.DeleteAsync(id);
            if (!success) return NotFound();
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
