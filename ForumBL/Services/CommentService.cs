using AutoMapper;
using ForumBL.DTOs;
using ForumBL.Interfaces;
using ForumBL.Validators;
using ForumDAL.Entities;
using ForumDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBL.Services
{
    public class CommentService :ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CommentDTO model)
        {
            var validator = new CommentCreateNewValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var comment = new Comment
            {
                UserId = model.UserId,//fix later
                PostId = model.PostId.Value,
                Body = model.Body,
                Updated = DateTime.Now,
                Created = DateTime.Now
            };

            var storedComment = _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            return storedComment.Id; //fix later
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            try
            {
                await _unitOfWork.CommentRepository.DeleteByIdAsync(modelId);
            }
            catch(Exception ex)
            {

            }
            await _unitOfWork.SaveAsync();
        }

        public async Task FillChildren(CommentDTO comment)
        {
            comment.Children = GetAll()
                .Where(c => c.ParentId == comment.Id)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    ParentId = c.ParentId,
                    Body = c.Body,
                    Updated = c.Updated,
                    Created = c.Created,
                    User = new UserDTO
                    {
                        UserName = c.User.UserName,
                    }
                }).ToList();

            if (comment.Children.Count > 0)
            {
                foreach (var child in comment.Children)
                {
                    await FillChildren(child);
                }
            }
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<CommentDTO>>(_unitOfWork.CommentRepository.FindAll());
        }

        public async Task<CommentDTO> GetByIdAsync(int id)
        {
            var res = await _unitOfWork.CommentRepository.GetByIdAsync(id);
            return _mapper.Map<CommentDTO>(res);
        }

        public async Task<List<CommentDTO>> GetByPostId(int id, int page, int pageSize)
        {
            var data = GetAll()
                .OrderBy(p => p.Created)
                .Where(c => c.PostId == id && !c.ParentId.HasValue)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    ParentId = c.ParentId,
                    Body = c.Body,
                    Updated = c.Updated,
                    Created = c.Created,
                    User = new UserDTO
                    {
                        UserName =  c.User.UserName
                    }
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            foreach (var comment in data)
            {
                await FillChildren(comment);
            }
            return data;
        }

        public async Task UpdateAsync(CommentDTO model)
        {
            _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, CommentDTO model)
        {
            var validator = new CommentEditValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var comment = await GetByIdAsync(id);

            if (comment == null)
                throw new Exception();

            comment.Body = model.Body;

            await UpdateAsync(comment);
        }
    }
}
