using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TikToken.DTOs
{
    public class LoginDTO
    {
        private string? username { get; set; }
        [Required]
        public string? UserName { get => username; set => username = value!.ToLower(); }
        [Required]
        public string? Password { get; set; }
    }
}