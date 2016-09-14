using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using DAL.Interface.Entities;
using DAL.Interface.Repository;
using ToDoClient.Models;
using ToDoClient.Services;
using WebApplication.Infrastructure;
using WebApplication.Services;

namespace ToDoClient.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController
    {
        private readonly ToDoService todoService = new ToDoService();
        private readonly UserService userService = new UserService();

        private readonly IRepository<ToDoItem> repository;
        private readonly MainService service = new MainService(new DropBoxService(), new ToDoService());
        public ToDosController()
        {
            repository = DependencyResolver.Current.GetService<IRepository<ToDoItem>>();
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public async Task<IList<ToDoItemViewModel>> Get()
        {
            var userId = userService.GetOrCreateUser();
            //return (await todoService.GetAll(userId)).ToList();
            return (await service.GetAll(userId)).ToList();
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public async void Put(ToDoItemViewModel todo)
        {
            todo.UserId = userService.GetOrCreateUser();
            //await todoService.Update(todo.UserId, todo);
            await service.Update(todo.UserId, todo);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public async void Delete(ToDoItemViewModel model)
        {
            var userId = userService.GetOrCreateUser();
            //await todoService.Delete(userId, id);
            await service.Delete(model.UserId, model);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public async void Post(ToDoItemViewModel todo)
        {
            
            todo.Id = IdGenerator.Instance.GenerateId();
            todo.UserId = userService.GetOrCreateUser();
            await service.Create(todo.UserId, todo);
        }
    }
}
