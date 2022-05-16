using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMHelper;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices.Implementation
{
    public class ServiceManager : IServiceManager
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }
        public async Task<ContactDto> CreateContactAsync(CreateContactDto contact)
        {
            var response = new ContactDto();
            try
            {
                var contactEntity = _mapper.Map<Contact>(contact);
                contactEntity.Status = (int)contact.ContactStatus;
                _repositoryManager.Contact.CreateContact(contactEntity);
                await _repositoryManager.SaveAsync();
                response = _mapper.Map<ContactDto>(contactEntity);
                response.Successful = true;
                response.Message = "Contact created successfully.";
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.Message = "Contact creation failed.Error : " + ex.Message;
            }
            return response;
        }

        public CommmonListResponse<ContactDto> GetContacts(ContactParameters contactParameters)
        {
            var repoRespose = _repositoryManager.Contact.GetContacts(contactParameters,false);
            var listContacts = _mapper.Map <List<ContactDto>>(repoRespose);
            var response = new CommmonListResponse<ContactDto>(listContacts, repoRespose.MetaData.TotalCount, 
                                                    repoRespose.MetaData.CurrentPage, repoRespose.MetaData.PageSize);
            return response;
        }
    }
}
