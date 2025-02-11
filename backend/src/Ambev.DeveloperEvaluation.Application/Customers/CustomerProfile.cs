using Ambev.DeveloperEvaluation.Application.Customers.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>();
        }
    }
}
