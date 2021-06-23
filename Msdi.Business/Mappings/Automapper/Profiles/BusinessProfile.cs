using AutoMapper;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs;
using Msdi.ViewModels.DTOs.Authentication;

namespace Msdi.Business.Mappings.Automapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<OperationClaim, OperationClaimDTO>();
            CreateMap<OperationClaimDTO, OperationClaim>();
        }
    }
}
