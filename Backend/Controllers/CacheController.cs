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
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cache;

        public CacheController(ICacheService cache)
        {
            _cache = cache;
        }

        [HttpPost("ping/{userId}")]
        public async Task<IActionResult> Ping(string userId)
        {
            await _cache.SetUserOnlineAsync(userId);
            return Ok();
        }

        [HttpGet("online-status/{userId}")]
        public async Task<IActionResult> CheckOnline(string userId)
        {
            var isOnline = await _cache.IsUserOnlineAsync(userId);
            return Ok(new { userId, isOnline });
        }

    }
}