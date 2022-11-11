using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Command.CreateCommand
{
    public class CreateSellerCommand : BaseSellerCommand, IRequest<ResponseSellerModel>
    {
    }
}
