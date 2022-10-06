using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TikToken.Models
{
    public class User
    {
        public User(string userName, string email, byte[] hash, byte[] salt)
        {
            UserName = userName;
            Email = email;
            PasswordHash = hash;
            PasswordSalt = salt;
        }
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}