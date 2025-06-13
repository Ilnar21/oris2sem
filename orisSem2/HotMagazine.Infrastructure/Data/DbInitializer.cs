using HotMagazine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HotMagazine.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace HotMagazine.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(NewsPortalDbContext context)
        {
            context.Database.EnsureCreated();
        }
        
        /*public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Создаем пользователя админа
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser { UserName = adminEmail, Email = adminEmail };
                var createUser = await userManager.CreateAsync(adminUser, "Admin123!"); // пароль
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }*/


    }
}
