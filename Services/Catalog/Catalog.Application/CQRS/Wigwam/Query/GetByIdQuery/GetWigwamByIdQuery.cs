using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Query.GetByIdQuery
{
    public class GetWigwamByIdQuery : IRequest<ResponseWigwamModel>
    {
        public int Id { get; }

        public GetWigwamByIdQuery(int id)
        {
            Id = id;
        }
    }
}
