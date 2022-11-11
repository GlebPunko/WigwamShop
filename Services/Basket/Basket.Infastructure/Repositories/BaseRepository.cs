using Basket.Domain.Entities;
using Basket.Infastructure.Context;
using Basket.Infastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected BasketDbContext _context;

        public BaseRepository(BasketDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken)
        {
            var res = await _context.Set<TEntity>().AddAsync(model, cancellationToken);

            return res.Entity;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Remove(entity);

            return entity.Id;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken)
        {
            var res = _context.Set<TEntity>().Update(model);

            return res.Entity;
        }

        public virtual async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
