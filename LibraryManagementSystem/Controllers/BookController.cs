using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private  IBookRepo _bookRepo;

		public BookController(IUnitOfWork unitOfWork, IBookRepo bookRepo)
		{
			_unitOfWork = unitOfWork;
			_bookRepo = bookRepo;
		}
		//working
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			var books = await _bookRepo.GetAllAsync();
			return Ok(books);
		}
		//working
		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook(int id)
		{
			var book = await _bookRepo.GetByIdAsync(id);
			if (book == null) return NotFound();
			return Ok(book);
		}

		[HttpPost]
		public async Task<ActionResult<Book>> PostBook(Book book)
		{
			var createdBook = await _bookRepo.AddAsync(book);
			await _unitOfWork.CompleteAsync();
			return CreatedAtAction("GetBook", new { id = createdBook.Book_Id }, createdBook);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutBook(int id, Book book)
		{
			if (id != book.Book_Id) return BadRequest();
			await _bookRepo.UpdateAsync(book);
			await _unitOfWork.CompleteAsync();
			return NoContent();
		}
		//working
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var result = await _bookRepo.DeleteAsync(id);
			if (!result) return NotFound();
			await _unitOfWork.CompleteAsync();
			return NoContent();
		}

		
	}
}
