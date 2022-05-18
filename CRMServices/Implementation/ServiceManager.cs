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
        public long LoggedInUserId;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            LoggedInUserId = 1;
        }
        public async Task<ContactDto> CreateContactAsync(CreateContactDto contact)
        {
            var response = new ContactDto();
            try
            {
                var contactEntity = _mapper.Map<Contact>(contact);
                contactEntity.Status = (int)contact.ContactStatus;
                contactEntity.CreatedBy = LoggedInUserId;
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
        public async Task<ContactDto> UpdateContactAsync(UpdateContactDto contactDto)
        {
            var response = new ContactDto();
            try
            {
                var contactEntity =await _repositoryManager.Contact.GetContactByIdAsync(contactDto.Id, true);
                if (contactEntity == null)
                {
                    response.Successful = false;
                    response.Message = $"Record with Id: {contactDto.Id} not found.";
                    return response;
                }
                if ((int)contactDto.ContactStatus != contactEntity.Status)
                    contactDto.LastStatusChangeDate = DateTime.UtcNow;
                _mapper.Map(contactDto, contactEntity);
                _repositoryManager.Contact.MarkModified(contactEntity, contactDto);
                contactEntity.SetModificationTracking(LoggedInUserId);
                await _repositoryManager.SaveAsync();
                _mapper.Map(contactEntity, response);
                response.Successful = true;
                response.Message = "Contact update successful.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.Successful = false;
                response.Message = "Contact update failed.Error : " + ex.Message;
            }
            return response;
        }
        public async Task<CommonResponse> DeleteContact(long id)
        {
            var response = new CommonResponse();
            try
            {
                var contactEntity = await _repositoryManager.Contact.GetContactByIdAsync(id,true);
                if (contactEntity == null)
                {
                    response.Successful = false;
                    response.Message = $"Record with Id: {id} not found.";
                    return response;
                }
                contactEntity.SetDeletedTracking(LoggedInUserId);
                contactEntity.IsDeleted = true;
                await _repositoryManager.SaveAsync();
                response.Successful = true;
                response.Message = "Deleted successfully";
            }
            catch(Exception ex)
            {
                response.Successful = false;
                response.Message = "Contact deletion failed.Error : " + ex.Message;
            }
            return response;
        }
    }
}
