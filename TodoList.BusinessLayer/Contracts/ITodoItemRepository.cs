using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.BusinessLayer.DTOs;

namespace TodoList.BusinessLayer.Contracts
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItemDTO>> Get(int? ListID);
        Task<TodoItemDTO> GetByID(int ID);
        Task<TodoItemDTO> Add(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> Update(TodoItemDTO todoItemDTO);
        Task<bool> Delete(int ID);
        bool IsExists(int ID);
    }
}
