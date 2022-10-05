using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TikToken.DTOs;
using TikToken.Models;

namespace TikToken.Interfaces
{
    public interface IUserService
    {
        public Task<int> RegisterAsync(RegisterDTO newUser);
        public User? Login(LoginDTO login);
        public List<UserViewDTO> GetAll();
    }
}