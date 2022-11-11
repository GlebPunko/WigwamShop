using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Query.GetAllQuery
{
    public class GetAllQuery : IRequest<List<ResponseWigwamModel>>
    {
        public int Id { get; set; }
        public string WigwamsName { get; set; } = null!;
        public double Height { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
