using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Supplier, SupplierDTO>()
                .ForMember(dest => dest.SupplierProducts, opt => opt.MapFrom(src => src.SupplierProduct))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<SupplierProduct, SupplierProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<SupplierProductDTO, SupplierProduct>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName)); 
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>(); 
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId));
            CreateMap<OrderProducts, OrderProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
            CreateMap<OrderProductDTO, OrderProducts>();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Manager, ManagerDTO>().ReverseMap();
        }
    }
}

