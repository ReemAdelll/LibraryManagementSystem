using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public interface IUnitOfWork:IDisposable
	{
		IAuthorRepo Authors { get; }
		IBookRepo Books { get; }
		Task<int> CompleteAsync();

	}
}
