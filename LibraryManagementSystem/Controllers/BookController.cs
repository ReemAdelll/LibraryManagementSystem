using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Shared;

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
                Book_Id = bookDto.Book_Id,
                Title = bookDto.Title,
                Author_Id = bookDto.Author_Id,
                PublishedYear = bookDto.PublishedYear,
                //Creation_Time = DateTime.Now,
                //LastUpdate_Time = DateTime.Now
            };
        }

        private BookDTO MapToBookDTO(Book book)
        {
            return new BookDTO
            {
                Book_Id = book.Book_Id,
                Title = book.Title,
                Author_Id = book.Author_Id,
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

        //working
        [HttpGet]
		public IQueryable<BookDTO> GetAll()
		{
			return _unitOfWork.Books.GetAll();
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

			return CreatedAtAction(nameof(GetBookById), new { id = book.Book_Id }, book);
		}
		//working
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
			if (existingBook == null) return NotFound();

			bookDto.Book_Id = id;
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
