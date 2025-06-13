using HotMagazine.Domain.Models;
using HotMagazine.Infrastructure.Identity; // где AppUser
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotMagazine.Infrastructure.Data
{
    public class NewsPortalDbContext : IdentityDbContext<AppUser>
    {
        public NewsPortalDbContext(DbContextOptions<NewsPortalDbContext> options) 
            : base(options) { }

        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}