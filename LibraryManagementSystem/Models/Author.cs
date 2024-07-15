using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
	public class Author
	{
		[Key]
        public int Author_Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string Name { get; set;}
		public ICollection<Book> Books { get; set; }
	}
}
