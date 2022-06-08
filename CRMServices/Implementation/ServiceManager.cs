using AutoMapper;
using CRMContracts;
using CRMEntities.Models;
using CRMHelper;
using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices.Implementation
{
    public class ServiceManager : IServiceManager
    {
        #region declerations
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
        #endregion
        #region Contact
        public async Task<ContactDto> CreateContactAsync(CreateContactDto contact)
        {
            var response = new ContactDto();
            try
            {
                var res = await _repositoryManager.Contact.GetContactByNumberAndCreatedBy(contact.MobileNumber, contact.OfficeNumber, contact.HomeNumber, LoggedInUserId);
                if (res)
                    return new ContactDto() { Successful = false, Message = "Contact already present." };

                var contactEntity = _mapper.Map<Contact>(contact);
                contactEntity.Status = (int)contact.ContactStatus;
                contactEntity.CreatedBy = LoggedInUserId;
                contactEntity.IsImported = false;
                _repositoryManager.Contact.CreateContact(contactEntity);
                await _repositoryManager.SaveAsync();
                response = _mapper.Map<ContactDto>(contactEntity);
                response.Successful = true;
                response.Message = "Contact created successfully.";
            }
            catch(Exception ex)
            {
                response.Successful = false;
                response.Message = "Contact creation failed.Error : " + ex.Message;
                _logger.LogError("Method CreateContactAsync:Error while ceating contact.Error : " + ex.ToString());
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
                response.Successful = false;
                response.Message = "Contact update failed.Error : " + ex.Message;
                _logger.LogError("Method UpdateContactAsync:Error while updating contact.Error : " + ex.ToString());
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
                _logger.LogError("Method DeleteContact:Error while deleting contact.Error : " + ex.ToString());
            }
            return response;
        }
        public async Task<CommonResponse> ImportContactsAync(List<CreateContactDto> importedContacts)
        {
            var response = new CommonResponse();
            try
            {
                _logger.LogInfo("Contact import started.Total contacts in file:"+importedContacts.Count);
                List<Contact> importList = new List<Contact>();
                foreach(var contact in importedContacts)
                {
                    var res =await _repositoryManager.Contact.GetContactByNumberAndCreatedBy(contact.MobileNumber, contact.OfficeNumber, contact.HomeNumber, LoggedInUserId);
                    if (res)
                        continue;

                    var contactEntity = _mapper.Map<Contact>(contact);
                    contactEntity.Status = (int)contact.ContactStatus;
                    contactEntity.CreatedBy = LoggedInUserId;
                    contactEntity.IsImported = true;
                    importList.Add(contactEntity);
                }
                if(importList.Count == 0)
                {
                    response.Successful = true;
                    response.Message = importedContacts.Count + " Contacs already present.Nothing to import.";
                }
                else
                {
                    _logger.LogInfo("Total contacts to import: " + importList.Count);
                    _repositoryManager.Contact.CreateContacts(importList);
                    await _repositoryManager.SaveAsync();
                    response.Successful = true;
                    response.Message = importList.Count + " Contacts imported successfully.";
                }
            }
            catch(Exception ex)
            {
                response.Successful = false;
                response.Message = "Contacts imported failed";
                _logger.LogError("Method ImportContactsAync:Error while importing contact.Error : " + ex.ToString());
            }
            return response;
        }
        public async Task<ContactDto> GetContactByIdAsync(long id)
        {
            var response = new ContactDto();
            var contactEntity = await _repositoryManager.Contact.GetContactByIdAsync(id, false);
            if (contactEntity == null)
            {
                response.Successful = false;
                response.Message = $"Record with Id: {id} not found.";
                return response;
            }
            _mapper.Map(contactEntity, response);
            response.Successful = true;
            response.Message = "Record found successfully.";
            return response;
        }
        #endregion
        #region attachments
        public async Task<CommonResponse> AddAttchmentsAsync(List<CreateAttachmentDto> attachments)
        {
            var response = new CommonResponse();
            try
            {
                if(attachments.Count == 0)
                    return new CommonResponse(false,"No files to save.");

                _logger.LogInfo("Total attachments to save: " + attachments.Count);
                var attachmentEntity = _mapper.Map<List<Attachment>>(attachments);
                _repositoryManager.Attachment.AddAttchmentsAsync(attachmentEntity);
                await _repositoryManager.SaveAsync();
                response.Successful = true;
                response.Message = attachmentEntity.Count + " Attachments saveed successfully.";
            }
            catch(Exception ex)
            {
                response.Successful = false;
                response.Message = "Failed to save attachments.Error : " + ex.Message;
                _logger.LogError("Method AddAttchments:Error while creating attachments.Error : " + ex.ToString());
            }
            return response;
        }
        public List<AttachmentGroups> GetAttachments(AttachmentParameters attachmentParameters)
        {
            var repoRespose = _repositoryManager.Attachment.GetAttachments(attachmentParameters, false);
            var listContacts = _mapper.Map<List<AttachmentDto>>(repoRespose);
            var attachmentGroups = listContacts.GroupBy(a => a.UploadedBy)
                                    .Select(a=> new AttachmentGroups()
                                    {
                                        UploadedBy = a.Key,
                                        attachments  =a.ToList()
                                    }).ToList();
            return attachmentGroups;
        }
        public async Task<IEnumerable<AttachmentDto>> GetAttachmentsByIdsAsync(IEnumerable<long> ids)
        {
            var attachmentToReturn = new List<AttachmentDto>();
            try
            {
                var attachmentEntities =await  _repositoryManager.Attachment.GetByIdsAsync(ids, trackChanges: false);
                if (attachmentEntities == null || attachmentEntities.Count() == 0)
                {
                    return attachmentToReturn;
                }
                if (ids.Count() != attachmentEntities.Count())
                    _logger.LogError("Some ids are not valid in a collection");
                attachmentToReturn = _mapper.Map<List<AttachmentDto>>(attachmentEntities);
            }
            catch(Exception ex)
            {
                _logger.LogError("Method GetAttachmentsByIds:Error while getting attachments.Error : " + ex.ToString());
            }
            return attachmentToReturn;
        }
        public async Task<AttachmentDto> GetAttachmentsByIdAsync(long id)
        {
            var attachmentToReturn = new AttachmentDto();
            try
            {
                var attachmentEntity =await _repositoryManager.Attachment.GetByIdAsync(id, trackChanges: false);
                if (attachmentEntity == null)
                {
                    return null;
                }
                attachmentToReturn = _mapper.Map<AttachmentDto>(attachmentEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Method GetAttachmentsById:Error while getting attachment.Error : " + ex.ToString());
            }
            return attachmentToReturn;
        }
        #endregion

        #region Job
        public async Task<JobDto> CreateJobAsync(CreateJobDto job)
        {
            var response = new JobDto();
            try
            {
                var jobEntity = _mapper.Map<Job>(job);
                jobEntity.Status = (int)job.JobStatus;
                jobEntity.CreatedBy = LoggedInUserId;
                _repositoryManager.Job.CreateJob(jobEntity);
                await _repositoryManager.SaveAsync();
                response = _mapper.Map<JobDto>(jobEntity);
                response.Successful = true;
                response.Message = "Job created successfully.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Record creation failed.Error : " + ex.Message;
                _logger.LogError("Method CreateJobAsync:Error while ceating record.Error : " + ex.ToString());
            }
            return response;
        }
        public CommmonListResponse<JobDto> GetJobs(JobParameters jobParameters)
        {
            var repoRespose = _repositoryManager.Job.GetJobs(jobParameters, false);
            var listJobs = _mapper.Map<List<JobDto>>(repoRespose);
            var response = new CommmonListResponse<JobDto>(listJobs, repoRespose.MetaData.TotalCount,
                                                    repoRespose.MetaData.CurrentPage, repoRespose.MetaData.PageSize);
            return response;
        }
        public async Task<JobDto> UpdateJobAsync(UpdateJobDto jobDto)
        {
            var response = new JobDto();
            try
            {
                var jobEntity = await _repositoryManager.Job.GetJobByIdAsync(jobDto.Id, true);
                if (jobEntity == null)
                {
                    response.Successful = false;
                    response.Message = $"Record with Id: {jobDto.Id} not found.";
                    return response;
                }
                if ((int)jobDto.JobStatus != jobEntity.Status)
                    jobDto.LastStatusChangeDate = DateTime.UtcNow;
                _mapper.Map(jobDto, jobEntity);
                _repositoryManager.Job.MarkModified(jobEntity, jobDto);
                jobEntity.SetModificationTracking(LoggedInUserId);
                await _repositoryManager.SaveAsync();
                _mapper.Map(jobEntity, response);
                response.Successful = true;
                response.Message = "Job update successful.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Record update failed.Error : " + ex.Message;
                _logger.LogError("Method UpdateJobAsync:Error while updating record.Error : " + ex.ToString());
            }
            return response;
        }
        public async Task<CommonResponse> DeleteJob(long id)
        {
            var response = new CommonResponse();
            try
            {
                var jobEntity = await _repositoryManager.Job.GetJobByIdAsync(id, true);
                if (jobEntity == null)
                {
                    response.Successful = false;
                    response.Message = $"Record with Id: {id} not found.";
                    return response;
                }
                jobEntity.SetDeletedTracking(LoggedInUserId);
                jobEntity.IsDeleted = true;
                await _repositoryManager.SaveAsync();
                response.Successful = true;
                response.Message = "Deleted successfully";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Record deletion failed.Error : " + ex.Message;
                _logger.LogError("Method DeleteJob:Error while deleting record.Error : " + ex.ToString());
            }
            return response;
        }
        public async Task<JobDto> GetJobByIdAsync(long id)
        {
            var response = new JobDto();
            var jobEntity = await _repositoryManager.Job.GetJobByIdAsync(id, false);
            if (jobEntity == null)
            {
                response.Successful = false;
                response.Message = $"Record with Id: {id} not found.";
                return response;
            }
            _mapper.Map(jobEntity, response);
            response.Successful = true;
            response.Message = "Record found successfully.";
            return response;
        }
        #endregion
    }
}
