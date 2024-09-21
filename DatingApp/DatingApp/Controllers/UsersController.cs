using System.Security.Claims;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Helper;
using DatingApp.Helper;
using DatingApp.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{

    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;


        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                var user = await _repo.GetUser(currentUserId, true);
                userParams.Gender = user.Gender == "male" ? "male" : "female" ;
            }

            var users = await _repo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
            
            // Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }
        


    }
}