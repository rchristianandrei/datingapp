using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace api.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IAppUsersRepo usersRepo) : ControllerBase
    {
        private readonly IAppUsersRepo _usersRepo = usersRepo;

        #region POST
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO regDto)
        {
            try
            {
                using var hmac = new HMACSHA512();
                var user = new AppUser() { 
                    Username = (regDto.Username ?? "").ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regDto.Password ?? "")),
                    PasswordSalt = hmac.Key
                };

                // Check if username exists first
                var exists = await this._usersRepo.UsernameExists(user.Username);
                if (exists) return StatusCode(409, "The username is already taken");

                // Save User
                await this._usersRepo.Insert(user);
                return StatusCode(200,"successfully registered");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, "Something went wrong. Please try again later");
            }
        }
        #endregion

        #region PUT
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        #endregion

        #region GET
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
