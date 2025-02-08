using Ambev.DeveloperEvaluation.Application.Products.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public class SaleItemDTO
    {
        public ProductDTO Product { get; set; } = new ProductDTO();
        public int Quantity { get; set; }
    }
}
