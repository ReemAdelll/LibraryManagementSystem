using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
	public class AuthorRepo : IAuthorRepo
	{
		public Task<Author> GetAuthorByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<Author>> GetAllAuthorsAsync()
		{
			throw new NotImplementedException();
		}
		public Task<Author> CreateAuthorAsync(Author author)
		{
			throw new NotImplementedException();
		}
		public Task<Author> UpdateAuthorAsync(Author author)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAuthorAsync(int id)
		{
			throw new NotImplementedException();
		}

	
	}
}
