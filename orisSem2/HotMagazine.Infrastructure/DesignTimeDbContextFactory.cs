using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HotMagazine.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NewsPortalDbContext>
    {
        public NewsPortalDbContext CreateDbContext(string[] args)
        {
            // Путь к папке с веб-проектом, где лежит appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "HotMagazine.WebUI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<NewsPortalDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection"); // или как у тебя называется строка подключения

            optionsBuilder.UseNpgsql(connectionString);

            return new NewsPortalDbContext(optionsBuilder.Options);
        }
    }
}