using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaxManager.Data.Model;
using TaxManager.ViewModel;

namespace TaxManager.Service.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ImportTaxViewModel, TaxDetails>();
            CreateMap<TaxDetails, ImportTaxViewModel>();
        }
    }
}
