using Catalog.Domain.Entities;
using Catalog.Infastructure.Context;
using Catalog.Infastructure.Interfaces;

namespace Catalog.Infastructure.Repositories
{
    public class SellerRepository : BaseRepository<SellerInfo>, ISellerRepository
    {
        public SellerRepository(CatalogDbContext context) : base(context)
        { }
    }
}
