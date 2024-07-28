using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;


namespace LibraryManagementSystem.Services
{
    public class GenreRepo : IGenreRepo
    {
        private readonly LibraryContext _context;
        public GenreRepo(LibraryContext context)
        {
            _context = context;
        }
        public async Task<GenreDTO> AddAsync(GenreDTO genreDTO)
        {
            var genre = new Genre { Id = genreDTO.Id, GenreName = genreDTO.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            genreDTO.Id = genre.Id;
            return genreDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;

        }

        public IQueryable<GenreDTO> GetAll()
        {
            return _context.Genres.Select(a=> new GenreDTO { Id = a.Id, GenreName= a.GenreName });
        }

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            //var genres = await _context.Genres.FirstOrDefaultAsync(a => a.Id == id);
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null) return null;
            return new GenreDTO { Id= genres.Id, GenreName  = genres.GenreName };
        }

        public async Task<GenreDTO> UpdateAsync(GenreDTO genreDTO)
        {
            var genre = await _context.Genres.FindAsync(genreDTO.Id);
            if (genre == null) return null;
            genre.Id = genreDTO.Id;
            genre.GenreName = genreDTO.GenreName;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genreDTO;

        }
    }
}
