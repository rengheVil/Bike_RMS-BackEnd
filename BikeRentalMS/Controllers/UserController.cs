using BikeRentalMS.Models;
using BikeRentalMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BikeRentalMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

            public UserController(UserService userService)
            {
                _userService = userService;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid login request.");
                }

                try
                {
                    var user = await _userService.LoginAsync(request.Username, request.Password, request.Role);
                    if (user == null)
                    {
                        return Unauthorized("Invalid username, password, or role.");
                    }

                    return Ok(user);
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            //[HttpPost("register")]
            //public async Task<IActionResult> Register([FromBody] User user)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest("Invalid registration details.");
            //    }

            //    try
            //    {
            //        bool isRegistered = await _userService.RegisterAsync(user);
            //        if (isRegistered)
            //        {
            //            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
            //        }

            //        return Conflict("User registration failed. Username may already exist.");
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log exception here
            //        return StatusCode(500, $"Internal server error: {ex.Message}");
            //    }
            //}

            [HttpGet("{userId}")]
            public async Task<IActionResult> GetUserById(int userId)
            {
                try
                {
                    var user = await _userService.GetUserByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound("User not found.");
                    }

                    return Ok(user);
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPut]
            public async Task<IActionResult> UpdateUser([FromBody] User user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid user details.");
                }

                try
                {
                    bool isUpdated = await _userService.UpdateUserAsync(user);
                    if (isUpdated)
                    {
                        return NoContent(); // 204 No Content
                    }

                    return NotFound("User not found or update failed.");
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpDelete("{userId}")]
            public async Task<IActionResult> DeleteUser(int userId)
            {
                try
                {
                    bool isDeleted = await _userService.DeleteUserAsync(userId);
                    if (isDeleted)
                    {
                        return NoContent(); // 204 No Content
                    }

                    return NotFound("User not found.");
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpGet]
            public async Task<IActionResult> GetAllUsers()
            {
                try
                {
                    var users = await _userService.GetAllUsersAsync();
                    return Ok(users);
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }


