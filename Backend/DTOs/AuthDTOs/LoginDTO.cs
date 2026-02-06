using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.AuthDTOs
{
    public record LoginDTO
    (
        [Required]
        string Email,
        [Required]
        string Password
    );
}
