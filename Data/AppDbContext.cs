using Contacts.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Contacts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) {  }

        public DbSet<Person> People { get; set; }
    }
}