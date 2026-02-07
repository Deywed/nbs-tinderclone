using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwipeController : ControllerBase
    {
        private readonly ISwipeService _swipeService;
        public SwipeController(ISwipeService swipeService)
        {
            _swipeService = swipeService;
        }

        [HttpPost("Like")]
        public async Task<IActionResult> LikeUser(string userId, string likedUserId)
        {
            var result = await _swipeService.LikeUserAsync(userId, likedUserId);
            if (result)
            {
                return Ok("It's a match!");
            }
            return Ok("User liked successfully");
        }
        [HttpPost("Dislike")]
        public async Task<IActionResult> DislikeUser(string userId, string dislikedUserId)
        {
            await _swipeService.DislikeUserAsync(userId, dislikedUserId);
            return Ok("User disliked successfully");
        }

        [HttpGet("Matches/{userId}")]
        public async Task<IActionResult> GetMatches(string userId)
        {
            var matches = await _swipeService.GetMatchesByUserIdAsync(userId);
            return Ok(matches);
        }
        [HttpDelete("RemoveMatch")]
        public async Task<IActionResult> RemoveMatch(string userId, string matchedUserId)
        {
            await _swipeService.RemoveMatchAsync(userId, matchedUserId);
            return Ok("Match removed successfully");
        }
        [HttpPut("Block")]
        public async Task<IActionResult> BlockUser(string userId, string blockedUserId)
        {
            await _swipeService.BlockUserAsync(userId, blockedUserId);
            return Ok("User blocked successfully");
        }
    }
}