using System;

namespace Hiper.SynchronizationAPI.Core.Models
{
    public class ProductSnapshot : ModelBase
    {
        public string Name { get; private set; }
        public int StockQuantity { get; private set; }

        private ProductSnapshot() { }
        public ProductSnapshot(string name, int stockQuantity)
        {
            SetValues(name, stockQuantity);
        }

        public void SetValues(string name, int stockQuantity)
        {
            ValidateInputs(name, stockQuantity);

            Name = name;
            StockQuantity = stockQuantity;
        }

        private void ValidateInputs(string name, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do produto é necessário");
            if (stockQuantity < 0)
                throw new ArgumentException("Quantidade em estoque deve ser maior que zero");
        }
    }
}
