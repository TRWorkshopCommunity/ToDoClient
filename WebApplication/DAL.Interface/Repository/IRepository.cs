using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity GetById(int userId, int id);

        IEnumerable<TEntity> GetAll(int userId);

        void Delete(int userID, int id);

        int Create(int userId, TEntity entity);

        Task<TEntity> GetByIdAsync(int userId, int id);

        Task<IEnumerable<TEntity>> GetAllAsync(int userId);

        Task DeleteAsync(int userId, int id);

        Task<int> CreateAsync(int userId, TEntity entity);

        void Update(int userId, TEntity entity);
        Task UpdateAsync(int userId, TEntity entity);

    }
}
