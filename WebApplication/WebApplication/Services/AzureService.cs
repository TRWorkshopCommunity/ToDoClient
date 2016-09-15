using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication.Models;
using WebApplication.Services.Interface;

namespace WebApplication.Services
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class AzureService
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";

        /// <summary>
        /// The url for updating a todo.
        /// </summary>
        private const string UpdateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's deletion.
        /// </summary>
        private const string DeleteUrl = "ToDos/{0}";

        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public AzureService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public async Task<IEnumerable<ToDoItemViewModel>> GetAll(int userId)
        {
            var dataAsString =
                await httpClient.GetStringAsync(string.Format(serviceApiUrl + GetAllUrl, userId)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<ToDoItemViewModel>>(dataAsString);
        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public async Task<int> Create(int userId, ToDoItemViewModel item)
        {
            var result = await httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, item).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
            return item.ToDoId;
        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public async Task Update(int userId, ToDoItemViewModel item)
        {
            var response = await httpClient.PutAsJsonAsync(serviceApiUrl + UpdateUrl, item).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public async Task Delete(int userId, ToDoItemViewModel item)
        {
            var response =
                await
                    httpClient.DeleteAsync(string.Format(serviceApiUrl + DeleteUrl, item.ToDoId)).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}