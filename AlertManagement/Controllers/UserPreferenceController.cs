using Microsoft.AspNetCore.Mvc;
using AlertManagement.Services.Interfaces;
using AlertManagement.Models;
using AlertManagement.Constants;

namespace FlightAlertSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferenceService _userPreferencesService;

        public UserPreferencesController(IUserPreferenceService userPreferencesService)
        {
            _userPreferencesService = userPreferencesService;
        }

        [HttpPost("createUserPreference")]
        public async Task<IActionResult> CreateUserPreference([FromBody] UserPreference preference)
        {
            try
            {
                UserPreference userPreference = await _userPreferencesService.CreateUserPreference(preference);
                if (userPreference == null)
                {
                    return StatusCode(500, ResponseMessage.Error);
                }
                return CreatedAtAction(nameof(CreateUserPreference), new { id = userPreference.Id }, userPreference);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }

        [HttpGet("getUserPreference/{id}")]
        public async Task<IActionResult> GetUserPreference(int id)
        {
            try
            {
                UserPreference userPreference = await _userPreferencesService.GetUserPreference(id);
                if (userPreference == null)
                {
                    return NotFound(ResponseMessage.UserPreferenceNotExists);
                }
                return Ok(userPreference);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }


        [HttpGet("getAllUserPreferences/{userId}")]
        public async Task<IActionResult> GetAllUserPreferences(int userId)
        {
            try
            {
                List<UserPreference> userPreferences = await _userPreferencesService.GetAllUserPreferences(userId);
                if (userPreferences == null)
                {
                    return NotFound(ResponseMessage.UserPreferenceNotExists);
                }
                return Ok(userPreferences);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseMessage.InternalServerError + ex.Message);
            }
        }


        [HttpPut("updateUserPreferences")]
        public async Task<IActionResult> UpdateUserPreferences([FromBody] UserPreference updatedPreference)
        {
            try
            {
                bool isUpdated = await _userPreferencesService.UpdateUserPreference(updatedPreference);
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
        [HttpDelete("deleteUserPreference/{id}")]
        public async Task<IActionResult> DeleteUserPreference(int id)
        {
            try
            {
                UserPreference preference = await _userPreferencesService.GetUserPreference(id);
                if (preference == null)
                {
                    return NotFound(ResponseMessage.UserPreferenceNotExists);
                }
                bool isDeleted = await _userPreferencesService.DeleteUserPreference(id);
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
