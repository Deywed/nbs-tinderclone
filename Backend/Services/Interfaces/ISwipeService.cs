using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ISwipeService
    {
        Task RecordSwipeAsync(Guid swiperId, Guid swipeeId, SwipeType type);
        Task<bool> IsMatchAsync(Guid userId1, Guid userId2);
        
    }
}