using System;
using DAL.Interface.Entities;

namespace WebApplication.Models
{
    public class ToDoItemViewModel : IEntity, IEquatable<ToDoItemViewModel>
    {
        public int ToDoId { get; set; }

        public int UserId { get; set; }

        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var castObj = obj as ToDoItemViewModel;
            if (castObj == null)
                return false;
            return Equals(castObj);
        }

        public bool Equals(ToDoItemViewModel other)
        {
            if (other == null)
                return false;
            return (this.Id == other.Id) && (this.ToDoId == other.ToDoId)
                   && (this.UserId == other.UserId) && (this.IsCompleted == other.IsCompleted) &&
                   (this.Name == other.Name);
        }

        public override int GetHashCode()
        {
            return $"{Name} {UserId}".GetHashCode();
        }
        
    }
}