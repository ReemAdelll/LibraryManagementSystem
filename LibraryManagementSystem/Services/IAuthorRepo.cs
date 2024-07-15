using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
	public interface IAuthorRepo : IBaseRepo<Author>
	{
		//specific operations for BookService
		Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync();
	}
}
