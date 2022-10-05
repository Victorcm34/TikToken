using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TikToken.Models
{
    public class User
    {
        public enum Roles
        {
            Admin,
            User
        }

        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public Roles Role { get; set; } = Roles.User;
    }
}