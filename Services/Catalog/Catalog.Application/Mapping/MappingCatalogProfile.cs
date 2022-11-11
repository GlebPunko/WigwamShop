using AutoMapper;
using Catalog.Application.CQRS.Seller.Command.CreateCommand;
using Catalog.Application.CQRS.Seller.Command.UpdateCommand;
using Catalog.Application.CQRS.Wigwam.Command.CreateCommand;
using Catalog.Application.CQRS.Wigwam.Command.UpdateCommand;
using Catalog.Application.DTO;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mapping
{
    public class MappingCatalogProfile : Profile
    {
        public MappingCatalogProfile()
        {
            CreateMap<CreateSellerCommand, SellerInfo>().ReverseMap();
            CreateMap<SellerInfo, ResponseSellerModel>().ReverseMap();
            CreateMap<UpdateSellerCommand, SellerInfo>().ReverseMap();

            CreateMap<CreateWigwamCommand, WigwamsInfo>()
                .ForMember(x => x.Id, s => s.Ignore())
                .ForMember(x => x.SellerInfo, s => s.Ignore())
                .ReverseMap();
            CreateMap<WigwamsInfo, ResponseWigwamModel>().ReverseMap();
            CreateMap<UpdateWigwamCommand, WigwamsInfo>().ReverseMap();
        }
    }
}
