using AutoMapper;
using Hiper.Application.Core.Models;
using Hiper.Application.Presentation.DTO;
using Hiper.Application.Presentation.ViewModels;

namespace Hiper.Application.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.StockId, opt => opt.MapFrom(x => x.Stock.Id))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(x => x.Stock.Quantity));

            CreateMap<ProductViewModel, Product>()
                .ForCtorParam("id", opt => opt.MapFrom(x => x.ProductId))
                .ForCtorParam("name", opt => opt.MapFrom(x => x.ProductName));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(x => x.Stock.Quantity));

            CreateMap<Stock, StockViewModel>()
                .ForMember(dest => dest.StockId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.ProductId));

            CreateMap<StockViewModel, Stock>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.StockId))
                .ForCtorParam("productId", opt => opt.MapFrom(x => x.ProductId))
                .ForCtorParam("quantity", opt => opt.MapFrom(x => x.StockQuantity));
        }
    }
}
