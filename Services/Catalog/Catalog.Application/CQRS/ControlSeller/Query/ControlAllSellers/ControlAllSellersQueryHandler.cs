using Catalog.Infastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.CQRS.ControlSeller.Query.ControlAllSellersQuery
{
    public class ControlAllSellersQueryHandler : IRequestHandler<ControlAllSellersQuery, List<string>>
    {
        private readonly CatalogDbContext _context;
        private const int MaxWigwamCount = 10;

        public ControlAllSellersQueryHandler(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(ControlAllSellersQuery request, CancellationToken cancellationToken)
        {
            var resultInfoSellers = new List<string>();
            int sellerNumber = 0;

            for (int sellerId = 0; sellerId < _context.SellerInfos.Count(); sellerId++)
            {
                var countWigwams = await _context.WigwamsInfos
                    .Where(x => x.SellerInfoId == sellerId)
                    .CountAsync(cancellationToken);

                resultInfoSellers.Add(CheckSumWigwamCount(countWigwams, ++sellerNumber));
            }

            return resultInfoSellers;
        }

        private static string CheckSumWigwamCount(int countWigawms, int id)
        {
            if (countWigawms > MaxWigwamCount)
            {
                return $"Seller with id = {id} wigwams count limit exceeded!";
            }

            return $"Seller with id = {id} wigwams count limit don`t exceeded!";
        }
    }
}
