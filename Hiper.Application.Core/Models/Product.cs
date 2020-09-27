using Hiper.Application.Core.Events;
using System;

namespace Hiper.Application.Core.Models
{
    public class Product : ModelBase
    {
        public string Name { get; private set; }
        public virtual Stock Stock { get; private set; }

        private Product() { }
        public Product(string name, int id = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do produto é necessário");
            if (id == 0)
                AddDomainEvent(new NewProductEvent { ProductName = name });

            Id = id;
            Name = name;

            AddDomainEvent(new NewOrUpdatedProductEvent
            {
                ProductName = Name,
                StockQuantity = Stock?.Quantity ?? 0
            });
        }
    }
}
