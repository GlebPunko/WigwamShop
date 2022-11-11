using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Query.GetByIdQuery
{
    public class GetByIdSellerQueryHandler : IRequest<ResponseSellerModel>
    {
        public int Id { get; }

        public GetByIdSellerQueryHandler(int id)
        {
            Id = id;
        }
    }
}
