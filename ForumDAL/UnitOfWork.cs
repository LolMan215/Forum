using ForumDAL.Interfaces;
using ForumDAL.Repositories;
using System.Threading.Tasks;

namespace ForumDAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private ICommentRepository _commentRepository;

        private IForumRepository _forumRepository;

        private IPostRepository _postRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if(_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_dbContext);
                }
                return _commentRepository;
            }
        }

        public IForumRepository ForumRepository
        {
            get
            {
                if (_forumRepository == null)
                {
                    _forumRepository = new ForumRepository(_dbContext);
                }
                return _forumRepository;
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_dbContext);
                }
                return _postRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
