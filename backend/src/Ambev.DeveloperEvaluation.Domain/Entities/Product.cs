﻿

using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        private Product() { }
        public Product(int productNumber, string name, decimal unitPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
            SetCreatedAt();
        }

        public int ProductNumber { get; }
        public string Name { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }

        public void PrepareToUpdate(Product existingProduct)
        {
            Name = existingProduct.Name;
            UnitPrice = existingProduct.UnitPrice;
            SetUpdatedAt();
        }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            var otherObj = (Product)obj;
            return Id.Equals(otherObj.Id) || ProductNumber.Equals(otherObj.ProductNumber);
        }
    }
}
