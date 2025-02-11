﻿using Ambev.DeveloperEvaluation.Application.Branches.DTOs;
using Ambev.DeveloperEvaluation.Application.Customers.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public CustomerDTO Customer { get; set; } = new CustomerDTO();
        public BranchDTO Branch { get; set; } = new BranchDTO();
        public List<SaleItemDTO> Items { get; set; } = new List<SaleItemDTO>();
    }
}
