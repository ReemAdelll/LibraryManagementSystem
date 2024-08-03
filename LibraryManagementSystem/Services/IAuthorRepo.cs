using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Services
{
	public interface IAuthorRepo : IBaseRepo<Author>
	{
		//specific operations for Author
		Task<IEnumerable<AuthorBooksDTO>> GetAllAuthorsWithBooksAsync(string authorName,string bookName);
		
	}
}
