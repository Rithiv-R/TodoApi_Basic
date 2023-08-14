using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class TodoDbContext : DbContext
    {

        public TodoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }
}
