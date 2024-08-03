using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
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
		//	return books.Select(b => new BookDTO { Id = b.Id, Title = b.Title });
		//}
		public IQueryable<Book> GetAll()
		{
			return _context.Books.Select(b => new Book
			{
				Id = b.Id,
				Title = b.Title,
				AuthorId= b.AuthorId,
				PublishedYear = b.PublishedYear,
			}); ;
		}

		public async Task<Book> GetByIdAsync(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null) return null;
			return new Book { Id = book.Id, Title = book.Title, AuthorId=book.AuthorId , PublishedYear= book.PublishedYear};
		}


		public async Task<Book> AddAsync(Book entity)
		{
            //var book = new Book { Title = bookDto.Title, AuthorId = bookDto.AuthorId, PublishedYear=bookDto.PublishedYear };
            //_context.Books.Add(book);
            //await _context.SaveChangesAsync();
            //bookDto.Id = book.Id;
            //return bookDto;
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

		public async Task<Book> UpdateAsync(Book entity)
		{
			//var book = await _context.Books.FindAsync(bookDto.Id);
			//if (book == null) return null;
			//book.Title = bookDto.Title;
			//book.PublishedYear = bookDto.PublishedYear;
			//_context.Books.Update(book);
			//await _context.SaveChangesAsync();
			//return bookDto;

			var existingBook = await _context.Books.FindAsync(entity.Id);
            if (existingBook == null) return null;

            existingBook.Title = entity.Title;
            existingBook.PublishedYear = entity.PublishedYear;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
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
