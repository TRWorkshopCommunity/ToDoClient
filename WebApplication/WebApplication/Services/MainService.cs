using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL.Interface.Entities;
using DAL.Interface.Repository;
using ToDoClient.Models;
using ToDoClient.Services;
using WebApplication.Infrastructure;
using WebApplication.Services.Interface;

namespace WebApplication.Services
{
    public class MainService
    {
        DropBoxService dbService;
        ToDoService tdService;
        IQueueRepository queueRepository;
        public MainService(DropBoxService dbService, ToDoService tdService)
        {
            this.dbService = dbService;
            this.tdService = tdService;
        }
        public async Task<int> Create(int userId, ToDoItemViewModel entity)
        {
            int dbResult = await dbService.Create(userId, entity.ToToDoItem());
            int tdResult = await tdService.Create(userId, entity);
            entity.ToDoId = tdResult;
            await UpdateToDoService(entity);
            return dbResult;
        }

        private async Task UpdateToDoService(ToDoItemViewModel model)
        {
            var item = await queueRepository.TakeFromQueueAsync(0, model.Id);
            if (item.action == 1)
            {
                await tdService.Update(model.UserId, model);
            }
            else
            {
                await tdService.Delete(model.UserId, model.ToDoId);
            }
        }

        public async Task Delete(int userId, ToDoItemViewModel entity)
        {
            if (entity.ToDoId != 0)
            {
                await tdService.Delete(userId, entity.ToDoId);
            }
            else
            {
                await queueRepository.SetInQueueAsync(0, new ToDoItemAction() {action = 2, item = entity.ToToDoItem()});           
            }
            await dbService.Delete(userId, entity.Id);
        }

        public async Task<IEnumerable<ToDoItemViewModel>> GetAll(int userId)
        {
            return (await tdService.GetAll(userId)).ToList();
        }

        public Task<ToDoItemViewModel> GetById(int userId, int id, int toDoId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int userId, ToDoItemViewModel entity)
        {
            if (entity.ToDoId != 0)
            {
                await tdService.Update(userId, entity);
            }
            else
            {
                await queueRepository.SetInQueueAsync(0, new ToDoItemAction() { action = 1, item = entity.ToToDoItem() });
            }
            await dbService.Update(userId, entity.ToToDoItem());
        }
    }
}