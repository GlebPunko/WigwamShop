using Basket.Domain.Entities;
using Basket.Infastructure.Context;
using Basket.Infastructure.Repositories.Interfaces;

namespace Basket.Infastructure.Repositories
{
    public class BasketRepository : BaseRepository<Order> ,IBasketRepository
    {
        public BasketRepository(BasketDbContext context) : base(context)
        {
        }
    }
}
