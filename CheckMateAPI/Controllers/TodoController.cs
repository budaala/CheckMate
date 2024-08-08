using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CheckMateAPI.Models;
using CheckMateAPI.Data;


namespace CheckMateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoController(TodoDbContext context)
        {
            _todoDbContext = context;
        }

        [HttpGet("/todo/getAll/")]
        public async Task<ActionResult> GetTodos()
        {
            var todos = await _todoDbContext.Todos.ToListAsync();

            return Ok(todos);
        }

        [HttpGet("/todo/get/{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPost("/todo/add/")]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
        {
            // todo.Id = Guid.NewGuid();

            var maxId = await _todoDbContext.Todos.MaxAsync(t => (int?)t.Id) ?? 0;
            todo.Id = maxId + 1;

            todo.CreatedAt = DateTime.Now;
            todo.IsCompleted = false;

            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("/todo/update/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _todoDbContext.Entry(todo).State = EntityState.Modified;

            try
            {
                await _todoDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("/todo/delete/{id}")]
        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _todoDbContext.Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _todoDbContext.Todos.Any(e => e.Id == id);
        }
    }
}