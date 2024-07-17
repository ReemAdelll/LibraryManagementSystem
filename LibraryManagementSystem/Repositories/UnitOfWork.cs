using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LibraryContext _context;
		private readonly IBookRepo _bookRepo;
		private readonly IAuthorRepo _authorRepo;
		public UnitOfWork(LibraryContext context, IBookRepo bookRepo, IAuthorRepo authorRepo)
		{
			_context = context;
			_bookRepo = bookRepo;
			_authorRepo = authorRepo;
		}
		public IBookRepo Books => _bookRepo;
		public IAuthorRepo Authors => _authorRepo;
		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}

	}
}
