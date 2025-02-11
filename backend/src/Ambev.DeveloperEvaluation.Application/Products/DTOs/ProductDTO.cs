namespace Ambev.DeveloperEvaluation.Application.Products.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public int ProductNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
