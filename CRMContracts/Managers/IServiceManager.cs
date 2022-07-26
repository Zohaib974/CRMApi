using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IServiceManager
    {
        #region Contact
        public Task<ContactDto> CreateContactAsync(CreateContactDto contact);
        CommmonListResponse<ContactDto> GetContacts(ContactParameters contactParameterss);
        Task<ContactDto> UpdateContactAsync(UpdateContactDto contact);
        Task<CommonResponse> DeleteContact(long id);
        Task<CommonResponse> ImportContactsAync(List<CreateContactDto> importedContacts);
        Task<ContactDto> GetContactByIdAsync(long id);
        Task<List<ContactDto>> GetRelatedContacts(long companyId);
        #endregion
        #region Attachment
        Task<CommonResponse> AddAttchmentsAsync(List<CreateAttachmentDto> attachments);
        List<AttachmentGroups> GetAttachments(AttachmentParameters attachmentParameters);
        Task<IEnumerable<AttachmentDto>> GetAttachmentsByIdsAsync(IEnumerable<long> ids);
        Task<AttachmentDto> GetAttachmentsByIdAsync(long id);
        #endregion
        #region Job
        public Task<JobDto> CreateJobAsync(CreateJobDto job);
        CommmonListResponse<JobDto> GetJobs(JobParameters jobParameterss);
        Task<JobDto> UpdateJobAsync(UpdateJobDto job);
        Task<CommonResponse> DeleteJob(long id);
        Task<JobDto> GetJobByIdAsync(long id);
        #endregion
        #region Event
        public Task<EventDto> CreateEventAsync(CreateEventDto eventDto);
        CommmonListResponse<EventDto> GetEvents(EventParameters jobParameterss);
        Task<EventDto> UpdateEventAsync(UpdateEventDto eventDto);
        Task<CommonResponse> DeleteEvent(long id);
        Task<EventDto> GetEventByIdAsync(long id);
        #endregion
        #region WorkOrder
        public Task<WorkOrderDto> CreateWorkOrderAsync(CreateWorkOrderDto workOrderDto);
        CommmonListResponse<WorkOrderDto> GetWorkOrders(WorkOrderParameters workOrderParameterss);
        Task<WorkOrderDto> UpdateWorkOrderAsync(UpdateWorkOrderDto workOrderDto);
        Task<CommonResponse> DeleteWorkOrder(long id);
        Task<WorkOrderDto> GetWorkOrderByIdAsync(long id);
        #endregion

    }
}
