using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using share.Models.User;

namespace ElinorStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize()]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _userService.GetAsync(id);
            if (result == null)
            {
                return NotFound("کاربر پیدا نشد!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _userService.GetsAsync();
            return Ok(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddRequestDto user)
        {
            await _userService.AddAsync(user);

            string username = user.Name;

            return Ok($" عزیز خوش آمدی{username} ");


        }


        [Authorize()]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] AppUser user)
        {
            await _userService.EditAsync(user);
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);

            return Ok("خداحافظ...");
        }
    }
}