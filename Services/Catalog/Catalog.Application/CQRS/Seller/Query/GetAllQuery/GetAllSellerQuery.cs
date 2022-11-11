using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Query.GetAllQuery
{
    public class GetAllSellerQuery : IRequest<List<ResponseSellerModel>>
    {
        public string SellerName { get; set; } = null!;
        public string SellerDescription { get; set; } = null!;
        public string SellerPhoneNumber { get; set; } = null!;
    }
}
