using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.UserPreferencesDTO
{
    public record UpdateUserPreferencesDTO
    (
        int MinAgePref,
        int MaxAgePref,
        string InterestedIn
    );
}
