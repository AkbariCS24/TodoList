using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.DTOs;
using TodoList.DataLayer.Context;
using TodoList.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TodoList.BusinessLayer.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {

        private readonly TodoListContext _context;

        public TodoItemRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemDTO>> Get(int? ListID)
        {
            if (ListID != null)
            return await _context.TodoItems.Where(p => p.TodoItemListID == ListID).Select(p => ItemToDTO(p)).ToListAsync();
            else
            return await _context.TodoItems.Select(p => ItemToDTO(p)).ToListAsync();
        }

        public async Task<TodoItemDTO> GetByID(int ID)
        {
            var TodoItem = await _context.TodoItems.FindAsync(ID);
            if (TodoItem != null)
                return ItemToDTO(TodoItem);
            else
                return null;
        }

        public async Task<TodoItemDTO> Add(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem = new TodoItem()
                {
                    ID = todoItemDTO.ID,
                    TodoItemListID = todoItemDTO.TodoItemListID,
                    Title = todoItemDTO.Title,
                    IsComplete = todoItemDTO.IsComplete
                };
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                todoItemDTO.ID = todoItem.ID;
                return todoItemDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TodoItemDTO> Update(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem = _context.TodoItems.Find(todoItemDTO.ID);
                todoItem.TodoItemListID = todoItemDTO.TodoItemListID;
                todoItem.Title = todoItemDTO.Title;
                todoItem.IsComplete = todoItemDTO.IsComplete;

                _context.Entry(todoItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return todoItemDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(int ID)
        {
            try
            {
                var todoItem = await _context.TodoItems.FindAsync(ID);
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExists(int id)
        {
            try
            {
                if (_context.TodoItems.Any(e => e.ID == id))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            ID = todoItem.ID,
            Title = todoItem.Title,
            IsComplete = todoItem.IsComplete,
            TodoItemListID = todoItem.TodoItemListID
        };
    }
}
