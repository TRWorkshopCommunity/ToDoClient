using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int userId);

        Task DeleteAsync(int userId, int id);

        Task<int> CreateAsync(int userId, TEntity entity);

        Task UpdateAsync(int userId, TEntity entity);

    }
}
