using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface.Entities;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public static class Mapper
    {
        public static ToDoItem ToToDoItem(this ToDoItemViewModel model)
        {
            return new ToDoItem()
            {
                Id = model.Id,
                IsCompleted = model.IsCompleted,
                Name = model.Name,
                ToDoId = model.ToDoId,
                UserId = model.UserId
            };
        }

        public static ToDoItemViewModel ToToDoItemViewModel(this ToDoItem item)
        {
            return new ToDoItemViewModel()
            {
                Id = item.Id,
                IsCompleted = item.IsCompleted,
                Name = item.Name,
                ToDoId = item.ToDoId,
                UserId = item.UserId
            };
        }
    }
}