using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Infastructure.Interfaces;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Query.GetAllQuery
{
    public class GetAllSellerQueryHandler : IRequestHandler<GetAllSellerQuery, List<ResponseSellerModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSellerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
            
        public async Task<List<ResponseSellerModel>> Handle(GetAllSellerQuery request, CancellationToken cancellationToken)
        {
            var sellers = await _unitOfWork.Seller.GetAllAsync(cancellationToken);
            
            return _mapper.Map<List<ResponseSellerModel>>(sellers);
        }
    }
}
