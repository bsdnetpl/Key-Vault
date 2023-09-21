using Key_Vault.Models;
using Key_Vault.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Key_Vault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult>AddUser(UserDto userDto)
        {
            if (userDto == null) 
            {
                return BadRequest();

            }
            await _userService.AddUser(userDto);
            return Ok();
        }

    }
}
