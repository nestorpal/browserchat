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

            CreateMap<Post, PostReadDTO>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => Util.General.EncryptStringEncoded(src.RoomId.ToString())))
                .ForMember(dest => dest.TimeStampStr, opt => opt.MapFrom(src => src.TimeStamp.ToString("HH:mm:ss")));
        }
    }
}
