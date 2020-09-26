using AutoMapper;
using Hiper.Application.Core.Models;
using Hiper.Application.Data;
using Hiper.Application.Domain.Services.Base;
using Hiper.Application.Presentation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiper.Application.Domain.Services
{
    public class ProductService : ServiceBase
    {
        public ProductService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

        public async Task<IEnumerable<ProductViewModel>> List()
        {
            var products = await _uow.Products.ListIncludeStock();
            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductViewModel> Create(ProductViewModel productViewModel)
        {
            await _uow.Products.Add(_mapper.Map<Product>(productViewModel));
            await _uow.SaveChanges();

            var product = await _uow.Products.GetByName(productViewModel.ProductName);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> Update(ProductViewModel productViewModel)
        {
            var product = await _uow.Products.GetById(productViewModel.ProductId);
            product.Update(productViewModel.ProductName);
            await _uow.SaveChanges();

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task Delete(int productId)
        {
            await _uow.Products.Remove(productId);
            await _uow.SaveChanges();
        }
    }
}
