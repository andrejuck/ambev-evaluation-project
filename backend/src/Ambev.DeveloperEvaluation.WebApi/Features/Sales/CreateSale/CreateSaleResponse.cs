﻿using Ambev.DeveloperEvaluation.Application.Branches.DTOs;
using Ambev.DeveloperEvaluation.Application.Customers.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public DateTime SaleDate { get; set; } = DateTime.Now;
        public CustomerDTO Customer { get; set; } = new CustomerDTO();
        public BranchDTO Branch { get; set; } = new BranchDTO();
        public List<SaleItemDTO> Items { get; set; } = new List<SaleItemDTO>();
    }
}
