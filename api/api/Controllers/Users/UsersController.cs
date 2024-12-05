using api.DTOs;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
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
