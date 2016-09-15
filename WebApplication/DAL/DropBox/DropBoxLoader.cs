using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Interface.Entities;
using DAL.Interface.Repository;

namespace DAL.DropBox
{
    public class DropBoxLoader : IDropboxRepository<ToDoItem>, IDisposable
    {
        private readonly DropBoxClient client = new DropBoxClient();
        private readonly JsonSerializer serializer = new JsonSerializer();

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(int userId)
        {
            var json = await client.DownloadFileAsync(userId).ConfigureAwait(false);
            var items = serializer.Deserialize<ToDoItem>(json);
            return items;
        }

        public async Task DeleteAsync(int userId, int id)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json).ToList();
            items.RemoveAll(e => e.Id == id);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json).ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(int userId, ToDoItem entity)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json)?.ToList() ?? new List<ToDoItem>();
            items.Add(entity);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json).ConfigureAwait(false);
            return entity.Id;
        }

        public async Task UpdateAsync(int userId, ToDoItem entity)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json).ToList();
            var index = items.FindIndex(e => e.Id == entity.Id);
            items[index] = entity;
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json).ConfigureAwait(false);
        }

        public async Task UploadItemsAsync(int userId, IEnumerable<ToDoItem> items)
        {
            string json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json).ConfigureAwait(false);
        }

        #region IDisposable Support

        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                }
                disposedValue = true;
            }
        }

        ~DropBoxLoader()
        {
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
