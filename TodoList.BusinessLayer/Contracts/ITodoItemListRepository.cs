using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.BusinessLayer.DTOs;


namespace TodoList.BusinessLayer.Contracts
{
    public interface ITodoItemListRepository
    {
        Task<List<TodoItemListDTO>> Get();
        Task<TodoItemListDTO> GetByID(int ID);
        Task<TodoItemListDTO> Add(TodoItemListDTO todoItemList);
        Task<TodoItemListDTO> Update(TodoItemListDTO todoItemList);
        Task<bool> Delete(int ID);
        bool IsExists(int ID);
    }
}
