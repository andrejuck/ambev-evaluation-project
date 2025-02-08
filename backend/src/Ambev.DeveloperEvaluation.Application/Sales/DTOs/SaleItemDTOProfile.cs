using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public class SaleItemDTOProfile : Profile
    {
        public SaleItemDTOProfile()
        {
            CreateMap<SaleItemDTO, SaleItem>();
            CreateMap<SaleItem, SaleItemDTO>();
        }
    }
}
