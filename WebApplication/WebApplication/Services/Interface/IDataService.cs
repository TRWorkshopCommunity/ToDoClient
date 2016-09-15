using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace WebApplication.Services.Interface
{
    public interface IDataService<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> Create(int userId, TEntity entity);
        Task<IEnumerable<TEntity>> Update(int userId, TEntity entity);
        Task<IEnumerable<TEntity>> Delete(int userId, TEntity entity);
        Task<IEnumerable<TEntity>> GetAll(int userId);

    }
}
