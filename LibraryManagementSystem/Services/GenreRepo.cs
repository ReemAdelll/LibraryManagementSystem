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
        public async Task<Genre> AddAsync(Genre entity)
        {
            _context.Genres.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;

        }

        public IQueryable<Genre> GetAll()
        {
            return _context.Genres.Select(a=> new Genre { Id = a.Id, GenreName= a.GenreName, CreationTime = a.CreationTime, LastUpdateTime = a.LastUpdateTime });
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            //var genres = await _context.Genres.FirstOrDefaultAsync(a => a.Id == id);
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null) return null;
            return new Genre { Id= genres.Id, GenreName  = genres.GenreName, CreationTime = genres.CreationTime, LastUpdateTime = genres.LastUpdateTime };
        }

        public async Task<Genre> UpdateAsync(Genre entity)
        {
            var existingGenre = await _context.Genres.FindAsync(entity.Id);
            if (existingGenre == null) return null;
            existingGenre.Id= entity.Id;
            existingGenre.GenreName = entity.GenreName;
            _context.Genres.Update(existingGenre);
            await _context.SaveChangesAsync();
            return existingGenre;
        }
    }
}
