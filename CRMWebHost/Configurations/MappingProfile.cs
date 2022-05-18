using AutoMapper;
using CRMModels.DataTransfersObjects;
using CRMEntities.Models;
using Contact = CRMEntities.Models.Contact;

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
            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
            CreateMap<Contact, ContactDto>();
        }
    }
}
