using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ToDoClient.Services;
using WebApplication.Infrastructure;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController
    {
        private readonly UserService userService = new UserService();
        private readonly MainService service = new MainService();

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public async Task<IList<ToDoItemViewModel>> Get()
        {
            var userId = userService.GetOrCreateUser();
            var items = await service.GetAll(userId).ConfigureAwait(false);
            return items.ToList();
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public void Put(ToDoItemViewModel todo)
        {
            todo.UserId = userService.GetOrCreateUser();
            service.Update(todo.UserId, todo);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="model">The todo model.</param>
        public void Delete(ToDoItemViewModel model)
        {
            model.UserId = userService.GetOrCreateUser();
            service.Delete(model.UserId, model);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public void Post(ToDoItemViewModel todo)
        {       
            todo.UserId = userService.GetOrCreateUser();
            service.Create(todo.UserId, todo);
        }
    }
}
