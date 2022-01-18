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
            await _dbContext.SaveChangesAsync();
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
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Forum> FindAll()
        {
            return _forum;
        }

        public async Task<Forum> GetByIdAsync(int id)
        {
            return await _forum.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Forum entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
