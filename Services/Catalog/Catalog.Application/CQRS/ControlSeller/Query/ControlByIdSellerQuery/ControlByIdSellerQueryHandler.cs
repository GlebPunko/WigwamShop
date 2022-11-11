using MediatR;
using Catalog.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Catalog.Application.Exception;

namespace Catalog.Application.CQRS.ControlSeller.Query.ControlByIdQuery
{
    public class ControlByIdSellerQueryHandler : IRequestHandler<ControlByIdSellerQuery, string>
    {
        private readonly CatalogDbContext _context;
        private const int MaxWigwamCount = 10;

        public ControlByIdSellerQueryHandler(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(ControlByIdSellerQuery request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var wigwamCount = await _context.WigwamsInfos.Where(x => x.SellerInfoId == request.Id)
                .CountAsync(cancellationToken);

            if (wigwamCount > MaxWigwamCount)
            {
                return $"The maximum number of wigwams for the seller with id = {request.Id} has been exceeded!";
            }

            return $"The maximum number of wigwams for the seller with id = {request.Id} has not been exceeded!";
        }
    }
}
