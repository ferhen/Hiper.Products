using AutoMapper;
using Hiper.Application.Core.Models;
using Hiper.Application.Data;
using Hiper.Application.Domain.Services.Base;
using Hiper.Application.Presentation.ViewModels;
using System.Threading.Tasks;

namespace Hiper.Application.Domain.Services
{
    public class StockService : ServiceBase
    {
        public StockService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public async Task<StockViewModel> Update(StockViewModel stockViewModel)
        {
            var stock = await _uow.Stocks.Update(_mapper.Map<Stock>(stockViewModel));
            await _uow.SaveChanges();
            return _mapper.Map<StockViewModel>(stock);
        }
    }
}
