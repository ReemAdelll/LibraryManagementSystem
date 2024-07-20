using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
	public class Book: BaseEntity<int>
	{
        public int BookId { get; set; }
		public string Title { get; set; }
		public int PublishedYear { get; set; }
		public int AuthorId { get; set; }
		//Navigation Property
		public Author Author { get; set; }
	}
}
