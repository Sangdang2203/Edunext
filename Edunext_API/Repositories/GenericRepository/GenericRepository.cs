using Edunext_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext databaseContext;

        public GenericRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<T>> GetEntities()
        {
            return await databaseContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityByID(int id)
        {
            return await databaseContext.Set<T>().FindAsync(id);
        }
        public async Task InsertEntity(T entity)
        {
            await databaseContext.Set<T>().AddAsync(entity);
        }
        public void UpdateEntity(T entity)
        {
            databaseContext.Entry(entity).State = EntityState.Modified;
        }

        public async void DeleteEntity(int id)
        {
            T entity = await databaseContext.Set<T>().FindAsync(id);
            databaseContext.Set<T>().Remove(entity);
        }
    }
}
