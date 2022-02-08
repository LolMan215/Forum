using ForumDAL.Entities;
using ForumDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly DbSet<Comment> _comment;

        public CommentRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _comment = _dbContext.Set<Comment>();
        }

        public async Task AddAsync(Comment entity)
        {
            await _comment.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Comment entity)
        {
            _comment.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<Comment> FindAll()
        {
            return _comment.Include(x => x.User).Include(y => y.Post);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var res = await _comment.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Entry(res).State = EntityState.Detached;
            return res;
        }

        public void Update(Comment entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
