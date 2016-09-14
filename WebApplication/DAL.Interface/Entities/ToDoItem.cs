using System;

namespace DAL.Interface.Entities
{
    public class ToDoItem: IEntity
    {
        /// <summary>
        /// Gets or sets to do identifier.
        /// </summary>
        /// <value>
        /// To do identifier.
        /// </value>
        public int Id { get; set; }

        public int ToDoId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this to do-item is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this to do-item is completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the name (description) of to do-item.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var castObj = obj as ToDoItem;
            if (castObj == null)
                return false;
            return (((ToDoItem)obj).Name == Name)
                   && (((ToDoItem)obj).UserId == UserId);
        }

        public override int GetHashCode()
        {
            return $"{Name} {UserId}".GetHashCode();
        }

        public bool Equals(ToDoItem other)
        {
            return Name == other.Name && UserId == other.UserId;
        }
    }
}
