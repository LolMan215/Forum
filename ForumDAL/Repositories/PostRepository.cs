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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly DbSet<Post> _post;

        public PostRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _post = _dbContext.Set<Post>();
        }

        public async Task AddAsync(Post entity)
        {
            await _post.AddAsync(entity);
             _dbContext.SaveChanges();
        }

        public void Delete(Post entity)
        {
            _post.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Post> FindAll()
        {
            return _post.Include(x => x.User).Include(y => y.Forum);
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var res = await _post.Include(x => x.User).Include(y => y.Forum).FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Entry(res).State = EntityState.Detached;
            return res;
        }

        public void Update(Post entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
