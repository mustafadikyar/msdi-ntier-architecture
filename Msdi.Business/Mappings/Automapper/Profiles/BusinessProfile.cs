using AutoMapper;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Business.Mappings.Automapper.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
