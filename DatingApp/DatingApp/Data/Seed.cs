using DatingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly UserManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, UserManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            throw  new NotImplementedException();
        }
    }
}