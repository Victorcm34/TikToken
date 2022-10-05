using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TikToken.Models.User;

namespace TikToken.DTOs
{
    public class UserViewDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Roles Role { get; set; }
    }
}