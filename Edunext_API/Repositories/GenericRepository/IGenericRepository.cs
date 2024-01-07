namespace Edunext_API.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetEntities();
        Task<T> GetEntityByID(int id);
        Task InsertEntity(T entity);
        void DeleteEntity(int id);
        void UpdateEntity(T entity);
    }
}
