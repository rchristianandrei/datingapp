using api.DTOs;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IUsersRepo user) : ControllerBase
    {
        public IUsersRepo usersRepo { get; } = user;

        #region POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            await this.usersRepo.SaveAsync(user);
            return StatusCode(200, "Successfully created user");
        }
        #endregion

        #region Update
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO userDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username?.ToLower() != userDto.Username.ToLower()) return Unauthorized();

            await this.usersRepo.SaveAsync(userDto);
            return StatusCode(204, "Updated successfully");
        }
        #endregion

        #region Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            return StatusCode(200, await this.usersRepo.GetAsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDTO>> Get(string username)
        {
            return StatusCode(200, await this.usersRepo.GetbyUsernameAsync(username));
        }
        #endregion
    }
}
