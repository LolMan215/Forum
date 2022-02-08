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
    public class ForumService : IForumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ForumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ForumDTO model)
        {
            var validator = new ForumCreateNewValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var forum = new Forum
            {
                //ParentId = model.ParentId,
                Name = model.Name,
                Created = DateTime.Now
            };

            _unitOfWork.ForumRepository.AddAsync(forum);
            return 0;
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            try
            {
                await _unitOfWork.ForumRepository.DeleteByIdAsync(modelId);
            }
            catch (Exception ex)
            {

            }
            await _unitOfWork.SaveAsync();
        }

        public List<ForumDTO> Get()
        {
            var data =  GetAll()
                .OrderByDescending(f => f.Name)
                .Select(f => new ForumDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Created = f.Created
                }).ToList();

            return data;
        }

        public IEnumerable<ForumDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<ForumDTO>>(_unitOfWork.ForumRepository.FindAll());
        }

        public async Task<List<ForumDTO>> GetAllTopLevels()
        {
            var data = GetAll().ToList();
                /*.Where(f => !f.ParentId.HasValue)
                .Take(pageSize)
                .Select(f => new ForumDTO
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .OrderBy(f => f.Name)
                //.Skip((page - 1) * pageSize)
                .ToList();*/

            /*foreach (var f in data)
            {
                var subForums = GetAll()
                    .Where(sf => sf.ParentId.HasValue ? sf.ParentId.Value == f.Id : false)
                    .Select(sf => new ForumDTO
                    {
                        Id = sf.Id,
                        Name = sf.Name
                    }).ToList();

                f.SubForums = subForums.Count > 0 ? subForums : null;
            }*/

            return data;
        }

        public async Task<ForumDTO> GetByIdAsync(int id)
        {
            var forum = await _unitOfWork.ForumRepository.GetByIdAsync(id);

            if (forum == null)
                throw new Exception();

            var data = new ForumDTO
            {
                Id = forum.Id,
                Name = forum.Name,
                Created = forum.Created
            };

           /* if (forum.ParentId.HasValue)
            {
                data.Parent = new ForumDTO
                {
                    Id = forum.Parent.Id,
                    Name = forum.Parent.Name
                };
            }*/

           /* if (forum.SubForums.Count > 0)
            {
                data.SubForums = forum.SubForums.Select(f => new ForumDTO
                {
                    Id = f.Id,
                    Name = f.Name
                }).ToList();
            }*/

            return data;
        }

        public async Task UpdateAsync(ForumDTO model)
        {
            _unitOfWork.ForumRepository.Update(_mapper.Map<Forum>(model));
            
            //await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, ForumDTO model)
        {
            var validator = new ForumEditValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var forum = await GetByIdAsync(id);

            if (forum == null)
                throw new Exception();

            forum.Name = model.Name;
            
            _unitOfWork.ForumRepository.Update(_mapper.Map<Forum>(forum));
            await _unitOfWork.SaveAsync();
            //await UpdateAsync(forum);
        }
    }
}
