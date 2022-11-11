using AutoMapper;
using Basket.Application.Models;
using Basket.Domain.Entities;

namespace Basket.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, ViewOrderModel>().ReverseMap();
            CreateMap<CreateOrderModel, Order>().ReverseMap();
            CreateMap<UpdateOrderModel, Order>().ReverseMap();
        }
    }
}
