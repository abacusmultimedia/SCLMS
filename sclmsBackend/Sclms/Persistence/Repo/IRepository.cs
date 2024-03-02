public interface IRepository<T> where T : class
{
     IQueryable<T>  GetAllAsync();
    Task<T> GetByIdAsync(long id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
