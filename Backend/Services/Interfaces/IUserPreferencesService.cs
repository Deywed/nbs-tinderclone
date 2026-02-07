using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.UserPreferencesDTO;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IUserPreferencesService
    {
        Task UpdateUserPreferencesAsync(string userId, UpdateUserPreferencesDTO preferences);
    }
}