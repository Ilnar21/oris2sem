using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using HotMagazine.Infrastructure.Data;

namespace HotMagazine.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Замените строку подключения на вашу из appsettings.json или secrets
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HotMagazineDb;Trusted_Connection=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}