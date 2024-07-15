using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
	public class BookRepo : IBookRepo
	{
		private readonly LibraryContext _context;

		public BookRepo(LibraryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<Book> GetByIdAsync(int id)
		{
			return await _context.Books.FindAsync(id);
		}
		

		public async Task<Book> AddAsync(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public async Task<Book> UpdateAsync(Book book)
		{
			_context.Entry(book).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return book;
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
