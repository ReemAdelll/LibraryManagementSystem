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

        public static implicit operator AuthorDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Country = author.Country,
                CreationTime = author.CreationTime,
                LastUpdateTime = author.LastUpdateTime,
            };
        }

        public static implicit operator Author(AuthorCreateDTO authorCreateDTO)
        {
            return new Author
            {
                Name = authorCreateDTO.Name,
                Bio = authorCreateDTO.Bio,
                Country = authorCreateDTO.Country,
            };
        }
        public static implicit operator AuthorCreateDTO(Author author)
		{
			return new AuthorCreateDTO
			{
				Id = author.Id,
				Name = author.Name,
				Bio = author.Bio,
				Country = author.Country,
			};
		}
        public static implicit operator AuthorEditDTO(Author author)
        {
            return new AuthorEditDTO
            {
                Name = author.Name,
                Country = author.Country,
            };
        }
        public static implicit operator Author(AuthorEditDTO authorEditDto)
        {
            return new Author
            {
                Id = authorEditDto.Id,
                Name = authorEditDto.Name,
                Country = authorEditDto.Country,
            };
        }
    }
}
