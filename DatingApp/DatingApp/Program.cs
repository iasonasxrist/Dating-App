
using DatingApp;
using DatingApp.Data;
using DatingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try {
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                context.Database.Migrate();
                Seed.SeedUSers(userManager, roleManager);
                    
            }
            catch(Exception ex) {
                // var logger = services.GetRequiredService<ILogger>();
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Error occurred during migration -");
            }
        } 
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}