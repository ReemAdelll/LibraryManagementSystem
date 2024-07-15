namespace LibraryManagementSystem.Repositories
{
	
		public interface IBaseRepo<T>
		{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<bool> DeleteAsync(int id);
	}
	}

