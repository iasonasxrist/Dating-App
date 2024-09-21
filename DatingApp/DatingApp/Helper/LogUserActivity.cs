using System.Security.Claims;
using DatingApp.Repos;
using Microsoft.AspNetCore.Mvc.Filters;
namespace DatingApp.Helper;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        var userIdClaim = resultContext.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return;
        }
        
        var repo = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();
        var user = await repo.GetUser(userId, true);
        user.LastActive = DateTime.Now;
        await repo.SaveAll();
        
    }
}