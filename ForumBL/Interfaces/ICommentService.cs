using ForumBL.DTOs;
using ForumDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBL.Interfaces
{
    public interface ICommentService: ICrud<CommentDTO>
    {
        Task FillChildren(CommentDTO comment);
        Task<List<CommentDTO>> GetByPostId(int id);

        Task UpdateAsync(int id, CommentDTO model);
    }
}
