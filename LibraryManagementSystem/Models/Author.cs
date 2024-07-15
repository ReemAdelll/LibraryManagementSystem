﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
	public class Author
	{
        public int Author_Id { get; set; }
		public string Name { get; set;}
		public string Bio { get; set; }
		public string Country { get; set; }

		//Navigation Property
		public ICollection<Book> Books { get; set; }
	}
}
