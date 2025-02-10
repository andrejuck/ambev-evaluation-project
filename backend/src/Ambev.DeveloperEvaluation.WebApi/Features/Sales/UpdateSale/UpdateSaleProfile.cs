using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {

        public UpdateSaleProfile()
        {
            CreateMap<(Guid id, UpdateSaleRequest request), UpdateSaleCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.request.Items))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.request.Customer))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.request.Branch));

            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}
