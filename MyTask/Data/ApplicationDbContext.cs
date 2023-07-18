using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace MyTask.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> ticket { get; set; } 
    }
}
