using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.DataLayer.Models
{
    public class TodoItem
    {
        public int ID { get; set; }
        public int TodoItemListID { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public TodoItemList TodoItemList { get; set; }
    }
}
