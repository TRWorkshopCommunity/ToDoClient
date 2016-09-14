using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.DropBox;
using DAL.Interface.Entities;
using DAL.Interface.Repository;

namespace DAL.Repository
{
    public class DropBoxQueueRepository : IQueueRepository
    {
        private readonly DropBoxClient client = new DropBoxClient();
        private readonly JsonSerializer serializer = new JsonSerializer();

        public async Task<int> SetInQueueAsync(int userId, ToDoItemAction entity)
        {
            var json = await client.DownloadFileAsync(0);
            var items = serializer.Deserialize<ToDoItemAction>(json)?.ToList() ?? new List<ToDoItemAction>();
            if (entity.action == 2)
            {
                items.RemoveAll(e => e.item.Id == entity.item.Id);
            }
            if (entity.action == 1 && !items.Any(e => e.item.Id == entity.item.Id && e.action == 2))
            {
                items.RemoveAll(e => e.item.Id == entity.item.Id);
            }
            entity.Id = items.Count > 0 ? items.Max(e => e.Id) + 1 : 1;
            items.Add(entity);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(0, json);
            return entity.Id;
        }

        public async Task<ToDoItemAction> TakeFromQueueAsync(int userId, int id)
        {
            var json = await client.DownloadFileAsync(0);
            var items = serializer.Deserialize<ToDoItemAction>(json).ToList();
            var item = items.FirstOrDefault(e => e.item.Id == id);
            items.RemoveAll(e => item != null && e.Id == item.Id);
            json = serializer.Serialize(items);
            await client.UploadFileAsync(0, json);
            return item;
        }
    }
}
