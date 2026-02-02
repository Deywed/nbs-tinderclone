using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserPreferences
    {
        public Guid UserId { get; set; }
        public int MinAgePref { get; set; }
        public int MaxAgePref { get; set; }
        public string InterestedIn { get; set; } = string.Empty;

    }
}