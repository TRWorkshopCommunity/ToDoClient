using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class ToDoItemAction : IEntity
    {
        public int Id { get; set; }
        public ToDoItem item;
        public int action; //1-update,2-delete
    }
}
