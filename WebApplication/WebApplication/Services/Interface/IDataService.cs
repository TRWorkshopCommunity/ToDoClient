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
        Task<int> Create(int userId, TEntity entity);
        Task Update(int userId, TEntity entity);
        Task Delete(int userId, TEntity entity);
        Task<IEnumerable<TEntity>> GetAll(int userId);

    }
}
