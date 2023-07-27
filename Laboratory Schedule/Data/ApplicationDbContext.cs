using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Laboratory_Schedule.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Laboratory_Schedule.Models.Request>? Request { get; set; }
        public DbSet<Laboratory_Schedule.Models.Mangement>? Mangement { get; set; }
    }
}