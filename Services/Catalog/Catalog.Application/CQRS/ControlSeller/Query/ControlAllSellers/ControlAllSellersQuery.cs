using MediatR;

namespace Catalog.Application.CQRS.ControlSeller.Query.ControlAllSellersQuery
{
    public class ControlAllSellersQuery : IRequest<List<string>>
    {
    }
}
