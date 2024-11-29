using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.AppUsers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController(IAppUsersRepo appUserRepo) : ControllerBase
    {
        private readonly IAppUsersRepo _appUserRepo = appUserRepo;

        #region Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppUser appuser)
        {
            await _appUserRepo.Insert(appuser);
            return StatusCode(201, new { message = "successfully created appuser" });
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser?>>> Get()
        {
            var list = await _appUserRepo.Get1000();
            return list;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser?>> Get(int id)
        {
            var user = await _appUserRepo.Get1(id);
            return user;
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appUserRepo.Delete(id);
            return StatusCode(200, new { message = "successfully deleted user" });
        }
        #endregion
    }
}
