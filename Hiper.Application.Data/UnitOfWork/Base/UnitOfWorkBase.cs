using Hiper.Application.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiper.Application.Data
{
    public abstract class UnitOfWorkBase
    {
        protected IProductRepository _productRepository;
        protected IStockRepository _stockRepository;
    }
}
