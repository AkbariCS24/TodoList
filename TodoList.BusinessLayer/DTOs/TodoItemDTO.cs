using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.BusinessLayer.DTOs
{
    public class TodoItemDTO
    {
        public int ID { get; set; }
        public int TodoItemListID { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
