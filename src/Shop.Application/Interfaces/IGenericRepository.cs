namespace Shop.Application.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> GetQueryable();

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();
    }
}