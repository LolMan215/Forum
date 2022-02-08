using AutoMapper;
using ForumBL.DTOs;
using ForumBL.Interfaces;
using ForumBL.Validators;
using ForumDAL.Entities;
using ForumDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(PostDTO model)
        {
            var validator = new PostCreateNewValidator();
            var validationResult = await validator.ValidateAsync(_mapper.Map<PostDTO>(model));

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var post = new Post
            {
                UserId = model.UserId, ////TODO
                ForumId = (int)model.ForumId,
                Title = model.Title,
                Body = model.Body,
                IsLocked = false,
                LockedReason = null,
                Updated = DateTime.Now,
                Created = DateTime.Now
            };

            var storedPost = _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveAsync();

            return storedPost.Id;
        }


        public async Task DeleteByIdAsync(int modelId)
        {
            try
            {
                await _unitOfWork.PostRepository.DeleteByIdAsync(modelId);
            }
            catch (Exception ex)
            {

            }
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<PostDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<PostDTO>>(_unitOfWork.PostRepository.FindAll());
        }

        public async Task<IEnumerable<PostDTO>> GetByForumId(int id)
        {
            var data = GetAll()
                .OrderByDescending(p => p.Updated)
                .OrderByDescending(p => p.Created)
                .OrderByDescending(p => p.Id)
                .Where(p => p.ForumId == id)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    ForumId = p.ForumId,
                    Title = p.Title,
                    Body = p.Body,
                    UserId = p.UserId,
                    IsLocked = p.IsLocked,
                    Updated = p.Updated,
                    Created = p.Created,
                    User = new UserDTO
                    {
                        UserName = p.User.UserName
                    }
                });
               

            return data;
        }

        public async Task<PostDTO> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);

            if (post == null)
                throw new Exception();

            var data = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                IsLocked = post.IsLocked,
                LockedReason = post.LockedReason,
                Updated = post.Updated,
                Created = post.Created,
                UserId = post.UserId,
                ForumId = post.ForumId,
                User = new UserDTO
                {
                    UserName = post.User.UserName
                },
                Forum = new ForumDTO
                {
                    Id = post.Forum.Id,
                    Name = post.Forum.Name
                }
            };

            return data;
        }

        public async Task UpdateAsync(PostDTO model)
        {
            _unitOfWork.PostRepository.Update(_mapper.Map<Post>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, PostDTO model)
        {
            var validator = new PostEditValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var post = await GetByIdAsync(id);

            if (post == null)
                throw new Exception();

            post.Title = model.Title;
            post.Body = model.Body;

            await UpdateAsync(post);

        }
    }
}
