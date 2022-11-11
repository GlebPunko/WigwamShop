namespace Basket.Infastructure.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
        Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken);
        Task<int> DeleteAsync(TEntity model, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
