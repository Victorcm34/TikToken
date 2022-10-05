using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TikToken.DTOs;
using TikToken.Models;

namespace TikToken.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<User, UserViewDTO>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                                          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                                          .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}