using AutoMapper;
using Hiper.SynchronizationAPI.Core.Models;
using Hiper.SynchronizationAPI.Data;
using Hiper.SynchronizationAPI.Domain.Services.Base;
using Hiper.SynchronizationAPI.Presentation.DTO;
using Hiper.SynchronizationAPI.Presentation.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Domain.Services
{
    public class ProductSnapshotService : ServiceBase
    {
        public ProductSnapshotService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public async Task Save(ProductSnapshotDTO productSnapshotDTO)
        {
            var productSnapshot = _mapper.Map<ProductSnapshot>(productSnapshotDTO);
            await _uow.ProductSnapshots.AddOrUpdateByName(productSnapshot);
            await _uow.SaveChanges();
        }

        public async Task<IEnumerable<ProductSnapshotViewModel>> List()
        {
            var products = await _uow.ProductSnapshots.List();
            return _mapper.Map<IEnumerable<ProductSnapshotViewModel>>(products);
        }
    }
}
