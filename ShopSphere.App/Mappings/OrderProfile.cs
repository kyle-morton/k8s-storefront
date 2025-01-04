using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopSphere.App.Clients.Models;
using ShopSphere.App.Domain;
using ShopSphere.App.ViewModels;
using ShopSphere.App.ViewModels.Orders;

namespace ShopSphere.App.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Source -> Target
        CreateMap<Order, OrderViewModel>();
        CreateMap<Order, CreatedOrder>();

        CreateMap<User, UserViewModel>();
        CreateMap<User, CreatedOrderUser>();

        CreateMap<Address, AddressViewModel>();
        CreateMap<Address, CreatedOrderAddress>();

        // CreateMap<PlatformPublishedModel, Platform>()
        //     .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        // CreateMap<GrpcPlatformModel, Platform>()
        //     .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //     .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }
}
