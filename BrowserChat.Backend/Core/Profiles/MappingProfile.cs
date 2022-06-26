using AutoMapper;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;
using static BrowserChat.Backend.Core.Application.PostPublish;

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

            CreateMap<PostPublishRequest, Post>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => System.Web.HttpUtility.UrlDecode(src.Message)))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => DecryptInteger(src.RoomId)))
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Post, PostPublishDTO>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => Util.General.EncryptStringEncoded(src.RoomId.ToString())))
                .ForMember(dest => dest.TimeStampStr, opt => opt.MapFrom(src => src.TimeStamp.ToString("HH:mm:ss")));
        }

        private int DecryptInteger(string str)
        {
            int result = 0;
            int.TryParse(Util.General.DecryptStringEncoded(str), out result);
            return result;
        }
    }
}
