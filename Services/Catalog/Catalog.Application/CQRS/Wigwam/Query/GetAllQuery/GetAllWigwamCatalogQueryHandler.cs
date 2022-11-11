using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Infastructure.Interfaces;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Query.GetAllQuery
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<ResponseWigwamModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
           
        public async Task<List<ResponseWigwamModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var res = await _unitOfWork.Wigwam.GetAllAsync(cancellationToken);
            
            return _mapper.Map<List<ResponseWigwamModel>>(res);
        }
    }
}
