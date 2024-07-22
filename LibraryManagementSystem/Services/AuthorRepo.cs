using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
	public class AuthorRepo : IAuthorRepo
	{
		private readonly LibraryContext _context;

		public AuthorRepo(LibraryContext context)
		{
			_context = context;
		}

		//public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
		//{
		//	var authors = await _context.Authors.ToListAsync();
		//	return authors.Select(a => new AuthorDTO { AuthorId = a.AuthorId, Name = a.Name ,Country=a.Country});
		//}
		public IQueryable<AuthorDTO> GetAll()
		{
			return _context.Authors
				.Select(a => new AuthorDTO
				{
					AuthorId = a.AuthorId,
					Name = a.Name,
					Country = a.Country
					
				});
		}
		public async Task<AuthorDTO> GetByIdAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return null;
			return new AuthorDTO { AuthorId = author.AuthorId, Name = author.Name };
		}
		//special method for author, not from base
		public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsWithBooksAsync()
		{
			var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
			return authors.Select(a => new AuthorDTO { AuthorId = a.AuthorId, Name = a.Name,Country=a.Country});
		}

		public async Task<AuthorDTO> AddAsync(AuthorDTO authorDto)
		{
			var author = new Author { Name = authorDto.Name, Country= authorDto.Country, Bio= authorDto.Bio };
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			authorDto.AuthorId = author.AuthorId;
			return authorDto;
		}

		public async Task<AuthorDTO> UpdateAsync(AuthorDTO authorDto)
		{
			var author = await _context.Authors.FindAsync(authorDto.AuthorId);
			if (author == null) return null;
			author.Name = authorDto.Name;
			author.Country = authorDto.Country;
			_context.Authors.Update(author);
			await _context.SaveChangesAsync();
			return authorDto;
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
