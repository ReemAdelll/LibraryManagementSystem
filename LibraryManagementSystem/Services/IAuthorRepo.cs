using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
	public interface IAuthorRepo : IBaseRepo<AuthorDTO>
	{
		//specific operations for Author
		Task<IEnumerable<AuthorDTO>> GetAllAuthorsWithBooksAsync();
		
	}
}
