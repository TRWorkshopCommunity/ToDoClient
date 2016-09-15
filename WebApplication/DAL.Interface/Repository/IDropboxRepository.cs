using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repository
{
    public interface IDropboxRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        Task UploadItemsAsync(int userId, IEnumerable<ToDoItem> items);
    }
}
