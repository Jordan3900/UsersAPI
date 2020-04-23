using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Data.Contracts
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
