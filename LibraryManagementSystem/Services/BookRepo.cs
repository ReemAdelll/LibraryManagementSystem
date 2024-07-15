using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
	public class BookRepo : IBookRepo
	{
		public Task<Book> GetBookByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<Book>> GetAllBooksAsync()
		{
			throw new NotImplementedException();
		}
		public Task<Book> CreateBookAsync(Book book)
		{
			throw new NotImplementedException();
		}
		public Task<Book> UpdateBookAsync(Book book)
		{
			throw new NotImplementedException();
		}

		public Task DeleteBookAsync(int id)
		{
			throw new NotImplementedException();
		}

		
	}
}
