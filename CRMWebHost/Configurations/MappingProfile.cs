using AutoMapper;
using CRMServices.DataTransferObjects;
using CRMEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebHost.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                            .ForMember(c => c.FullAddress,
                            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Company, CompanyDto>()
                            .ForMember(c => c.FullAddress,
                            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>(); 
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>(); 
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
