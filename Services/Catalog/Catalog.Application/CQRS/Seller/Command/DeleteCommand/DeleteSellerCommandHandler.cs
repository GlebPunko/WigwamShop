using Catalog.Application.CustomException;
using Catalog.Application.Exception;
using Catalog.Infastructure.Interfaces;
using MediatR;

namespace Catalog.Application.CQRS.Seller.Command.DeleteCommand
{
    public class DeleteSellerCommandHandler : IRequestHandler<DeleteSellerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSellerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
        {
            if (request.Id is default(int))
            {
                throw new ArgumentIdException("Id have default value!");
            }

            var seller = await _unitOfWork.Seller.GetAsync(request.Id, cancellationToken);

            if (seller is null)
            {
                throw new NotFoundException("Seller not found");
            } 

            var deletedSellerId = await _unitOfWork.Seller.DeleteAsync(seller, cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);

            return deletedSellerId;
        }
    }
}
