using Ambev.DeveloperEvaluation.Application.Branches.DTOs;
using Ambev.DeveloperEvaluation.Application.Customers.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public SaleStatus SaleStatus { get; set; }
        public CustomerDTO Customer { get; set; } = new CustomerDTO();
        public BranchDTO Branch { get; set; } = new BranchDTO();
        public List<SaleItemDTO> Items { get; set; } = new List<SaleItemDTO>();
    }
}
