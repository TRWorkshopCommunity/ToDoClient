using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Interface.Entities;
using DAL.Interface.Repository;

namespace DAL.DropBox
{
    public class DropBoxLoader : IRepository<ToDoItem>, IDisposable
    {
        private readonly DropBoxClient client = new DropBoxClient();
        private readonly JsonSerializer serializer = new JsonSerializer();

        public ToDoItem GetById(int userId, int id)
        {
            return GetByIdAsync(userId, id).Result;
        }

        public IEnumerable<ToDoItem> GetAll(int userId)
        {
            return GetAllAsync(userId).Result;
        }

        public void Delete(int userId, int id)
        {
            DeleteAsync(userId, id).RunSynchronously();
        }

        public int Create(int userId, ToDoItem entity)
        {
            return CreateAsync(userId, entity).Result;
        }

        public async Task<ToDoItem> GetByIdAsync(int userId, int id)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json) ?? new ToDoItem[0];
            return items.FirstOrDefault(e => e.Id == id);
        }


        public async Task<IEnumerable<ToDoItem>> GetAllAsync(int userId)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json);
            return items;
        }

        public async Task DeleteAsync(int userId, int id)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json).ToList();
            items.RemoveAll(e => e.Id == id);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json);
        }

        public async Task<int> CreateAsync(int userId, ToDoItem entity)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json)?.ToList() ?? new List<ToDoItem>();
            items.Add(entity);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json);
            return entity.Id;
        }


        public void Update(int userId, ToDoItem entity)
        {
            UpdateAsync(userId,entity).RunSynchronously();
        }

        public async Task UpdateAsync(int userId, ToDoItem entity)
        {
            var json = await client.DownloadFileAsync(userId);
            var items = serializer.Deserialize<ToDoItem>(json).ToList();
            var index = items.FindIndex(e => e.Id == entity.Id);
            items[index] = entity;
            json = serializer.Serialize(items);
            await client.UploadFileAsync(userId, json);
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
