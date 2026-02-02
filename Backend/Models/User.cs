using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum UserGender
    {
        Male,
        Female,
        Other
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Bio { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
        public UserPreferences UserPreferences { get; set; } = new UserPreferences();
        public UserGender Gender { get; set; }
    }
}