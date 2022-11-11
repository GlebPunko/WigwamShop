using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Command.CreateCommand
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand, ResponseSellerModel>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSellerCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSellerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateSellerCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResponseSellerModel> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<SellerInfo>(request);

            var createdSeller = await _unitOfWork.Seller.CreateAsync(map, cancellationToken);

            if (createdSeller is null)
            {
                throw new ArgumentNullException();
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<ResponseSellerModel>(createdSeller);
        }
    }
}
