using AutoMapper;
using Catalog.Application.CQRS.Seller.Command.DeleteCommand;
using Catalog.Application.CQRS.Seller.Query.GetAllQuery;
using Catalog.Application.CQRS.Seller.Query.GetByIdQuery;
using Catalog.Application.CQRS.Wigwam.Command.DeleteCommand;
using Catalog.Application.CQRS.Wigwam.Query.GetAllQuery;
using Catalog.Application.CQRS.Wigwam.Query.GetByIdQuery;
using Catalog.Application.Mapping;
using Catalog.Domain.Entities;

namespace Catalog.XUnitTests.Helpers
{
    internal static class TestsHelper
    {
        /// <summary>
        /// Mappings for the CQRS (Catalog service) tests, because there are no such maps in profile
        /// </summary>
        /// <returns>MapperConfiguration</returns>
        internal static IMapper SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddMaps(typeof(MappingCatalogProfile));
                //map only for tests!!!
                cfg.CreateMap<SellerInfo, GetAllSellerQuery>();
                cfg.CreateMap<SellerInfo, GetByIdSellerQueryHandler>();
                cfg.CreateMap<WigwamsInfo, GetWigwamByIdQuery>();
                cfg.CreateMap<WigwamsInfo, GetAllQuery>();
                cfg.CreateMap<SellerInfo, DeleteSellerCommand>().ReverseMap();
                cfg.CreateMap<WigwamsInfo, DeleteWigwamByIdCommand>();
            });

            return new Mapper(mapperConfig);
        }
    }
}