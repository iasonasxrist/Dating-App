using Microsoft.AspNetCore.Identity;

namespace DatingApp.Models;
public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
    
}