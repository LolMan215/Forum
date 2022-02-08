
using AutoMapper;
using ForumBL.DTOs;
using ForumDAL.Entities;

namespace ForumBL
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Comment, CommentDTO>()
                .ReverseMap();

            CreateMap<Forum, ForumDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(forum => forum.Id))
                .ReverseMap();

            CreateMap<Post, PostDTO>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap();

        }
    }
}
