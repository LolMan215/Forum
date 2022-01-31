
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
                .ReverseMap();

            CreateMap<Post, PostDTO>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap();

        }
    }
}
