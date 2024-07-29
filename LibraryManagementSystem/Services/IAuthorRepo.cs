using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Services
{
	public interface IAuthorRepo : IBaseRepo<AuthorDTO>
	{
		//specific operations for Author
		Task<IEnumerable<AuthorBooksDTO>> GetAllAuthorsWithBooksAsync(string authorName,string bookName);
		
	}
}
