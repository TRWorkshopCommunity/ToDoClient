using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Repository
{
    public interface IQueueRepository
    {
        Task<int> SetInQueueAsync(int userId, ToDoItemAction entity);
        Task<ToDoItemAction> TakeFromQueueAsync(int userId, int id);
    }
}
