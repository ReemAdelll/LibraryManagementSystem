using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public interface IUnitOfWork:IDisposable
	{
		IAuthorRepo Authors { get; }
		IBookRepo Books { get; }
		IGenreRepo Genres { get; }
		IMemberRepo Members { get; }
		IBorrowedBookRepo borrowedBooks {  get; }
		Task<int> CompleteAsync();

	}
}
