using LibraryManagementSystem.Models;
using static LibraryManagementSystem.Repositories.IGenericRepo;

namespace LibraryManagementSystem.Repositories
{
	public interface IUnitOfWork:IDisposable
	{
		IGenericRepo<Author> Authors { get; }
		IGenericRepo<Book> Books { get; }
		Task<int> CommitAsync();
	}
}
