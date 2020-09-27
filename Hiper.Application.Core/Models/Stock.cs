using Hiper.Application.Core.Events;
using System;

namespace Hiper.Application.Core.Models
{
    public class Stock : ModelBase
    {
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }

        private Stock() { }
        public Stock(int quantity, int productId)
        {
            if (productId < 0)
                throw new ArgumentException("Produto deve existir");
            if (quantity < 0)
                throw new ArgumentException("Quantidade em estoque deve ser maior ou igual a zero");

            ProductId = productId;
            Quantity = quantity;


            AddDomainEvent(new NewOrUpdatedProductEvent
            {
                ProductId = ProductId
            });
        }
    }
}
