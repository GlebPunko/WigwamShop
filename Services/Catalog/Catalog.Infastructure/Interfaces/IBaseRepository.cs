namespace Catalog.Infastructure.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
        Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken);
        Task<int> DeleteAsync(TEntity id, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);
    }
}
