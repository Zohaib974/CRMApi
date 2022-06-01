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
        #endregion
        #region Attachment
        Task<CommonResponse> AddAttchments(List<CreateAttachmentDto> attachments);
        CommmonListResponse<AttachmentDto> GetAttachments(AttachmentParameters attachmentParameters);
        Task<IEnumerable<AttachmentDto>> GetAttachmentsByIds(IEnumerable<long> ids);
        #endregion
    }
}
