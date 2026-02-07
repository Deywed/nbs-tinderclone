using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.DTOs.AuthDTOs;
using Backend.Models;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userRepository;
        public AuthController(ITokenService tokenService, IUserService userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);

            if (user == null) return Unauthorized("Invalid email");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);

            if (!isPasswordValid) return Unauthorized("Invalid password");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
            if (user != null) return BadRequest("Email already in use");

            var newUser = new User
            {
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Age = registerDTO.Age,
                Bio = registerDTO.Bio,
                Gender = registerDTO.Gender,
                UserPreferences = new UserPreferences
                {
                    MinAgePref = registerDTO.MinAgePref,
                    MaxAgePref = registerDTO.MaxAgePref,
                    InterestedIn = registerDTO.InterestedIn
                }
            };
            await _userRepository.CreateUserAsync(newUser);
            return Ok("User registered successfully");
        }
    }
}