using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
	public interface IBookRepo
	{
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<Book> GetBookByIdAsync(int id);
		Task<Book> CreateBookAsync(Book book);
		Task<Book> UpdateBookAsync(Book book);
		Task DeleteBookAsync(int id);
	}
}
