using AutoMapper;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.CreateCommand
{
    public class CreateWigwamCommandHandler : IRequestHandler<CreateWigwamCommand, ResponseWigwamModel>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateWigwamCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWigwamCommandHandler(IMapper mapper, IValidator<CreateWigwamCommand> validator, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWigwamModel> Handle(CreateWigwamCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<WigwamsInfo>(request);

            var createdWigwam = await _unitOfWork.Wigwam.CreateAsync(map, cancellationToken);

            if (createdWigwam is null)
            {
                throw new ArgumentNullException("Entity is null");
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<ResponseWigwamModel>(createdWigwam);
        }
    }
}