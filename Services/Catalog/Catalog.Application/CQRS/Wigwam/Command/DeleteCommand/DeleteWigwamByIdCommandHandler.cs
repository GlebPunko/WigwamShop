using Catalog.Application.CustomException;
using Catalog.Application.Exception;
using Catalog.Infastructure.Interfaces;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.DeleteCommand
{
    public class DeleteWigwamByIdCommandHandler : IRequestHandler<DeleteWigwamByIdCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWigwamByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteWigwamByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var entity = await _unitOfWork.Wigwam.GetAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException("Entity is not found");
            }

            var res = await _unitOfWork.Wigwam.DeleteAsync(entity, cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);

            return res;
        }
    }
}
