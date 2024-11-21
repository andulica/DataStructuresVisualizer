using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorAppForDataStructures.Models;

namespace BlazorAppForDataStructures.Data
{
    public class BlazorAppForDataStructuresContext : IdentityDbContext<ApplicationUser>
    {
        public BlazorAppForDataStructuresContext(DbContextOptions<BlazorAppForDataStructuresContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
