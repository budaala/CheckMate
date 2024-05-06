using Microsoft.EntityFrameworkCore;
using CheckMateAPI.Models;

namespace CheckMateAPI.Data {
    public class TodoDbContext : DbContext {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        
        public DbSet<Todo> Todos { get; set; }
    }

}