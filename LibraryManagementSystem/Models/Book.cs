using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
	public class Book: BaseEntity<int>
	{
        public int Book_Id { get; set; }
		public string Title { get; set; }
		public int PublishedYear { get; set; }
		public int Author_Id { get; set; }
		//Navigation Property
		public Author Author { get; set; }
	}
}
