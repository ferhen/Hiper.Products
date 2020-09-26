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
            SetName(name);

            AddDomainEvent(new NewProductEvent { ProductName = Name });
        }

        public void Update(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            ValidateName(name);
            Name = name;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do produto é necessário");
        }
    }
}
