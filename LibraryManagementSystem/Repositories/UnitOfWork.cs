using LibraryManagementSystem.Models;
using static LibraryManagementSystem.Repositories.IGenericRepo;

namespace LibraryManagementSystem.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LibraryContext _context;
		private IGenericRepo<Author> _authors;
		private IGenericRepo<Book> _books;

		public UnitOfWork(LibraryContext context)
		{
			_context = context;
		}

		public IGenericRepo<Author> Authors => _authors ??= new GenericRepo<Author>(_context);
		public IGenericRepo<Book> Books => _books ??= new GenericRepo<Book>(_context);


		public Task<int> CommitAsync()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
