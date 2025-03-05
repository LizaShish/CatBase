using CatBase.DTOs;
using CatBase.Models;
using Microsoft.EntityFrameworkCore;

namespace CatBase
{
    public class AppDBContext: DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
