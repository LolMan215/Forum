using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Interfaces
{
    interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        IForumRepository ForumRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveAsync();
    }
}
