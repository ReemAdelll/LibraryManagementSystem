using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LibraryContext _context;
		private IAuthorRepo _authorRepo;
		private IBookRepo _bookRepo;

		public UnitOfWork(LibraryContext context)
		{
			_context = context;
		}

		public IAuthorRepo AuthorRepo
		{
			get
			{
				if (_authorRepo == null)
				{
					_authorRepo = new AuthorRepo(_context);
				}
				return _authorRepo;
			}
		}

		public IBookRepo BookService
		{
			get
			{
				if (_bookRepo == null)
				{
					_bookRepo = new BookRepo(_context);
				}
				return _bookRepo;
			}
		}

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
