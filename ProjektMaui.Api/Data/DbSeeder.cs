using Microsoft.EntityFrameworkCore;
using ProjektMaui.Api.Models;

namespace ProjektMaui.Api.Data
{
    public static class DbSeeder
    {
        public static void SeedAdminUser(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate(); // upewnij się, że baza istnieje

            if (!context.Users.Any(u => u.Role == UserRole.Admin))
            {
                var admin = new User
                {
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), // zabezpieczone hasło
                    FirstName = "Admin",
                    LastName = "Systemowy",
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
