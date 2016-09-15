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
    public class DropBoxRepository : IDropboxRepository<ToDoItem>, IDisposable
    {
        private readonly DropBoxLoader loader = new DropBoxLoader();

        public async Task<IEnumerable<ToDoItem>> CreateAsync(int userId, ToDoItem entity)
        {
            return await loader.CreateAsync(userId, entity).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ToDoItem>> DeleteAsync(int userId, int id)
        {
            return await loader.DeleteAsync(userId, id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(int userId)
        {
            return await loader.GetAllAsync(userId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ToDoItem>> UpdateAsync(int userId, ToDoItem entity)
        {
            return await loader.UpdateAsync(userId, entity).ConfigureAwait(false);
        }


        public async Task UploadItemsAsync(int userId, IEnumerable<ToDoItem> items)
        {
            await loader.UploadItemsAsync(userId, items).ConfigureAwait(false);
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
