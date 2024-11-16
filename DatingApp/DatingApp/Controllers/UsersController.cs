using System.Security.Claims;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Helper;
using DatingApp.Dtos;
using DatingApp.Repos;
using DatingApp.Helper;
using DatingApp.Models;
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
                userParams.Gender = user.Gender == "male" ? "male" : "female";
            }

            var users = await _repo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);

            // Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }


        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            // var user = await _repo.GetUser(id);
            // var usersToReturn = await _mapper.Map<IEnumerable<UserForDetailedDto>>(user);
            // return Ok(usersToReturn);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {

            // if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            //
            // User userFromRepo = await _repo.GetUser(id);
            // _mapper.Map(userUpdateDto, userFromRepo);
            //
            // if (await _repo.SaveAll()) return NoContent();
            throw new Exception($"Updating user {id} failed on save");
        }
        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            // var like = await _repo.GetLike(id, recipientId);
            //
            // if (like != null) return BadRequest("You are already like the user");
            //
            // if (await _repo.GetUser(recipientId) == null) return NotFound();
            //
            //
            // like = new Like()
            // {
            //     LikerId = id,
            //     LikeeId = recipientId
            // };
            //
            // _repo.Add<Like>(like);
            //
            // if (await _repo.SaveAll()) return NoContent();


            return BadRequest("Failed to like user");

        }
    }
}