using Catalog.Domain.Entities;
using Catalog.Infastructure.Context;
using Catalog.Infastructure.Interfaces;

namespace Catalog.Infastructure.Repositories
{
    public class WigwamRepository : BaseRepository<WigwamsInfo> ,IWigwamRepository
    {
        public WigwamRepository(CatalogDbContext context) : base(context)
        { }
    }
}