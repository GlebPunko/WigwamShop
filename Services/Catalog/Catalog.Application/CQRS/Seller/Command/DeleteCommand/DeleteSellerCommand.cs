using MediatR;

namespace Catalog.Application.CQRS.Seller.Command.DeleteCommand
{
    public class DeleteSellerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteSellerCommand(int id)
        {
            Id = id;
        }
    }
}
