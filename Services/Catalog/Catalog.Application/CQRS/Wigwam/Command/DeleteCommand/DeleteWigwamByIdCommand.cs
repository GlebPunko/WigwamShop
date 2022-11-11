using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.DeleteCommand
{
    public class DeleteWigwamByIdCommand : IRequest<int>
    {
        public int Id { get; }

        public DeleteWigwamByIdCommand(int id)
        {
            Id = id;
        }
    }
}
