using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
	public class Book: BaseEntity<int>
	{
		public string Title { get; set; }
		public int PublishedYear { get; set; }
		public int AuthorId { get; set; }
		//Navigation Property
		public Author Author { get; set; }

		public static implicit operator BookDTO(Book book)
		{
			return new BookDTO {
			Id = book.Id,
			Title = book.Title,
			PublishedYear = book.PublishedYear,
			AuthorId = book.AuthorId,
			};
		}
		public static implicit operator Book(BookCreateDTO bookCreateDTO)
		{
			return new Book
			{
				Title = bookCreateDTO.Title,
				PublishedYear = bookCreateDTO.PublishedYear,
				AuthorId = bookCreateDTO.AuthorId,
			};
		}
		public static implicit operator BookCreateDTO(Book book)
		{
			return new BookCreateDTO
			{
				Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId,
            };
		}
        public static implicit operator BookEditDTO(Book book) 
		{
			return new BookEditDTO
			{ 
				Title = book.Title,
				PublishedYear = book.PublishedYear,
			};
		}
		public static implicit operator Book(BookEditDTO bookEditDTO)
		{
			return new Book
			{ 
			  Id = bookEditDTO.Id,
			  Title = bookEditDTO.Title,
			  PublishedYear = bookEditDTO.PublishedYear

			};
		}
    }
}
