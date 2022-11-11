using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Application.Exception;
using Catalog.Domain.Entities;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Command.UpdateCommand
{
    public class UpdateSellerCommandHandler : IRequestHandler<UpdateSellerCommand, ResponseSellerModel>
    {
        private readonly IValidator<UpdateSellerCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSellerCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateSellerCommand> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;   
        }

        public async Task<ResponseSellerModel> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var map = _mapper.Map<SellerInfo>(request);

            var updatedSeller = await _unitOfWork.Seller.UpdateAsync(map, cancellationToken);

            if (updatedSeller is null)
            {
                throw new ArgumentNullException();
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<ResponseSellerModel>(updatedSeller);
        }
    }
}
