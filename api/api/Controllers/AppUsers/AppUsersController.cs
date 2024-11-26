using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers.AppUsers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController(IAppUserRepo appUserRepo) : ControllerBase
    {
        private readonly IAppUserRepo _appUserRepo = appUserRepo;

        #region Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppUser appuser)
        {
            try
            {
                await this._appUserRepo.Save(appuser);

                return StatusCode(201, new { message = "successfully created appuser" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

                return StatusCode(500, new {error = "failed to create appuser"});
            }
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
        public async Task<ActionResult<IEnumerable<AppUser?>>> Get()
        {
            try
            {
                var list = await this._appUserRepo.Get1000();

                return StatusCode(200, list);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, new {error = "failed to get appusers"}); ;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser?>> Get(int id)
        {
            try
            {
                var user = await this._appUserRepo.Get1(id);

                if (user == null)
                    return StatusCode(400, new { error = "failed to get user" });

                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, new { error = "failed to get user" });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this._appUserRepo.Delete(id);

                return StatusCode(200, new { message = "successfully deleted user" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, new { message = "failed to delete user" });
            }
        }
        #endregion
    }
}
