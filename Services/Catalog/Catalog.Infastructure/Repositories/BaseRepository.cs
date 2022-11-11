using Catalog.Domain.Entities;
using Catalog.Infastructure.Context;
using Catalog.Infastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseModel
    {
        protected CatalogDbContext _context;

        public BaseRepository(CatalogDbContext context)
        {
            _context = context;
        }
        
        public virtual async Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken)
        {
            var createdEntity =  await _context.Set<TEntity>().AddAsync(model, cancellationToken);

            return createdEntity.Entity;
        }

        public async virtual Task<int> DeleteAsync(TEntity model, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Remove(model);

            return model.Id;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken)
        {
            var updatedEntity = _context.Set<TEntity>().Update(model);

            return updatedEntity.Entity;
        }
    }
}
