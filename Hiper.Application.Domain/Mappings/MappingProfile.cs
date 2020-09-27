﻿using AutoMapper;
using Hiper.Application.Core.Models;
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
                .ForCtorParam("name", opt => opt.MapFrom(x => x.ProductName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.ProductId));

            CreateMap<Stock, StockViewModel>()
                .ForMember(dest => dest.StockId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.ProductId))
                .ReverseMap();
        }
    }
}
