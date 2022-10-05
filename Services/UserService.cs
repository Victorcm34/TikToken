using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TikToken.Database;
using TikToken.DTOs;
using TikToken.Interfaces;
using TikToken.Models;

namespace TikToken.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> RegisterAsync(RegisterDTO newUser)
        {
            if (UserExists(newUser.UserName!) || EmailExists(newUser.Email!)) return -1;
            var hmac = new HMACSHA512();
            User user = new()
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password!)),
                PasswordSalt = hmac.Key
            };
            try
            {
                _context.User!.Add(user);
                await _context.SaveChangesAsync();
                return Int32.Parse(_context.User.Where(user => user.UserName == newUser.UserName).FirstOrDefault()!.Id.ToString());
            }
            catch (Exception ex)
            {
                //Log Error
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public User? Login(LoginDTO login)
        {
            User user = _context.User!.Where(u => u.UserName == login.UserName).SingleOrDefault()!;
            if (user == null) return null;
            var hmac = new HMACSHA512(user.PasswordSalt!);
            var passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password!));
            if (!user.PasswordHash!.SequenceEqual(passHash)) return null;
            return user;
        }

        private bool UserExists(string user)
        {
            return _context.User!.Where(u => u.UserName == user).Any();
        }

        private bool EmailExists(string mail)
        {
            return _context.User!.Where(u => u.UserName == mail).Any();
        }

        public List<UserViewDTO> GetAll()
        {
            List<User> users = _context.User!.ToList();
            return _mapper.Map<List<User>, List<UserViewDTO>>(users);
        }
    }
}