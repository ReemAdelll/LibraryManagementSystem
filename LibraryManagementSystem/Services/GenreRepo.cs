using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Shared;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Services;

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
            var genre = new Genre { GenreId = genreDTO.GenreId, GenreName = genreDTO.GenreName };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            genreDTO.GenreId = genre.GenreId;
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
            return _context.Genres.Select(a=> new GenreDTO { GenreId = a.GenreId, GenreName= a.GenreName });
        }

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            //var genres = await _context.Genres.FirstOrDefaultAsync(a => a.GenreId == id);
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null) return null;
            return new GenreDTO { GenreId= genres.GenreId, GenreName  = genres.GenreName };
        }

        public async Task<GenreDTO> UpdateAsync(GenreDTO genreDTO)
        {
            var genre = await _context.Genres.FindAsync(genreDTO.GenreId);
            if (genre == null) return null;
            genre.GenreId = genreDTO.GenreId;
            genre.GenreName = genreDTO.GenreName;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genreDTO;

        }
    }
}
