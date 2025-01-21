using Microsoft.AspNetCore.Mvc;
using AlertManagement.Models;
using AlertManagement.Constants;
using AlertManagement.Services.Interfaces;

namespace FlightAlertSystem.Controllers
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

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                User createdUser = await _userService.CreateUser(user);
                if (createdUser == null)
                {
                    return StatusCode(500, ResponseMessage.Error);
                }
                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }

        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                User user = await _userService.GetUser(id);
                if (user == null)
                {
                    return NotFound(ResponseMessage.UserNotExists);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User updatedUser)
        {
            try
            {
                User user = await _userService.GetUser(updatedUser.Id);
                if (user == null)
                {
                    return NotFound(ResponseMessage.UserNotExists);
                }
                bool isUpdated = await _userService.UpdateUser(updatedUser);
                if (!isUpdated)
                {
                    return StatusCode(500, ResponseMessage.Error);
                }
                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User user = await _userService.GetUser(id);
                if (user == null)
                {
                    return NotFound(ResponseMessage.UserNotExists);
                }
                bool isDeleted = await _userService.DeleteUser(id);
                if (!isDeleted)
                {
                    return StatusCode(500, ResponseMessage.Error);
                }
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }
    }
}
