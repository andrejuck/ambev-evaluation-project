using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.DTOs
{
    internal class BranchDTOProfile : Profile
    {
        public BranchDTOProfile()
        {
            CreateMap<BranchDTO, Branch>();
            CreateMap<Branch, BranchDTO>();
        }
    }
}
