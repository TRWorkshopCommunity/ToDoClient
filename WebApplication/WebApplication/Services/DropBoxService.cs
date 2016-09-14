using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL.Interface.Entities;
using DAL.Interface.Repository;
using WebApplication.Services.Interface;

namespace WebApplication.Services
{
    public class DropBoxService : IDataService<ToDoItem>
    {
        IRepository<ToDoItem> repository; 
        public DropBoxService()
        {
            repository = DependencyResolver.Current.GetService<IRepository<ToDoItem>>();
        }
        public async Task<int> Create(int userId, ToDoItem entity)
        {
            return await repository.CreateAsync(userId, entity);
        }

        public async Task Delete(int userId, int id)
        {
            await repository.DeleteAsync(userId, id);
        }

        public async Task<IEnumerable<ToDoItem>> GetAll(int userId)
        {
            return await repository.GetAllAsync(userId);
        }

        public Task<ToDoItem> GetById(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int userId, ToDoItem entity)
        {
            await repository.UpdateAsync(userId, entity);
        }
    }
}