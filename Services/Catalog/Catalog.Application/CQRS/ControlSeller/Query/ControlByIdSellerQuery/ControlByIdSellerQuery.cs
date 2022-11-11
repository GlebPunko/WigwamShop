using MediatR;

namespace Catalog.Application.CQRS.ControlSeller.Query.ControlByIdQuery
{
    public class ControlByIdSellerQuery : IRequest<string>
    {
        public int Id { get; set; }

        public ControlByIdSellerQuery(int id)
        {
            Id = id;
        }
    }
}
