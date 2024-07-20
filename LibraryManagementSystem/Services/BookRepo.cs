using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
	public class BookRepo : IBookRepo
	{
		private readonly LibraryContext _context;

		public BookRepo(LibraryContext context)
		{
			_context = context;
		}

		//public async Task<IEnumerable<BookDTO>> GetAllAsync()
		//{
		//	var books = await _context.Books.ToListAsync();
		//	return books.Select(b => new BookDTO { BookId = b.BookId, Title = b.Title });
		//}
		public IQueryable<BookDTO> GetAll()
		{
			return _context.Books.Select(b => new BookDTO
			{
				BookId = b.BookId,
				Title = b.Title,
			}); ;
		}

		public async Task<BookDTO> GetByIdAsync(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null) return null;
			return new BookDTO { BookId = book.BookId, Title = book.Title, AuthorId=book.AuthorId , PublishedYear= book.PublishedYear};
		}


		public async Task<BookDTO> AddAsync(BookDTO bookDto)
		{
			var book = new Book { Title = bookDto.Title, AuthorId = bookDto.AuthorId, PublishedYear=bookDto.PublishedYear };
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			bookDto.BookId = book.BookId;
			return bookDto;
		}

		public async Task<BookDTO> UpdateAsync(BookDTO bookDto)
		{
			var book = await _context.Books.FindAsync(bookDto.BookId);
			if (book == null) return null;
			book.Title = bookDto.Title;
			book.PublishedYear = bookDto.PublishedYear;
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
			return bookDto;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null) return false;
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
