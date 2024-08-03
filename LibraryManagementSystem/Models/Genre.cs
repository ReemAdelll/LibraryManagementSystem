using LibraryManagementSystem.Shared;

namespace LibraryManagementSystem.Models
{
    public class Genre : BaseEntity<int>
    {
        public string GenreName { get; set; }

        public static implicit operator GenreDTO(Genre genre)
        {
            return new GenreDTO
            {
                Id = genre.Id,
                GenreName = genre.GenreName,
            };
        }

        public static implicit operator GenreCreateDTO(Genre genre)
        {
            return new GenreCreateDTO
            {
                Id = genre.Id,
                GenreName = genre.GenreName
                
            };
        }
        public static implicit operator Genre(GenreCreateDTO genreCreateDTO)
        {
            return new Genre
            { Id = genreCreateDTO.Id ,
            GenreName = genreCreateDTO.GenreName,
            };   
        }
        public static implicit operator GenreEditDTO(Genre genre)
        {
            return new GenreEditDTO {
                Id = genre.Id,
                GenreName = genre.GenreName,
            };
        }
        public static implicit operator Genre(GenreEditDTO genreEditDTO)
        {
            return new Genre
            {
                Id = genreEditDTO.Id,
                GenreName = genreEditDTO.GenreName
            };
        }
    }
}
