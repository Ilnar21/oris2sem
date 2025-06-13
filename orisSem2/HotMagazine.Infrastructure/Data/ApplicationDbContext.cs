using HotMagazine.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotMagazine.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<News> News { get; set; }
    }
}