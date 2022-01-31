using ForumBL.DTOs;
using ForumDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBL.Interfaces
{
    public interface IPostService : ICrud<PostDTO>
    {
        Task<IEnumerable<PostDTO>> GetByForumId(int id, int page, int pageSize);

        Task UpdateAsync(int id, PostDTO model);
    }
}
