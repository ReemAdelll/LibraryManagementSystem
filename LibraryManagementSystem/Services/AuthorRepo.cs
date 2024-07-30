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
		//	return authors.Select(a => new AuthorDTO { Id = a.Id, Name = a.Name ,Country=a.Country});
		//}
		public IQueryable<AuthorDTO> GetAll()
		{
			return _context.Authors
				.Select(a => new AuthorDTO
				{
					Id = a.Id,
					Name = a.Name,
					Country = a.Country
					
				});
		}
		public async Task<AuthorDTO> GetByIdAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return null;
			return new AuthorDTO { Id = author.Id, Name = author.Name };
		}
      
        //special method for author, not from base
        //authors with their books (With filtering)
        public async Task<IEnumerable<AuthorBooksDTO>> GetAllAuthorsWithBooksAsync(string? authorName, string? bookName)
        {
            var query = _context.Authors.Include(a => a.Books).AsQueryable();

            if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(a => a.Name.ToLower().Contains(authorName.ToLower()));
            }

            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(a => a.Books.Any(b => b.Title.ToLower().Contains(bookName.ToLower())));
            }

            var authors = await query.ToListAsync();

            // Map the results to DTOs
            return authors.Select(a => new AuthorBooksDTO
            {
                Id = a.Id,
                Name = a.Name,
                Country = a.Country,
                Books = a.Books.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorId = b.AuthorId
                }).ToList()
            });
        }



        public async Task<AuthorDTO> AddAsync(AuthorDTO authorDto)
		{
			var author = new Author { Name = authorDto.Name, Country= authorDto.Country, Bio= authorDto.Bio };
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			authorDto.Id = author.Id;
			return authorDto;
		}

		public async Task<AuthorDTO> UpdateAsync(AuthorDTO authorDto)
		{
			var author = await _context.Authors.FindAsync(authorDto.Id);
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
