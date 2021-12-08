using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    //Helps us map from one object to another. Thats all it does. 
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
            src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            //We want to go from a member UpdateDto to our AppUser
            CreateMap<MemberUpdateDto, AppUser>();
            //We don't have tom map the properties manually. 
            CreateMap<RegisterDto, AppUser>();
            //Create map for Message to message DTO
            //There's one prop we cannot get automapper to do for us which is the photoURL
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
                src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
                src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}