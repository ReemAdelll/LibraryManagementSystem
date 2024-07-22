using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Services;


namespace LibraryManagementSystem.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly LibraryContext _context;
		private readonly IBookRepo _bookRepo;
		private readonly IAuthorRepo _authorRepo;
		private readonly IGenreRepo _genreRepo;
		private readonly IMemberRepo _memberRepo;
		private readonly IBorrowedBookRepo _borrowedBookRepo;
        public UnitOfWork(LibraryContext context, IBookRepo bookRepo, IAuthorRepo authorRepo,
			IGenreRepo genreRepo, IMemberRepo memberRepo, IBorrowedBookRepo borrowedBookRepo)
		{
			_context = context;
			_bookRepo = bookRepo;
			_authorRepo = authorRepo;
			_genreRepo = genreRepo;
            _memberRepo = memberRepo;
            _borrowedBookRepo = borrowedBookRepo;



        }
		public IBookRepo Books => _bookRepo;
		public IAuthorRepo Authors => _authorRepo;
		public IGenreRepo Genres => _genreRepo;
		public IMemberRepo Members => _memberRepo;
		public IBorrowedBookRepo borrowedBooks => _borrowedBookRepo;
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
