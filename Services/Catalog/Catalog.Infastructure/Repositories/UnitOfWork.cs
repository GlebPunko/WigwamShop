using Catalog.Infastructure.Context;
using Catalog.Infastructure.Interfaces;

namespace Catalog.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;

        public UnitOfWork(CatalogDbContext context)
        {
            _context = context;
        }

        public IWigwamRepository Wigwam => new WigwamRepository(_context);

        public ISellerRepository Seller => new SellerRepository(_context);

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
