using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
	public class AuthorRepo : IAuthorRepo
	{
		private readonly LibraryContext _context;

		public AuthorRepo(LibraryContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Author>> GetAllAsync()
		{
			return await _context.Authors.ToListAsync();
		}

		public async Task<Author> GetByIdAsync(int id)
		{
			return await _context.Authors.FindAsync(id);
		}
		//special method for author, not from base
		public async Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync()
		{
			return await _context.Authors.Include(a => a.Books).ToListAsync();
		}

		public async Task<Author> AddAsync(Author author)
		{
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			return author;
		}

		public async Task<Author> UpdateAsync(Author author)
		{
			_context.Entry(author).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return author;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return false;

			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
