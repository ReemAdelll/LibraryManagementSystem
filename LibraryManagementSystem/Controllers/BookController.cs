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

        private Book MapToBook(BookDTO bookDto)
        {
            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublishedYear = bookDto.PublishedYear,
                //Creation_Time = DateTime.Now,
                //LastUpdate_Time = DateTime.Now
            };
        }

        private BookDTO MapToBookDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublishedYear = book.PublishedYear
            };
        }

		//working
		//[HttpGet]
		//public async Task<IActionResult> GetAllBooks()
		//{
		//	var books = await _unitOfWork.Books.GetAllAsync();
		//	return Ok(books);
		//}

		//old get without filter
		//working
		//      [HttpGet]
		//public IQueryable<BookDTO> GetAll()
		//{
		//	return _unitOfWork.Books.GetAll();
		//}

		//new get with filter
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] string? publishyear)
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
			
			var books = await booksQuery.ToListAsync();
			return Ok(books);

        }


            //working
            [HttpGet("{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);
			if (book == null) return NotFound();
			return Ok(book);

		}
		//working
		[HttpPost]
		public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var book = await _unitOfWork.Books.AddAsync(bookDto);
			await _unitOfWork.CompleteAsync();

			return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
		}
		//working
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
			if (existingBook == null) return NotFound();

			bookDto.Id = id;
			await _unitOfWork.Books.UpdateAsync(bookDto);
			await _unitOfWork.CompleteAsync();

			return NoContent();
		}
		//working
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
