
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using CheckMateAPI.Models;
using CheckMateAPI.Data;


namespace CheckMateAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class TodoController : ControllerBase {
        private readonly TodoDbContext _todoDbContext;

        public TodoController(TodoDbContext context) {
            _todoDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetTodos() {
            var todos = await _todoDbContext.Todos.ToListAsync();

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(Guid id) {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null) {
                return NotFound();
            }

            return todo;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo) {
            todo.Id = Guid.NewGuid();
            todo.CreatedAt = DateTime.Now;
            todo.IsCompleted = false;

            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, Todo todo) {
            if (id != todo.Id) {
                return BadRequest();
            }

            _todoDbContext.Entry(todo).State = EntityState.Modified;

            try {
                await _todoDbContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!TodoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoById(Guid id) {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null) {
                return NotFound();
            }

            _todoDbContext.Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(Guid id) {
            return _todoDbContext.Todos.Any(e => e.Id == id);
        }
    }
}
