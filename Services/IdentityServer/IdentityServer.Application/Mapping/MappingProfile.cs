using AutoMapper;
using IdentityServer.Application.Models;
using IdentityServer.Domain.Entity;

namespace IdentityServer.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginModel, User>().ReverseMap();
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}
