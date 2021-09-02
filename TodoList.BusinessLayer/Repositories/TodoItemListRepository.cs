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
    public class TodoItemListRepository : ITodoItemListRepository
    {

        private readonly TodoListContext _context;

        public TodoItemListRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemListDTO>> Get()
        {
            return await _context.TodoItemLists.Include(p => p.TodoItems).Select(p => ItemToDTO(p)).ToListAsync();
        }

        public async Task<TodoItemListDTO> GetByID(int ID)
        {
            var TodoItemList = await _context.TodoItemLists.FindAsync(ID);
            if (TodoItemList != null)
                return ItemToDTO(TodoItemList);
            else
                return null;
        }

        public async Task<TodoItemListDTO> Add(TodoItemListDTO todoItemListDTO)
        {
            try
            {
                var todoItemList = new TodoItemList()
                {
                    ID = todoItemListDTO.ID,
                    Title = todoItemListDTO.Title,
                };
                _context.TodoItemLists.Add(todoItemList);
                await _context.SaveChangesAsync();
                todoItemListDTO.ID = todoItemList.ID;
                return todoItemListDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<TodoItemListDTO> Update(TodoItemListDTO todoItemListDTO)
        {
            try
            {
                var todoItemList = _context.TodoItemLists.Find(todoItemListDTO.ID);
                todoItemList.Title = todoItemListDTO.Title;
                _context.Entry(todoItemList).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return todoItemListDTO;
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
                var todoItemList = await _context.TodoItemLists.FindAsync(ID);
                _context.TodoItemLists.Remove(todoItemList);
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
                if (_context.TodoItemLists.Any(e => e.ID == id))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static TodoItemListDTO ItemToDTO(TodoItemList todoItemList) =>
        new TodoItemListDTO
        {
            ID = todoItemList.ID,
            Title = todoItemList.Title,
        };
    }
}
