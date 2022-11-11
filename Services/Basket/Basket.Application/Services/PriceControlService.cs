using Basket.Application.Exception;
using Basket.Application.Interfaces;
using Basket.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Basket.Application.Services
{
    public class PriceControlService : IPriceControlService
    {
        private readonly BasketDbContext _context;
        private const decimal MaxPrice = 10000.0M;

        public PriceControlService(BasketDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetPricesSeller(int id, CancellationToken cancellationToken)
        {
            if (id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var prices = await _context.Orders
                .Where(x => x.SellerId == id)
                .Select(o => o.Price)
                .ToListAsync(cancellationToken);

            return CheckSumPrice(prices, id);
        }

        public async Task<List<string>> GetPricesSellers(CancellationToken cancellationToken)
        {
            var resPrices = new List<string>();
            int sellerNumber = 0;

            for (int sellerId = 0; sellerId < _context.Sellers.Count(); sellerId++)
            {
                var pricesTepm = await _context.Orders
                    .Where(x => x.SellerId == sellerId)
                    .Select(o => o.Price)
                    .ToListAsync(cancellationToken);

                resPrices.Add(CheckSumPrice(pricesTepm, ++sellerNumber));
            }

            return resPrices;
        }

        private static string CheckSumPrice(List<decimal> pricesSeller, int id)
        {
            if (pricesSeller.Sum() > MaxPrice)
            {
                return $"Seller with id = {id} price limit exceeded!";
            }

            return $"Seller with id = {id} price limit don`t exceeded!";
        }
    }
}
