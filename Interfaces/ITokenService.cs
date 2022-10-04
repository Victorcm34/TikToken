using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TikToken.Models;

namespace TikToken.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}