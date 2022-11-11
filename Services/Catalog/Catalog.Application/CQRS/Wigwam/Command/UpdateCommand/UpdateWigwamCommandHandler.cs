using AutoMapper;
using Catalog.Application.CustomException;
using Catalog.Application.DTO;
using Catalog.Application.Exception;
using Catalog.Domain.Entities;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.UpdateCommand
{
    public class UpdateWigwamCommandHandler : IRequestHandler<UpdateWigwamCommand, ResponseWigwamModel>
    {
        private readonly IValidator<UpdateWigwamCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWigwamCommandHandler(IValidator<UpdateWigwamCommand> validator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
            
        public async Task<ResponseWigwamModel> Handle(UpdateWigwamCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var map = _mapper.Map<WigwamsInfo>(request);
            var seller =  await _unitOfWork.Seller.GetAsync(request.SellerInfoId, cancellationToken);

            if (seller is null)
            {
                throw new NotFoundException("Seller");
            }

            var updatedWigwam = await _unitOfWork.Wigwam.UpdateAsync(map, cancellationToken);

            if (updatedWigwam is null)
            {
                throw new ArgumentNullException("Entity is be null");
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<ResponseWigwamModel>(updatedWigwam);
        }
    }
}
