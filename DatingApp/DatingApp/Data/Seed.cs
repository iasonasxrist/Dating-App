using DatingApp.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DatingApp.Data
{
    public static class Seed
    {
        public static void SeedUSers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("./Data/UserSeedData.json");
                Console.WriteLine(userData);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new () {Name="Member"},
                    new (){Name="Admin"},
                    new (){Name="Moderator"},
                    new (){Name="VIP"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    userManager.CreateAsync(user, "password").Wait();
                    userManager.AddToRoleAsync(user, "Member");
                }

                var userAdmin = new User
                {
                    UserName = "Admin"
                };

                var result = userManager.CreateAsync(userAdmin, "password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
                }

            }
        }

    }

}