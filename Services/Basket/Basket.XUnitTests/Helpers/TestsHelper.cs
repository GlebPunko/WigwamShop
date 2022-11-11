using AutoMapper;
using Basket.Application.Mapping;

namespace Basket.XUnitTests.Helpers
{
    internal static class TestsHelper
    {
        internal static IMapper SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddMaps(typeof(MappingProfile));
            });

            return new Mapper(mapperConfig);
        }
    }
}
