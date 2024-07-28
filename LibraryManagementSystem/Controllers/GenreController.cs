using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Shared;
using NPOI.SS.Formula.Functions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;


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
        [HttpGet]
        public IQueryable<GenreDTO> GetAll()
        {
            return _unitOfWork.Genres.GetAll();
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
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO GenreDto) 
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            var existingGenre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (existingGenre == null) return NotFound();
            GenreDto.Id = id;
            await _unitOfWork.Genres.UpdateAsync(GenreDto);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        //working
        [HttpPost]
        public async Task<IActionResult>CreateGenre([FromBody] GenreDTO GenreDto)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            var genre = await _unitOfWork.Genres.AddAsync(GenreDto);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
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
