using AutoMapper;
using BrowserChat.Entity.DTO;
using BrowserChat.Security.Core.Entities;

namespace BrowserChat.Security.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadDTO>();
        }
    }
}
