using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
	public class Author: BaseEntity<int>
	{
		public string Name { get; set;}
		public string Bio { get; set; }
		public string Country { get; set; }

		//Navigation Property
		public ICollection<Book> Books { get; set; }
	}
}
