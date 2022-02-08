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
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly DbSet<Forum> _forum;

        public ForumRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _forum = _dbContext.Set<Forum>();
        }

        public async Task AddAsync(Forum entity)
        {
            await _forum.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Forum entity)
        {
            _forum.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<Forum> FindAll()
        {
            return _forum;
        }

        public async Task<Forum> GetByIdAsync(int id)
        {
            var res = await _forum.Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Entry(res).State = EntityState.Detached;
            return res;
        }

        public void Update(Forum entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
