using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.DataLayer.Models
{
    public class TodoItemList
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
