using Catalog.Application.DTO;
using MediatR;

namespace Catalog.Application.CQRS.Wigwam.Command.UpdateCommand
{
    public class UpdateWigwamCommand : BaseWigwamCommand, IRequest<ResponseWigwamModel>
    {
        public int Id { get; set; }
    }
}
