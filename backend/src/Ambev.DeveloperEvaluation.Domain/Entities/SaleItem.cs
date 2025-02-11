using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        private SaleItem() { }
        public SaleItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            SetCreatedAt();
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalAmount => (Quantity * Product.UnitPrice) - (Quantity * Product.UnitPrice * Discount);
        public bool Cancelled { get; private set; }
        public decimal Discount
        {
            get
            {
                if (Quantity >= 4 && Quantity < 10)
                    return 0.10m;

                if (Quantity >= 10 && Quantity < 20)
                    return 0.20m;

                return 0;
            }
        }

        public void CancelItem()
        {
            Cancelled = true;
            SetUpdatedAt();
        }

        public void BindProduct(Product product)
        {
            Product = product;
        }
    }
}
