using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMongoUserService _mongoUserService;

        public UsersController(IMongoUserService mongoUserService)
        {
            _mongoUserService = mongoUserService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();

            await _mongoUserService.CreateUserAsync(user);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mongoUserService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _mongoUserService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            var existingUser = await _mongoUserService.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            updatedUser.Id = id.ToString();
            await _mongoUserService.UpdateUserAsync(updatedUser);
            return Ok(updatedUser);
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await _mongoUserService.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _mongoUserService.DeleteUserAsync(id);
            return NoContent();
        }
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _mongoUserService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}