using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface ICacheService
    {
        Task SetUserOnlineAsync(string userId);
        Task MatchAlertAsync(string userId, string matchedWithId);
        Task<bool> IsUserOnlineAsync(string userId);
    }
}