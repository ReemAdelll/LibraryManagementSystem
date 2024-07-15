using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private IAuthorRepo _authorRepo;

		public AuthorController(IUnitOfWork unitOfWork, IAuthorRepo authorRepo)
		{
			_unitOfWork = unitOfWork;
			_authorRepo = authorRepo;
		}
		//working
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
		{
			var authors = await _authorRepo.GetAllAsync();
			return Ok(authors);
		}
		//working
		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetAuthor(int id)
		{
			var author = await _authorRepo.GetByIdAsync(id);
			if (author == null) return NotFound();
			return Ok(author);
		}
		//error
		[HttpPost]
		public async Task<ActionResult<Author>> PostAuthor(Author author)
		{
			var createdAuthor = await _authorRepo.AddAsync(author);
			await _unitOfWork.CompleteAsync();
			return CreatedAtAction("GetAuthor", new { id = createdAuthor.Author_Id }, createdAuthor);
		}
		//error
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAuthor(int id, Author author)
		{
			if (id != author.Author_Id) return BadRequest();
			await _authorRepo.UpdateAsync(author);
			await _unitOfWork.CompleteAsync();
			return NoContent();
		}
		//working
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
		{
			var result = await _authorRepo.DeleteAsync(id);
			if (!result) return NotFound();
			await _unitOfWork.CompleteAsync();
			return NoContent();
		}
		//working
		[HttpGet("authors-with-books")]
		public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthorsWithBooks()
		{
			var authors = await _authorRepo.GetAllAuthorsWithBooksAsync();
			return Ok(authors);
		}
	}
}
