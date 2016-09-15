using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using DAL.Interface.Repository;
using WebApplication.Services.Interface;

namespace WebApplication.Services
{
    public class DropBoxService : IDataService<ToDoItem>
    {
        private readonly IDropboxRepository<ToDoItem> repository;

        public DropBoxService()
        {
            repository = DependencyResolver.Current.GetService<IDropboxRepository<ToDoItem>>();
        }

        public async Task Load(int userId, IEnumerable<ToDoItem> items)
        {
            await repository.UploadItemsAsync(userId, items).ConfigureAwait(false);
        }

        public async Task<int> Create(int userId, ToDoItem entity)
        {
            return await repository.CreateAsync(userId, entity).ConfigureAwait(false);
        }

        public async Task Delete(int userId, ToDoItem entity)
        {
            await repository.DeleteAsync(userId, entity.Id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ToDoItem>> GetAll(int userId)
        {
            return await repository.GetAllAsync(userId).ConfigureAwait(false);
        }

        public async Task Update(int userId, ToDoItem entity)
        {
            await repository.UpdateAsync(userId, entity).ConfigureAwait(false);
        }
    }
}