using AutoMapper;
using Catalog.Application.CustomException;
using Catalog.Application.DTO;
using Catalog.Application.Exception;
using Catalog.Infastructure.Interfaces;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Query.GetByIdQuery
{
    public class GetWigwamByIdQueryHandler : IRequestHandler<GetWigwamByIdQuery, ResponseWigwamModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWigwamByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseWigwamModel> Handle(GetWigwamByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var result = await _unitOfWork.Wigwam.GetAsync(request.Id, cancellationToken);

            if (result is null)
            {
                throw new NotFoundException("Entity is not found");
            }

            return _mapper.Map<ResponseWigwamModel>(result);
        }
    }
}
