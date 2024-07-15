using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
	public interface IAuthorRepo
	{
		Task<IEnumerable<Author>> GetAllAuthorsAsync();
		Task<Author> GetAuthorByIdAsync(int id);
		Task<Author> CreateAuthorAsync(Author author);
		Task<Author> UpdateAuthorAsync(Author author);
		Task DeleteAuthorAsync(int id);
	}
}
