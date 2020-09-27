using Hiper.Application.Core.Events;
using System;

namespace Hiper.Application.Core.Models
{
    public class Product : ModelBase
    {
        public string Name { get; private set; }
        public virtual Stock Stock { get; private set; }

        private Product() { }
        public Product(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do produto é necessário");
            Name = name;

            if (Id != 0)
                AddDomainEvent(new NewProductEvent { ProductName = Name });
        }
    }
}
