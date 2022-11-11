using Catalog.Application.DTO;
using MediatR;

namespace  Catalog.Application.CQRS.Seller.Command.UpdateCommand
{
    public class UpdateSellerCommand : BaseSellerCommand, IRequest<ResponseSellerModel>
    {
        public int Id { get; set;  }
    }
}
