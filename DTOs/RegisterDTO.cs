using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TikToken.DTOs
{
    public class RegisterDTO
    {
        [Required]
        private string? username { get; set; }
        [Required]
        private string? email { get; set; }
        [Required, RegularExpression(@"^[\w\.]{5,20}$")]
        public string? UserName { get => username; set => username = value!.ToLower() ; }
        [Required, EmailAddress]
        public string? Email { get => email; set => email = value!.ToLower(); }
        [StringLength(30, MinimumLength = 8)]
        public string? Password { get; set; }
    }
}