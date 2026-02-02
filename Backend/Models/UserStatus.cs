using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserStatus
    {
        public Guid UserId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastActive { get; set; }
    }
}