using AutoMapper;
using Hiper.SynchronizationAPI.Core.Models;
using Hiper.SynchronizationAPI.Presentation.DTO;
using Hiper.SynchronizationAPI.Presentation.ViewModel;

namespace Hiper.SynchronizationAPI.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductSnapshotDTO, ProductSnapshot>()
                .ForCtorParam("name", opt => opt.MapFrom(x => x.ProductName))
                .ForCtorParam("stockQuantity", opt => opt.MapFrom(x => x.StockQuantity));

            CreateMap<ProductSnapshot, ProductSnapshotViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.Name));
        }
    }
}
