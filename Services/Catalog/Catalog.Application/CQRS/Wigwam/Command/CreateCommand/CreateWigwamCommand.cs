using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.CreateCommand
{
    public class CreateWigwamCommand : BaseWigwamCommand, IRequest<ResponseWigwamModel>
    {
    }
}
