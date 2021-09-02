using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public TodoItemsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(int? ListID)
        {
            return await _repository.TodoItem.Get(ListID);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItemDTO = await _repository.TodoItem.GetByID(id);

            if (todoItemDTO == null)
            {
                return NotFound();
            }

            return todoItemDTO;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.ID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.TodoItem.Update(todoItemDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.TodoItem.IsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTodoItem", new { id = todoItemDTO.ID }, todoItemDTO);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            await _repository.TodoItem.Add(todoItemDTO);
            return CreatedAtAction("GetTodoItem", new { id = todoItemDTO.ID }, todoItemDTO);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _repository.TodoItem.Delete(id);
            return NoContent();
        }
    }
}
