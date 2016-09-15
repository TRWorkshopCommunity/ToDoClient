using System;
using System.Collections.Generic;
using System.Linq;

using WebApplication.Services.Interface;
using WebApplication.Models;
using System.Threading.Tasks;
using WebApplication.Infrastructure;

namespace WebApplication.Services
{
    public class MainService : IDataService<ToDoItemViewModel>
    {
        private readonly AzureService azure = new AzureService();
        private readonly DropBoxService dropbox = new DropBoxService();
        private readonly SyncStateRepository syncRepository = SyncStateRepository.Instance;
        private readonly IdGenerator generator = IdGenerator.Instance;

        public async Task<int> Create(int userId, ToDoItemViewModel entity)
        {
            entity.Id = generator.GenerateId();
            entity.UserId = userId;
            await dropbox.Create(userId, entity.ToToDoItem()).ConfigureAwait(false);
            entity.ToDoId = await azure.Create(userId, entity).ConfigureAwait(false);
            await UpdateAzureCreatedItem(entity).ConfigureAwait(false);
            return entity.Id;
        }

        private async Task UpdateAzureCreatedItem(ToDoItemViewModel model)
        {
            var item = (await dropbox.GetAll(model.UserId).ConfigureAwait(false)).FirstOrDefault(e => e.Id == model.Id)?.ToToDoItemViewModel();
            if (item != null)
            {
                item.ToDoId = model.ToDoId;
                if (!item.Equals(model))
                    await azure.Update(model.UserId, model).ConfigureAwait(false);
            }
            else
            {
                await azure.Delete(model.UserId, model).ConfigureAwait(false);
            }
        }

        public async Task Delete(int userId, ToDoItemViewModel entity)
        {
            await dropbox.Delete(userId, entity.ToToDoItem()).ConfigureAwait(false);
            if (entity.ToDoId > 0)
                await azure.Delete(userId, entity).ConfigureAwait(false);
        }

        public async Task Update(int userId, ToDoItemViewModel entity)
        {
            await dropbox.Update(userId, entity.ToToDoItem()).ConfigureAwait(false);
            if (entity.ToDoId > 0)
                await azure.Update(userId, entity).ConfigureAwait(false);
        }

        #region GetAll Method
        public async Task<IEnumerable<ToDoItemViewModel>> GetAll(int userId)
        {
            if (syncRepository.IsSynchronized(userId))
            {
                var items = await dropbox.GetAll(userId).ConfigureAwait(false);
                return items.Select(e => e.ToToDoItemViewModel());
            }
            return await SyncStorages(userId).ConfigureAwait(false);
        }

        private async Task<IEnumerable<ToDoItemViewModel>> SyncStorages(int userId)
        {
            var items = await azure.GetAll(userId);
            var list = items.ToList();
            foreach (var item in list)
            {
                item.Id = generator.GenerateId();
            }
            await dropbox.Load(userId, list.Select(e => e.ToToDoItem())).ConfigureAwait(false);
            syncRepository.Add(new SyncState() {Sync = true, UserId = userId});
            return items;
        }
        #endregion 
    }
}