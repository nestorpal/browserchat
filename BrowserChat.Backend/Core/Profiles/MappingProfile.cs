using AutoMapper;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;

namespace BrowserChat.Backend.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomReadDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Util.General.EncryptStringEncoded(src.Id.ToString())));
        }
    }
}
