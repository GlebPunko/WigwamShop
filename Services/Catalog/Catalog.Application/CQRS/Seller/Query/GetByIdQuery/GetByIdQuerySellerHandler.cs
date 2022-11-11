using AutoMapper;
using Catalog.Infastructure.Interfaces;
using MediatR;
using Catalog.Application.CustomException;
using Catalog.Application.DTO;
using Catalog.Application.Exception;

namespace Catalog.Application.CQRS.Seller.Query.GetByIdQuery
{
    public class GetByIdQuerySellerHandler : IRequestHandler<GetByIdSellerQueryHandler, ResponseSellerModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdQuerySellerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseSellerModel> Handle(GetByIdSellerQueryHandler request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var seller = await _unitOfWork.Seller.GetAsync(request.Id, cancellationToken);

            if (seller is null)
            {
                throw new NotFoundException($"{nameof(seller)} entity is null");
            }

            return _mapper.Map<ResponseSellerModel>(seller);
        }
    }
}
