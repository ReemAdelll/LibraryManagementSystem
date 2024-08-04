using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public BookController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		//new get with filter
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] string? publishyear, [FromQuery] int page = 1,[FromQuery] int pageSize = 10)
		{ 
			var booksQuery = _unitOfWork.Books.GetAll();
			if(!string.IsNullOrEmpty(title))
			{
				booksQuery = booksQuery.Where(a => a.Title.ToLower().Contains(title.ToLower()));
            }
			if(publishyear == "desc")
			{
				booksQuery = booksQuery.OrderByDescending(a => a.PublishedYear);
            }
			else
			{
                booksQuery = booksQuery.OrderBy(a => a.PublishedYear);
            }

            var totalCount = await booksQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var books = await booksQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Books = books
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);
			if (book == null) return NotFound();
			return Ok(book);

		}

		[HttpPost]
		public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO bookCreateDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// implicit conversion
			Book book = bookCreateDTO;
            var addedBook = await _unitOfWork.Books.AddAsync(book);
			await _unitOfWork.CompleteAsync();

			return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, (BookDTO)addedBook);
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, [FromBody] BookEditDTO bookEditDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
			if (existingBook == null) return NotFound();

			//implicit conversion
			Book book = bookEditDTO;

            book.Id = id;
            var updatedAuthor = await _unitOfWork.Books.UpdateAsync(book);
			await _unitOfWork.CompleteAsync();

			return NoContent();
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var success = await _unitOfWork.Books.DeleteAsync(id);
			if (!success) return NotFound();

			await _unitOfWork.CompleteAsync();
			return NoContent();
		}
	}
}
