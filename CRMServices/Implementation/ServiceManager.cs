using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMHelper;
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
                _repositoryManager.Contact.CreateContact(contactEntity);
                await _repositoryManager.SaveAsync();
                response = _mapper.Map<ContactDto>(contactEntity);
                response.Successful = true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.Message = "Contact Creation Failed,Error : " + ex.Message;
            }
            return response;
        }
    }
}
