using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class UserRole : IdentityRole<int>
{
    public User User { get; set; }
    public Role Role { get; set; }
}