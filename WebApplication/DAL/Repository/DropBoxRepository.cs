using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DropBox;
using DAL.Interface.Entities;
using DAL.Interface.Repository;

namespace DAL.Repository
{
    public class DropBoxRepository : IRepository<ToDoItem>, IDisposable
    {
        private readonly DropBoxLoader loader = new DropBoxLoader();

        public int Create(int userId, ToDoItem entity)
        {
            return CreateAsync(userId, entity).Result;
        }

        public async Task<int> CreateAsync(int userId, ToDoItem entity)
        {
            return await loader.CreateAsync(userId, entity);
        }

        public void Delete(int userId, int id)
        {
            DeleteAsync(userId, id).RunSynchronously();
        }

        public async Task DeleteAsync(int userId, int id)
        {
            await loader.DeleteAsync(userId, id);
        }

        public IEnumerable<ToDoItem> GetAll(int userId)
        {
            return GetAllAsync(userId).Result;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(int userId)
        {
            return await loader.GetAllAsync(userId);
        }

        public ToDoItem GetById(int userId, int id)
        {
            return GetByIdAsync(userId, id).Result;
        }

        public async Task<ToDoItem> GetByIdAsync(int userId, int id)
        {
            return await loader.GetByIdAsync(userId, id);
        }


        public void Update(int userId, ToDoItem entity)
        {
            UpdateAsync(userId,entity).RunSynchronously();
        }

        public async Task UpdateAsync(int userId, ToDoItem entity)
        {
            await loader.UpdateAsync(userId, entity);
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    loader.Dispose();
                }

                disposedValue = true;
            }
        }

        ~DropBoxRepository()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
