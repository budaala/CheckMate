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

            return Ok();
        }

        [HttpPut("/todo/update/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo todo)
        {
            todo.Id = id;
            var todoEntity = _todoDbContext.Todos.Find(todo.Id) ?? throw new Exception("Todo not found");

            todoEntity.Title = todo.Title;
            todoEntity.Subtitle = todo.Subtitle;
            todoEntity.IsCompleted = todo.IsCompleted;
            if (todo.IsCompleted)
            {
                todoEntity.CompletedDate = DateTime.Now;
            }
            else
            {
                todoEntity.CompletedDate = null;
            }

            try
            {
                await _todoDbContext.SaveChangesAsync();
                return Ok();
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
        }

        [HttpDelete("/todo/delete/{id}")]
        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            try
            {
                _todoDbContext.Todos.Remove(todo);
                await _todoDbContext.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        private bool TodoExists(int id)
        {
            return _todoDbContext.Todos.Any(e => e.Id == id);
        }
    }
}