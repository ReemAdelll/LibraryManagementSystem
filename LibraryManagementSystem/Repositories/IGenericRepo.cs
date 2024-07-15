namespace LibraryManagementSystem.Repositories
{
	public interface IGenericRepo
	{
		public interface IGenericRepo<T> where T : class
		{
			Task<IEnumerable<T>> GetAllAsync();
			Task<T> GetByIdAsync(int id);
			Task AddAsync(T entity);
			void Update(T entity);
			void Remove(T entity);
		}
	}
}
