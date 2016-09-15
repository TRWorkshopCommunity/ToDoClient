using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int userId);

        Task<IEnumerable<TEntity>> DeleteAsync(int userId, int id);

        Task<IEnumerable<TEntity>> CreateAsync(int userId, TEntity entity);

        Task<IEnumerable<TEntity>> UpdateAsync(int userId, TEntity entity);

    }
}
