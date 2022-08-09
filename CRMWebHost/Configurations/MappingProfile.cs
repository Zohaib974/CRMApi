using AutoMapper;
using CRMModels.DataTransfersObjects;
using CRMEntities.Models;
using Contact = CRMEntities.Models.Contact;
using System.Collections.Generic;

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

            CreateMap<CreateAttachmentDto, Attachment>();
            CreateMap<AttachmentDto, Attachment>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Attachment, CreateAttachmentDto>();

            CreateMap<CreateJobDto, Job>();
            CreateMap<UpdateJobDto, Job>();
            CreateMap<Job, JobDto>();

            CreateMap<CreateEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
            CreateMap<Event, EventDto>();

            CreateMap<CreateUserColumnDto, UserColumn>();
            CreateMap<UserColumn, UserColumnDto>();

            CreateMap<CreateWorkOrderDto, WorkOrder>();
            CreateMap<UpdateWorkOrderDto, WorkOrder>();
            CreateMap<WorkOrder, WorkOrderDto>();
        }
    }
}
