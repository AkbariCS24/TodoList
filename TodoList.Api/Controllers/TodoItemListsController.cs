using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.DTOs;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoItemListsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public TodoItemListsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItemLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemListDTO>>> GetTodoItemLists()
        {
            return await _repository.TodoItemList.Get();
        }

        // GET: api/TodoItemLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemListDTO>> GetTodoItemList(int id)
        {
            var todoItemListDTO = await _repository.TodoItemList.GetByID(id);

            if (todoItemListDTO == null)
            {
                return NotFound();
            }

            return todoItemListDTO;
        }

        // PUT: api/TodoItemLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemList(int id, TodoItemListDTO todoItemListDTO)
        {
            if (id != todoItemListDTO.ID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.TodoItemList.Update(todoItemListDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.TodoItemList.IsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTodoItemList", new { id = todoItemListDTO.ID }, todoItemListDTO);
        }

        // POST: api/TodoItemLists
        [HttpPost]
        public async Task<ActionResult> PostTodoItemList(TodoItemListDTO todoItemListDTO)
        {
            await _repository.TodoItemList.Add(todoItemListDTO);
            return CreatedAtAction("GetTodoItemList", new { id = todoItemListDTO.ID }, todoItemListDTO);
        }

        // DELETE: api/TodoItemLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemList(int id)
        {
            await _repository.TodoItemList.Delete(id);
            return NoContent();
        }
    }
}
