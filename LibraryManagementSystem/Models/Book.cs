using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
	public class Book
	{
		[Key]
        public int Book_Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Title { get; set; }
		public int Author_Id { get; set; }
		public Author Author { get; set; }
	}
}
