using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public interface IUnitOfWork:IDisposable
	{
		IAuthorRepo AuthorRepo { get; }
		IBookRepo BookService { get; }
		Task<int> CompleteAsync();

	}
}
