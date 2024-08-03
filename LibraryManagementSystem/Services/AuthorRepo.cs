using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;

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
		public IQueryable<Author> GetAll()
		{
			return _context.Authors
				.Select(a => new Author
				{
					Id = a.Id,
					Name = a.Name,
					Country = a.Country
					
				});
		}
		public async Task<Author> GetByIdAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return null;
			return new Author { Id = author.Id, Name = author.Name };
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



		//public async Task<Author> AddAsync(AuthorCreateDTO authorCreateDTO)
		//{
  //          //var author = new Author { Name = AuthorCreateDTO.Name, Country = AuthorCreateDTO.Country, Bio = AuthorCreateDTO.Bio };
  //          //_context.Authors.Add(author);
  //          //await _context.SaveChangesAsync();
  //          //AuthorCreateDTO.Id = author.Id;
  //          //return author;

  //          Author author = authorCreateDTO; // Implicit conversion
  //          _context.Authors.Add(author);
  //          await _context.SaveChangesAsync();
  //          authorCreateDTO.Id = author.Id;
  //          return author;
  //      }

        //public async Task<Author> UpdateAsync(AuthorEditDTO AuthorEditDto)
        //{
        //    //var author = await _context.Authors.FindAsync(authorDto.Id);
        //    //if (author == null) return null;
        //    //author.Name = authorDto.Name;
        //    //author.Country = authorDto.Country;
        //    //_context.Authors.Update(author);
        //    //await _context.SaveChangesAsync();
        //    //return authorDto;
        //    var author = (Author)AuthorEditDto; //implicit conversion
        //    var existingAuthor = await _context.Authors.FindAsync(author.Id);
        //    if (existingAuthor == null) return null;

        //    // Update existing author fields
        //    existingAuthor.Name = author.Name;
        //    existingAuthor.Country = author.Country;

        //    _context.Authors.Update(existingAuthor);
        //    await _context.SaveChangesAsync();

        //    return existingAuthor;
        //}

		public async Task<bool> DeleteAsync(int id)
		{
			var author = await _context.Authors.FindAsync(id);
			if (author == null) return false;
			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();
			return true;
		}

        public async Task<Author> AddAsync(Author entity)
        {
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            var existingAuthor = await _context.Authors.FindAsync(entity.Id);
            if (existingAuthor == null) return null;

            existingAuthor.Name = entity.Name;
            existingAuthor.Country = entity.Country;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
