using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using static LibraryManagementSystem.Repositories.IGenericRepo;

namespace LibraryManagementSystem.Repositories
{
	public class GenericRepo<T> : IGenericRepo<T> where T : class
	{
		protected readonly LibraryContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepo(LibraryContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}
	}
}
